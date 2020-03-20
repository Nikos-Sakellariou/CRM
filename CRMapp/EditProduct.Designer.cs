namespace CRMapp
{
    partial class EditProduct
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
            this.DescrLbl = new System.Windows.Forms.Label();
            this.SearchNameTxt = new System.Windows.Forms.TextBox();
            this.LongDescrTxt = new System.Windows.Forms.TextBox();
            this.LongDescrLbl = new System.Windows.Forms.Label();
            this.QuantTxt = new System.Windows.Forms.TextBox();
            this.QuantLbl = new System.Windows.Forms.Label();
            this.ManufacTxt = new System.Windows.Forms.TextBox();
            this.ManufacLbl = new System.Windows.Forms.Label();
            this.MinStockTxt = new System.Windows.Forms.TextBox();
            this.MinStockLbl = new System.Windows.Forms.Label();
            this.EditBtn = new System.Windows.Forms.Button();
            this.SearchNameListBox = new System.Windows.Forms.ListBox();
            this.DescrTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchIdTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectNameCmb = new System.Windows.Forms.ComboBox();
            this.RetrieveBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.IdTxt = new System.Windows.Forms.TextBox();
            this.IdLbl = new System.Windows.Forms.Label();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DocumentLbx = new System.Windows.Forms.ListBox();
            this.UnitTxt = new System.Windows.Forms.TextBox();
            this.UnitLbl = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ProfitPercLbl = new System.Windows.Forms.Label();
            this.ProfitPercTxt = new System.Windows.Forms.TextBox();
            this.SupplierDescrTxt = new System.Windows.Forms.TextBox();
            this.SupplierDescrLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DescrLbl
            // 
            this.DescrLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DescrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescrLbl.Location = new System.Drawing.Point(434, 53);
            this.DescrLbl.Name = "DescrLbl";
            this.DescrLbl.Size = new System.Drawing.Size(80, 21);
            this.DescrLbl.TabIndex = 0;
            this.DescrLbl.Text = "Περιγραφή";
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
            // LongDescrTxt
            // 
            this.LongDescrTxt.Enabled = false;
            this.LongDescrTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongDescrTxt.Location = new System.Drawing.Point(544, 86);
            this.LongDescrTxt.MaxLength = 2000;
            this.LongDescrTxt.Multiline = true;
            this.LongDescrTxt.Name = "LongDescrTxt";
            this.LongDescrTxt.Size = new System.Drawing.Size(250, 76);
            this.LongDescrTxt.TabIndex = 7;
            // 
            // LongDescrLbl
            // 
            this.LongDescrLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.LongDescrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongDescrLbl.Location = new System.Drawing.Point(434, 86);
            this.LongDescrLbl.Name = "LongDescrLbl";
            this.LongDescrLbl.Size = new System.Drawing.Size(80, 42);
            this.LongDescrLbl.TabIndex = 0;
            this.LongDescrLbl.Text = "Αναλυτική Περιγραφή";
            // 
            // QuantTxt
            // 
            this.QuantTxt.Enabled = false;
            this.QuantTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantTxt.Location = new System.Drawing.Point(544, 306);
            this.QuantTxt.MaxLength = 10;
            this.QuantTxt.Name = "QuantTxt";
            this.QuantTxt.ReadOnly = true;
            this.QuantTxt.Size = new System.Drawing.Size(70, 21);
            this.QuantTxt.TabIndex = 12;
            this.QuantTxt.TextChanged += new System.EventHandler(this.QuantTxt_TextChanged);
            // 
            // QuantLbl
            // 
            this.QuantLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.QuantLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantLbl.Location = new System.Drawing.Point(434, 306);
            this.QuantLbl.Name = "QuantLbl";
            this.QuantLbl.Size = new System.Drawing.Size(80, 21);
            this.QuantLbl.TabIndex = 0;
            this.QuantLbl.Text = "Ποσότητα";
            // 
            // ManufacTxt
            // 
            this.ManufacTxt.Enabled = false;
            this.ManufacTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManufacTxt.Location = new System.Drawing.Point(544, 207);
            this.ManufacTxt.MaxLength = 100;
            this.ManufacTxt.Name = "ManufacTxt";
            this.ManufacTxt.Size = new System.Drawing.Size(250, 21);
            this.ManufacTxt.TabIndex = 9;
            // 
            // ManufacLbl
            // 
            this.ManufacLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ManufacLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManufacLbl.Location = new System.Drawing.Point(434, 207);
            this.ManufacLbl.Name = "ManufacLbl";
            this.ManufacLbl.Size = new System.Drawing.Size(112, 21);
            this.ManufacLbl.TabIndex = 0;
            this.ManufacLbl.Text = "Κατασκευαστής";
            // 
            // MinStockTxt
            // 
            this.MinStockTxt.Enabled = false;
            this.MinStockTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinStockTxt.Location = new System.Drawing.Point(544, 339);
            this.MinStockTxt.MaxLength = 10;
            this.MinStockTxt.Name = "MinStockTxt";
            this.MinStockTxt.Size = new System.Drawing.Size(70, 21);
            this.MinStockTxt.TabIndex = 13;
            this.MinStockTxt.TextChanged += new System.EventHandler(this.MinStockTxt_TextChanged);
            // 
            // MinStockLbl
            // 
            this.MinStockLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.MinStockLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinStockLbl.Location = new System.Drawing.Point(434, 339);
            this.MinStockLbl.Name = "MinStockLbl";
            this.MinStockLbl.Size = new System.Drawing.Size(104, 21);
            this.MinStockLbl.TabIndex = 0;
            this.MinStockLbl.Text = "Ελάχιστο Stock";
            // 
            // EditBtn
            // 
            this.EditBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.EditBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.EditBtn.Location = new System.Drawing.Point(544, 382);
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(70, 38);
            this.EditBtn.TabIndex = 14;
            this.EditBtn.Text = "Edit";
            this.EditBtn.UseVisualStyleBackColor = false;
            this.EditBtn.Visible = false;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
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
            this.SearchNameListBox.Leave += new System.EventHandler(this.SearchNameListBox_Leave);
            // 
            // DescrTxt
            // 
            this.DescrTxt.Enabled = false;
            this.DescrTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescrTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.DescrTxt.Location = new System.Drawing.Point(544, 53);
            this.DescrTxt.MaxLength = 200;
            this.DescrTxt.Name = "DescrTxt";
            this.DescrTxt.Size = new System.Drawing.Size(250, 21);
            this.DescrTxt.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Αναζήτηση Προϊόντος";
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(93, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Αναζήτηση Κωδικού";
            // 
            // SearchIdTxt
            // 
            this.SearchIdTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchIdTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.SearchIdTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchIdTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SearchIdTxt.Location = new System.Drawing.Point(96, 163);
            this.SearchIdTxt.MaxLength = 9;
            this.SearchIdTxt.Name = "SearchIdTxt";
            this.SearchIdTxt.Size = new System.Drawing.Size(92, 21);
            this.SearchIdTxt.TabIndex = 4;
            this.SearchIdTxt.Enter += new System.EventHandler(this.SearchAfmTxt_Enter);
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(93, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Επιλογή Προϊόντος";
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
            this.RetrieveBtn.Location = new System.Drawing.Point(25, 186);
            this.RetrieveBtn.Name = "RetrieveBtn";
            this.RetrieveBtn.Size = new System.Drawing.Size(250, 33);
            this.RetrieveBtn.TabIndex = 4;
            this.RetrieveBtn.Text = "Ανάκτηση";
            this.RetrieveBtn.UseVisualStyleBackColor = true;
            this.RetrieveBtn.Click += new System.EventHandler(this.RetrieveBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.RetrieveBtn);
            this.panel1.Location = new System.Drawing.Point(70, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 230);
            this.panel1.TabIndex = 0;
            // 
            // IdTxt
            // 
            this.IdTxt.Enabled = false;
            this.IdTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.IdTxt.Location = new System.Drawing.Point(544, 20);
            this.IdTxt.MaxLength = 100;
            this.IdTxt.Name = "IdTxt";
            this.IdTxt.Size = new System.Drawing.Size(70, 21);
            this.IdTxt.TabIndex = 5;
            // 
            // IdLbl
            // 
            this.IdLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.IdLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdLbl.Location = new System.Drawing.Point(434, 20);
            this.IdLbl.Name = "IdLbl";
            this.IdLbl.Size = new System.Drawing.Size(80, 21);
            this.IdLbl.TabIndex = 0;
            this.IdLbl.Text = "Κωδικός";
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.Lime;
            this.SaveBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SaveBtn.Location = new System.Drawing.Point(634, 382);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(70, 38);
            this.SaveBtn.TabIndex = 15;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Visible = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.Red;
            this.CancelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelBtn.Location = new System.Drawing.Point(724, 382);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(70, 38);
            this.CancelBtn.TabIndex = 16;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Visible = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // label6
            // 
            this.label6.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(247, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ποσότητα | Τιμή";
            // 
            // label5
            // 
            this.label5.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(160, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ημερομηνία |";
            // 
            // label4
            // 
            this.label4.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(67, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Παραστατικό |";
            // 
            // DocumentLbx
            // 
            this.DocumentLbx.FormattingEnabled = true;
            this.DocumentLbx.Location = new System.Drawing.Point(70, 305);
            this.DocumentLbx.Name = "DocumentLbx";
            this.DocumentLbx.Size = new System.Drawing.Size(302, 173);
            this.DocumentLbx.TabIndex = 16;
            this.DocumentLbx.DoubleClick += new System.EventHandler(this.DocumentLbx_DoubleClick);
            // 
            // UnitTxt
            // 
            this.UnitTxt.Enabled = false;
            this.UnitTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnitTxt.Location = new System.Drawing.Point(544, 240);
            this.UnitTxt.MaxLength = 10;
            this.UnitTxt.Name = "UnitTxt";
            this.UnitTxt.Size = new System.Drawing.Size(70, 21);
            this.UnitTxt.TabIndex = 10;
            // 
            // UnitLbl
            // 
            this.UnitLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.UnitLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnitLbl.Location = new System.Drawing.Point(433, 240);
            this.UnitLbl.Name = "UnitLbl";
            this.UnitLbl.Size = new System.Drawing.Size(106, 21);
            this.UnitLbl.TabIndex = 0;
            this.UnitLbl.Text = "Μον.Μέτρησης";
            // 
            // label11
            // 
            this.label11.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(617, 273);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 21);
            this.label11.TabIndex = 19;
            this.label11.Text = "(%)";
            // 
            // ProfitPercLbl
            // 
            this.ProfitPercLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ProfitPercLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitPercLbl.Location = new System.Drawing.Point(433, 273);
            this.ProfitPercLbl.Name = "ProfitPercLbl";
            this.ProfitPercLbl.Size = new System.Drawing.Size(108, 21);
            this.ProfitPercLbl.TabIndex = 18;
            this.ProfitPercLbl.Text = "Περιθ. Κέρδους";
            // 
            // ProfitPercTxt
            // 
            this.ProfitPercTxt.Enabled = false;
            this.ProfitPercTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitPercTxt.Location = new System.Drawing.Point(544, 273);
            this.ProfitPercTxt.MaxLength = 10;
            this.ProfitPercTxt.Name = "ProfitPercTxt";
            this.ProfitPercTxt.Size = new System.Drawing.Size(70, 21);
            this.ProfitPercTxt.TabIndex = 11;
            this.ProfitPercTxt.TextChanged += new System.EventHandler(this.ProfitPercTxt_TextChanged);
            // 
            // SupplierDescrTxt
            // 
            this.SupplierDescrTxt.Enabled = false;
            this.SupplierDescrTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierDescrTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SupplierDescrTxt.Location = new System.Drawing.Point(544, 174);
            this.SupplierDescrTxt.MaxLength = 200;
            this.SupplierDescrTxt.Name = "SupplierDescrTxt";
            this.SupplierDescrTxt.Size = new System.Drawing.Size(250, 21);
            this.SupplierDescrTxt.TabIndex = 8;
            // 
            // SupplierDescrLbl
            // 
            this.SupplierDescrLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.SupplierDescrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierDescrLbl.Location = new System.Drawing.Point(434, 167);
            this.SupplierDescrLbl.Name = "SupplierDescrLbl";
            this.SupplierDescrLbl.Size = new System.Drawing.Size(94, 33);
            this.SupplierDescrLbl.TabIndex = 0;
            this.SupplierDescrLbl.Text = "Περιγραφή Προμηθευτή";
            // 
            // EditProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SupplierDescrTxt);
            this.Controls.Add(this.SupplierDescrLbl);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ProfitPercLbl);
            this.Controls.Add(this.ProfitPercTxt);
            this.Controls.Add(this.UnitTxt);
            this.Controls.Add(this.UnitLbl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DocumentLbx);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.IdLbl);
            this.Controls.Add(this.IdTxt);
            this.Controls.Add(this.SearchNameListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DescrTxt);
            this.Controls.Add(this.QuantLbl);
            this.Controls.Add(this.EditBtn);
            this.Controls.Add(this.MinStockTxt);
            this.Controls.Add(this.MinStockLbl);
            this.Controls.Add(this.QuantTxt);
            this.Controls.Add(this.ManufacTxt);
            this.Controls.Add(this.ManufacLbl);
            this.Controls.Add(this.LongDescrTxt);
            this.Controls.Add(this.LongDescrLbl);
            this.Controls.Add(this.SearchNameTxt);
            this.Controls.Add(this.DescrLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SelectNameCmb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SearchIdTxt);
            this.Controls.Add(this.panel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "EditProduct";
            this.Size = new System.Drawing.Size(845, 483);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DescrLbl;
        private System.Windows.Forms.TextBox SearchNameTxt;
        private System.Windows.Forms.TextBox LongDescrTxt;
        private System.Windows.Forms.Label LongDescrLbl;
        private System.Windows.Forms.TextBox QuantTxt;
        private System.Windows.Forms.Label QuantLbl;
        private System.Windows.Forms.TextBox ManufacTxt;
        private System.Windows.Forms.Label ManufacLbl;
        private System.Windows.Forms.TextBox MinStockTxt;
        private System.Windows.Forms.Label MinStockLbl;
        private System.Windows.Forms.Button EditBtn;
        private System.Windows.Forms.ListBox SearchNameListBox;
        private System.Windows.Forms.TextBox DescrTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SearchIdTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SelectNameCmb;
        private System.Windows.Forms.Button RetrieveBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox IdTxt;
        private System.Windows.Forms.Label IdLbl;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox DocumentLbx;
        private System.Windows.Forms.TextBox UnitTxt;
        private System.Windows.Forms.Label UnitLbl;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label ProfitPercLbl;
        private System.Windows.Forms.TextBox ProfitPercTxt;
        private System.Windows.Forms.TextBox SupplierDescrTxt;
        private System.Windows.Forms.Label SupplierDescrLbl;
    }
}
