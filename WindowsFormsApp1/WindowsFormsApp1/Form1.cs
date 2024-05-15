using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<string> headlines = new List<string>();//Список всех тегов
        public List<string> links = new List<string>();//Список всех ссылок
        public List<string> images = new List<string>();//Список всех изображений
        public List<string> texts = new List<string>();//Список всех текстов
        private void button1_Click(object sender, EventArgs e)
        {
            string URL = textBox1.Text;
            WebClient client = new WebClient();

            byte[] bytedata = client.DownloadData(URL);
            richTextBox1.Text = Encoding.UTF8.GetString(bytedata);

            client.DownloadFile(URL, "Z:\\Index.html");

            webBrowser1.DocumentText = richTextBox1.Text;

            richTextBox2.Text = "";
            //Список тегов для поиска заголовков
            string tegsH = @"(<h[1-6].*[A-z].*>|<h[1-6]>)(.*[А-яЁё].*)(<\/h[1-6]>)";
            Regex regexH = new Regex(tegsH);
            //Список всех ссылок (название ссылки и сама ссылка)
            string href = "href=";
            //Список всех картинок
            string img = "<img src=";
            //Чистый неформатированный текст (например, из тегов <p>)
            string p = "<p>";

            //Создаем список в котороый запишем весь текст построчно
            List<string> splitLine = new List<string>();
            //Считываем текст из файла
            StreamReader myStreamReader = File.OpenText(@"Z:\\Index.html");
            //Записываем каждую строку текста отдельно
            while (!myStreamReader.EndOfStream)
            {
                splitLine.Add(myStreamReader.ReadLine());
            }
          
            foreach (var line in splitLine)//Проход всех строк текста
            {                
                for(int n = 1; n<=6;n++)//Поиск списка тегов заголовков
                {
                    if (regexH.IsMatch(line))
                    {
                            var rg = new Regex(@">(.*?)<");
                            var result = rg.Match(line).Groups[1].Value;
                            headlines.Add($"\tЗаголовок h{n}: {result}\r\n");
                            richTextBox2.Text += $"Заголовок h{n}: {result}\r\n";
                    }
                }

                //Поиск ссылок
                if (line.Contains(href))
                {
                    var rg = new Regex(@">(.*?)<");
                    var result = rg.Match(line).Groups[1].Value;
                    var rg1 = new Regex("<a.+?href=[\"'](.+?)[\"'].*?>");
                    var result1 = rg1.Match(line).Groups[1].Value;
                    if (result.Length > 1)
                        links.Add($"\tНазвание ссылки: {result}\r\n\t\t Ссылка: {result1}\r\n");  
                }

                //Поиск картинок
                if (line.Contains(img))
                {
                    var rg = new Regex("<img.+?src=[\"'](.+?)[\"'].*?>");
                    var result = rg.Match(line).Groups[1].Value;
                    var rg1 = new Regex("<img.+?alt=[\"'](.+?)[\"'].*?>");
                    var result1 = rg1.Match(line).Groups[1].Value;
                    images.Add($"\tКартинка: {result1}\r\n\t\t Ссылка: {result}\r\n");
                }
                //Поиск текста
                if (line.Contains(p))
                {
                    var rg = new Regex(@">(.*?)<");
                    var result = rg.Match(line).Groups[1].Value;
                    if (result.Length > 1)
                        texts.Add($"\tТекст: {result}\r\n");
                }
            }
            ShowAll();
        }

        private void ShowAll()
        {
            richTextBox2.Text = "Список всех тегов:\n";
            foreach (var h in headlines)
                richTextBox2.Text += h;
            richTextBox2.Text += "\nСписок всех ссылок:\n";
            foreach (var l in links)
                richTextBox2.Text += l;
            richTextBox2.Text += "\nСписок всех изображений:\n";
            foreach (var i in images)
                richTextBox2.Text += i;
            richTextBox2.Text += "\nСписок всех текстов:\n";
            foreach (var t in texts)
                richTextBox2.Text += t;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
