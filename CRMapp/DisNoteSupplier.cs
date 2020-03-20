using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class DisNoteSupplier : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        Dictionary<string,string> dc = new Dictionary<string,string>();
        Dictionary<string, string> dscr = new Dictionary<string, string>();
        Checks chk = new Checks();
        ToolTip Tool1 = new ToolTip();

        public DisNoteSupplier()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetDataProd();
            DateTimeInvoicePicker.Value = System.DateTime.Today.AddDays(2);
        }


        private void GetDataProd()
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        ls.Clear();
                        dc.Clear();
                        dscr.Clear();
                        IdProdTxt1.AutoCompleteCustomSource.Clear();
                        string query = "select Id,SupplierDescr,LongDescr from Products order by Id,SupplierDescr";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ls.Add(dt.Rows[i][1].ToString());
                            dc.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            dscr.Add(dt.Rows[i][0].ToString(), dt.Rows[i][2].ToString());
                            IdProdTxt1.AutoCompleteCustomSource.Add(dt.Rows[i][0].ToString());
                        }
                        ls.Sort();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση περιγραφής/κωδικού Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }


        void FormClose(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }

        private void AddProdBtn_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.ProductsPanel.Height += 21;
            this.Height += 21;
            int i = Convert.ToInt16(ProdItemsTxt.Text);
            if (i == 1)
            {
                this.RemoveProdBtn1.Visible = true;
            }
            i++;
            ProdItemsTxt.Text = i.ToString();
            this.AddProdBtn1.Location = new Point(AddProdBtn1.Location.X, AddProdBtn1.Location.Y + 21);
            this.RemoveProdBtn1.Location = new Point(RemoveProdBtn1.Location.X, RemoveProdBtn1.Location.Y + 21);
            this.ProdPanel.Height += 21;
            System.Windows.Forms.Label AaTxt = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox IdProdTxt = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox DescrProdTxt = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox QuantProdTxt = new System.Windows.Forms.TextBox();
            System.Windows.Forms.ListBox SearchNameListBox = new System.Windows.Forms.ListBox();
            AaTxt.Text = i.ToString();
            AaTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            AaTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            AaTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            AaTxt.Name = "AaTxt" + i;
            AaTxt.Location = new Point(AaTxt1.Location.X, AaTxt1.Location.Y + 21 * (i - 1));
            AaTxt.Size = AaTxt1.Size;
            AaTxt.Font = AaTxt1.Font;
            IdProdTxt.Name = "IdProdTxt" + i;
            IdProdTxt.Location = new Point(IdProdTxt1.Location.X, IdProdTxt1.Location.Y + 21 * (i - 1));
            IdProdTxt.Size = IdProdTxt1.Size;
            IdProdTxt.AutoCompleteCustomSource = IdProdTxt1.AutoCompleteCustomSource;
            IdProdTxt.AutoCompleteMode = IdProdTxt1.AutoCompleteMode;
            IdProdTxt.AutoCompleteSource = IdProdTxt1.AutoCompleteSource;
            IdProdTxt.Font = IdProdTxt1.Font;
            SearchNameListBox.Name = "SearchNameListBox" + i;
            SearchNameListBox.Location = new Point(SearchNameListBox1.Location.X, SearchNameListBox1.Location.Y + 21 * (i - 1));
            SearchNameListBox.Size = SearchNameListBox1.Size;
            SearchNameListBox.Visible = false;
            SearchNameListBox.Font = SearchNameListBox1.Font;
            DescrProdTxt.Name = "DescrProdTxt" + i;
            DescrProdTxt.Location = new Point(DescrProdTxt1.Location.X, DescrProdTxt1.Location.Y + 21 * (i - 1));
            DescrProdTxt.Size = DescrProdTxt1.Size;
            DescrProdTxt.Font = DescrProdTxt1.Font;
            QuantProdTxt.Name = "QuantProdTxt" + i;
            QuantProdTxt.Location = new Point(QuantProdTxt1.Location.X, QuantProdTxt1.Location.Y + 21 * (i - 1));
            QuantProdTxt.Size = QuantProdTxt1.Size;
            QuantProdTxt.Font = QuantProdTxt1.Font;
            ToolTip Tool = new ToolTip();
            this.ProductsPanel.Controls.Add(AaTxt);
            this.ProductsPanel.Controls.Add(IdProdTxt);
            this.ProductsPanel.Controls.Add(SearchNameListBox);
            this.ProductsPanel.Controls.Add(DescrProdTxt);
            this.ProductsPanel.Controls.Add(QuantProdTxt);
            this.ProdPanel.SendToBack();
            {
                SearchNameListBox.KeyPress += (object sender11, KeyPressEventArgs e11) =>
                {
                    if (e11.KeyChar == (char)13)
                    {
                        if (SearchNameListBox.SelectedItem != null)
                        {
                            this.SuspendLayout();
                            DescrProdTxt.Focus();
                            DescrProdTxt.Text = SearchNameListBox.SelectedItem.ToString();
                            SearchNameListBox.Visible = false;
                            this.ResumeLayout(false);
                        }
                    }
                };

                DescrProdTxt.KeyDown += (object sender12, KeyEventArgs e12) =>
                {
                    if (e12.KeyData == Keys.Down)
                    {
                        if (SearchNameListBox.Visible == true && SearchNameListBox.Items.Count >= 1)
                        {
                            SearchNameListBox.Focus();
                            SearchNameListBox.SelectedIndex = 0;
                        }
                    }
                };
                QuantProdTxt.TextChanged += (object sender1, EventArgs e1) =>
                {
                    QuantProdTxt.Text = QuantProdTxt.Text.Replace(',', '.');
                    QuantProdTxt.SelectionStart = QuantProdTxt.Text.Length;
                };
                IdProdTxt.TextChanged += (object sender2, EventArgs e2) =>
                {
                    if (IdProdTxt.Focused)
                    {
                        if (dc.ContainsKey(IdProdTxt.Text))
                        {
                            string outcome;
                            dc.TryGetValue(IdProdTxt.Text, out outcome);
                            DescrProdTxt.Text = outcome;
                            string outcome2;
                            dscr.TryGetValue(IdProdTxt.Text, out outcome2);
                            Tool.Active = true;
                            Tool.InitialDelay = 100;
                            Tool.ReshowDelay = 100;
                            Tool.IsBalloon = true;
                            Tool.ToolTipIcon = ToolTipIcon.Info;
                            Tool.ToolTipTitle = "Αναλυτική Περιγραφή";
                            Tool.SetToolTip(DescrProdTxt, outcome2);
                            Tool.SetToolTip(IdProdTxt, outcome2);
                        }
                        else
                        {
                            DescrProdTxt.Text = "";
                            Tool.SetToolTip(DescrProdTxt, null);
                            Tool.SetToolTip(IdProdTxt, null);
                            Tool.Active = false;
                        }
                    }
                };
                DescrProdTxt.TextChanged += (object sender3, EventArgs e3) =>
                {
                    this.SuspendLayout();
                    if (DescrProdTxt.Focused)
                    {
                            SearchNameListBox.Height = 21;
                            SearchNameListBox.Items.Clear();
                            if (DescrProdTxt.TextLength > 0)
                            {
                                SearchNameListBox.Height = 21;
                                string[] s2 = DescrProdTxt.Text.Split(' ');
                                foreach (var item in ls)
                                {
                                    int Contain = 0;
                                    foreach (var item2 in s2)
                                    {

                                        if (Checks.RemoveDiacritics(item.ToUpper()).Contains(Checks.RemoveDiacritics(item2.ToUpper())) && item2 != "")
                                        {
                                            Contain++;
                                        }
                                    }
                                    if (Contain == s2.Count())
                                    {
                                        SearchNameListBox.Visible = true;
                                        SearchNameListBox.Items.Add(item);

                                    }
                                }
                                if (SearchNameListBox.Items.Count == 1)
                                {
                                    SearchNameListBox.Height = 42;
                                }
                                else if (SearchNameListBox.Items.Count == 2)
                                {
                                    SearchNameListBox.Height = 63;
                                }
                                else if (SearchNameListBox.Items.Count >= 3)
                                {
                                    SearchNameListBox.Height = 84;
                                }


                            }
                            else
                            {
                                SearchNameListBox.Height = 21;
                                SearchNameListBox.Visible = false;
                            }
                            if (dc.ContainsValue(DescrProdTxt.Text))
                            {
                                foreach (var item in dc)
                                {
                                    if (item.Value == DescrProdTxt.Text)
                                    {
                                        IdProdTxt.Text = item.Key;
                                        string outcome2;
                                        dscr.TryGetValue(IdProdTxt.Text, out outcome2);
                                        Tool.Active = true;
                                        Tool.InitialDelay = 100;
                                        Tool.ReshowDelay = 100;
                                        Tool.IsBalloon = true;
                                        Tool.ToolTipIcon = ToolTipIcon.Info;
                                        Tool.ToolTipTitle = "Αναλυτική Περιγραφή";
                                        Tool.SetToolTip(DescrProdTxt, outcome2);
                                        Tool.SetToolTip(IdProdTxt, outcome2);
                                    }
                                }
                            }
                            else
                            {
                                IdProdTxt.Text = "";
                                Tool.SetToolTip(DescrProdTxt, null);
                                Tool.SetToolTip(IdProdTxt, null);
                                Tool.Active = false;
                            }
                    }
                    this.ResumeLayout(false);
                };

                DescrProdTxt.Leave += (object sender4, EventArgs e5) =>
                {
                    this.SuspendLayout();
                    if (SearchNameListBox.Focused != true)
                     {
                         SearchNameListBox.Visible = false;
                     }
                     this.ResumeLayout(false);
                 };

                SearchNameListBox.Click += (object sender5, EventArgs e5) =>
                {
                    if (SearchNameListBox.SelectedItem != null)
                    {
                        this.SuspendLayout();
                        DescrProdTxt.Focus();
                        DescrProdTxt.Text = SearchNameListBox.SelectedItem.ToString();
                        SearchNameListBox.Visible = false;
                        this.ResumeLayout(false);
                    }
                };

                SearchNameListBox.Leave += (object sender6, EventArgs e6) =>
                {
                    SearchNameListBox.Visible = false;
                };
            }
            this.ResumeLayout(false);
        }

        private void SearchSupplierBtn_Click(object sender, EventArgs e)
        {
            OrderFindSupplier FindSupplier = new OrderFindSupplier();
            FindSupplier.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindSupplier.ReturnSupplierId()!=null)
                {
                    this.IdSupplierTxt.Text = FindSupplier.ReturnSupplierId();
                    this.NameSupplierTxt.Text = FindSupplier.ReturnSupplierName();
                    this.AfmSupplierTxt.Text = FindSupplier.ReturnSupplierAfm();
                }
            };
            FindSupplier.FormClosed += new FormClosedEventHandler(FormClose);
            this.Enabled = false;
            FindSupplier.ShowDialog();
        }

        private void AddSupplierBtn_Click(object sender, EventArgs e)
        {
            OrderAddSupplier AddSupplier = new OrderAddSupplier();
            AddSupplier.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (AddSupplier.ReturnSupplierId() != null)
                {
                    this.IdSupplierTxt.Text = AddSupplier.ReturnSupplierId();
                    this.NameSupplierTxt.Text = AddSupplier.ReturnSupplierName();
                    this.AfmSupplierTxt.Text = AddSupplier.ReturnSupplierAfm();
                }
            };
            AddSupplier.FormClosed += new FormClosedEventHandler(FormClose);
            this.Enabled = false;
            AddSupplier.ShowDialog();
        }

        private void IdProdTxt1_TextChanged(object sender, EventArgs e)
        {
            if (IdProdTxt1.Focused)
            {
                if (dc.ContainsKey(IdProdTxt1.Text))
                {
                    string outcome;
                    dc.TryGetValue(IdProdTxt1.Text, out outcome);
                    DescrProdTxt1.Text = outcome;
                    string outcome2;
                    dscr.TryGetValue(IdProdTxt1.Text, out outcome2);
                    Tool1.Active = true;
                    Tool1.InitialDelay = 100;
                    Tool1.ReshowDelay = 100;
                    Tool1.IsBalloon = true;
                    Tool1.ToolTipIcon = ToolTipIcon.Info;
                    Tool1.ToolTipTitle = "Αναλυτική Περιγραφή";
                    Tool1.SetToolTip(DescrProdTxt1, outcome2);
                    Tool1.SetToolTip(IdProdTxt1, outcome2);
                }
                else
                {
                    DescrProdTxt1.Text = "";
                    Tool1.SetToolTip(DescrProdTxt1, null);
                    Tool1.SetToolTip(IdProdTxt1, null);
                    Tool1.Active = false;
                }
            }
        }

        private void RemoveProdBtn1_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            int i = Convert.ToInt16(ProdItemsTxt.Text);
            if (i == 2)
            {
                this.RemoveProdBtn1.Visible = false;
            }
            this.AddProdBtn1.Location = new Point(AddProdBtn1.Location.X, AddProdBtn1.Location.Y - 21);
            this.RemoveProdBtn1.Location = new Point(RemoveProdBtn1.Location.X, RemoveProdBtn1.Location.Y - 21);
            this.ProdPanel.Height -= 21;
            this.ProductsPanel.Height -= 21;
            this.Height -= 21;
            this.ProductsPanel.Controls.Remove(Controls.Find("AaTxt" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("IdProdTxt" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("DescrProdTxt" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("QuantProdTxt" + i, true).First());
            i--;
            ProdItemsTxt.Text = i.ToString();
            this.ResumeLayout(false);
        }

        private void DescrProdTxt1_TextChanged(object sender, EventArgs e)
        {
            if (DescrProdTxt1.Focused)
            {
                if (DescrProdTxt1.TextLength > 0)
                {
                    SearchNameListBox1.Height = 21;
                    SearchNameListBox1.Items.Clear();
                    if (DescrProdTxt1.TextLength > 0)
                    {
                        SearchNameListBox1.Height = 21;
                        string[] s2 = DescrProdTxt1.Text.Split(' ');
                        foreach (var item in ls)
                        {
                            int Contain = 0;
                            foreach (var item2 in s2)
                            {
                                
                                if (Checks.RemoveDiacritics(item.ToUpper()).Contains(Checks.RemoveDiacritics(item2.ToUpper())) && item2!="")
                                {
                                    Contain++;
                                }
                            }
                            if (Contain == s2.Count())
                            {
                                SearchNameListBox1.Visible = true;
                                SearchNameListBox1.Items.Add(item);

                            }
                        }
                        if (SearchNameListBox1.Items.Count == 1)
                        {
                            SearchNameListBox1.Height = 42;
                        }
                        else if (SearchNameListBox1.Items.Count == 2)
                        {
                            SearchNameListBox1.Height = 63;
                        }
                        else if (SearchNameListBox1.Items.Count >= 3)
                        {
                            SearchNameListBox1.Height = 84;
                        }


                    }
                    else
                    {
                        SearchNameListBox1.Height = 21;
                        SearchNameListBox1.Visible = false;
                    }
                    if (dc.ContainsValue(DescrProdTxt1.Text))
                    {
                        foreach (var item in dc)
                        {
                            if (item.Value == DescrProdTxt1.Text)
                            {
                                IdProdTxt1.Text = item.Key;
                                string outcome2;
                                dscr.TryGetValue(IdProdTxt1.Text, out outcome2);
                                Tool1.Active = true;
                                Tool1.InitialDelay = 100;
                                Tool1.ReshowDelay = 100;
                                Tool1.IsBalloon = true;
                                Tool1.ToolTipIcon = ToolTipIcon.Info;
                                Tool1.ToolTipTitle = "Αναλυτική Περιγραφή";
                                Tool1.SetToolTip(DescrProdTxt1, outcome2);
                                Tool1.SetToolTip(IdProdTxt1, outcome2);
                            }
                        }
                    }
                    else
                    {
                        IdProdTxt1.Text = "";
                        Tool1.SetToolTip(DescrProdTxt1, null);
                        Tool1.SetToolTip(IdProdTxt1, null);
                        Tool1.Active = false;
                    }
                }
                else
                {
                    SearchNameListBox1.Visible = false;
                }
            }
            
        }

        private void DescrProdTxt1_Leave(object sender, EventArgs e)
        {
            if (SearchNameListBox1.Focused != true)
            {
                SearchNameListBox1.Visible = false;
            }
        }

        private void SearchNameListBox_Click(object sender, EventArgs e)
        {
            if (SearchNameListBox1.SelectedItem!=null)
            {
                DescrProdTxt1.Focus();
                DescrProdTxt1.Text = SearchNameListBox1.SelectedItem.ToString();
                SearchNameListBox1.Visible = false;
            }
        }
        
        private void SearchNameListBox1_Leave(object sender, EventArgs e)
        {
            SearchNameListBox1.Visible = false;
        }

        private void QuantProdTxt1_TextChanged(object sender, EventArgs e)
        {
            QuantProdTxt1.Text=QuantProdTxt1.Text.Replace(',', '.');
            QuantProdTxt1.SelectionStart = QuantProdTxt1.Text.Length;
        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            bool noerrors = true;
            double sumquant = 0;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                if (dc.ContainsKey(ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text))
                {
                    ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().BackColor = SystemColors.Window;
                    string outcome;
                    dc.TryGetValue(ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text, out outcome);
                    if (ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First().Text != outcome)
                    {
                        ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First().BackColor = Color.Red;
                        noerrors = false;
                    }
                    else
                    {
                        ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First().BackColor = SystemColors.Window;
                    }
                }
                else
                {
                    ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                if (chk.CheckQuant(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) == "")
                {
                    ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = SystemColors.Window;
                    sumquant += Convert.ToDouble(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                }
                else
                {
                    ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
            }
            if (IdSupplierTxt.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε προμηθευτή.");
            }
            else if (IdInvoiceTxt.Text == "" || SeriesInvoiceTxt.Text == "")
            {
                MessageBox.Show("Θα πρέπει να συμπληρώσετε αριθμό και σειρά τιμολογίου.");
            }
            else if (DateTimeInvoicePicker.Value > System.DateTime.Today.AddDays(1))
            {
                MessageBox.Show("Η ημερομηνία δεν μπορεί να είναι μεταγενέστερη της σημερινής.");
            }
            else if (noerrors==false)
            {
                MessageBox.Show("Θα πρέπει να διορθώσετε τα στοιχεία των προϊόντων.");
            }
            else
            {
                AddBtn.Visible = true;
                CorrectBtn.Visible = true;
                CalcBtn.Visible = false;
                ClearBtn.Visible = false;
                IdInvoiceTxt.ReadOnly = true;
                SeriesInvoiceTxt.ReadOnly = true;
                AddProdBtn1.Enabled = false;
                RemoveProdBtn1.Enabled = false;
                AddSupplierBtn.Enabled = false;
                SearchSupplierBtn.Enabled = false;
                QuantDisNoteTxt.Text = sumquant.ToString();
                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                {
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = true;
                }

            }
        }

        private void CorrectBtn_Click(object sender, EventArgs e)
        {
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            IdInvoiceTxt.ReadOnly = false;
            SeriesInvoiceTxt.ReadOnly = false;
            QuantDisNoteTxt.Text = "";
            AddProdBtn1.Enabled = true;
            RemoveProdBtn1.Enabled = true;
            AddSupplierBtn.Enabled = true;
            SearchSupplierBtn.Enabled = true;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = false;
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        { 
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from SupplierDisNote where SupplierId=@supid and DisNoteId=@invid and DisNoteSeries=@invser", sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"supid", IdSupplierTxt.Text);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"invid", IdInvoiceTxt.Text);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"invser", SeriesInvoiceTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Υπάρχει ήδη καταχωρημένο δελτίο αποστολής με τον αριθμό, τη σειρά και τον προμηθευτή που έχετε εισάγει.");
                        }
                        else
                        {
                            SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                            SqlCommand InsCmd1 = sqlcon.CreateCommand();
                            InsCmd1.Connection = sqlcon;
                            InsCmd1.Transaction = InsTrans;
                            try
                            {
                                InsCmd1.CommandText = "insert into SupplierDisNote (Id, SupplierId, DisNoteId, DisNoteSeries, DisNoteDate, Invoice) values((select dbo.nvl(Max(Id)+1,0) from dbo.SupplierDisNote), @supid, @invid, @ser, @date,'0')";
                                InsCmd1.Parameters.AddWithValue("@supid", IdSupplierTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@invid", IdInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@ser", SeriesInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                                InsCmd1.ExecuteNonQuery();
                               
                                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                                {
                                    SqlCommand InsCmd3 = sqlcon.CreateCommand();
                                    InsCmd3.Connection = sqlcon;
                                    SqlCommand InsCmd4 = sqlcon.CreateCommand();
                                    InsCmd4.Connection = sqlcon;
                                    InsCmd3.Transaction = InsTrans;
                                    InsCmd4.Transaction = InsTrans;
                                    InsCmd3.CommandText = "insert into SupplierDisNoteProducts (Id,SupplierDisNoteId,ProductId,ProductQuant) values ((select dbo.nvl(Max(Id)+1,0) from SupplierDisNoteProducts),(select dbo.nvl(Max(Id),0) from dbo.SupplierDisNote),@prodid,@prodquant)";
                                    InsCmd3.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@prodquant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    InsCmd3.ExecuteNonQuery();
                                    InsCmd4.CommandText = "insert into ProductsReserve (Id,DocId,ProductId,Quant,SupplierId,Date) values ((select dbo.nvl(Max(Id)+1,0) from ProductsReserve),@doc,@proddid,@prodquant,@supid,@dat)";
                                    InsCmd4.Parameters.AddWithValue("@doc", IdInvoiceTxt.Text+((SeriesInvoiceTxt.Text=="") ?"" :" - "+SeriesInvoiceTxt.Text));
                                    InsCmd4.Parameters.AddWithValue("@proddid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@prodquant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@supid", IdSupplierTxt.Text);
                                    InsCmd4.Parameters.AddWithValue("@dat", System.DateTime.Today.ToShortDateString());
                                    InsCmd4.ExecuteNonQuery();
                                }
                                
                                InsTrans.Commit();
                                MessageBox.Show("Το δελτίο αποστολής του προμηθευτή καταχωρήθηκε με επιτυχία.");
                                ClearValues();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του δελτίου αποστολής του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                try
                                {
                                    InsTrans.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του δελτίου αποστολής του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                }
                            }
                        }
                        sqlcon.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                }
        }


        private void ClearValues()
        {

            this.SuspendLayout();
            IdSupplierTxt.Text = "";
            NameSupplierTxt.Text = "";
            AfmSupplierTxt.Text = "";
            IdInvoiceTxt.Text = "";
            SeriesInvoiceTxt.Text = "";
            QuantDisNoteTxt.Text = "";
            DateTimeInvoicePicker.Value = System.DateTime.Today.AddDays(2);
            IdProdTxt1.Text = "";
            DescrProdTxt1.Text = "";
            QuantProdTxt1.Text = "";
            IdProdTxt1.ReadOnly = false;
            DescrProdTxt1.ReadOnly = false;
            QuantProdTxt1.ReadOnly = false;
            IdProdTxt1.BackColor = SystemColors.Window;
            DescrProdTxt1.BackColor = SystemColors.Window;
            QuantProdTxt1.BackColor = SystemColors.Window;
            AddProdBtn1.Enabled = true;
            RemoveProdBtn1.Enabled = true;
            AddSupplierBtn.Enabled = true;
            SearchSupplierBtn.Enabled = true;
            for (int i = Convert.ToInt16(ProdItemsTxt.Text); i >= 2; i--)
            {
                RemoveProdBtn1_Click(null, null);
            }
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            IdInvoiceTxt.ReadOnly = false;
            SeriesInvoiceTxt.ReadOnly = false;
            this.ResumeLayout(false);
        }

        private void AddNewProdBtn_Click(object sender, EventArgs e)
        {
            OrderAddProduct AddProduct = new OrderAddProduct();
            AddProduct.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (AddProduct.has_added_prod() == true)
                {
                    GetDataProd();
                }
            };
            AddProduct.FormClosed += new FormClosedEventHandler(FormClose);
            AddProduct.FormClosed += (object sender3, FormClosedEventArgs e3) =>
            {
                this.SuspendLayout();
                AddProduct.Dispose();
                this.ResumeLayout(false);
            };
            this.Enabled = false;
            AddProduct.ShowDialog();
        }

        private void SearchNameListBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchNameListBox_Click(null, null);
            }
        }

        private void DescrProdTxt1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (SearchNameListBox1.Visible == true && SearchNameListBox1.Items.Count >= 1)
                {
                    SearchNameListBox1.Focus();
                    SearchNameListBox1.SelectedIndex = 0;
                }
            }
        }
    }
}
