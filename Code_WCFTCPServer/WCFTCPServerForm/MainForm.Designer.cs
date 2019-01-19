namespace WCFTCPServerForm
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.listBoxClient = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.contextMenuStripLBClient = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.클라이언트짜르기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.클라이언트에메시지보내기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripLBClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(12, 96);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(582, 280);
            this.listBoxLog.TabIndex = 0;
            // 
            // listBoxClient
            // 
            this.listBoxClient.ContextMenuStrip = this.contextMenuStripLBClient;
            this.listBoxClient.FormattingEnabled = true;
            this.listBoxClient.ItemHeight = 12;
            this.listBoxClient.Location = new System.Drawing.Point(600, 36);
            this.listBoxClient.Name = "listBoxClient";
            this.listBoxClient.Size = new System.Drawing.Size(110, 340);
            this.listBoxClient.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "접속 중인 클라이언트";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(61, 15);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(450, 21);
            this.textBoxMessage.TabIndex = 0;
            // 
            // contextMenuStripLBClient
            // 
            this.contextMenuStripLBClient.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.클라이언트짜르기ToolStripMenuItem,
            this.클라이언트에메시지보내기ToolStripMenuItem});
            this.contextMenuStripLBClient.Name = "contextMenuStripLBClient";
            this.contextMenuStripLBClient.Size = new System.Drawing.Size(227, 48);
            // 
            // 클라이언트짜르기ToolStripMenuItem
            // 
            this.클라이언트짜르기ToolStripMenuItem.Name = "클라이언트짜르기ToolStripMenuItem";
            this.클라이언트짜르기ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.클라이언트짜르기ToolStripMenuItem.Text = "클라이언트 짜르기";
            this.클라이언트짜르기ToolStripMenuItem.Click += new System.EventHandler(this.클라이언트짜르기ToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "메시지:";
            // 
            // 클라이언트에메시지보내기ToolStripMenuItem
            // 
            this.클라이언트에메시지보내기ToolStripMenuItem.Name = "클라이언트에메시지보내기ToolStripMenuItem";
            this.클라이언트에메시지보내기ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.클라이언트에메시지보내기ToolStripMenuItem.Text = "클라이언트에 메시지 보내기";
            this.클라이언트에메시지보내기ToolStripMenuItem.Click += new System.EventHandler(this.클라이언트에메시지보내기ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 382);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxClient);
            this.Controls.Add(this.listBoxLog);
            this.Name = "MainForm";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStripLBClient.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.ListBox listBoxClient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLBClient;
        private System.Windows.Forms.ToolStripMenuItem 클라이언트짜르기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 클라이언트에메시지보내기ToolStripMenuItem;
        private System.Windows.Forms.Label label2;
    }
}

