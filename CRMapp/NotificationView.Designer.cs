namespace CRMapp
{
    partial class NotificationView
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.CheckBtn = new System.Windows.Forms.Button();
            this.NotesPanel = new System.Windows.Forms.Panel();
            this.Date2Txt = new System.Windows.Forms.TextBox();
            this.RepeatTxt = new System.Windows.Forms.TextBox();
            this.IdTxt = new System.Windows.Forms.TextBox();
            this.NoteTxt = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.DateTxt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ControlsPanel.SuspendLayout();
            this.NotesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.AutoSize = true;
            this.ControlsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ControlsPanel.BackColor = System.Drawing.Color.White;
            this.ControlsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ControlsPanel.Controls.Add(this.DeleteBtn);
            this.ControlsPanel.Controls.Add(this.CheckBtn);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlsPanel.Location = new System.Drawing.Point(170, 0);
            this.ControlsPanel.MaximumSize = new System.Drawing.Size(50, 120);
            this.ControlsPanel.MinimumSize = new System.Drawing.Size(50, 40);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(50, 62);
            this.ControlsPanel.TabIndex = 0;
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DeleteBtn.BackColor = System.Drawing.Color.Red;
            this.DeleteBtn.Location = new System.Drawing.Point(0, 22);
            this.DeleteBtn.MaximumSize = new System.Drawing.Size(50, 60);
            this.DeleteBtn.MinimumSize = new System.Drawing.Size(50, 20);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(50, 28);
            this.DeleteBtn.TabIndex = 4;
            this.DeleteBtn.Text = "x";
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.CheckCancelBtn_Click);
            // 
            // CheckBtn
            // 
            this.CheckBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CheckBtn.BackColor = System.Drawing.Color.LimeGreen;
            this.CheckBtn.Location = new System.Drawing.Point(0, 0);
            this.CheckBtn.MaximumSize = new System.Drawing.Size(50, 60);
            this.CheckBtn.MinimumSize = new System.Drawing.Size(50, 20);
            this.CheckBtn.Name = "CheckBtn";
            this.CheckBtn.Size = new System.Drawing.Size(50, 24);
            this.CheckBtn.TabIndex = 3;
            this.CheckBtn.Text = "v";
            this.CheckBtn.UseVisualStyleBackColor = false;
            this.CheckBtn.Click += new System.EventHandler(this.CheckCancelBtn_Click);
            // 
            // NotesPanel
            // 
            this.NotesPanel.AutoSize = true;
            this.NotesPanel.Controls.Add(this.Date2Txt);
            this.NotesPanel.Controls.Add(this.RepeatTxt);
            this.NotesPanel.Controls.Add(this.IdTxt);
            this.NotesPanel.Controls.Add(this.NoteTxt);
            this.NotesPanel.Controls.Add(this.splitter1);
            this.NotesPanel.Controls.Add(this.DateTxt);
            this.NotesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesPanel.Location = new System.Drawing.Point(0, 0);
            this.NotesPanel.Name = "NotesPanel";
            this.NotesPanel.Size = new System.Drawing.Size(170, 62);
            this.NotesPanel.TabIndex = 0;
            // 
            // Date2Txt
            // 
            this.Date2Txt.Location = new System.Drawing.Point(0, 0);
            this.Date2Txt.Name = "Date2Txt";
            this.Date2Txt.Size = new System.Drawing.Size(0, 20);
            this.Date2Txt.TabIndex = 0;
            this.Date2Txt.TabStop = false;
            this.Date2Txt.Visible = false;
            // 
            // RepeatTxt
            // 
            this.RepeatTxt.Location = new System.Drawing.Point(0, 0);
            this.RepeatTxt.Name = "RepeatTxt";
            this.RepeatTxt.Size = new System.Drawing.Size(0, 20);
            this.RepeatTxt.TabIndex = 0;
            this.RepeatTxt.TabStop = false;
            this.RepeatTxt.Visible = false;
            // 
            // IdTxt
            // 
            this.IdTxt.Location = new System.Drawing.Point(0, 0);
            this.IdTxt.Name = "IdTxt";
            this.IdTxt.Size = new System.Drawing.Size(0, 20);
            this.IdTxt.TabIndex = 0;
            this.IdTxt.TabStop = false;
            this.IdTxt.Visible = false;
            // 
            // NoteTxt
            // 
            this.NoteTxt.AutoSize = true;
            this.NoteTxt.BackColor = System.Drawing.Color.White;
            this.NoteTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NoteTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NoteTxt.ForeColor = System.Drawing.Color.Maroon;
            this.NoteTxt.Location = new System.Drawing.Point(0, 19);
            this.NoteTxt.MaximumSize = new System.Drawing.Size(170, 80);
            this.NoteTxt.MaxLength = 3647;
            this.NoteTxt.MinimumSize = new System.Drawing.Size(170, 20);
            this.NoteTxt.Name = "NoteTxt";
            this.NoteTxt.ReadOnly = true;
            this.NoteTxt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.NoteTxt.Size = new System.Drawing.Size(170, 43);
            this.NoteTxt.TabIndex = 2;
            this.NoteTxt.Text = "";
            this.NoteTxt.Enter += new System.EventHandler(this.NoteTxt_Enter);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.White;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(0, 15);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(170, 4);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // DateTxt
            // 
            this.DateTxt.BackColor = System.Drawing.Color.White;
            this.DateTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DateTxt.Dock = System.Windows.Forms.DockStyle.Top;
            this.DateTxt.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.DateTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DateTxt.Location = new System.Drawing.Point(0, 0);
            this.DateTxt.Name = "DateTxt";
            this.DateTxt.ReadOnly = true;
            this.DateTxt.Size = new System.Drawing.Size(170, 15);
            this.DateTxt.TabIndex = 1;
            this.DateTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DateTxt.WordWrap = false;
            this.DateTxt.Enter += new System.EventHandler(this.DateTxt_Enter);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(220, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(16, 62);
            this.panel1.TabIndex = 0;
            // 
            // NotificationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.NotesPanel);
            this.Controls.Add(this.ControlsPanel);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(240, 120);
            this.MinimumSize = new System.Drawing.Size(240, 55);
            this.Name = "NotificationView";
            this.Size = new System.Drawing.Size(236, 62);
            this.ControlsPanel.ResumeLayout(false);
            this.NotesPanel.ResumeLayout(false);
            this.NotesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ControlsPanel;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Button CheckBtn;
        private System.Windows.Forms.Panel NotesPanel;
        private System.Windows.Forms.RichTextBox NoteTxt;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox DateTxt;
        private System.Windows.Forms.TextBox IdTxt;
        private System.Windows.Forms.TextBox RepeatTxt;
        private System.Windows.Forms.TextBox Date2Txt;
        private System.Windows.Forms.Panel panel1;
    }
}
