namespace CRMapp
{
    partial class AlertView
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
            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            {
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NotesPanel = new System.Windows.Forms.Panel();
            this.Date2Txt = new System.Windows.Forms.TextBox();
            this.RepeatTxt = new System.Windows.Forms.TextBox();
            this.IdTxt = new System.Windows.Forms.TextBox();
            this.AlertTxt = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.HeaderTxt = new System.Windows.Forms.TextBox();
            this.NotesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotesPanel
            // 
            this.NotesPanel.AutoSize = true;
            this.NotesPanel.Controls.Add(this.Date2Txt);
            this.NotesPanel.Controls.Add(this.RepeatTxt);
            this.NotesPanel.Controls.Add(this.IdTxt);
            this.NotesPanel.Controls.Add(this.AlertTxt);
            this.NotesPanel.Controls.Add(this.splitter1);
            this.NotesPanel.Controls.Add(this.HeaderTxt);
            this.NotesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesPanel.Location = new System.Drawing.Point(0, 0);
            this.NotesPanel.Name = "NotesPanel";
            this.NotesPanel.Size = new System.Drawing.Size(234, 69);
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
            // AlertTxt
            // 
            this.AlertTxt.AutoSize = true;
            this.AlertTxt.BackColor = System.Drawing.Color.White;
            this.AlertTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AlertTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AlertTxt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlertTxt.ForeColor = System.Drawing.Color.Black;
            this.AlertTxt.Location = new System.Drawing.Point(0, 22);
            this.AlertTxt.MaximumSize = new System.Drawing.Size(234, 80);
            this.AlertTxt.MaxLength = 3647;
            this.AlertTxt.MinimumSize = new System.Drawing.Size(234, 20);
            this.AlertTxt.Name = "AlertTxt";
            this.AlertTxt.ReadOnly = true;
            this.AlertTxt.Size = new System.Drawing.Size(234, 47);
            this.AlertTxt.TabIndex = 2;
            this.AlertTxt.Text = "";
            this.AlertTxt.Enter += new System.EventHandler(this.AlertTxt_Enter);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.DimGray;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(0, 18);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(234, 4);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // HeaderTxt
            // 
            this.HeaderTxt.BackColor = System.Drawing.Color.White;
            this.HeaderTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HeaderTxt.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderTxt.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderTxt.ForeColor = System.Drawing.Color.Black;
            this.HeaderTxt.Location = new System.Drawing.Point(0, 0);
            this.HeaderTxt.Name = "HeaderTxt";
            this.HeaderTxt.ReadOnly = true;
            this.HeaderTxt.Size = new System.Drawing.Size(234, 18);
            this.HeaderTxt.TabIndex = 1;
            this.HeaderTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.HeaderTxt.WordWrap = false;
            this.HeaderTxt.Enter += new System.EventHandler(this.HeaderTxt_Enter);
            // 
            // AlertView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.NotesPanel);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MaximumSize = new System.Drawing.Size(238, 120);
            this.MinimumSize = new System.Drawing.Size(238, 55);
            this.Name = "AlertView";
            this.Size = new System.Drawing.Size(234, 69);
            this.NotesPanel.ResumeLayout(false);
            this.NotesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel NotesPanel;
        private System.Windows.Forms.RichTextBox AlertTxt;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox HeaderTxt;
        private System.Windows.Forms.TextBox IdTxt;
        private System.Windows.Forms.TextBox RepeatTxt;
        private System.Windows.Forms.TextBox Date2Txt;
    }
}
