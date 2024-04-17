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
        private static Socket Server; // Создаем объект сокета-сервера
        private static Socket Handler; // Создаем объект вспомогательного сокета
        private static IPHostEntry ipHost; // Класс для сведений об адресе веб-узла
        private static IPAddress ipAddr; // Предоставляет IP-адрес
        private static IPEndPoint ipEndPoint; // Локальная конечная точка
        private static Thread socketThread; // Создаем поток для поддержки потока
        private static Thread WaitingForMessage; // Создаем поток для приёма сообщений

        // Функция запуска сокета
        private void startSocket()
        {
            // IP-адрес сервера, для подключения
            string HostName = "0.0.0.0";
            // Порт подключения
            string Port = tbPort.Text;
            // Разрешает DNS-имя узла или IP-адрес в экземпляр IPHostEntry.
            ipHost = Dns.Resolve(HostName);
            // Получаем из списка адресов первый (адресов может быть много)
            ipAddr = ipHost.AddressList[0];
            // Создаем конечную локальную точку подключения на каком-то порту
            ipEndPoint = new IPEndPoint(ipAddr, int.Parse(Port));
            try
            {
                // Создаем сокет сервера на текущей машине
                Server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            }
            catch (SocketException error)
            {
                // 10061 — порт подключения занят/закрыт
                if (error.ErrorCode == 10061)
                {
                    MessageBox.Show("Порт подключения закрыт!");
                    Application.Exit();
                }
            }
            // Ждем подключений
            try
            {
                // Связываем удаленную точку с сокетом
                Server.Bind(ipEndPoint);
                // Не более 10 подключения на сокет
                Server.Listen(10);
                // Начинаем "прослушивать" подключения
                while (true)
                {
                    Handler = Server.Accept();
                    if (Handler.Connected)
                    {
                        // Позеленим кнопочку для красоты, чтобы пользователь знал, чтосоединение установлено
                        bConnect.Invoke(new Action(() => bConnect.Text = "Клиентподключен"));
                        bConnect.Invoke(new Action(() => bConnect.BackColor = Color.Green));
                        // Создаем новый поток, указываем на ф-цию получения сообщений в классе Worker
                        WaitingForMessage = new System.Threading.Thread(new
                        System.Threading.ParameterizedThreadStart(GetMessages));
                        // Запускаем, в параметрах передаем листбокс (история чата)
                        WaitingForMessage.Start(new Object[] { lbHistory });
                    }
                    break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Проблемы с подключением");
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
            bConnect.Text = "Ожидание подключения";
            bConnect.BackColor = Color.Yellow;

        }

        // Ф-ция, работающая в новом потоке: получение новых сообщений ————
        public static void GetMessages(Object obj)
        {
            // Получаем объект истории чата (лист бокс)
            Object[] Temp = (Object[])obj;
            System.Windows.Forms.ListBox ChatListBox =
           (System.Windows.Forms.ListBox)Temp[0];
            // В бесконечном цикле получаем сообщения
            while (true)
            {
                // Ставим паузу, чтобы на время освобождать порт для отправки сообщений
                Thread.Sleep(50);
                try
                {
                    // Получаем сообщение от клиента
                    string Message = GetDataFromClient();
                    // Добавляем в историю + текущее время
                    ChatListBox.Invoke(new Action(() =>
                   ChatListBox.Items.Add(DateTime.Now.ToShortTimeString() + " Client: " + Message)));
                }
                catch { }

            }
        }
        // Получение информации от клиента
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
            // Посылаем клиенту новое сообщение
            SendDataToClient(tbMessage.Text);
            // Добавляем в историю свое же сообщение + ник + время написания
            lbHistory.Items.Add(DateTime.Now.ToShortTimeString() + " Server: " + tbMessage.Text.ToString());
            // Автопрокрутка истории чата
            lbHistory.TopIndex = lbHistory.Items.Count - 1;
            // Убираем текст из поля ввода
            tbMessage.Text = "";
        }
        // Отправка информации на клиент
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