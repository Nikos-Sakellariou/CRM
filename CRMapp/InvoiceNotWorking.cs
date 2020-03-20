using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CRMapp
{

    public partial class InvoiceNotWorking : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        Dictionary<string,string> dc = new Dictionary<string,string>();
        Checks chk = new Checks();

        public InvoiceNotWorking()
        {
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\InvoiceDB.mdf;Integrated Security=True;Connect Timeout=30";
            con.UserID = "Sarkiris";
            con.Password = "dim1234";
            InitializeComponent();
            GetDataProd();
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
                        string query = "select Id,Description from Products order by Id,Description";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ls.Add(dt.Rows[i][1].ToString());
                            dc.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
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
            this.ProductsPanel.SendToBack();

            {
                ValueProdTxt.TextChanged += (object sender6, EventArgs e6)=>
                {
                    ValueProdTxt.Text = ValueProdTxt.Text.Replace(',', '.');
                    ValueProdTxt.SelectionStart = ValueProdTxt.Text.Length;
                    SumValueWorker_DoWork(null, null);
                };
                DiscProdTxt.TextChanged += (object sender7, EventArgs e7) =>
                {
                    DiscProdTxt.Text = DiscProdTxt.Text.Replace(',', '.');
                    DiscProdTxt.SelectionStart = DiscProdTxt.Text.Length;
                    SumValueWorker_DoWork(null, null);
                };
                QuantProdTxt.TextChanged += (object sender1, EventArgs e1) =>
                {
                    QuantProdTxt.Text = QuantProdTxt.Text.Replace(',', '.');
                    QuantProdTxt.SelectionStart = QuantProdTxt.Text.Length;
                    SumQuantWorker_DoWork(null, null);
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
                    if (DescrProdTxt.Focused)
                    {
                        if (DescrProdTxt.TextLength > 0)
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
                        else
                        {
                            SearchNameListBox.Visible = false;
                        }
                    }
                };

                DescrProdTxt.Leave += (object sender4, EventArgs e5) =>
                 {
                     if (SearchNameListBox.Focused != true)
                     {
                         SearchNameListBox.Visible = false;
                     }
                 };

                SearchNameListBox.Click += (object sender5, EventArgs e5) =>
                  {
                      DescrProdTxt.Focus();
                      DescrProdTxt.Text = SearchNameListBox.SelectedItem.ToString();
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
            DescrProdTxt1.Focus();
            DescrProdTxt1.Text = SearchNameListBox1.SelectedItem.ToString();
            SearchNameListBox1.Visible = false;
        }

        private void SumQuantWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            double sumquant = 0;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                if (chk.CheckQuant(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) == "")
                {
                    SumQuantInvoiceTxt.BackColor = SystemColors.Window;
                    ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = SystemColors.Window;
                    sumquant += Convert.ToDouble(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                }
                else
                {
                    ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = Color.Red;
                    SumQuantInvoiceTxt.BackColor = Color.Red;
                }
            }
            SumQuantInvoiceTxt.Text = sumquant.ToString();
        }

        private void QuantProdTxt1_TextChanged(object sender, EventArgs e)
        {
            QuantProdTxt1.Text=QuantProdTxt1.Text.Replace(',', '.');
            QuantProdTxt1.SelectionStart = QuantProdTxt1.Text.Length;
            SumQuantWorker_DoWork(null, null);
        }

        private void SumValueWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            double prevdiscsumvalue = 0;
            double afterdiscsumvalue = 0;
            double sumdisc = 0;
            double sumvat = 0;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                double disc = 0;
                if (chk.CheckQuant(ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text) == "")
                {
                    ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().BackColor = SystemColors.Window;
                    ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().BackColor = SystemColors.Window;
                    disc = Convert.ToDouble(ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text);
                }
                else
                {
                    if (ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text != "")
                    {
                        ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().BackColor = Color.Red;
                    }
                }
                if (chk.CheckQuant(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) == "")
                {
                    ValueInvoicePrevDiscTxt.BackColor = SystemColors.Window;
                    ValueInvoiceAfterDiscTxt.BackColor = SystemColors.Window;
                    DiscInvoiceTxt.BackColor = SystemColors.Window;
                    VatInvoiceTxt.BackColor = SystemColors.Window;
                    PriceInvoiceTxt.BackColor = SystemColors.Window;
                    ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().BackColor = SystemColors.Window;
                    prevdiscsumvalue += Convert.ToDouble(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                    afterdiscsumvalue += Math.Round(Convert.ToDouble(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) * (1 - disc / 100), 2);
                    sumdisc += prevdiscsumvalue - afterdiscsumvalue;
                    sumvat += Math.Round(afterdiscsumvalue * 24 / 100,2);
                }
                else
                {
                    ValueInvoicePrevDiscTxt.BackColor = Color.Red;
                    ValueInvoiceAfterDiscTxt.BackColor = Color.Red;
                    DiscInvoiceTxt.BackColor = Color.Red;
                    VatInvoiceTxt.BackColor = Color.Red;
                    PriceInvoiceTxt.BackColor = Color.Red;
                    ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().BackColor = Color.Red;
                }
            }
            ValueInvoicePrevDiscTxt.Text = prevdiscsumvalue.ToString();
            ValueInvoiceAfterDiscTxt.Text = afterdiscsumvalue.ToString();
            DiscInvoiceTxt.Text = sumdisc.ToString();
            VatInvoiceTxt.Text = sumvat.ToString();
            PriceInvoiceTxt.Text = (afterdiscsumvalue+sumvat).ToString();
            NextDebitSupplierTxt.Text = (Convert.ToDouble(PrevDebitSupplierTxt.Text) + (afterdiscsumvalue + sumvat)).ToString();
        }

        private void ValueProdTxt1_TextChanged(object sender, EventArgs e)
        {
            ValueProdTxt1.Text = ValueProdTxt1.Text.Replace(',', '.');
            ValueProdTxt1.SelectionStart = ValueProdTxt1.Text.Length;
            SumValueWorker_DoWork(null, null);
        }

        private void DiscProdTxt1_TextChanged(object sender, EventArgs e)
        {
            DiscProdTxt1.Text = DiscProdTxt1.Text.Replace(',', '.');
            DiscProdTxt1.SelectionStart = DiscProdTxt1.Text.Length;
            SumValueWorker_DoWork(null, null);
        }
    }
}
