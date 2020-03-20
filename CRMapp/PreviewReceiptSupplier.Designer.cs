namespace CRMapp
{
    partial class PreviewReceiptSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewReceiptSupplier));
            this.PreviewDoc = new System.Drawing.Printing.PrintDocument();
            this.PrintPrev = new System.Windows.Forms.PrintPreviewDialog();
            this.PrintDial = new System.Windows.Forms.PrintDialog();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SeriesInvoiceTxt = new System.Windows.Forms.TextBox();
            this.SeriesInvoiceLbl = new System.Windows.Forms.Label();
            this.DateTimeInvoicePicker = new System.Windows.Forms.DateTimePicker();
            this.IdInvoiceTxt = new System.Windows.Forms.TextBox();
            this.IdInvoiceLbl = new System.Windows.Forms.Label();
            this.DateInvoiceLbl = new System.Windows.Forms.Label();
            this.SupplierLbl = new System.Windows.Forms.Label();
            this.NameCustomerTxt = new System.Windows.Forms.TextBox();
            this.AfmCustomerTxt = new System.Windows.Forms.TextBox();
            this.AfmSupplierLbl = new System.Windows.Forms.Label();
            this.NameSupplierLbl = new System.Windows.Forms.Label();
            this.IdSupplierLbl = new System.Windows.Forms.Label();
            this.IdCustomerTxt = new System.Windows.Forms.TextBox();
            this.CustomerPanel = new System.Windows.Forms.Panel();
            this.ProdItemsTxt = new System.Windows.Forms.TextBox();
            this.PrintDoc = new System.Drawing.Printing.PrintDocument();
            this.ProductsPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.ValueDocPriceTxt1 = new System.Windows.Forms.TextBox();
            this.ValueDocDatePkr1 = new System.Windows.Forms.DateTimePicker();
            this.ValueDocIssuerTxt1 = new System.Windows.Forms.TextBox();
            this.ValueDocPriceLbl = new System.Windows.Forms.Label();
            this.AaTxt1 = new System.Windows.Forms.Label();
            this.AaLabel = new System.Windows.Forms.Label();
            this.ValueDocIdTxt1 = new System.Windows.Forms.TextBox();
            this.ValueDocDateLbl = new System.Windows.Forms.Label();
            this.ValueDocIdLbl = new System.Windows.Forms.Label();
            this.ProdLbl = new System.Windows.Forms.Label();
            this.ValueDocIssuerLbl = new System.Windows.Forms.Label();
            this.ProdPanel = new System.Windows.Forms.Panel();
            this.CashTxt = new System.Windows.Forms.TextBox();
            this.CashLbl = new System.Windows.Forms.Label();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PriceReceiptTxt = new System.Windows.Forms.TextBox();
            this.PriceReceiptLbl = new System.Windows.Forms.Label();
            this.HeaderPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.CustomerPanel.SuspendLayout();
            this.ProductsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ProdPanel.SuspendLayout();
            this.FooterPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
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
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.label1);
            this.HeaderPanel.Controls.Add(this.panel1);
            this.HeaderPanel.Controls.Add(this.SupplierLbl);
            this.HeaderPanel.Controls.Add(this.NameCustomerTxt);
            this.HeaderPanel.Controls.Add(this.AfmCustomerTxt);
            this.HeaderPanel.Controls.Add(this.AfmSupplierLbl);
            this.HeaderPanel.Controls.Add(this.NameSupplierLbl);
            this.HeaderPanel.Controls.Add(this.IdSupplierLbl);
            this.HeaderPanel.Controls.Add(this.IdCustomerTxt);
            this.HeaderPanel.Controls.Add(this.CustomerPanel);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(855, 114);
            this.HeaderPanel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SkyBlue;
            this.label1.Location = new System.Drawing.Point(554, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Στοιχεία Τιμολόγησης";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SeriesInvoiceTxt);
            this.panel1.Controls.Add(this.SeriesInvoiceLbl);
            this.panel1.Controls.Add(this.DateTimeInvoicePicker);
            this.panel1.Controls.Add(this.IdInvoiceTxt);
            this.panel1.Controls.Add(this.IdInvoiceLbl);
            this.panel1.Controls.Add(this.DateInvoiceLbl);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Location = new System.Drawing.Point(434, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 95);
            this.panel1.TabIndex = 0;
            // 
            // SeriesInvoiceTxt
            // 
            this.SeriesInvoiceTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeriesInvoiceTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SeriesInvoiceTxt.Location = new System.Drawing.Point(291, 12);
            this.SeriesInvoiceTxt.MaxLength = 100;
            this.SeriesInvoiceTxt.Name = "SeriesInvoiceTxt";
            this.SeriesInvoiceTxt.ReadOnly = true;
            this.SeriesInvoiceTxt.Size = new System.Drawing.Size(101, 21);
            this.SeriesInvoiceTxt.TabIndex = 19;
            // 
            // SeriesInvoiceLbl
            // 
            this.SeriesInvoiceLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.SeriesInvoiceLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeriesInvoiceLbl.Location = new System.Drawing.Point(206, 11);
            this.SeriesInvoiceLbl.Name = "SeriesInvoiceLbl";
            this.SeriesInvoiceLbl.Size = new System.Drawing.Size(79, 21);
            this.SeriesInvoiceLbl.TabIndex = 0;
            this.SeriesInvoiceLbl.Text = "Σειρά";
            // 
            // DateTimeInvoicePicker
            // 
            this.DateTimeInvoicePicker.CustomFormat = "dd/MM/yyyy";
            this.DateTimeInvoicePicker.Enabled = false;
            this.DateTimeInvoicePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimeInvoicePicker.Location = new System.Drawing.Point(103, 37);
            this.DateTimeInvoicePicker.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.DateTimeInvoicePicker.Name = "DateTimeInvoicePicker";
            this.DateTimeInvoicePicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DateTimeInvoicePicker.Size = new System.Drawing.Size(90, 20);
            this.DateTimeInvoicePicker.TabIndex = 17;
            // 
            // IdInvoiceTxt
            // 
            this.IdInvoiceTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdInvoiceTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.IdInvoiceTxt.Location = new System.Drawing.Point(103, 12);
            this.IdInvoiceTxt.MaxLength = 100;
            this.IdInvoiceTxt.Name = "IdInvoiceTxt";
            this.IdInvoiceTxt.ReadOnly = true;
            this.IdInvoiceTxt.Size = new System.Drawing.Size(90, 21);
            this.IdInvoiceTxt.TabIndex = 16;
            // 
            // IdInvoiceLbl
            // 
            this.IdInvoiceLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.IdInvoiceLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdInvoiceLbl.Location = new System.Drawing.Point(10, 11);
            this.IdInvoiceLbl.Name = "IdInvoiceLbl";
            this.IdInvoiceLbl.Size = new System.Drawing.Size(92, 21);
            this.IdInvoiceLbl.TabIndex = 0;
            this.IdInvoiceLbl.Text = "Αριθμός";
            // 
            // DateInvoiceLbl
            // 
            this.DateInvoiceLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DateInvoiceLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateInvoiceLbl.Location = new System.Drawing.Point(10, 37);
            this.DateInvoiceLbl.Name = "DateInvoiceLbl";
            this.DateInvoiceLbl.Size = new System.Drawing.Size(92, 21);
            this.DateInvoiceLbl.TabIndex = 0;
            this.DateInvoiceLbl.Text = "Ημερομηνία";
            // 
            // SupplierLbl
            // 
            this.SupplierLbl.AutoSize = true;
            this.SupplierLbl.BackColor = System.Drawing.Color.SkyBlue;
            this.SupplierLbl.Location = new System.Drawing.Point(142, 4);
            this.SupplierLbl.Name = "SupplierLbl";
            this.SupplierLbl.Size = new System.Drawing.Size(115, 13);
            this.SupplierLbl.TabIndex = 0;
            this.SupplierLbl.Text = "Στοιχεία Προμηθευτή";
            // 
            // NameCustomerTxt
            // 
            this.NameCustomerTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameCustomerTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.NameCustomerTxt.Location = new System.Drawing.Point(133, 49);
            this.NameCustomerTxt.MaxLength = 100;
            this.NameCustomerTxt.Name = "NameCustomerTxt";
            this.NameCustomerTxt.ReadOnly = true;
            this.NameCustomerTxt.Size = new System.Drawing.Size(277, 21);
            this.NameCustomerTxt.TabIndex = 12;
            // 
            // AfmCustomerTxt
            // 
            this.AfmCustomerTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AfmCustomerTxt.Location = new System.Drawing.Point(133, 75);
            this.AfmCustomerTxt.MaxLength = 9;
            this.AfmCustomerTxt.Name = "AfmCustomerTxt";
            this.AfmCustomerTxt.ReadOnly = true;
            this.AfmCustomerTxt.Size = new System.Drawing.Size(70, 21);
            this.AfmCustomerTxt.TabIndex = 13;
            // 
            // AfmSupplierLbl
            // 
            this.AfmSupplierLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AfmSupplierLbl.BackColor = System.Drawing.Color.SkyBlue;
            this.AfmSupplierLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AfmSupplierLbl.Location = new System.Drawing.Point(34, 74);
            this.AfmSupplierLbl.Name = "AfmSupplierLbl";
            this.AfmSupplierLbl.Size = new System.Drawing.Size(96, 21);
            this.AfmSupplierLbl.TabIndex = 0;
            this.AfmSupplierLbl.Text = "Α.Φ.Μ.";
            // 
            // NameSupplierLbl
            // 
            this.NameSupplierLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.NameSupplierLbl.BackColor = System.Drawing.Color.SkyBlue;
            this.NameSupplierLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameSupplierLbl.Location = new System.Drawing.Point(34, 49);
            this.NameSupplierLbl.Name = "NameSupplierLbl";
            this.NameSupplierLbl.Size = new System.Drawing.Size(96, 21);
            this.NameSupplierLbl.TabIndex = 0;
            this.NameSupplierLbl.Text = "Επωνυμία";
            // 
            // IdSupplierLbl
            // 
            this.IdSupplierLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.IdSupplierLbl.BackColor = System.Drawing.Color.SkyBlue;
            this.IdSupplierLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdSupplierLbl.Location = new System.Drawing.Point(34, 23);
            this.IdSupplierLbl.Name = "IdSupplierLbl";
            this.IdSupplierLbl.Size = new System.Drawing.Size(96, 21);
            this.IdSupplierLbl.TabIndex = 0;
            this.IdSupplierLbl.Text = "Κωδικός";
            // 
            // IdCustomerTxt
            // 
            this.IdCustomerTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdCustomerTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.IdCustomerTxt.Location = new System.Drawing.Point(133, 23);
            this.IdCustomerTxt.MaxLength = 100;
            this.IdCustomerTxt.Name = "IdCustomerTxt";
            this.IdCustomerTxt.ReadOnly = true;
            this.IdCustomerTxt.Size = new System.Drawing.Size(70, 21);
            this.IdCustomerTxt.TabIndex = 11;
            // 
            // CustomerPanel
            // 
            this.CustomerPanel.BackColor = System.Drawing.Color.SkyBlue;
            this.CustomerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CustomerPanel.Controls.Add(this.ProdItemsTxt);
            this.CustomerPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.CustomerPanel.Location = new System.Drawing.Point(22, 10);
            this.CustomerPanel.Name = "CustomerPanel";
            this.CustomerPanel.Size = new System.Drawing.Size(400, 95);
            this.CustomerPanel.TabIndex = 0;
            // 
            // ProdItemsTxt
            // 
            this.ProdItemsTxt.Enabled = false;
            this.ProdItemsTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdItemsTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ProdItemsTxt.Location = new System.Drawing.Point(317, 63);
            this.ProdItemsTxt.MaxLength = 100;
            this.ProdItemsTxt.Name = "ProdItemsTxt";
            this.ProdItemsTxt.Size = new System.Drawing.Size(70, 21);
            this.ProdItemsTxt.TabIndex = 314;
            this.ProdItemsTxt.Text = "1";
            this.ProdItemsTxt.Visible = false;
            // 
            // ProductsPanel
            // 
            this.ProductsPanel.AutoSize = true;
            this.ProductsPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ProductsPanel.Controls.Add(this.panel2);
            this.ProductsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductsPanel.Location = new System.Drawing.Point(0, 114);
            this.ProductsPanel.Name = "ProductsPanel";
            this.ProductsPanel.Size = new System.Drawing.Size(855, 156);
            this.ProductsPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.ValueDocPriceTxt1);
            this.panel2.Controls.Add(this.ValueDocDatePkr1);
            this.panel2.Controls.Add(this.ValueDocIssuerTxt1);
            this.panel2.Controls.Add(this.ValueDocPriceLbl);
            this.panel2.Controls.Add(this.AaTxt1);
            this.panel2.Controls.Add(this.AaLabel);
            this.panel2.Controls.Add(this.ValueDocIdTxt1);
            this.panel2.Controls.Add(this.ValueDocDateLbl);
            this.panel2.Controls.Add(this.ValueDocIdLbl);
            this.panel2.Controls.Add(this.ProdLbl);
            this.panel2.Controls.Add(this.ValueDocIssuerLbl);
            this.panel2.Controls.Add(this.ProdPanel);
            this.panel2.Controls.Add(this.FooterPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(855, 156);
            this.panel2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(669, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 334;
            this.label4.Text = "Στοιχεία Μετρητών";
            // 
            // ValueDocPriceTxt1
            // 
            this.ValueDocPriceTxt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueDocPriceTxt1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ValueDocPriceTxt1.Location = new System.Drawing.Point(323, 42);
            this.ValueDocPriceTxt1.MaxLength = 15;
            this.ValueDocPriceTxt1.Name = "ValueDocPriceTxt1";
            this.ValueDocPriceTxt1.ReadOnly = true;
            this.ValueDocPriceTxt1.Size = new System.Drawing.Size(70, 21);
            this.ValueDocPriceTxt1.TabIndex = 329;
            // 
            // ValueDocDatePkr1
            // 
            this.ValueDocDatePkr1.CustomFormat = "dd/MM/yyyy";
            this.ValueDocDatePkr1.Enabled = false;
            this.ValueDocDatePkr1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ValueDocDatePkr1.Location = new System.Drawing.Point(227, 42);
            this.ValueDocDatePkr1.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.ValueDocDatePkr1.MinimumSize = new System.Drawing.Size(97, 21);
            this.ValueDocDatePkr1.Name = "ValueDocDatePkr1";
            this.ValueDocDatePkr1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ValueDocDatePkr1.Size = new System.Drawing.Size(97, 21);
            this.ValueDocDatePkr1.TabIndex = 327;
            // 
            // ValueDocIssuerTxt1
            // 
            this.ValueDocIssuerTxt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueDocIssuerTxt1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ValueDocIssuerTxt1.Location = new System.Drawing.Point(392, 42);
            this.ValueDocIssuerTxt1.MaxLength = 30;
            this.ValueDocIssuerTxt1.Name = "ValueDocIssuerTxt1";
            this.ValueDocIssuerTxt1.ReadOnly = true;
            this.ValueDocIssuerTxt1.Size = new System.Drawing.Size(131, 21);
            this.ValueDocIssuerTxt1.TabIndex = 330;
            // 
            // ValueDocPriceLbl
            // 
            this.ValueDocPriceLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ValueDocPriceLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ValueDocPriceLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueDocPriceLbl.Location = new System.Drawing.Point(323, 23);
            this.ValueDocPriceLbl.Name = "ValueDocPriceLbl";
            this.ValueDocPriceLbl.Size = new System.Drawing.Size(70, 21);
            this.ValueDocPriceLbl.TabIndex = 318;
            this.ValueDocPriceLbl.Text = "Αξία";
            this.ValueDocPriceLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AaTxt1
            // 
            this.AaTxt1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AaTxt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.AaTxt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AaTxt1.Location = new System.Drawing.Point(35, 42);
            this.AaTxt1.Name = "AaTxt1";
            this.AaTxt1.Size = new System.Drawing.Size(29, 21);
            this.AaTxt1.TabIndex = 325;
            this.AaTxt1.Text = "1";
            this.AaTxt1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AaLabel
            // 
            this.AaLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AaLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.AaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AaLabel.Location = new System.Drawing.Point(35, 23);
            this.AaLabel.Name = "AaLabel";
            this.AaLabel.Size = new System.Drawing.Size(29, 21);
            this.AaLabel.TabIndex = 322;
            this.AaLabel.Text = "Α/Α";
            this.AaLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ValueDocIdTxt1
            // 
            this.ValueDocIdTxt1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ValueDocIdTxt1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ValueDocIdTxt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueDocIdTxt1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ValueDocIdTxt1.Location = new System.Drawing.Point(70, 42);
            this.ValueDocIdTxt1.MaxLength = 20;
            this.ValueDocIdTxt1.Name = "ValueDocIdTxt1";
            this.ValueDocIdTxt1.ReadOnly = true;
            this.ValueDocIdTxt1.Size = new System.Drawing.Size(158, 21);
            this.ValueDocIdTxt1.TabIndex = 326;
            // 
            // ValueDocDateLbl
            // 
            this.ValueDocDateLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ValueDocDateLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ValueDocDateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueDocDateLbl.Location = new System.Drawing.Point(231, 23);
            this.ValueDocDateLbl.Name = "ValueDocDateLbl";
            this.ValueDocDateLbl.Size = new System.Drawing.Size(93, 19);
            this.ValueDocDateLbl.TabIndex = 323;
            this.ValueDocDateLbl.Text = "Ημ/νία Λήξης";
            this.ValueDocDateLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ValueDocIdLbl
            // 
            this.ValueDocIdLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ValueDocIdLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ValueDocIdLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueDocIdLbl.Location = new System.Drawing.Point(70, 23);
            this.ValueDocIdLbl.Name = "ValueDocIdLbl";
            this.ValueDocIdLbl.Size = new System.Drawing.Size(158, 21);
            this.ValueDocIdLbl.TabIndex = 321;
            this.ValueDocIdLbl.Text = "Αριθμός Αξιογράφου";
            this.ValueDocIdLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProdLbl
            // 
            this.ProdLbl.AutoSize = true;
            this.ProdLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ProdLbl.Location = new System.Drawing.Point(142, 10);
            this.ProdLbl.Name = "ProdLbl";
            this.ProdLbl.Size = new System.Drawing.Size(116, 13);
            this.ProdLbl.TabIndex = 320;
            this.ProdLbl.Text = "Στοιχεία Αξιογράφων";
            // 
            // ValueDocIssuerLbl
            // 
            this.ValueDocIssuerLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ValueDocIssuerLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ValueDocIssuerLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueDocIssuerLbl.Location = new System.Drawing.Point(394, 23);
            this.ValueDocIssuerLbl.Name = "ValueDocIssuerLbl";
            this.ValueDocIssuerLbl.Size = new System.Drawing.Size(129, 16);
            this.ValueDocIssuerLbl.TabIndex = 319;
            this.ValueDocIssuerLbl.Text = "Εκδότης";
            this.ValueDocIssuerLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProdPanel
            // 
            this.ProdPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ProdPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProdPanel.Controls.Add(this.CashTxt);
            this.ProdPanel.Controls.Add(this.CashLbl);
            this.ProdPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.ProdPanel.Location = new System.Drawing.Point(22, 16);
            this.ProdPanel.Name = "ProdPanel";
            this.ProdPanel.Size = new System.Drawing.Size(812, 66);
            this.ProdPanel.TabIndex = 317;
            // 
            // CashTxt
            // 
            this.CashTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CashTxt.Location = new System.Drawing.Point(638, 24);
            this.CashTxt.MaxLength = 15;
            this.CashTxt.Name = "CashTxt";
            this.CashTxt.ReadOnly = true;
            this.CashTxt.Size = new System.Drawing.Size(122, 21);
            this.CashTxt.TabIndex = 335;
            // 
            // CashLbl
            // 
            this.CashLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.CashLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CashLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashLbl.Location = new System.Drawing.Point(638, 6);
            this.CashLbl.Name = "CashLbl";
            this.CashLbl.Size = new System.Drawing.Size(122, 21);
            this.CashLbl.TabIndex = 0;
            this.CashLbl.Text = "Μετρητά";
            this.CashLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FooterPanel
            // 
            this.FooterPanel.Controls.Add(this.label2);
            this.FooterPanel.Controls.Add(this.panel3);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FooterPanel.Location = new System.Drawing.Point(0, 89);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(855, 67);
            this.FooterPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Tan;
            this.label2.Location = new System.Drawing.Point(142, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Σύνολο Απόδειξης";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.BurlyWood;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.PriceReceiptTxt);
            this.panel3.Controls.Add(this.PriceReceiptLbl);
            this.panel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel3.Location = new System.Drawing.Point(22, 14);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(812, 40);
            this.panel3.TabIndex = 0;
            // 
            // PriceReceiptTxt
            // 
            this.PriceReceiptTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceReceiptTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.PriceReceiptTxt.Location = new System.Drawing.Point(110, 10);
            this.PriceReceiptTxt.MaxLength = 15;
            this.PriceReceiptTxt.Name = "PriceReceiptTxt";
            this.PriceReceiptTxt.ReadOnly = true;
            this.PriceReceiptTxt.Size = new System.Drawing.Size(135, 21);
            this.PriceReceiptTxt.TabIndex = 6;
            this.PriceReceiptTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PriceReceiptLbl
            // 
            this.PriceReceiptLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.PriceReceiptLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceReceiptLbl.Location = new System.Drawing.Point(5, 9);
            this.PriceReceiptLbl.Name = "PriceReceiptLbl";
            this.PriceReceiptLbl.Size = new System.Drawing.Size(102, 21);
            this.PriceReceiptLbl.TabIndex = 0;
            this.PriceReceiptLbl.Text = "Ποσό";
            // 
            // PreviewReceiptSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 270);
            this.Controls.Add(this.ProductsPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Name = "PreviewReceiptSupplier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Απόδειξη Είσπραξης Προμηθευτή";
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.CustomerPanel.ResumeLayout(false);
            this.CustomerPanel.PerformLayout();
            this.ProductsPanel.ResumeLayout(false);
            this.ProductsPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ProdPanel.ResumeLayout(false);
            this.ProdPanel.PerformLayout();
            this.FooterPanel.ResumeLayout(false);
            this.FooterPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Drawing.Printing.PrintDocument PreviewDoc;
        private System.Windows.Forms.PrintPreviewDialog PrintPrev;
        private System.Windows.Forms.PrintDialog PrintDial;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox SeriesInvoiceTxt;
        private System.Windows.Forms.Label SeriesInvoiceLbl;
        private System.Windows.Forms.DateTimePicker DateTimeInvoicePicker;
        private System.Windows.Forms.TextBox IdInvoiceTxt;
        private System.Windows.Forms.Label IdInvoiceLbl;
        private System.Windows.Forms.Label DateInvoiceLbl;
        private System.Windows.Forms.Label SupplierLbl;
        private System.Windows.Forms.TextBox NameCustomerTxt;
        private System.Windows.Forms.TextBox AfmCustomerTxt;
        private System.Windows.Forms.Label AfmSupplierLbl;
        private System.Windows.Forms.Label NameSupplierLbl;
        private System.Windows.Forms.Label IdSupplierLbl;
        private System.Windows.Forms.TextBox IdCustomerTxt;
        private System.Windows.Forms.Panel CustomerPanel;
        private System.Windows.Forms.TextBox ProdItemsTxt;
        private System.Drawing.Printing.PrintDocument PrintDoc;
        private System.Windows.Forms.Panel ProductsPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ValueDocPriceTxt1;
        private System.Windows.Forms.DateTimePicker ValueDocDatePkr1;
        private System.Windows.Forms.TextBox ValueDocIssuerTxt1;
        private System.Windows.Forms.Label ValueDocPriceLbl;
        private System.Windows.Forms.Label AaTxt1;
        private System.Windows.Forms.Label AaLabel;
        private System.Windows.Forms.TextBox ValueDocIdTxt1;
        private System.Windows.Forms.Label ValueDocDateLbl;
        private System.Windows.Forms.Label ValueDocIdLbl;
        private System.Windows.Forms.Label ProdLbl;
        private System.Windows.Forms.Label ValueDocIssuerLbl;
        private System.Windows.Forms.Panel ProdPanel;
        private System.Windows.Forms.TextBox CashTxt;
        private System.Windows.Forms.Label CashLbl;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox PriceReceiptTxt;
        private System.Windows.Forms.Label PriceReceiptLbl;
    }
}