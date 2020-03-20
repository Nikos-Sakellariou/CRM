namespace CRMapp
{
    partial class OrderFindSupplier
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
            this.SearchAfmTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectNameCmb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchNameListBox = new System.Windows.Forms.ListBox();
            this.SearchNameTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Αναζήτηση Επωνυμίας";
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
            // SearchAfmTxt
            // 
            this.SearchAfmTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchAfmTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.SearchAfmTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchAfmTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SearchAfmTxt.Location = new System.Drawing.Point(19, 148);
            this.SearchAfmTxt.MaxLength = 9;
            this.SearchAfmTxt.Name = "SearchAfmTxt";
            this.SearchAfmTxt.Size = new System.Drawing.Size(92, 21);
            this.SearchAfmTxt.TabIndex = 11;
            this.SearchAfmTxt.Enter += new System.EventHandler(this.SearchAfmTxt_Enter);
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 21);
            this.label2.TabIndex = 8;
            this.label2.Text = "Αναζήτηση Α.Φ.Μ.";
            // 
            // SelectNameCmb
            // 
            this.SelectNameCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectNameCmb.FormattingEnabled = true;
            this.SelectNameCmb.Location = new System.Drawing.Point(19, 93);
            this.SelectNameCmb.MaxDropDownItems = 3;
            this.SelectNameCmb.Name = "SelectNameCmb";
            this.SelectNameCmb.Size = new System.Drawing.Size(250, 21);
            this.SelectNameCmb.TabIndex = 10;
            this.SelectNameCmb.Enter += new System.EventHandler(this.SelectNameCmb_Enter);
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Επιλογή Επωνυμίας";
            // 
            // SearchNameListBox
            // 
            this.SearchNameListBox.FormattingEnabled = true;
            this.SearchNameListBox.Location = new System.Drawing.Point(19, 58);
            this.SearchNameListBox.Name = "SearchNameListBox";
            this.SearchNameListBox.Size = new System.Drawing.Size(250, 17);
            this.SearchNameListBox.TabIndex = 5;
            this.SearchNameListBox.Visible = false;
            this.SearchNameListBox.Click += new System.EventHandler(this.SearchNameListBox_Click);
            this.SearchNameListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchNameListBox_KeyPress);
            // 
            // SearchNameTxt
            // 
            this.SearchNameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchNameTxt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SearchNameTxt.Location = new System.Drawing.Point(19, 38);
            this.SearchNameTxt.MaxLength = 100;
            this.SearchNameTxt.Name = "SearchNameTxt";
            this.SearchNameTxt.Size = new System.Drawing.Size(250, 21);
            this.SearchNameTxt.TabIndex = 9;
            this.SearchNameTxt.TextChanged += new System.EventHandler(this.NameTxt_TextChanged);
            this.SearchNameTxt.Enter += new System.EventHandler(this.SearchNameTxt_Enter);
            this.SearchNameTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchNameTxt_KeyDown);
            this.SearchNameTxt.Leave += new System.EventHandler(this.SearchNameTxt_Leave);
            // 
            // OrderFindSupplier
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.SearchNameListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchNameTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SelectNameCmb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SearchAfmTxt);
            this.Controls.Add(this.RetrieveBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderFindSupplier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Εύρεση Προμηθευτή";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RetrieveBtn;
        private System.Windows.Forms.TextBox SearchAfmTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SelectNameCmb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox SearchNameListBox;
        private System.Windows.Forms.TextBox SearchNameTxt;
    }
}