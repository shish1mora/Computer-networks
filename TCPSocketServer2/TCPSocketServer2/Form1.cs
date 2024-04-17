// Добавляем пространства имен для работы сокетов
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

        private static Socket Client; // Создаем объект сокета-сервера
        private static IPHostEntry ipHost; // Класс для сведений об адресе веб-узла
        private static IPAddress ipAddr; // Предоставляет IP-адрес
        private static IPEndPoint ipEndPoint; // Локальная конечная точка
        private static Thread socketThread; // Создаем поток для поддержки потока
        private static Thread WaitingForMessage; // Создаем поток для приёма сообщений


        private void startSocket()
        {
            // IP-адрес сервера, для подключения
            string HostName = tbAddress.Text;
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
                // Создаем сокет на текущей машине
                Client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
                while (true)
                {
                    // Пытаемся подключиться к удаленной точке
                    Client.Connect(ipEndPoint);
                    if (Client.Connected) // Если клиент подключился
                    {
                        // Позеленим кнопочку для красоты, чтобы пользователь знал, что соединение установлено
                        bConnect.Invoke(new Action(() => bConnect.Text = "Подключение установлено"));
                        bConnect.Invoke(new Action(() => bConnect.BackColor = Color.Green));
                        // Создаем новый поток, указываем на ф-цию получения сообщений в классе Worker
                        WaitingForMessage = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(GetMessages));
                        // Запускаем, в параметрах передаем листбокс (история чата)
                        WaitingForMessage.Start(new Object[] { lbHistory });
                    }
                    break;
                }
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
        }

        // Ф-ция, работающая в новом потоке: получение новых сообщенй ————
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
                    string Message = GetDataFromServer();
                    ChatListBox.Invoke(new Action(() =>
                    ChatListBox.Items.Add(DateTime.Now.ToShortTimeString() + " Server: " + Message)));
                }
                catch { }
            }
        }

        // Получение данных от сервера
        public static string GetDataFromServer()
        {
            string GetInformation = "";
            // Создаем пустое “хранилище” байтов, куда будем получать информацию
            byte[] GetBytes = new byte[1024];
            int BytesRec = Client.Receive(GetBytes);
            // Переводим из массива битов в строку
            GetInformation += Encoding.Unicode.GetString(GetBytes, 0, BytesRec);
            return GetInformation;
        }

        // Отправка информации на сервер
        public static void SendDataToServer(string Data)
        {
            // Используем unicode, чтобы не было проблем с кодировкой, при приеме информации
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
            // Посылаем клиенту новое сообщение
            SendDataToServer(tbMessage.Text);
            // Добавляем в историю свое же сообщение + ник + время написания
            lbHistory.Items.Add(DateTime.Now.ToShortTimeString() + " Client: " + tbMessage.Text.ToString());
            // Автопрокрутка истории чата
            lbHistory.TopIndex = lbHistory.Items.Count - 1;
            // Убираем текст из поля ввода
            tbMessage.Text = "";
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            List<int> set1 = new List<int> { 5, 3, 1 };
            List<int> set2 = new List<int> { 4, 2 }; 

        }

        }
    }
