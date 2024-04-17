// ��������� ������������ ���� ��� ������ �������
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using System.IO;


namespace TCPSocketServer2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static Socket Client; // ������� ������ ������-�������
        private static IPHostEntry ipHost; // ����� ��� �������� �� ������ ���-����
        private static IPAddress ipAddr; // ������������� IP-�����
        private static IPEndPoint ipEndPoint; // ��������� �������� �����
        private static Thread socketThread; // ������� ����� ��� ��������� ������
        private static Thread WaitingForMessage; // ������� ����� ��� ����� ���������


        private void startSocket()
        {
            // IP-����� �������, ��� �����������
            string HostName = tbAddress.Text;
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
                // ������� ����� �� ������� ������
                Client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
                while (true)
                {
                    // �������� ������������ � ��������� �����
                    Client.Connect(ipEndPoint);
                    if (Client.Connected) // ���� ������ �����������
                    {
                        // ��������� �������� ��� �������, ����� ������������ ����, ��� ���������� �����������
                        bConnect.Invoke(new Action(() => bConnect.Text = "����������� �����������"));
                        bConnect.Invoke(new Action(() => bConnect.BackColor = Color.Green));
                        // ������� ����� �����, ��������� �� �-��� ��������� ��������� � ������ Worker
                        WaitingForMessage = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(GetMessages));
                        // ���������, � ���������� �������� �������� (������� ����)
                        WaitingForMessage.Start(new Object[] { lbHistory });
                    }
                    break;
                }
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
        }

        // �-���, ���������� � ����� ������: ��������� ����� �������� ����
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
                    string Message = GetDataFromServer();
                    ChatListBox.Invoke(new Action(() =>
                    ChatListBox.Items.Add(DateTime.Now.ToShortTimeString() + " Server: " + Message)));
                }
                catch { }
            }
        }

        // ��������� ������ �� �������
        public static string GetDataFromServer()
        {
            string GetInformation = "";
            // ������� ������ ���������� ������, ���� ����� �������� ����������
            byte[] GetBytes = new byte[1024];
            int BytesRec = Client.Receive(GetBytes);
            // ��������� �� ������� ����� � ������
            GetInformation += Encoding.Unicode.GetString(GetBytes, 0, BytesRec);
            return GetInformation;
        }

        // �������� ���������� �� ������
        public static void SendDataToServer(string Data)
        {
            // ���������� unicode, ����� �� ���� ������� � ����������, ��� ������ ����������
            byte[] SendMsg = Encoding.Unicode.GetBytes(Data);
            Client.Send(SendMsg);
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // �������� ������� ����� ���������
            SendDataToServer(tbMessage.Text);
            // ��������� � ������� ���� �� ��������� + ��� + ����� ���������
            lbHistory.Items.Add(DateTime.Now.ToShortTimeString() + " Client: " + tbMessage.Text.ToString());
            // ������������� ������� ����
            lbHistory.TopIndex = lbHistory.Items.Count - 1;
            // ������� ����� �� ���� �����
            tbMessage.Text = "";
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            List<int> set1 = new List<int> { 5, 3, 1 };
            List<int> set2 = new List<int> { 4, 2 }; 

        }

        }
    }
