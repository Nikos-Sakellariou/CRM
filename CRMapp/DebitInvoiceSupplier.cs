using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Drawing;
//using System.Data.Linq;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
//using System.Windows.Controls;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CRMapp
{

    public partial class DebitInvoiceSupplier : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        Dictionary<string, string> dc = new Dictionary<string, string>();
        Dictionary<string, string> dc2 = new Dictionary<string, string>();
        Checks chk = new Checks();

        public DebitInvoiceSupplier()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
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
                        string query = "select ProductId,Description,sum(CAST (Quant As decimal)) from Products a,ProductsReserve b where a.Id=b.ProductId and b.SupplierId=@supid group by ProductId,Description having sum(CAST (Quant As decimal))>0 order by ProductId,Description";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"supid", IdSupplierTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        ls.Clear();
                        dc.Clear();
                        dc2.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ls.Add(dt.Rows[i][1].ToString());
                            dc.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            dc2.Add(dt.Rows[i][0].ToString(), dt.Rows[i][2].ToString());
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
            System.Windows.Forms.TextBox ValueProdTxt = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox DiscProdTxt = new System.Windows.Forms.TextBox();
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
            ValueProdTxt.Name = "ValueProdTxt" + i;
            ValueProdTxt.Location = new Point(ValueProdTxt1.Location.X, ValueProdTxt1.Location.Y + 21 * (i - 1));
            ValueProdTxt.Size = ValueProdTxt1.Size;
            ValueProdTxt.Font = ValueProdTxt1.Font;
            DiscProdTxt.Name = "DiscProdTxt" + i;
            DiscProdTxt.Location = new Point(DiscProdTxt1.Location.X, DiscProdTxt1.Location.Y + 21 * (i - 1));
            DiscProdTxt.MaxLength = 5;
            DiscProdTxt.Size = DiscProdTxt1.Size;
            DiscProdTxt.Font = DiscProdTxt1.Font;
            this.ProductsPanel.Controls.Add(AaTxt);
            this.ProductsPanel.Controls.Add(IdProdTxt);
            this.ProductsPanel.Controls.Add(SearchNameListBox);
            this.ProductsPanel.Controls.Add(DescrProdTxt);
            this.ProductsPanel.Controls.Add(QuantProdTxt);
            this.ProductsPanel.Controls.Add(ValueProdTxt);
            this.ProductsPanel.Controls.Add(DiscProdTxt);
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
                ValueProdTxt.TextChanged += (object sender6, EventArgs e6)=>
                {
                    ValueProdTxt.Text = ValueProdTxt.Text.Replace(',', '.');
                    ValueProdTxt.SelectionStart = ValueProdTxt.Text.Length;
                };
                DiscProdTxt.TextChanged += (object sender7, EventArgs e7) =>
                {
                    DiscProdTxt.Text = DiscProdTxt.Text.Replace(',', '.');
                    DiscProdTxt.SelectionStart = DiscProdTxt.Text.Length;
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
                        }
                        else
                        {
                            DescrProdTxt.Text = "";
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
                                    }
                                }
                            }
                            else
                            {
                                IdProdTxt.Text = "";
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
                    this.PrevDebitSupplierTxt.Text = FindSupplier.ReturnSupplierDebit();
                    GetDataProd();
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
                    this.PrevDebitSupplierTxt.Text = AddSupplier.ReturnSupplierDebit();
                    GetDataProd();
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
                }
                else
                {
                    DescrProdTxt1.Text = "";
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
            this.ProductsPanel.Controls.Remove(Controls.Find("ValueProdTxt" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("DiscProdTxt" + i, true).First());
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
                            }
                        }
                    }
                    else
                    {
                        IdProdTxt1.Text = "";
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

       

        private void QuantProdTxt1_TextChanged(object sender, EventArgs e)
        {
            QuantProdTxt1.Text=QuantProdTxt1.Text.Replace(',', '.');
            QuantProdTxt1.SelectionStart = QuantProdTxt1.Text.Length;
        }

        private void ValueProdTxt1_TextChanged(object sender, EventArgs e)
        {
            ValueProdTxt1.Text = ValueProdTxt1.Text.Replace(',', '.');
            ValueProdTxt1.SelectionStart = ValueProdTxt1.Text.Length;
        }

        private void DiscProdTxt1_TextChanged(object sender, EventArgs e)
        {
            DiscProdTxt1.Text = DiscProdTxt1.Text.Replace(',', '.');
            DiscProdTxt1.SelectionStart = DiscProdTxt1.Text.Length;
        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            bool noerrors = true;
            string overcome = "";
            if (DiscInvoiceTxt.Text == "")
            {
                DiscInvoiceTxt.Text = "0";
            }
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
                }
                else
                {
                    ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                if (chk.CheckQuant(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) == "")
                {
                    ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().BackColor = SystemColors.Window;
                }
                else
                {
                    ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                if (ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text == "")
                {
                    ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().BackColor = SystemColors.Window;
                }
                else if (chk.CheckQuant(ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text) != "")
                {
                    ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                else if (Convert.ToDouble(ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text) <= 100)
                {
                    ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().BackColor = SystemColors.Window;
                }
                else
                {
                    ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                if (chk.CheckQuant(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) == "")
                {
                    string outcome;
                    dc2.TryGetValue(ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text, out outcome);
                    if (Convert.ToDecimal(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text)>Convert.ToDecimal(outcome))
                    {
                        ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = Color.DarkRed;
                        overcome += i+", ";
                    }
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
            else if (PayInvoiceCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε τρόπο πληρωμής του τιμολογίου.");
            }
            else if (DateTimeInvoicePicker.Value > System.DateTime.Today.AddDays(1))
            {
                MessageBox.Show("Η ημερομηνία δεν μπορεί να είναι μεταγενέστερη της σημερινής.");
            }
            else if (chk.CheckQuant(DiscInvoiceTxt.Text)!="" || Convert.ToDouble(DiscInvoiceTxt.Text)>100)
            {
                MessageBox.Show("Θα πρέπει να συμπληρώσετε ένα έγκυρο ποσοστό έκπτωσης πελάτη.");
            }
            else if (noerrors == false)
            {
                MessageBox.Show("Θα πρέπει να διορθώσετε τα στοιχεία των προϊόντων.");
            }
            else if (overcome != "")
            {
                MessageBox.Show("Τα προϊόντα με Α/Α: "+ overcome+" υπερβαίνουν το σύνολο των διαθέσιμων προϊόντων της αποθήκης.");
            }
            else
            {
                AddBtn.Visible = true;
                CorrectBtn.Visible = true;
                CalcBtn.Visible = false;
                ClearBtn.Visible = false;
                IdInvoiceTxt.ReadOnly = true;
                SeriesInvoiceTxt.ReadOnly = true;
                DiscInvoiceTxt.ReadOnly = true;
                PriceInvoiceTxt.Enabled = true;
                PayInvoiceCmb.Enabled = false;
                AddProdBtn1.Enabled = false;
                RemoveProdBtn1.Enabled = false;
                AddSupplierBtn.Enabled = false;
                SearchSupplierBtn.Enabled = false;
                double priceInv = 0;
                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                {
                    priceInv += Math.Round(Convert.ToDouble(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) * Convert.ToDouble(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) * (1-Convert.ToDouble((ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text == "" ? "0" : ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text))/100) * (1 - Convert.ToDouble(DiscInvoiceTxt.Text)/100)*1.24,2);
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First()).ReadOnly = true;
                }
                if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                {
                    AfterDebitSupplierTxt.Text = (-priceInv + Convert.ToDouble(PrevDebitSupplierTxt.Text)).ToString();
                }
                else
                {
                    AfterDebitSupplierTxt.Text = Convert.ToDouble(PrevDebitSupplierTxt.Text).ToString();
                }
                PriceInvoiceTxt.Value = Convert.ToDecimal(priceInv);
                PriceInvoiceTxt.Text = Convert.ToDecimal(priceInv).ToString();

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
            DiscInvoiceTxt.ReadOnly = false;
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            AddProdBtn1.Enabled = true;
            RemoveProdBtn1.Enabled = true;
            AddSupplierBtn.Enabled = true;
            SearchSupplierBtn.Enabled = true;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First()).ReadOnly = false;
            }
            PriceInvoiceTxt.Value = 0;
            PriceInvoiceTxt.Text = "";
            AfterDebitSupplierTxt.Text = "";
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
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from SupplierDebitInvoice where SupplierId=@supid and DebitInvoiceId=@invid and DebitInvoiceSeries=@invser", sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"supid", IdSupplierTxt.Text);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"invid", IdInvoiceTxt.Text);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"invser", SeriesInvoiceTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Υπάρχει ήδη καταχωρημένο τιμολόγιο με τον αριθμό, τη σειρά και τον προμηθευτή που έχετε εισάγει.");
                        }
                        else
                        {
                            DataTable dt2 = new DataTable();
                            if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                            {
                                string query = "select Debit from Suppliers where Id=@id";
                                SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query, sqlcon);
                                SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", IdSupplierTxt.Text);
                                SearchAdapt2.Fill(dt2);
                            }
                            SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                            SqlCommand InsCmd1 = sqlcon.CreateCommand();
                            InsCmd1.Connection = sqlcon;
                            SqlCommand InsCmd2 = sqlcon.CreateCommand();
                            InsCmd2.Connection = sqlcon;
                            InsCmd1.Transaction = InsTrans;
                            InsCmd2.Transaction = InsTrans;
                            try
                            {
                                InsCmd1.CommandText = "insert into SupplierDebitInvoice (Id, SupplierId, DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, DebitInvoicePayment, DebitInvoiceDisc, DebitInvoicePrice) values((select dbo.nvl(Max(Id)+1,0) from dbo.SupplierDebitInvoice), @supid, @invid, @ser, @date, @pay, @disc, @price)";
                                InsCmd1.Parameters.AddWithValue("@supid", IdSupplierTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@invid", IdInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@ser", SeriesInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                                InsCmd1.Parameters.AddWithValue("@pay", PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem));
                                InsCmd1.Parameters.AddWithValue("@disc", DiscInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@price", PriceInvoiceTxt.Text);
                                InsCmd1.ExecuteNonQuery();
                                
                                if (dt2.Rows.Count==1)
                                {
                                    decimal oldDebit = Convert.ToDecimal(dt2.Rows[0][0].ToString());
                                    decimal newDebit = oldDebit - Convert.ToDecimal(PriceInvoiceTxt.Text);
                                    InsCmd2.CommandText = "update Suppliers set Debit= @debit where Id=@id";
                                    InsCmd2.Parameters.AddWithValue("@debit", newDebit);
                                    InsCmd2.Parameters.AddWithValue("@id", IdSupplierTxt.Text);
                                    InsCmd2.ExecuteNonQuery();
                                }

                                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                                {
                                    SqlCommand InsCmd3 = sqlcon.CreateCommand();
                                    InsCmd3.Connection = sqlcon;
                                    SqlCommand InsCmd4 = new SqlCommand("DebitInv_ProdReserve_Proc");
                                    InsCmd4.Connection = sqlcon;
                                    InsCmd4.CommandType = CommandType.StoredProcedure;
                                    InsCmd3.Transaction = InsTrans;
                                    InsCmd4.Transaction = InsTrans;
                                    InsCmd3.CommandText = "insert into SupplierDebitInvoiceProducts (Id,SupplierDebitInvoiceId,ProductId,ProductQuant,ProductPrice,ProductDisc) values ((select dbo.nvl(Max(Id)+1,0) from SupplierDebitInvoiceProducts),(select dbo.nvl(Max(Id),0) from dbo.SupplierDebitInvoice),@prodid,@prodquant,@prodprice,@proddisc)";
                                    InsCmd3.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@prodquant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@prodprice", ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@proddisc", ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text);
                                    InsCmd3.ExecuteNonQuery();
                                    InsCmd4.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@quant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@price", ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@disc", (ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text=="" ? "0" : ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text));
                                    InsCmd4.Parameters.AddWithValue("@supplierid", IdSupplierTxt.Text);
                                    InsCmd4.Parameters.AddWithValue("@supplierdisc", DiscInvoiceTxt.Text);
                                    InsCmd4.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                                    InsCmd4.ExecuteNonQuery();
                                }
                                
                                InsTrans.Commit();
                                MessageBox.Show("Το πιστωτικό τιμολόγιο του προμηθευτή καταχωρήθηκε με επιτυχία.");
                                ClearValues();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πιστωτικού τιμολογίου του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                try
                                {
                                    InsTrans.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πιστωτικού τιμολογίου του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
            DiscInvoiceTxt.Text = "";
            PayInvoiceCmb.SelectedIndex = -1;
            PrevDebitSupplierTxt.Text = "";
            AfterDebitSupplierTxt.Text = "";
            PriceInvoiceTxt.Value = 0;
            PriceInvoiceTxt.Text = "";
            DateTimeInvoicePicker.Value = System.DateTime.Today;
            IdProdTxt1.Text = "";
            DescrProdTxt1.Text = "";
            QuantProdTxt1.Text = "";
            ValueProdTxt1.Text = "";
            DiscProdTxt1.Text = "";
            IdProdTxt1.ReadOnly = false;
            DescrProdTxt1.ReadOnly = false;
            QuantProdTxt1.ReadOnly = false;
            ValueProdTxt1.ReadOnly = false;
            DiscProdTxt1.ReadOnly = false;
            IdProdTxt1.BackColor = SystemColors.Window;
            DescrProdTxt1.BackColor = SystemColors.Window;
            QuantProdTxt1.BackColor = SystemColors.Window;
            ValueProdTxt1.BackColor = SystemColors.Window;
            DiscProdTxt1.BackColor = SystemColors.Window;
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
            DiscInvoiceTxt.ReadOnly = false;
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            this.ResumeLayout(false);
        }

        private void PriceInvoiceTxt_ValueChanged(object sender, EventArgs e)
        {
            if (PriceInvoiceTxt.Value != 0 && PriceInvoiceTxt.Text != "")
            {
                if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                {
                    AfterDebitSupplierTxt.Text = (Convert.ToDouble(PriceInvoiceTxt.Value) + Convert.ToDouble(PrevDebitSupplierTxt.Text)).ToString();
                }
                else
                {
                    AfterDebitSupplierTxt.Text = Convert.ToDouble(PrevDebitSupplierTxt.Text).ToString();
                }
            }
        }

        private void SearchNameListBox1_Leave(object sender, EventArgs e)
        {
            SearchNameListBox1.Visible = false;
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
