namespace CRMapp
{
    partial class AddNoteForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddBtn = new System.Windows.Forms.Button();
            this.NoteBox = new System.Windows.Forms.TextBox();
            this.NoteLbl = new System.Windows.Forms.Label();
            this.NoteDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.RepeatBox = new System.Windows.Forms.CheckBox();
            this.DateLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddBtn
            // 
            this.AddBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AddBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.AddBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.AddBtn.Location = new System.Drawing.Point(197, 226);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(75, 23);
            this.AddBtn.TabIndex = 4;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = false;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // NoteBox
            // 
            this.NoteBox.Location = new System.Drawing.Point(23, 36);
            this.NoteBox.Multiline = true;
            this.NoteBox.Name = "NoteBox";
            this.NoteBox.Size = new System.Drawing.Size(231, 106);
            this.NoteBox.TabIndex = 1;
            // 
            // NoteLbl
            // 
            this.NoteLbl.AutoSize = true;
            this.NoteLbl.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoteLbl.Location = new System.Drawing.Point(20, 18);
            this.NoteLbl.Name = "NoteLbl";
            this.NoteLbl.Size = new System.Drawing.Size(62, 15);
            this.NoteLbl.TabIndex = 0;
            this.NoteLbl.Text = "Σημείωση";
            // 
            // NoteDateTimePicker
            // 
            this.NoteDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.NoteDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NoteDateTimePicker.Location = new System.Drawing.Point(23, 177);
            this.NoteDateTimePicker.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.NoteDateTimePicker.Name = "NoteDateTimePicker";
            this.NoteDateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NoteDateTimePicker.Size = new System.Drawing.Size(99, 20);
            this.NoteDateTimePicker.TabIndex = 2;
            // 
            // RepeatBox
            // 
            this.RepeatBox.AutoSize = true;
            this.RepeatBox.Location = new System.Drawing.Point(23, 203);
            this.RepeatBox.Name = "RepeatBox";
            this.RepeatBox.Size = new System.Drawing.Size(144, 17);
            this.RepeatBox.TabIndex = 3;
            this.RepeatBox.Text = "  επανάληψη κάθε μήνα";
            this.RepeatBox.UseVisualStyleBackColor = true;
            // 
            // DateLbl
            // 
            this.DateLbl.AutoSize = true;
            this.DateLbl.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLbl.Location = new System.Drawing.Point(20, 159);
            this.DateLbl.Name = "DateLbl";
            this.DateLbl.Size = new System.Drawing.Size(74, 15);
            this.DateLbl.TabIndex = 5;
            this.DateLbl.Text = "Ημερομηνία";
            // 
            // AddNoteForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.DateLbl);
            this.Controls.Add(this.RepeatBox);
            this.Controls.Add(this.NoteDateTimePicker);
            this.Controls.Add(this.NoteLbl);
            this.Controls.Add(this.NoteBox);
            this.Controls.Add(this.AddBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddNoteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Προσθήκη Σημείωσης";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.TextBox NoteBox;
        private System.Windows.Forms.Label NoteLbl;
        private System.Windows.Forms.DateTimePicker NoteDateTimePicker;
        private System.Windows.Forms.CheckBox RepeatBox;
        private System.Windows.Forms.Label DateLbl;
    }
}