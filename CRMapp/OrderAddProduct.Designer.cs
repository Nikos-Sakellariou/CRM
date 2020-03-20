namespace CRMapp
{
    partial class OrderAddProduct
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
            this.SearchNameListBox = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ProfitPercLbl = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ProfitPercTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.UnitTxt = new System.Windows.Forms.TextBox();
            this.UnitLbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DiscTxt = new System.Windows.Forms.TextBox();
            this.DiskLbl = new System.Windows.Forms.Label();
            this.PriceTxt = new System.Windows.Forms.TextBox();
            this.PriceLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MinStockLbl = new System.Windows.Forms.Label();
            this.AddBtn = new System.Windows.Forms.Button();
            this.MinStockTxt = new System.Windows.Forms.TextBox();
            this.QuantTxt = new System.Windows.Forms.TextBox();
            this.QuantLbl = new System.Windows.Forms.Label();
            this.ManufacTxt = new System.Windows.Forms.TextBox();
            this.ManufacLbl = new System.Windows.Forms.Label();
            this.LongDescrTxt = new System.Windows.Forms.TextBox();
            this.LongDescrLbl = new System.Windows.Forms.Label();
            this.DescrTxt = new System.Windows.Forms.TextBox();
            this.DescrLbl = new System.Windows.Forms.Label();
            this.SearchSupplierDescrListBox = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.SupplierDescrTxt = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SearchNameListBox
            // 
            this.SearchNameListBox.FormattingEnabled = true;
            this.SearchNameListBox.Location = new System.Drawing.Point(111, 27);
            this.SearchNameListBox.Name = "SearchNameListBox";
            this.SearchNameListBox.Size = new System.Drawing.Size(250, 17);
            this.SearchNameListBox.TabIndex = 40;
            this.SearchNameListBox.Visible = false;
            this.SearchNameListBox.Click += new System.EventHandler(this.SearchNameListBox_Click);
            this.SearchNameListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchNameListBox_KeyPress);
            this.SearchNameListBox.Leave += new System.EventHandler(this.SearchNameListBox_Leave);
            // 
            // label11
            // 
            this.label11.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(184, 227);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 21);
            this.label11.TabIndex = 42;
            this.label11.Text = "(%)";
            // 
            // ProfitPercLbl
            // 
            this.ProfitPercLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ProfitPercLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitPercLbl.Location = new System.Drawing.Point(-1, 227);
            this.ProfitPercLbl.Name = "ProfitPercLbl";
            this.ProfitPercLbl.Size = new System.Drawing.Size(108, 21);
            this.ProfitPercLbl.TabIndex = 41;
            this.ProfitPercLbl.Text = "Περιθ. Κέρδους";
            // 
            // label10
            // 
            this.label10.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(219, 227);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 21);
            this.label10.TabIndex = 28;
            this.label10.Text = "*";
            // 
            // ProfitPercTxt
            // 
            this.ProfitPercTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitPercTxt.Location = new System.Drawing.Point(111, 227);
            this.ProfitPercTxt.MaxLength = 10;
            this.ProfitPercTxt.Name = "ProfitPercTxt";
            this.ProfitPercTxt.Size = new System.Drawing.Size(70, 21);
            this.ProfitPercTxt.TabIndex = 33;
            this.ProfitPercTxt.TextChanged += new System.EventHandler(this.ProfitPercTxt_TextChanged);
            // 
            // label7
            // 
            this.label7.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(187, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 21);
            this.label7.TabIndex = 27;
            this.label7.Text = "*";
            // 
            // UnitTxt
            // 
            this.UnitTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnitTxt.Location = new System.Drawing.Point(111, 194);
            this.UnitTxt.MaxLength = 10;
            this.UnitTxt.Name = "UnitTxt";
            this.UnitTxt.Size = new System.Drawing.Size(70, 21);
            this.UnitTxt.TabIndex = 32;
            // 
            // UnitLbl
            // 
            this.UnitLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.UnitLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnitLbl.Location = new System.Drawing.Point(-1, 194);
            this.UnitLbl.Name = "UnitLbl";
            this.UnitLbl.Size = new System.Drawing.Size(106, 21);
            this.UnitLbl.TabIndex = 26;
            this.UnitLbl.Text = "Μον.Μέτρησης";
            // 
            // label9
            // 
            this.label9.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(187, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 21);
            this.label9.TabIndex = 40;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(187, 293);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 21);
            this.label8.TabIndex = 37;
            this.label8.Text = "*";
            // 
            // DiscTxt
            // 
            this.DiscTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscTxt.Location = new System.Drawing.Point(111, 326);
            this.DiscTxt.MaxLength = 10;
            this.DiscTxt.Name = "DiscTxt";
            this.DiscTxt.Size = new System.Drawing.Size(70, 21);
            this.DiscTxt.TabIndex = 36;
            this.DiscTxt.TextChanged += new System.EventHandler(this.DiscTxt_TextChanged);
            // 
            // DiskLbl
            // 
            this.DiskLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DiskLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiskLbl.Location = new System.Drawing.Point(-1, 326);
            this.DiskLbl.Name = "DiskLbl";
            this.DiskLbl.Size = new System.Drawing.Size(106, 21);
            this.DiskLbl.TabIndex = 24;
            this.DiskLbl.Text = "Έκπτωση";
            // 
            // PriceTxt
            // 
            this.PriceTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceTxt.Location = new System.Drawing.Point(111, 293);
            this.PriceTxt.MaxLength = 15;
            this.PriceTxt.Name = "PriceTxt";
            this.PriceTxt.Size = new System.Drawing.Size(70, 21);
            this.PriceTxt.TabIndex = 35;
            this.PriceTxt.TextChanged += new System.EventHandler(this.PriceTxt_TextChanged);
            // 
            // PriceLbl
            // 
            this.PriceLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.PriceLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLbl.Location = new System.Drawing.Point(-1, 293);
            this.PriceLbl.Name = "PriceLbl";
            this.PriceLbl.Size = new System.Drawing.Size(106, 21);
            this.PriceLbl.TabIndex = 23;
            this.PriceLbl.Text = "Τιμή αγοράς";
            // 
            // label6
            // 
            this.label6.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.label6.Location = new System.Drawing.Point(111, 394);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(271, 21);
            this.label6.TabIndex = 14;
            this.label6.Text = "*: συμπληρώνονται υποχρεωτικά.";
            // 
            // label4
            // 
            this.label4.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(187, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 21);
            this.label4.TabIndex = 22;
            this.label4.Text = "*";
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(367, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 21);
            this.label3.TabIndex = 21;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(367, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 21);
            this.label2.TabIndex = 20;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(367, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "*";
            // 
            // MinStockLbl
            // 
            this.MinStockLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.MinStockLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinStockLbl.Location = new System.Drawing.Point(-1, 359);
            this.MinStockLbl.Name = "MinStockLbl";
            this.MinStockLbl.Size = new System.Drawing.Size(106, 21);
            this.MinStockLbl.TabIndex = 18;
            this.MinStockLbl.Text = "Ελάχιστο stock";
            // 
            // AddBtn
            // 
            this.AddBtn.BackColor = System.Drawing.Color.Lime;
            this.AddBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.AddBtn.Location = new System.Drawing.Point(240, 418);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(121, 38);
            this.AddBtn.TabIndex = 39;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = false;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // MinStockTxt
            // 
            this.MinStockTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinStockTxt.Location = new System.Drawing.Point(111, 359);
            this.MinStockTxt.MaxLength = 10;
            this.MinStockTxt.Name = "MinStockTxt";
            this.MinStockTxt.Size = new System.Drawing.Size(70, 21);
            this.MinStockTxt.TabIndex = 38;
            this.MinStockTxt.TextChanged += new System.EventHandler(this.MinStockTxt_TextChanged);
            // 
            // QuantTxt
            // 
            this.QuantTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantTxt.Location = new System.Drawing.Point(111, 260);
            this.QuantTxt.MaxLength = 10;
            this.QuantTxt.Name = "QuantTxt";
            this.QuantTxt.Size = new System.Drawing.Size(70, 21);
            this.QuantTxt.TabIndex = 34;
            this.QuantTxt.TextChanged += new System.EventHandler(this.QuantTxt_TextChanged);
            // 
            // QuantLbl
            // 
            this.QuantLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.QuantLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantLbl.Location = new System.Drawing.Point(-1, 260);
            this.QuantLbl.Name = "QuantLbl";
            this.QuantLbl.Size = new System.Drawing.Size(80, 21);
            this.QuantLbl.TabIndex = 17;
            this.QuantLbl.Text = "Ποσότητα";
            // 
            // ManufacTxt
            // 
            this.ManufacTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManufacTxt.Location = new System.Drawing.Point(111, 161);
            this.ManufacTxt.MaxLength = 100;
            this.ManufacTxt.Name = "ManufacTxt";
            this.ManufacTxt.Size = new System.Drawing.Size(250, 21);
            this.ManufacTxt.TabIndex = 31;
            // 
            // ManufacLbl
            // 
            this.ManufacLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ManufacLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManufacLbl.Location = new System.Drawing.Point(-1, 161);
            this.ManufacLbl.Name = "ManufacLbl";
            this.ManufacLbl.Size = new System.Drawing.Size(108, 21);
            this.ManufacLbl.TabIndex = 16;
            this.ManufacLbl.Text = "Κατασκευαστής";
            // 
            // LongDescrTxt
            // 
            this.LongDescrTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongDescrTxt.Location = new System.Drawing.Point(111, 40);
            this.LongDescrTxt.MaxLength = 2000;
            this.LongDescrTxt.Multiline = true;
            this.LongDescrTxt.Name = "LongDescrTxt";
            this.LongDescrTxt.Size = new System.Drawing.Size(250, 76);
            this.LongDescrTxt.TabIndex = 29;
            // 
            // LongDescrLbl
            // 
            this.LongDescrLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.LongDescrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongDescrLbl.Location = new System.Drawing.Point(-1, 40);
            this.LongDescrLbl.Name = "LongDescrLbl";
            this.LongDescrLbl.Size = new System.Drawing.Size(80, 42);
            this.LongDescrLbl.TabIndex = 15;
            this.LongDescrLbl.Text = "Αναλυτική περιγραφή";
            // 
            // DescrTxt
            // 
            this.DescrTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescrTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.DescrTxt.Location = new System.Drawing.Point(111, 7);
            this.DescrTxt.MaxLength = 200;
            this.DescrTxt.Name = "DescrTxt";
            this.DescrTxt.Size = new System.Drawing.Size(250, 21);
            this.DescrTxt.TabIndex = 28;
            this.DescrTxt.TextChanged += new System.EventHandler(this.DescrTxt_TextChanged);
            this.DescrTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DescrTxt_KeyDown);
            this.DescrTxt.Leave += new System.EventHandler(this.DescrTxt_Leave);
            // 
            // DescrLbl
            // 
            this.DescrLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DescrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescrLbl.Location = new System.Drawing.Point(-1, 7);
            this.DescrLbl.Name = "DescrLbl";
            this.DescrLbl.Size = new System.Drawing.Size(80, 21);
            this.DescrLbl.TabIndex = 25;
            this.DescrLbl.Text = "Περιγραφή";
            // 
            // SearchSupplierDescrListBox
            // 
            this.SearchSupplierDescrListBox.FormattingEnabled = true;
            this.SearchSupplierDescrListBox.Location = new System.Drawing.Point(111, 148);
            this.SearchSupplierDescrListBox.Name = "SearchSupplierDescrListBox";
            this.SearchSupplierDescrListBox.Size = new System.Drawing.Size(250, 17);
            this.SearchSupplierDescrListBox.TabIndex = 41;
            this.SearchSupplierDescrListBox.Visible = false;
            this.SearchSupplierDescrListBox.Click += new System.EventHandler(this.SearchSupplierDescrListBox_Click);
            this.SearchSupplierDescrListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchSupplierDescrListBox_KeyPress);
            this.SearchSupplierDescrListBox.Leave += new System.EventHandler(this.SearchSupplierDescrListBox_Leave);
            // 
            // label16
            // 
            this.label16.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(367, 128);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 21);
            this.label16.TabIndex = 0;
            this.label16.Text = "*";
            // 
            // SupplierDescrTxt
            // 
            this.SupplierDescrTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierDescrTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SupplierDescrTxt.Location = new System.Drawing.Point(111, 128);
            this.SupplierDescrTxt.MaxLength = 200;
            this.SupplierDescrTxt.Name = "SupplierDescrTxt";
            this.SupplierDescrTxt.Size = new System.Drawing.Size(250, 21);
            this.SupplierDescrTxt.TabIndex = 30;
            this.SupplierDescrTxt.TextChanged += new System.EventHandler(this.SupplierDescrTxt_TextChanged);
            this.SupplierDescrTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SupplierDescrTxt_KeyDown);
            this.SupplierDescrTxt.Leave += new System.EventHandler(this.SupplierDescrTxt_Leave);
            // 
            // label17
            // 
            this.label17.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(-1, 124);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 31);
            this.label17.TabIndex = 0;
            this.label17.Text = "Περιγραφή Προμηθευτή";
            // 
            // OrderAddProduct
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(380, 458);
            this.Controls.Add(this.SearchSupplierDescrListBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.SupplierDescrTxt);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.SearchNameListBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ProfitPercLbl);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ProfitPercTxt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.UnitTxt);
            this.Controls.Add(this.UnitLbl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DiscTxt);
            this.Controls.Add(this.DiskLbl);
            this.Controls.Add(this.PriceTxt);
            this.Controls.Add(this.PriceLbl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinStockLbl);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.MinStockTxt);
            this.Controls.Add(this.QuantTxt);
            this.Controls.Add(this.QuantLbl);
            this.Controls.Add(this.ManufacTxt);
            this.Controls.Add(this.ManufacLbl);
            this.Controls.Add(this.LongDescrTxt);
            this.Controls.Add(this.LongDescrLbl);
            this.Controls.Add(this.DescrTxt);
            this.Controls.Add(this.DescrLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderAddProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Προσθήκη Προϊόντος";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox SearchNameListBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label ProfitPercLbl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ProfitPercTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox UnitTxt;
        private System.Windows.Forms.Label UnitLbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DiscTxt;
        private System.Windows.Forms.Label DiskLbl;
        private System.Windows.Forms.TextBox PriceTxt;
        private System.Windows.Forms.Label PriceLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MinStockLbl;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.TextBox MinStockTxt;
        private System.Windows.Forms.TextBox QuantTxt;
        private System.Windows.Forms.Label QuantLbl;
        private System.Windows.Forms.TextBox ManufacTxt;
        private System.Windows.Forms.Label ManufacLbl;
        private System.Windows.Forms.TextBox LongDescrTxt;
        private System.Windows.Forms.Label LongDescrLbl;
        private System.Windows.Forms.TextBox DescrTxt;
        private System.Windows.Forms.Label DescrLbl;
        private System.Windows.Forms.ListBox SearchSupplierDescrListBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox SupplierDescrTxt;
        private System.Windows.Forms.Label label17;
    }
}