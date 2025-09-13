namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboRssUrl = new System.Windows.Forms.ComboBox();
            this.btnLoadRss = new System.Windows.Forms.Button();
            this.dataGridViewRss = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.cboDate = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRss)).BeginInit();
            this.SuspendLayout();
            // 
            // cboRssUrl
            // 
            this.cboRssUrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRssUrl.FormattingEnabled = true;
            this.cboRssUrl.Location = new System.Drawing.Point(80, 15);
            this.cboRssUrl.Name = "cboRssUrl";
            this.cboRssUrl.Size = new System.Drawing.Size(500, 24);
            this.cboRssUrl.TabIndex = 0;
            // 
            // btnLoadRss
            // 
            this.btnLoadRss.Location = new System.Drawing.Point(590, 13);
            this.btnLoadRss.Name = "btnLoadRss";
            this.btnLoadRss.Size = new System.Drawing.Size(100, 26);
            this.btnLoadRss.TabIndex = 1;
            this.btnLoadRss.Text = "Tải RSS";
            this.btnLoadRss.UseVisualStyleBackColor = true;
            this.btnLoadRss.Click += new System.EventHandler(this.btnLoadRss_Click);
            // 
            // dataGridViewRss
            // 
            this.dataGridViewRss.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRss.Location = new System.Drawing.Point(12, 100);
            this.dataGridViewRss.Name = "dataGridViewRss";
            this.dataGridViewRss.RowHeadersWidth = 51;
            this.dataGridViewRss.RowTemplate.Height = 24;
            this.dataGridViewRss.Size = new System.Drawing.Size(776, 320);
            this.dataGridViewRss.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 430);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(64, 16);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Sẵn sàng";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(12, 18);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(81, 16);
            this.lblUrl.TabIndex = 5;
            this.lblUrl.Text = "Nguồn RSS:";
            // 
            // cboDate
            // 
            this.cboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDate.FormattingEnabled = true;
            this.cboDate.Location = new System.Drawing.Point(80, 45);
            this.cboDate.Name = "cboDate";
            this.cboDate.Size = new System.Drawing.Size(500, 24);
            this.cboDate.TabIndex = 2;
            this.cboDate.SelectedIndexChanged += new System.EventHandler(this.cboDate_SelectedIndexChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 48);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(43, 16);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Ngày:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.cboDate);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dataGridViewRss);
            this.Controls.Add(this.btnLoadRss);
            this.Controls.Add(this.cboRssUrl);
            this.Name = "Form1";
            this.Text = "RSS Reader - Kết quả xổ số";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRss)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRssUrl;
        private System.Windows.Forms.Button btnLoadRss;
        private System.Windows.Forms.DataGridView dataGridViewRss;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.ComboBox cboDate;
        private System.Windows.Forms.Label lblDate;
    }
}

