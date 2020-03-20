using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class DisNoteReturnSupplier : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        Dictionary<string,string> dc = new Dictionary<string,string>();
        Dictionary<string, string> dc2 = new Dictionary<string, string>();
        Dictionary<string, string> dscr = new Dictionary<string, string>();
        Dictionary<string, string> unt = new Dictionary<string, string>();
        Checks chk = new Checks();
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
        ToolTip Tool1 = new ToolTip();
        int proditems = 1;
        double sumquant = 0;
        int pagecount = 1;
        Image InvoiceDrawImg = Properties.Resources.InvDraw;
        Image InvoicePlainImg = Properties.Resources.InvPlain;
        private string SupplierOccupation;
        private string SupplierAddress;
        private string SupplierTk;
        private string SupplierTax_office;
        private string SupplierPhone;
        private string SupplierEmail;
        private string SupplierRegion;
        private string SupplierPhone2;

        public DisNoteReturnSupplier()
        {
            GC.Collect();
            con = chk.Initiallize_con();
            InitializeComponent();
            GetDataProd();
        }


        private void GetDataProd()
        {
            SeriesInvoiceTxt.Text = chk.Return_DisNoteSeries();
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        GC.Collect();
                        ls.Clear();
                        dc.Clear();
                        dscr.Clear();
                        unt.Clear();
                        dc2.Clear();
                        string query = "select Id,SupplierDescr,LongDescr,Quant,Unit from ProductsReserveView order by Id,Description";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ls.Add(dt.Rows[i][1].ToString());
                            dc.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            dscr.Add(dt.Rows[i][0].ToString(), dt.Rows[i][2].ToString());
                            dc2.Add(dt.Rows[i][0].ToString(), dt.Rows[i][3].ToString());
                            unt.Add(dt.Rows[i][0].ToString(), dt.Rows[i][4].ToString());
                            IdProdTxt1.AutoCompleteCustomSource.Add(dt.Rows[i][0].ToString());
                        }
                        ls.Sort();
                        string query2 = @"select right('00000'+Max(a.Id),5) from
                                        (select dbo.nvl(Max(right(DisNoteId, 5)) + 1, 1) as Id from dbo.CustomerDisNote where DisNoteSeries=@series 
                                            union
                                        select dbo.nvl(Max(right(DisNoteId, 5)) + 1, 1) from dbo.SupplierReturnDisNote where DisNoteSeries=@series) a";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"series", chk.Return_DisNoteSeries());
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        if (dt2.Rows.Count == 1)
                        {
                            IdInvoiceTxt.Text = "ΔΑ-" + dt2.Rows[0][0].ToString();
                        }
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
            proditems = i;
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
                            string outcome2;
                            dc.TryGetValue(IdProdTxt.Text, out outcome);
                            dscr.TryGetValue(IdProdTxt.Text, out outcome2);
                            DescrProdTxt.Text = outcome;
                            Tool.Active = true;
                            Tool.InitialDelay = 100;
                            Tool.ReshowDelay = 100;
                            Tool.IsBalloon = false;
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
                                    string outcome;
                                    dscr.TryGetValue(item.Key, out outcome);
                                    Tool.Active = true;
                                    Tool.InitialDelay = 100;
                                    Tool.ReshowDelay = 100;
                                    Tool.IsBalloon = true;
                                    Tool.ToolTipIcon = ToolTipIcon.Info;
                                    Tool.ToolTipTitle = "Αναλυτική Περιγραφή";
                                    Tool.SetToolTip(DescrProdTxt, outcome);
                                    Tool.SetToolTip(IdProdTxt, outcome);
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

        private void SearchCustomerBtn_Click(object sender, EventArgs e)
        {
            OrderFindSupplier FindSupplier = new OrderFindSupplier();
            FindSupplier.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindSupplier.ReturnSupplierId() != null)
                {
                    this.IdSupplierTxt.Text = FindSupplier.ReturnSupplierId();
                    this.NameSupplierTxt.Text = FindSupplier.ReturnSupplierName();
                    this.AfmSupplierTxt.Text = FindSupplier.ReturnSupplierAfm();
                    SupplierOccupation = FindSupplier.ReturnSupplierOccupation();
                    SupplierAddress = FindSupplier.ReturnSupplierAddress();
                    SupplierTk = FindSupplier.ReturnSupplierTk();
                    SupplierTax_office = FindSupplier.ReturnSupplierTax_office();
                    SupplierPhone = FindSupplier.ReturnSupplierPhone();
                    SupplierEmail = FindSupplier.ReturnSupplierEmail();
                    SupplierPhone2 = FindSupplier.ReturnSupplierPhone2();
                    SupplierRegion = FindSupplier.ReturnSupplierRegion();
                }
            };
            FindSupplier.FormClosed += new FormClosedEventHandler(FormClose);
            FindSupplier.FormClosed += (object sender3, FormClosedEventArgs e3) =>
            {
                this.SuspendLayout();
                FindSupplier.Dispose();
                this.ResumeLayout(false);
            };
            this.Enabled = false;
            FindSupplier.ShowDialog();
        }
        
        private void IdProdTxt1_TextChanged(object sender, EventArgs e)
        {
            if (IdProdTxt1.Focused)
            {
                if (dc.ContainsKey(IdProdTxt1.Text))
                {
                    string outcome;
                    string outcome2;
                    dc.TryGetValue(IdProdTxt1.Text, out outcome);
                    dscr.TryGetValue(IdProdTxt1.Text, out outcome2);
                    DescrProdTxt1.Text = outcome;
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
            proditems = i;
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

                                if (Checks.RemoveDiacritics(item.ToUpper()).Contains(Checks.RemoveDiacritics(item2.ToUpper())) && item2 != "")
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
                                string outcome;
                                dscr.TryGetValue(item.Key, out outcome);
                                Tool1.Active = true;
                                Tool1.InitialDelay = 100;
                                Tool1.ReshowDelay = 100;
                                Tool1.IsBalloon = true;
                                Tool1.ToolTipIcon = ToolTipIcon.Info;
                                Tool1.ToolTipTitle = "Αναλυτική Περιγραφή";
                                Tool1.SetToolTip(DescrProdTxt1, outcome);
                                Tool1.SetToolTip(IdProdTxt1, outcome);
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
            string overcome = "";
            string storageAvail = "";
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
                if (chk.CheckQuant(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) == "")
                {
                    string outcome;
                    dc2.TryGetValue(ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text, out outcome);
                    if (Convert.ToDecimal(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) > Convert.ToDecimal(outcome))
                    {
                        ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = Color.DarkRed;
                        overcome += i + ", ";
                        storageAvail += "A/A " + i + " :   " + outcome + "\n";
                    }
                }
            }
            if (IdSupplierTxt.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε προμηθευτή.");
            }
            else if (DateTimeInvoicePicker.Value > System.DateTime.Today.AddDays(1))
            {
                MessageBox.Show("Η ημερομηνία δεν μπορεί να είναι μεταγενέστερη της σημερινής.");
            }
            else if (noerrors==false)
            {
                MessageBox.Show("Θα πρέπει να διορθώσετε τα στοιχεία των προϊόντων.");
            }
            else if (overcome != "")
            {
                DialogResult dialres = MessageBox.Show("Τα προϊόντα με Α/Α: " + overcome + " υπερβαίνουν το σύνολο των διαθέσιμων προϊόντων της αποθήκης.\n Διαθέσιμα Αποθήκης \n" + storageAvail + "\n\n Θέλετε να προσθέσετε προϊόντα στην αποθήκη;", "", MessageBoxButtons.YesNo);
                if (dialres == DialogResult.Yes)
                {
                    ReserveAddProduct AddProduct = new ReserveAddProduct();
                    AddProduct.FormClosed += new FormClosedEventHandler(FormClose);
                    AddProduct.FormClosed += (object sender3, FormClosedEventArgs e3) =>
                    {
                        this.SuspendLayout();
                        GetDataProd();
                        AddProduct.Dispose();
                        this.ResumeLayout(false);
                    };
                    this.Enabled = false;
                    AddProduct.ShowDialog();
                }
            }
            else
            {
                AddBtn.Visible = true;
                CorrectBtn.Visible = true;
                CalcBtn.Visible = false;
                ClearBtn.Visible = false;
                AddProdBtn1.Enabled = false;
                RemoveProdBtn1.Enabled = false;
                SearchCustomerBtn.Enabled = false;
                QuantDisNoteTxt.Text = sumquant.ToString();
                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                {
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = true;
                }
                InvoiceDocsTxt.ReadOnly = true;
                VehicleTxt.ReadOnly = true;
                NotesTxt.ReadOnly = true;
                CommentsTxt.ReadOnly = true;
            }
        }

        private void CorrectBtn_Click(object sender, EventArgs e)
        {
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            QuantDisNoteTxt.Text = "";
            AddProdBtn1.Enabled = true;
            RemoveProdBtn1.Enabled = true;
            SearchCustomerBtn.Enabled = true;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = false;
            }
            InvoiceDocsTxt.ReadOnly = false;
            VehicleTxt.ReadOnly = false;
            NotesTxt.ReadOnly = false;
            CommentsTxt.ReadOnly = false;
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
                            SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                            SqlCommand InsCmd1 = sqlcon.CreateCommand();
                            InsCmd1.Connection = sqlcon;
                            InsCmd1.Transaction = InsTrans;
                            try
                            {
                                InsCmd1.CommandText = "insert into SupplierReturnDisNote (Id, SupplierId, DisNoteId, DisNoteSeries, DisNoteDate, DisNoteDocs,Vehicle, Notes, Comments,DebitInvoice) values((select dbo.nvl(Max(Id)+1,0) from dbo.SupplierReturnDisNote), @supid, @invid, @ser, @date,@docs,@veh,@not,@comm,'0')";
                                InsCmd1.Parameters.AddWithValue("@supid", IdSupplierTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@invid", IdInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@ser", SeriesInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                                InsCmd1.Parameters.AddWithValue("@docs", InvoiceDocsTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@veh", VehicleTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@not", NotesTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@comm", CommentsTxt.Text);
                                InsCmd1.ExecuteNonQuery();
                               
                                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                                {
                                    SqlCommand InsCmd3 = sqlcon.CreateCommand();
                                    InsCmd3.Connection = sqlcon;
                                    // ΝΑ ΡΩΤΗΣΩ ΑΝ ΠΡΕΠΕΙ ΝΑ ΑΦΕΡΕΙ ΠΡΟΙΟΝΤΑ
                                    //SqlCommand InsCmd4 = new SqlCommand("CustomerInv_ProdReserve_Proc"); 
                                    //InsCmd4.Connection = sqlcon;
                                    //InsCmd4.CommandType = CommandType.StoredProcedure;
                                    InsCmd3.Transaction = InsTrans;
                                    //InsCmd4.Transaction = InsTrans;
                                    InsCmd3.CommandText = "insert into SupplierReturnDisNoteProducts (Id,SupplierDisNoteId,ProductId,ProductQuant) values ((select dbo.nvl(Max(Id)+1,0) from SupplierReturnDisNoteProducts),(select dbo.nvl(Max(Id),0) from dbo.SupplierReturnDisNote),@prodid,@prodquant)";
                                    InsCmd3.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@prodquant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    InsCmd3.ExecuteNonQuery();
                                    //InsCmd4.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    //InsCmd4.Parameters.AddWithValue("@quant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    //InsCmd4.ExecuteNonQuery();
                                }
                                
                                InsTrans.Commit();
                                MessageBox.Show("Το δελτίο αποστολής του προμηθευτή καταχωρήθηκε με επιτυχία.");
                                Do_Print();
                                ClearValues();
                                GetDataProd();
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
            QuantDisNoteTxt.Text = "";
            DateTimeInvoicePicker.Value = System.DateTime.Today;
            IdProdTxt1.Text = "";
            DescrProdTxt1.Text = "";
            QuantProdTxt1.Text = "";
            InvoiceDocsTxt.Text = "";
            VehicleTxt.Text = "";
            NotesTxt.Text = "";
            CommentsTxt.Text = "";
            IdProdTxt1.ReadOnly = false;
            DescrProdTxt1.ReadOnly = false;
            QuantProdTxt1.ReadOnly = false;
            InvoiceDocsTxt.ReadOnly = false;
            VehicleTxt.ReadOnly = false;
            NotesTxt.ReadOnly = false;
            CommentsTxt.ReadOnly = false;
            IdProdTxt1.BackColor = SystemColors.Window;
            DescrProdTxt1.BackColor = SystemColors.Window;
            QuantProdTxt1.BackColor = SystemColors.Window;
            AddProdBtn1.Enabled = true;
            RemoveProdBtn1.Enabled = true;
            SearchCustomerBtn.Enabled = true;
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
            sumquant = 0;
            pagecount = 1;
            SupplierOccupation = "";
            SupplierAddress = "";
            SupplierTk = "";
            SupplierTax_office = "";
            SupplierPhone = "";
            SupplierEmail = "";
            SupplierPhone2 = "";
            SupplierRegion = "";
            this.ResumeLayout(false);
        }

        private void Do_Print()
        {
            if (PrintPreviewChkBox.Checked)
            {
                PreviewDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 1094, 1546);
                PrintPrev.Document = PreviewDoc;
                ((Form)PrintPrev).StartPosition = FormStartPosition.CenterScreen;
                PrintPrev.ShowDialog();

            }
            else if (PrintChkBox.Checked)
            {
                PrintDial.Document = PreviewDoc;
                PrintDial.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 1094, 1546);
                PrintDial.PrinterSettings.Copies = 2;
                DialogResult result = PrintDial.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PrintDoc.Print();
                }
            }
        }

        private void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(InvoicePlainImg, 0, 0, InvoicePlainImg.Width, InvoicePlainImg.Height);
            if (proditems > 0)
            {
                PrintDoc_Elements(e);
            }
        }

        private void PreviewDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(InvoiceDrawImg, 0, 0, InvoiceDrawImg.Width, InvoiceDrawImg.Height);
            if (proditems > 0)
            {
                PrintDoc_Elements(e);
            }
        }

        private void PreviewDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            PrintPrev.Document = PrintDoc;
        }

        private void PrintDoc_Elements(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΤΙΜΟΛΟΓΙΟΥ
            e.Graphics.DrawString("Δελτίο Αποστολής", new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 193, 250);
            e.Graphics.DrawString(SeriesInvoiceTxt.Text, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 588, 250);
            e.Graphics.DrawString(IdInvoiceTxt.Text, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 690, 250);
            e.Graphics.DrawString(DateTimeInvoicePicker.Value.Day.ToString() + '/' + DateTimeInvoicePicker.Value.Month.ToString() + '/' + DateTimeInvoicePicker.Value.Year.ToString(), new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 821, 250);
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΠΕΛΑΤΗ
            e.Graphics.DrawString(IdSupplierTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 329);
            e.Graphics.DrawString(NameSupplierTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 356);
            e.Graphics.DrawString(SupplierAddress, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 383);
            e.Graphics.DrawString(SupplierRegion, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 410);
            e.Graphics.DrawString(SupplierOccupation, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 438);
            e.Graphics.DrawString(AfmSupplierTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 466);
            e.Graphics.DrawString(SupplierEmail, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 493);
            e.Graphics.DrawString(SupplierPhone.Substring(0, 10), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 329);
            e.Graphics.DrawString(SupplierPhone2.Substring(0, 10), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 356);
            e.Graphics.DrawString(SupplierTk, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 411);
            e.Graphics.DrawString(SupplierTax_office, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 466);
            //ΕΚΤΥΠΩΣΗ ΛΟΙΠΩΝ ΣΤΟΙΧΕΙΩΝ ΤΙΜΟΛΟΓΙΟΥ
            e.Graphics.DrawString(InvoiceDocsTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 307);   //SXET PARASTATIKA
            e.Graphics.DrawString("Έδρα μας", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 336);
            e.Graphics.DrawString("Έδρα προμηθευτή", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 365);
            e.Graphics.DrawString("Επιστροφή", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 394);
            e.Graphics.DrawString(VehicleTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 487);  
            //ΕΚΤΥΠΩΣΗ ΠΡΟΙΟΝΤΩΝ
            int rest = proditems;
            int cnt = Convert.ToInt16(ProdItemsTxt.Text) - proditems;
            bool hasmorepages = false;
            decimal vat = chk.Return_Vat("999999");
            string Unit;
            for (int i = 1; i <= rest; i++)
            {
                if (i == 19)
                {
                    hasmorepages = true;
                    break;
                }
                string qnt = ProductsPanel.Controls.Find("QuantProdTxt" + (i + cnt), true).First().Text;
                e.Graphics.DrawString(ProductsPanel.Controls.Find("IdProdTxt" + (i + cnt), true).First().Text, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 33, 558 + i * 25);
                string descr = ProductsPanel.Controls.Find("DescrProdTxt" + (i + cnt), true).First().Text;
                if (descr.Length > 60)
                {
                    descr = descr.Substring(0, 60);
                }
                e.Graphics.DrawString(descr, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 120, 558 + i * 25);
                e.Graphics.DrawString(chk.GrQuant(qnt), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(561, 558 + i * 25, 65, 25), format);
                //ΕΥΡΕΣΗ ΜΟΝΑΔΑΣ ΜΕΤΡΗΣΗΣ
                unt.TryGetValue(ProductsPanel.Controls.Find("IdProdTxt" + (i + cnt), true).First().Text, out Unit);
                //
                if (Unit.Length > 4)
                {
                    Unit = Unit.Substring(0, 4);
                }
                e.Graphics.DrawString(Unit, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 520, 558 + i * 25);
                proditems--;
                sumquant += Convert.ToDouble(qnt);
            }
            //ΕΚΤΥΠΩΣΗ ΑΡΙΘΜΙΣΗΣ ΣΕΛΙΔΩΝ ΤΙΜΟΛΟΓΙΟΥ
            e.Graphics.DrawString("Σελίδα " + pagecount + " από " + (Convert.ToInt16(ProdItemsTxt.Text) / 18 + 1), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 45, 1505);
            pagecount++;
            //ΕΚΤΥΠΩΣΗ ΣΥΝΟΛΩΝ ΤΙΜΟΛΟΓΙΟΥ
            if (hasmorepages == false)
            {
                if (NotesTxt.Text!="")
                {
                    string Note1 = NotesTxt.Text;
                    string Note2 = "";
                    if (Note1.Length > 70)
                    {
                        Note2 = Note1.Substring(70);
                        Note1 = Note1.Substring(0, 70);
                        if (Note2.Length > 70)
                        {
                            Note2 = Note2.Substring(0, 70);
                        }
                    }
                    e.Graphics.DrawString(Note1, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 39, 1180);
                    e.Graphics.DrawString(Note2, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 39, 1205);
                }
                if (CommentsTxt.Text != "")
                {
                    string Com1 = CommentsTxt.Text;
                    string Com2 = "";
                    if (Com1.Length > 70)
                    {
                        Com2 = Com1.Substring(70);
                        Com1 = Com1.Substring(0, 70);
                        if (Com2.Length > 70)
                        {
                            Com2 = Com2.Substring(0, 70);
                        }
                    }
                    e.Graphics.DrawString(Com1, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 39, 1271);
                    e.Graphics.DrawString(Com2, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 39, 1296);
                }
                e.Graphics.DrawString(chk.GrQuant(sumquant.ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 645, 1052);
                proditems = Convert.ToInt16(ProdItemsTxt.Text);
                sumquant = 0;
                pagecount = 1;
                GC.Collect();
            }
            e.HasMorePages = hasmorepages;
        }
                
        private void PrintPreviewChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PrintPreviewChkBox.Checked)
            {
                PrintChkBox.Checked = false;
            }
        }

        private void PrintChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PrintChkBox.Checked)
            {
                PrintPreviewChkBox.Checked = false;
            }
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
