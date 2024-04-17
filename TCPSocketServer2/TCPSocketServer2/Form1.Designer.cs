namespace TCPSocketServer2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbPort = new TextBox();
            tbMessage = new TextBox();
            bConnect = new Button();
            button2 = new Button();
            lbHistory = new ListBox();
            tbAddress = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnSendData = new Button();
            SuspendLayout();
            // 
            // tbPort
            // 
            tbPort.Location = new Point(193, 60);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(222, 27);
            tbPort.TabIndex = 0;
            // 
            // tbMessage
            // 
            tbMessage.Location = new Point(242, 380);
            tbMessage.Name = "tbMessage";
            tbMessage.Size = new Size(441, 27);
            tbMessage.TabIndex = 1;
            // 
            // bConnect
            // 
            bConnect.Location = new Point(460, 20);
            bConnect.Name = "bConnect";
            bConnect.Size = new Size(213, 69);
            bConnect.TabIndex = 2;
            bConnect.Text = "Подключится";
            bConnect.UseVisualStyleBackColor = true;
            bConnect.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(768, 351);
            button2.Name = "button2";
            button2.Size = new Size(213, 78);
            button2.TabIndex = 3;
            button2.Text = "Отправить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // lbHistory
            // 
            lbHistory.FormattingEnabled = true;
            lbHistory.ItemHeight = 20;
            lbHistory.Location = new Point(37, 124);
            lbHistory.Name = "lbHistory";
            lbHistory.Size = new Size(959, 204);
            lbHistory.TabIndex = 4;
            // 
            // tbAddress
            // 
            tbAddress.Location = new Point(193, 20);
            tbAddress.Name = "tbAddress";
            tbAddress.Size = new Size(222, 27);
            tbAddress.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 60);
            label1.Name = "label1";
            label1.Size = new Size(143, 20);
            label1.TabIndex = 6;
            label1.Text = "Порт подключения";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 20);
            label2.Name = "label2";
            label2.Size = new Size(150, 20);
            label2.TabIndex = 7;
            label2.Text = "Адрес подключения";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 380);
            label3.Name = "label3";
            label3.Size = new Size(190, 20);
            label3.TabIndex = 8;
            label3.Text = "Сообщение для передачи";
            // 
            // btnSendData
            // 
            btnSendData.Location = new Point(323, 436);
            btnSendData.Name = "btnSendData";
            btnSendData.Size = new Size(335, 67);
            btnSendData.TabIndex = 9;
            btnSendData.Text = "button1";
            btnSendData.UseVisualStyleBackColor = true;
            btnSendData.Click += btnSendData_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1044, 529);
            Controls.Add(btnSendData);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbAddress);
            Controls.Add(lbHistory);
            Controls.Add(button2);
            Controls.Add(bConnect);
            Controls.Add(tbMessage);
            Controls.Add(tbPort);
            Name = "Form1";
            Text = "Клиентская часть";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbPort;
        private TextBox tbMessage;
        private Button bConnect;
        private Button button2;
        private ListBox lbHistory;
        private TextBox tbAddress;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnSendData;
    }
}