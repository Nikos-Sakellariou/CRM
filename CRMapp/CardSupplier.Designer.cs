namespace CRMapp
{
    partial class CardSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardSupplier));
            this.SearchNameTxt = new System.Windows.Forms.TextBox();
            this.SearchNameListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchAfmTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectNameCmb = new System.Windows.Forms.ComboBox();
            this.RetrieveBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DateTo = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.DateFrom = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.CardSupplierLst = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BankDepositPanel = new System.Windows.Forms.Panel();
            this.BankDepositDocTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BankDepositPriceTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BankDepositPck = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.BankDepositBtn = new System.Windows.Forms.Button();
            this.PrintDoc = new System.Drawing.Printing.PrintDocument();
            this.PrintDial = new System.Windows.Forms.PrintDialog();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.PreviewDoc = new System.Drawing.Printing.PrintDocument();
            this.PrintPrev = new System.Windows.Forms.PrintPreviewDialog();
            this.IdTxt = new System.Windows.Forms.Label();
            this.AfmTxt = new System.Windows.Forms.Label();
            this.NameTxt = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.BankDepositPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchNameTxt
            // 
            this.SearchNameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchNameTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SearchNameTxt.Location = new System.Drawing.Point(96, 53);
            this.SearchNameTxt.MaxLength = 100;
            this.SearchNameTxt.Name = "SearchNameTxt";
            this.SearchNameTxt.Size = new System.Drawing.Size(250, 21);
            this.SearchNameTxt.TabIndex = 1;
            this.SearchNameTxt.TextChanged += new System.EventHandler(this.NameTxt_TextChanged);
            this.SearchNameTxt.Enter += new System.EventHandler(this.SearchNameTxt_Enter);
            this.SearchNameTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchNameTxt_KeyDown);
            this.SearchNameTxt.Leave += new System.EventHandler(this.SearchNameTxt_Leave);
            // 
            // SearchNameListBox
            // 
            this.SearchNameListBox.FormattingEnabled = true;
            this.SearchNameListBox.Location = new System.Drawing.Point(96, 73);
            this.SearchNameListBox.Name = "SearchNameListBox";
            this.SearchNameListBox.Size = new System.Drawing.Size(250, 17);
            this.SearchNameListBox.TabIndex = 2;
            this.SearchNameListBox.Visible = false;
            this.SearchNameListBox.Click += new System.EventHandler(this.SearchNameListBox_Click);
            this.SearchNameListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchNameListBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Αναζήτηση Επωνυμίας";
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(93, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Αναζήτηση Α.Φ.Μ.";
            // 
            // SearchAfmTxt
            // 
            this.SearchAfmTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchAfmTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.SearchAfmTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchAfmTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SearchAfmTxt.Location = new System.Drawing.Point(96, 163);
            this.SearchAfmTxt.MaxLength = 9;
            this.SearchAfmTxt.Name = "SearchAfmTxt";
            this.SearchAfmTxt.Size = new System.Drawing.Size(92, 21);
            this.SearchAfmTxt.TabIndex = 4;
            this.SearchAfmTxt.Enter += new System.EventHandler(this.SearchAfmTxt_Enter);
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(93, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Επιλογή Επωνυμίας";
            // 
            // SelectNameCmb
            // 
            this.SelectNameCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectNameCmb.FormattingEnabled = true;
            this.SelectNameCmb.Location = new System.Drawing.Point(96, 108);
            this.SelectNameCmb.MaxDropDownItems = 3;
            this.SelectNameCmb.Name = "SelectNameCmb";
            this.SelectNameCmb.Size = new System.Drawing.Size(250, 21);
            this.SelectNameCmb.TabIndex = 3;
            this.SelectNameCmb.Enter += new System.EventHandler(this.SelectNameCmb_Enter);
            // 
            // RetrieveBtn
            // 
            this.RetrieveBtn.Location = new System.Drawing.Point(24, 224);
            this.RetrieveBtn.Name = "RetrieveBtn";
            this.RetrieveBtn.Size = new System.Drawing.Size(251, 33);
            this.RetrieveBtn.TabIndex = 5;
            this.RetrieveBtn.Text = "Ανάκτηση";
            this.RetrieveBtn.UseVisualStyleBackColor = true;
            this.RetrieveBtn.Click += new System.EventHandler(this.RetrieveBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DateTo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.DateFrom);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.RetrieveBtn);
            this.panel1.Location = new System.Drawing.Point(70, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 278);
            this.panel1.TabIndex = 0;
            // 
            // DateTo
            // 
            this.DateTo.CustomFormat = "dd/MM/yyyy";
            this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTo.Location = new System.Drawing.Point(175, 197);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(100, 20);
            this.DateTo.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(172, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "Έως";
            // 
            // DateFrom
            // 
            this.DateFrom.CustomFormat = "dd/MM/yyyy";
            this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateFrom.Location = new System.Drawing.Point(25, 197);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(100, 20);
            this.DateFrom.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 21);
            this.label8.TabIndex = 7;
            this.label8.Text = "Από";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 635);
            this.splitter1.TabIndex = 23;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(3, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 635);
            this.splitter2.TabIndex = 24;
            this.splitter2.TabStop = false;
            // 
            // CardSupplierLst
            // 
            this.CardSupplierLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.CardSupplierLst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardSupplierLst.FullRowSelect = true;
            this.CardSupplierLst.GridLines = true;
            this.CardSupplierLst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.CardSupplierLst.Location = new System.Drawing.Point(70, 328);
            this.CardSupplierLst.MultiSelect = false;
            this.CardSupplierLst.Name = "CardSupplierLst";
            this.CardSupplierLst.Size = new System.Drawing.Size(747, 266);
            this.CardSupplierLst.TabIndex = 31;
            this.CardSupplierLst.TileSize = new System.Drawing.Size(5, 5);
            this.CardSupplierLst.UseCompatibleStateImageBehavior = false;
            this.CardSupplierLst.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ΗΜΕΡΟΜΗΝΙΑ";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ΠΑΡΑΣΤΑΤΙΚΟ";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ΑΙΤΙΟΛΟΓΙΑ";
            this.columnHeader3.Width = 313;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ΧΡΕΩΣΗ";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 69;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ΠΙΣΤΩΣΗ";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 69;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ΠΙΣΤ. ΥΠΟΛ.";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 72;
            // 
            // BankDepositPanel
            // 
            this.BankDepositPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BankDepositPanel.Controls.Add(this.BankDepositDocTxt);
            this.BankDepositPanel.Controls.Add(this.label9);
            this.BankDepositPanel.Controls.Add(this.label4);
            this.BankDepositPanel.Controls.Add(this.BankDepositPriceTxt);
            this.BankDepositPanel.Controls.Add(this.label5);
            this.BankDepositPanel.Controls.Add(this.BankDepositPck);
            this.BankDepositPanel.Controls.Add(this.label6);
            this.BankDepositPanel.Controls.Add(this.BankDepositBtn);
            this.BankDepositPanel.Enabled = false;
            this.BankDepositPanel.Location = new System.Drawing.Point(510, 20);
            this.BankDepositPanel.Name = "BankDepositPanel";
            this.BankDepositPanel.Size = new System.Drawing.Size(305, 278);
            this.BankDepositPanel.TabIndex = 33;
            // 
            // BankDepositDocTxt
            // 
            this.BankDepositDocTxt.Location = new System.Drawing.Point(24, 144);
            this.BankDepositDocTxt.MaxLength = 50;
            this.BankDepositDocTxt.Name = "BankDepositDocTxt";
            this.BankDepositDocTxt.Size = new System.Drawing.Size(251, 20);
            this.BankDepositDocTxt.TabIndex = 37;
            // 
            // label9
            // 
            this.label9.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 21);
            this.label9.TabIndex = 36;
            this.label9.Text = "Παραστατικό";
            // 
            // label4
            // 
            this.label4.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(279, 21);
            this.label4.TabIndex = 33;
            this.label4.Text = "Καταχώρηση Εμβάσματος σε Προμηθευτή";
            // 
            // BankDepositPriceTxt
            // 
            this.BankDepositPriceTxt.Location = new System.Drawing.Point(24, 199);
            this.BankDepositPriceTxt.MaxLength = 15;
            this.BankDepositPriceTxt.Name = "BankDepositPriceTxt";
            this.BankDepositPriceTxt.Size = new System.Drawing.Size(100, 20);
            this.BankDepositPriceTxt.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ποσό";
            // 
            // BankDepositPck
            // 
            this.BankDepositPck.CustomFormat = "dd/MM/yyyy";
            this.BankDepositPck.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.BankDepositPck.Location = new System.Drawing.Point(24, 88);
            this.BankDepositPck.Name = "BankDepositPck";
            this.BankDepositPck.Size = new System.Drawing.Size(100, 20);
            this.BankDepositPck.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 21);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ημερομηνία";
            // 
            // BankDepositBtn
            // 
            this.BankDepositBtn.Location = new System.Drawing.Point(24, 225);
            this.BankDepositBtn.Name = "BankDepositBtn";
            this.BankDepositBtn.Size = new System.Drawing.Size(251, 33);
            this.BankDepositBtn.TabIndex = 5;
            this.BankDepositBtn.Text = "Καταχώρηση";
            this.BankDepositBtn.UseVisualStyleBackColor = true;
            this.BankDepositBtn.Click += new System.EventHandler(this.BankDepositBtn_Click);
            // 
            // PrintDoc
            // 
            this.PrintDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDoc_PrintPage);
            // 
            // PrintDial
            // 
            this.PrintDial.UseEXDialog = true;
            // 
            // PrintBtn
            // 
            this.PrintBtn.BackColor = System.Drawing.Color.DarkOrange;
            this.PrintBtn.Location = new System.Drawing.Point(70, 600);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(126, 27);
            this.PrintBtn.TabIndex = 34;
            this.PrintBtn.Text = "Εκτύπωση";
            this.PrintBtn.UseVisualStyleBackColor = false;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // PreviewDoc
            // 
            this.PreviewDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PreviewDoc_PrintPage);
            // 
            // PrintPrev
            // 
            this.PrintPrev.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.PrintPrev.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.PrintPrev.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PrintPrev.ClientSize = new System.Drawing.Size(400, 300);
            this.PrintPrev.Enabled = true;
            this.PrintPrev.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintPrev.Icon")));
            this.PrintPrev.Name = "PrintPrev";
            this.PrintPrev.UseAntiAlias = true;
            this.PrintPrev.Visible = false;
            // 
            // IdTxt
            // 
            this.IdTxt.AutoSize = true;
            this.IdTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdTxt.ForeColor = System.Drawing.Color.DarkBlue;
            this.IdTxt.Location = new System.Drawing.Point(70, 309);
            this.IdTxt.Name = "IdTxt";
            this.IdTxt.Size = new System.Drawing.Size(0, 13);
            this.IdTxt.TabIndex = 0;
            // 
            // AfmTxt
            // 
            this.AfmTxt.AutoSize = true;
            this.AfmTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AfmTxt.ForeColor = System.Drawing.Color.DarkBlue;
            this.AfmTxt.Location = new System.Drawing.Point(148, 309);
            this.AfmTxt.Name = "AfmTxt";
            this.AfmTxt.Size = new System.Drawing.Size(0, 13);
            this.AfmTxt.TabIndex = 0;
            // 
            // NameTxt
            // 
            this.NameTxt.AutoSize = true;
            this.NameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTxt.ForeColor = System.Drawing.Color.DarkBlue;
            this.NameTxt.Location = new System.Drawing.Point(243, 309);
            this.NameTxt.Name = "NameTxt";
            this.NameTxt.Size = new System.Drawing.Size(0, 13);
            this.NameTxt.TabIndex = 0;
            // 
            // CardSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NameTxt);
            this.Controls.Add(this.AfmTxt);
            this.Controls.Add(this.IdTxt);
            this.Controls.Add(this.PrintBtn);
            this.Controls.Add(this.BankDepositPanel);
            this.Controls.Add(this.CardSupplierLst);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.SearchNameListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchNameTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SelectNameCmb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SearchAfmTxt);
            this.Controls.Add(this.panel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "CardSupplier";
            this.Size = new System.Drawing.Size(845, 635);
            this.panel1.ResumeLayout(false);
            this.BankDepositPanel.ResumeLayout(false);
            this.BankDepositPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox SearchNameTxt;
        private System.Windows.Forms.ListBox SearchNameListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SearchAfmTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SelectNameCmb;
        private System.Windows.Forms.Button RetrieveBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.DateTimePicker DateTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker DateFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView CardSupplierLst;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Panel BankDepositPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker BankDepositPck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BankDepositBtn;
        private System.Windows.Forms.TextBox BankDepositPriceTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox BankDepositDocTxt;
        private System.Windows.Forms.Label label9;
        private System.Drawing.Printing.PrintDocument PrintDoc;
        private System.Windows.Forms.PrintDialog PrintDial;
        private System.Windows.Forms.Button PrintBtn;
        private System.Drawing.Printing.PrintDocument PreviewDoc;
        private System.Windows.Forms.PrintPreviewDialog PrintPrev;
        private System.Windows.Forms.Label IdTxt;
        private System.Windows.Forms.Label AfmTxt;
        private System.Windows.Forms.Label NameTxt;
    }
}
