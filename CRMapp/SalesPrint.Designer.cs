namespace CRMapp
{
    partial class SalesPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesPrint));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectNameCmb = new System.Windows.Forms.ComboBox();
            this.RetrieveBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DateFrom = new System.Windows.Forms.DateTimePicker();
            this.DocumentsLbx = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.PrintChkBox = new System.Windows.Forms.CheckBox();
            this.PrintPreviewChkBox = new System.Windows.Forms.CheckBox();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.PrintDoc = new System.Drawing.Printing.PrintDocument();
            this.PreviewDoc = new System.Drawing.Printing.PrintDocument();
            this.PrintPrev = new System.Windows.Forms.PrintPreviewDialog();
            this.PrintDial = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Από";
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Είδος Παραστατικού";
            // 
            // SelectNameCmb
            // 
            this.SelectNameCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectNameCmb.FormattingEnabled = true;
            this.SelectNameCmb.Items.AddRange(new object[] {
            "Τιμολόγιο",
            "Δελτίο Αποστολής",
            "Πιστωτικό Τιμολόγιο",
            "Απόδειξη Είσπραξης",
            "Απόδειξη Πληρωμής (σε Προμηθευτή)",
            "Δελτίο Αποστολής (σε Προμηθευτή)"});
            this.SelectNameCmb.Location = new System.Drawing.Point(25, 37);
            this.SelectNameCmb.MaxDropDownItems = 10;
            this.SelectNameCmb.Name = "SelectNameCmb";
            this.SelectNameCmb.Size = new System.Drawing.Size(250, 21);
            this.SelectNameCmb.TabIndex = 1;
            // 
            // RetrieveBtn
            // 
            this.RetrieveBtn.BackColor = System.Drawing.Color.MintCream;
            this.RetrieveBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RetrieveBtn.FlatAppearance.BorderSize = 10;
            this.RetrieveBtn.Location = new System.Drawing.Point(25, 131);
            this.RetrieveBtn.Name = "RetrieveBtn";
            this.RetrieveBtn.Size = new System.Drawing.Size(250, 33);
            this.RetrieveBtn.TabIndex = 4;
            this.RetrieveBtn.Text = "Ανάκτηση";
            this.RetrieveBtn.UseVisualStyleBackColor = false;
            this.RetrieveBtn.Click += new System.EventHandler(this.RetrieveBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DateTo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DateFrom);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SelectNameCmb);
            this.panel1.Controls.Add(this.RetrieveBtn);
            this.panel1.Location = new System.Drawing.Point(70, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 187);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            // 
            // DateTo
            // 
            this.DateTo.CustomFormat = "dd/MM/yyyy";
            this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTo.Location = new System.Drawing.Point(175, 92);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(100, 20);
            this.DateTo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(172, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Έως";
            // 
            // DateFrom
            // 
            this.DateFrom.CustomFormat = "dd/MM/yyyy";
            this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateFrom.Location = new System.Drawing.Point(25, 92);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(100, 20);
            this.DateFrom.TabIndex = 2;
            // 
            // DocumentsLbx
            // 
            this.DocumentsLbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.DocumentsLbx.FormattingEnabled = true;
            this.DocumentsLbx.ItemHeight = 15;
            this.DocumentsLbx.Location = new System.Drawing.Point(27, 37);
            this.DocumentsLbx.Name = "DocumentsLbx";
            this.DocumentsLbx.Size = new System.Drawing.Size(250, 154);
            this.DocumentsLbx.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.PrintChkBox);
            this.panel2.Controls.Add(this.PrintPreviewChkBox);
            this.panel2.Controls.Add(this.PrintBtn);
            this.panel2.Controls.Add(this.DocumentsLbx);
            this.panel2.Location = new System.Drawing.Point(454, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(302, 304);
            this.panel2.TabIndex = 2;
            this.panel2.TabStop = true;
            // 
            // label4
            // 
            this.label4.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 21);
            this.label4.TabIndex = 25;
            this.label4.Text = "Επιλογή Παραστατικού";
            // 
            // PrintChkBox
            // 
            this.PrintChkBox.AutoSize = true;
            this.PrintChkBox.Location = new System.Drawing.Point(27, 217);
            this.PrintChkBox.Name = "PrintChkBox";
            this.PrintChkBox.Size = new System.Drawing.Size(76, 17);
            this.PrintChkBox.TabIndex = 7;
            this.PrintChkBox.Text = "Εκτύπωση";
            this.PrintChkBox.UseVisualStyleBackColor = true;
            this.PrintChkBox.CheckedChanged += new System.EventHandler(this.PrintChkBox_CheckedChanged);
            // 
            // PrintPreviewChkBox
            // 
            this.PrintPreviewChkBox.AutoSize = true;
            this.PrintPreviewChkBox.Checked = true;
            this.PrintPreviewChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PrintPreviewChkBox.Location = new System.Drawing.Point(27, 197);
            this.PrintPreviewChkBox.Name = "PrintPreviewChkBox";
            this.PrintPreviewChkBox.Size = new System.Drawing.Size(174, 17);
            this.PrintPreviewChkBox.TabIndex = 6;
            this.PrintPreviewChkBox.Text = "Προεπισκόπηση και εκτύπωση";
            this.PrintPreviewChkBox.UseVisualStyleBackColor = true;
            this.PrintPreviewChkBox.CheckedChanged += new System.EventHandler(this.PrintPreviewChkBox_CheckedChanged);
            // 
            // PrintBtn
            // 
            this.PrintBtn.BackColor = System.Drawing.Color.MintCream;
            this.PrintBtn.Enabled = false;
            this.PrintBtn.Location = new System.Drawing.Point(27, 250);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(250, 33);
            this.PrintBtn.TabIndex = 8;
            this.PrintBtn.Text = "Εκτύπωση";
            this.PrintBtn.UseVisualStyleBackColor = false;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // PrintDoc
            // 
            this.PrintDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDoc_PrintPage);
            // 
            // PreviewDoc
            // 
            this.PreviewDoc.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.PreviewDoc_EndPrint);
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
            // PrintDial
            // 
            this.PrintDial.UseEXDialog = true;
            // 
            // SalesPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "SalesPrint";
            this.Size = new System.Drawing.Size(845, 483);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SelectNameCmb;
        private System.Windows.Forms.Button RetrieveBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker DateFrom;
        private System.Windows.Forms.DateTimePicker DateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox DocumentsLbx;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.CheckBox PrintChkBox;
        private System.Windows.Forms.CheckBox PrintPreviewChkBox;
        private System.Drawing.Printing.PrintDocument PrintDoc;
        private System.Drawing.Printing.PrintDocument PreviewDoc;
        private System.Windows.Forms.PrintPreviewDialog PrintPrev;
        private System.Windows.Forms.PrintDialog PrintDial;
        private System.Windows.Forms.Label label4;
    }
}
