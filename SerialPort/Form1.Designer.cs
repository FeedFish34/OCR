﻿namespace OCRSerialPort
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.btnConn = new System.Windows.Forms.Button();
            this.btnColse = new System.Windows.Forms.Button();
            this.btnPhoto = new System.Windows.Forms.Button();
            this.tscbxCameras = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Location = new System.Drawing.Point(46, 130);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(668, 320);
            this.videoSourcePlayer1.TabIndex = 0;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // btnConn
            // 
            this.btnConn.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConn.Location = new System.Drawing.Point(436, 38);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(75, 28);
            this.btnConn.TabIndex = 1;
            this.btnConn.Text = "连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // btnColse
            // 
            this.btnColse.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnColse.Location = new System.Drawing.Point(538, 38);
            this.btnColse.Name = "btnColse";
            this.btnColse.Size = new System.Drawing.Size(75, 28);
            this.btnColse.TabIndex = 1;
            this.btnColse.Text = "关闭";
            this.btnColse.UseVisualStyleBackColor = true;
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // btnPhoto
            // 
            this.btnPhoto.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPhoto.Location = new System.Drawing.Point(639, 38);
            this.btnPhoto.Name = "btnPhoto";
            this.btnPhoto.Size = new System.Drawing.Size(75, 28);
            this.btnPhoto.TabIndex = 1;
            this.btnPhoto.Text = "拍照";
            this.btnPhoto.UseVisualStyleBackColor = true;
            this.btnPhoto.Click += new System.EventHandler(this.btnPhoto_Click);
            // 
            // tscbxCameras
            // 
            this.tscbxCameras.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tscbxCameras.FormattingEnabled = true;
            this.tscbxCameras.Location = new System.Drawing.Point(210, 40);
            this.tscbxCameras.Name = "tscbxCameras";
            this.tscbxCameras.Size = new System.Drawing.Size(197, 26);
            this.tscbxCameras.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(42, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "视频输入设备：";
            // 
            // txtIp
            // 
            this.txtIp.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIp.Location = new System.Drawing.Point(134, 90);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(236, 28);
            this.txtIp.TabIndex = 4;
            this.txtIp.Text = "192.168.1.105";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPort.Location = new System.Drawing.Point(525, 91);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(189, 28);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "8080";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(42, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "网口IP：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(432, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "端口：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 483);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tscbxCameras);
            this.Controls.Add(this.btnPhoto);
            this.Controls.Add(this.btnColse);
            this.Controls.Add(this.btnConn);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button btnColse;
        private System.Windows.Forms.Button btnPhoto;
        private System.Windows.Forms.ComboBox tscbxCameras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

