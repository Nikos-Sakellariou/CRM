namespace CRMapp
{
    partial class OrderFindInvoice
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
            this.label1 = new System.Windows.Forms.Label();
            this.RetrieveBtn = new System.Windows.Forms.Button();
            this.InvoiceLbx = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Επιλογή Τιμολογίων";
            // 
            // RetrieveBtn
            // 
            this.RetrieveBtn.Location = new System.Drawing.Point(19, 213);
            this.RetrieveBtn.Name = "RetrieveBtn";
            this.RetrieveBtn.Size = new System.Drawing.Size(250, 33);
            this.RetrieveBtn.TabIndex = 12;
            this.RetrieveBtn.Text = "Ανάκτηση";
            this.RetrieveBtn.UseVisualStyleBackColor = true;
            this.RetrieveBtn.Click += new System.EventHandler(this.RetrieveBtn_Click);
            // 
            // InvoiceLbx
            // 
            this.InvoiceLbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.InvoiceLbx.FormattingEnabled = true;
            this.InvoiceLbx.ItemHeight = 15;
            this.InvoiceLbx.Location = new System.Drawing.Point(19, 38);
            this.InvoiceLbx.Name = "InvoiceLbx";
            this.InvoiceLbx.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.InvoiceLbx.Size = new System.Drawing.Size(250, 154);
            this.InvoiceLbx.TabIndex = 14;
            // 
            // OrderFindInvoice
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.InvoiceLbx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RetrieveBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderFindInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Εύρεση Τιμολογίων";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RetrieveBtn;
        private System.Windows.Forms.ListBox InvoiceLbx;
    }
}