using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TCPSocketServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private static Socket Server; // ������� ������ ������-�������
        private static Socket Handler; // ������� ������ ���������������� ������
        private static IPHostEntry ipHost; // ����� ��� �������� �� ������ ���-����
        private static IPAddress ipAddr; // ������������� IP-�����
        private static IPEndPoint ipEndPoint; // ��������� �������� �����
        private static Thread socketThread; // ������� ����� ��� ��������� ������
        private static Thread WaitingForMessage; // ������� ����� ��� ����� ���������

        // ������� ������� ������
        private void startSocket()
        {
            // IP-����� �������, ��� �����������
            string HostName = "0.0.0.0";
            // ���� �����������
            string Port = tbPort.Text;
            // ��������� DNS-��� ���� ��� IP-����� � ��������� IPHostEntry.
            ipHost = Dns.Resolve(HostName);
            // �������� �� ������ ������� ������ (������� ����� ���� �����)
            ipAddr = ipHost.AddressList[0];
            // ������� �������� ��������� ����� ����������� �� �����-�� �����
            ipEndPoint = new IPEndPoint(ipAddr, int.Parse(Port));
            try
            {
                // ������� ����� ������� �� ������� ������
                Server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            }
            catch (SocketException error)
            {
                // 10061 � ���� ����������� �����/������
                if (error.ErrorCode == 10061)
                {
                    MessageBox.Show("���� ����������� ������!");
                    Application.Exit();
                }
            }
            // ���� �����������
            try
            {
                // ��������� ��������� ����� � �������
                Server.Bind(ipEndPoint);
                // �� ����� 10 ����������� �� �����
                Server.Listen(10);
                // �������� "������������" �����������
                while (true)
                {
                    Handler = Server.Accept();
                    if (Handler.Connected)
                    {
                        // ��������� �������� ��� �������, ����� ������������ ����, ������������� �����������
                        bConnect.Invoke(new Action(() => bConnect.Text = "���������������"));
                        bConnect.Invoke(new Action(() => bConnect.BackColor = Color.Green));
                        // ������� ����� �����, ��������� �� �-��� ��������� ��������� � ������ Worker
                        WaitingForMessage = new System.Threading.Thread(new
                        System.Threading.ParameterizedThreadStart(GetMessages));
                        // ���������, � ���������� �������� �������� (������� ����)
                        WaitingForMessage.Start(new Object[] { lbHistory });
                    }
                    break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("�������� � ������������");
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            socketThread = new Thread(new ThreadStart(startSocket));
            socketThread.IsBackground = true;
            socketThread.Start();
            bConnect.Enabled = false;
            bConnect.Text = "�������� �����������";
            bConnect.BackColor = Color.Yellow;

        }

        // �-���, ���������� � ����� ������: ��������� ����� ��������� ����
        public static void GetMessages(Object obj)
        {
            // �������� ������ ������� ���� (���� ����)
            Object[] Temp = (Object[])obj;
            System.Windows.Forms.ListBox ChatListBox =
           (System.Windows.Forms.ListBox)Temp[0];
            // � ����������� ����� �������� ���������
            while (true)
            {
                // ������ �����, ����� �� ����� ����������� ���� ��� �������� ���������
                Thread.Sleep(50);
                try
                {
                    // �������� ��������� �� �������
                    string Message = GetDataFromClient();
                    // ��������� � ������� + ������� �����
                    ChatListBox.Invoke(new Action(() =>
                   ChatListBox.Items.Add(DateTime.Now.ToShortTimeString() + " Client: " + Message)));
                }
                catch { }

            }
        }
        // ��������� ���������� �� �������
        public static string GetDataFromClient()
        {
            string GetInformation = "";
            byte[] GetBytes = new byte[1024];
            int BytesRec = Handler.Receive(GetBytes);
            GetInformation += Encoding.Unicode.GetString(GetBytes, 0, BytesRec);
            return GetInformation;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // �������� ������� ����� ���������
            SendDataToClient(tbMessage.Text);
            // ��������� � ������� ���� �� ��������� + ��� + ����� ���������
            lbHistory.Items.Add(DateTime.Now.ToShortTimeString() + " Server: " + tbMessage.Text.ToString());
            // ������������� ������� ����
            lbHistory.TopIndex = lbHistory.Items.Count - 1;
            // ������� ����� �� ���� �����
            tbMessage.Text = "";
        }
        // �������� ���������� �� ������
        public static void SendDataToClient(string Data)
        {
            byte[] SendMsg = Encoding.Unicode.GetBytes(Data);
            Handler.Send(SendMsg);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbMessage_TextChanged(object sender, EventArgs e)
        {


        }
    }
}