namespace IOSLogViewer
{
    partial class IOSLogViewer
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
            this.logView = new System.Windows.Forms.DataGridView();
            this.LogDetail = new System.Windows.Forms.TextBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.IsFollowLast = new System.Windows.Forms.CheckBox();
            this.ConditionText = new System.Windows.Forms.TextBox();
            this.StopBtn = new System.Windows.Forms.Button();
            this.ClearBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logView)).BeginInit();
            this.SuspendLayout();
            // 
            // logView
            // 
            this.logView.AllowUserToAddRows = false;
            this.logView.AllowUserToDeleteRows = false;
            this.logView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logView.Location = new System.Drawing.Point(12, 44);
            this.logView.Name = "logView";
            this.logView.ReadOnly = true;
            this.logView.RowTemplate.Height = 23;
            this.logView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logView.Size = new System.Drawing.Size(1160, 567);
            this.logView.TabIndex = 0;
            // 
            // LogDetail
            // 
            this.LogDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogDetail.Location = new System.Drawing.Point(12, 617);
            this.LogDetail.Multiline = true;
            this.LogDetail.Name = "LogDetail";
            this.LogDetail.ReadOnly = true;
            this.LogDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogDetail.Size = new System.Drawing.Size(1160, 132);
            this.LogDetail.TabIndex = 1;
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(350, 10);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 2;
            this.StartBtn.Text = "开始";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // IsFollowLast
            // 
            this.IsFollowLast.AutoSize = true;
            this.IsFollowLast.Location = new System.Drawing.Point(1004, 17);
            this.IsFollowLast.Name = "IsFollowLast";
            this.IsFollowLast.Size = new System.Drawing.Size(168, 16);
            this.IsFollowLast.TabIndex = 4;
            this.IsFollowLast.Text = "自动选中并定位到最新日志";
            this.IsFollowLast.UseVisualStyleBackColor = true;
            this.IsFollowLast.Click += new System.EventHandler(this.IsFollowLast_Click);
            // 
            // ConditionText
            // 
            this.ConditionText.Location = new System.Drawing.Point(12, 12);
            this.ConditionText.Name = "ConditionText";
            this.ConditionText.Size = new System.Drawing.Size(332, 21);
            this.ConditionText.TabIndex = 5;
            this.ConditionText.TextChanged += new System.EventHandler(this.ConditionText_TextChanged);
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(431, 10);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 6;
            this.StopBtn.Text = "刷新";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(512, 10);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearBtn.TabIndex = 7;
            this.ClearBtn.Text = "清空";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // IOSLogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.ConditionText);
            this.Controls.Add(this.IsFollowLast);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.LogDetail);
            this.Controls.Add(this.logView);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 800);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "IOSLogViewer";
            this.Text = "IOS日志查看器";
            ((System.ComponentModel.ISupportInitialize)(this.logView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView logView;
        private System.Windows.Forms.TextBox LogDetail;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.CheckBox IsFollowLast;
        private System.Windows.Forms.TextBox ConditionText;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button ClearBtn;
    }
}

