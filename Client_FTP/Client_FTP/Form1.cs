using BytesRoad.Net.Ftp;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Client_FTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       


        private void button1_Click(object sender, EventArgs e)
        {

            FtpClient client = new FtpClient();
            client.PassiveMode = true;

            int TimeoutFTP = 30000; //Таймаут.
            string FTP_SERVER = "localhost";
            int FTP_PORT = 21;
            string FTP_USER = "anonymous";
            string FTP_PASSWORD = "";

            /* FtpProxyInfo pinfo = new FtpProxyInfo(); //Это переменная параметров.
             pinfo.Server = "192.168.1.240";
             pinfo.Port = 3128; //Порт.
             pinfo.Type = FtpProxyType.HttpConnect; //Тип прокси - всего 4 вида.
             pinfo.PreAuthenticate = true; //Если на прокси есть идентификация
             pinfo.User = "one";
             pinfo.Password = "1234";
             //Присваиваем параметры прокси клиенту.
             client.ProxyInfo = pinfo;*/

            //Подключаемся к FTP серверу.
            try
            {
                client.Connect(TimeoutFTP, FTP_SERVER, FTP_PORT);
            }
            catch (BytesRoad.Net.Ftp.FtpTimeoutException error)
            {
                MessageBox.Show("Время ожидания истекло! Сервер не отвечает. " + error.Message,
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (System.Net.Sockets.SocketException error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                client.Login(TimeoutFTP, FTP_USER, FTP_PASSWORD);
            }
            catch (BytesRoad.Net.Ftp.FtpTimeoutException error)
            {
                MessageBox.Show("Время ожидания истекло! Сервер не отвечает. " + error.Message,
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (System.Net.Sockets.SocketException error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                string sourceDirectory = folderBrowserDialog.SelectedPath;
                string ftpServerPath = "ftp://localhost:21"; // FTP адрес и путь к удаленной папке на сервере
                CopyDirectoryToFTP(sourceDirectory, ftpServerPath, FTP_USER, FTP_PASSWORD);

                Console.WriteLine("Структура каталогов успешно воссоздана на FTP сервере.");
            }
        }
        static void CopyDirectoryToFTP(string sourceDirectory, string ftpServerPath, string ftpUsername, string ftpPassword)
        {
            // Получаем информацию о всех подпапках и файлов в исходной папке
            string[] directories = Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories);

            // Создаем структуру каталогов на FTP сервере
            foreach (string dirPath in directories)
            {
                string relativePath = dirPath.Replace(sourceDirectory, "").Replace("\\", "/");
                CreateDirectory(ftpServerPath + relativePath, ftpUsername, ftpPassword);
            }
        }
        static void CreateDirectory(string directoryPath, string ftpUsername, string ftpPassword)
        {
            try
            {
                // Создаем запрос FTP для создания папки
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directoryPath);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                // Получаем ответ от сервера
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Папка создана успешно: {directoryPath}");
                }
            }
            catch (WebException ex)
            {
                // Если папка уже существует, игнорируем ошибку
                if (((FtpWebResponse)ex.Response).StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    Console.WriteLine($"Папка уже существует: {directoryPath}");
                }
                else
                {
                    Console.WriteLine($"Ошибка при создании папки: {ex.Message}");
                }
            }
        }

        /*FtpItem[] dirs = client.GetDirectoryList(TimeoutFTP);
        foreach (FtpItem dir in dirs)
        {
            listBox1.Items.Add(dir.Name);
        }
        if (dirs.Count() > 0)
        {
            saveFileDialog1.FileName = dirs[0].Name;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                client.GetFile(TimeoutFTP, saveFileDialog1.FileName, dirs[0].Name);
            }
        }*/

    }

    
}

