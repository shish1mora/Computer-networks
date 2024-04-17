
namespace TCPSocketServer
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
            label1 = new Label();
            label2 = new Label();
            lbHistory = new ListBox();
            bConnect = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // tbPort
            // 
            tbPort.Location = new Point(140, 44);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(542, 27);
            tbPort.TabIndex = 0;
            // 
            // tbMessage
            // 
            tbMessage.Location = new Point(247, 489);
            tbMessage.Name = "tbMessage";
            tbMessage.Size = new Size(435, 27);
            tbMessage.TabIndex = 1;
            tbMessage.TextChanged += tbMessage_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(141, 20);
            label1.TabIndex = 2;
            label1.Text = "Порт кодключения";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(51, 489);
            label2.Name = "label2";
            label2.Size = new Size(190, 20);
            label2.TabIndex = 3;
            label2.Text = "Сообщение для передачи";
            // 
            // lbHistory
            // 
            lbHistory.FormattingEnabled = true;
            lbHistory.ItemHeight = 20;
            lbHistory.Location = new Point(51, 157);
            lbHistory.Name = "lbHistory";
            lbHistory.Size = new Size(1039, 244);
            lbHistory.TabIndex = 4;
            // 
            // bConnect
            // 
            bConnect.Location = new Point(705, 44);
            bConnect.Name = "bConnect";
            bConnect.Size = new Size(385, 69);
            bConnect.TabIndex = 5;
            bConnect.Text = "Запустить сервер";
            bConnect.UseVisualStyleBackColor = true;
            bConnect.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(705, 486);
            button2.Name = "button2";
            button2.Size = new Size(370, 74);
            button2.TabIndex = 6;
            button2.Text = "Отправить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1148, 629);
            Controls.Add(button2);
            Controls.Add(bConnect);
            Controls.Add(lbHistory);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbMessage);
            Controls.Add(tbPort);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbPort;
        private TextBox tbMessage;
        private Label label1;
        private Label label2;
        private ListBox lbHistory;
        private Button bConnect;
        private Button button2;
    }
}