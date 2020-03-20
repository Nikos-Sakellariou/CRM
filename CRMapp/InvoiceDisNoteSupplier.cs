using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class InvoiceDisNoteSupplier : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        List<string> DisLs = new List<string>();
        Checks chk = new Checks();

        public InvoiceDisNoteSupplier()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            DateTimeInvoicePicker.Value = System.DateTime.Today.AddDays(2);
        }
        
        void FormClose(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }
        
        private void FillProducts(List<string> list)
        {
            this.SuspendLayout();
            ClearProds();
            int j = 0;
            foreach (var item in list)
            {
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        try
                        {
                            string query = "select DisNoteId+' - '+DisNoteSeries,ProductId,SupplierDescr,ProductQuant from SupplierDisNote a,SupplierDisNoteProducts b, Products c where a.Id=b.SupplierDisNoteId and b.ProductId=c.Id and a.Id=@id";
                            SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", item.ToString());
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                j++;
                                this.ProductsPanel.Height += 21;
                                this.ProdPanel.Height += 21;
                                this.Height += 21;
                                ProdItemsTxt.Text = j.ToString();
                                if (j == 1)
                                {

                                    DisNoteProdTxt1.Text = dt.Rows[i][0].ToString();
                                    IdProdTxt1.Text = dt.Rows[i][1].ToString();
                                    DescrProdTxt1.Text = dt.Rows[i][2].ToString();
                                    QuantProdTxt1.Text = dt.Rows[i][3].ToString();
                                    DisNoteProdTxt1.Visible = true;
                                    IdProdTxt1.Visible = true;
                                    DescrProdTxt1.Visible = true;
                                    QuantProdTxt1.Visible = true;
                                    ValueProdTxt1.Visible = true;
                                    DiscProd1Txt1.Visible = true;
                                    DiscProd2Txt1.Visible = true;
                                    DiscProd3Txt1.Visible = true;
                                    SalesPriceTxt1.Visible = true;
                                    SalesDiscTxt1.Visible = true;
                                    AaLabel.Visible = true;
                                    AaTxt1.Visible = true;
                                    DisNoteProdLbl.Visible = true;
                                    IdProdLbl.Visible = true;
                                    DescrProdLbl.Visible = true;
                                    QuantProdLbl.Visible = true;
                                    ValueProdLbl.Visible = true;
                                    DiscProd1Lbl.Visible = true;
                                    DiscProd2Lbl.Visible = true;
                                    DiscProd3Lbl.Visible = true;
                                    SalesPriceLbl.Visible = true;
                                    SalesDiscLbl.Visible = true;
                                }
                                else
                                {
                                    System.Windows.Forms.Label AaTxt = new System.Windows.Forms.Label();
                                    System.Windows.Forms.TextBox DisNoteProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox IdProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox DescrProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox QuantProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox ValueProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox DiscProd1Txt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox DiscProd2Txt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox DiscProd3Txt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox SalesPriceTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox SalesDiscTxt = new System.Windows.Forms.TextBox();
                                    AaTxt.Text = j.ToString();
                                    AaTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                                    AaTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    AaTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                                    AaTxt.Name = "AaTxt" + j;
                                    AaTxt.Location = new Point(AaTxt1.Location.X, AaTxt1.Location.Y + 21 * (j - 1));
                                    AaTxt.Size = AaTxt1.Size;
                                    AaTxt.Font = AaTxt1.Font;
                                    DisNoteProdTxt.Name = "DisNoteProdTxt" + j;
                                    DisNoteProdTxt.Text = dt.Rows[i][0].ToString();
                                    DisNoteProdTxt.ReadOnly = true;
                                    DisNoteProdTxt.Location = new Point(DisNoteProdTxt1.Location.X, DisNoteProdTxt1.Location.Y + 21 * (j - 1));
                                    DisNoteProdTxt.Size = DisNoteProdTxt1.Size;
                                    DisNoteProdTxt.Font = DisNoteProdTxt1.Font;
                                    IdProdTxt.Name = "IdProdTxt" + j;
                                    IdProdTxt.Text = dt.Rows[i][1].ToString();
                                    IdProdTxt.ReadOnly = true;
                                    IdProdTxt.Location = new Point(IdProdTxt1.Location.X, IdProdTxt1.Location.Y + 21 * (j - 1));
                                    IdProdTxt.Size = IdProdTxt1.Size;
                                    IdProdTxt.Font = IdProdTxt1.Font;
                                    DescrProdTxt.Name = "DescrProdTxt" + j;
                                    DescrProdTxt.Text = dt.Rows[i][2].ToString();
                                    DescrProdTxt.ReadOnly = true;
                                    DescrProdTxt.Location = new Point(DescrProdTxt1.Location.X, DescrProdTxt1.Location.Y + 21 * (j - 1));
                                    DescrProdTxt.Size = DescrProdTxt1.Size;
                                    DescrProdTxt.Font = DescrProdTxt1.Font;
                                    QuantProdTxt.Name = "QuantProdTxt" + j;
                                    QuantProdTxt.Text = dt.Rows[i][3].ToString();
                                    QuantProdTxt.ReadOnly = true;
                                    QuantProdTxt.Location = new Point(QuantProdTxt1.Location.X, QuantProdTxt1.Location.Y + 21 * (j - 1));
                                    QuantProdTxt.Size = QuantProdTxt1.Size;
                                    QuantProdTxt.Font = QuantProdTxt1.Font;
                                    ValueProdTxt.Name = "ValueProdTxt" + j;
                                    ValueProdTxt.Location = new Point(ValueProdTxt1.Location.X, ValueProdTxt1.Location.Y + 21 * (j - 1));
                                    ValueProdTxt.Size = ValueProdTxt1.Size;
                                    ValueProdTxt.Font = ValueProdTxt1.Font;
                                    DiscProd1Txt.Name = "DiscProd1Txt" + j;
                                    DiscProd1Txt.Location = new Point(DiscProd1Txt1.Location.X, DiscProd1Txt1.Location.Y + 21 * (j - 1));
                                    DiscProd1Txt.MaxLength = 5;
                                    DiscProd1Txt.Size = DiscProd1Txt1.Size;
                                    DiscProd1Txt.Font = DiscProd1Txt1.Font;
                                    DiscProd2Txt.Name = "DiscProd2Txt" + j;
                                    DiscProd2Txt.Location = new Point(DiscProd2Txt1.Location.X, DiscProd2Txt1.Location.Y + 21 * (j - 1));
                                    DiscProd2Txt.MaxLength = 5;
                                    DiscProd2Txt.Size = DiscProd2Txt1.Size;
                                    DiscProd2Txt.Font = DiscProd2Txt1.Font;
                                    DiscProd3Txt.Name = "DiscProd3Txt" + j;
                                    DiscProd3Txt.Location = new Point(DiscProd3Txt1.Location.X, DiscProd3Txt1.Location.Y + 21 * (j - 1));
                                    DiscProd3Txt.MaxLength = 5;
                                    DiscProd3Txt.Size = DiscProd3Txt1.Size;
                                    DiscProd3Txt.Font = DiscProd3Txt1.Font;
                                    SalesPriceTxt.Name = "SalesPriceTxt" + j;
                                    SalesPriceTxt.Location = new Point(SalesPriceTxt1.Location.X, SalesPriceTxt1.Location.Y + 21 * (j - 1));
                                    SalesPriceTxt.MaxLength = 18;
                                    SalesPriceTxt.Size = SalesPriceTxt1.Size;
                                    SalesPriceTxt.Font = SalesPriceTxt1.Font;
                                    SalesDiscTxt.Name = "SalesDiscTxt" + j;
                                    SalesDiscTxt.Location = new Point(SalesDiscTxt1.Location.X, SalesDiscTxt1.Location.Y + 21 * (j - 1));
                                    SalesDiscTxt.MaxLength = 18;
                                    SalesDiscTxt.Size = SalesDiscTxt1.Size;
                                    SalesDiscTxt.Font = SalesDiscTxt1.Font;
                                    this.ProductsPanel.Controls.Add(AaTxt);
                                    this.ProductsPanel.Controls.Add(DisNoteProdTxt);
                                    this.ProductsPanel.Controls.Add(IdProdTxt);
                                    this.ProductsPanel.Controls.Add(DescrProdTxt);
                                    this.ProductsPanel.Controls.Add(QuantProdTxt);
                                    this.ProductsPanel.Controls.Add(ValueProdTxt);
                                    this.ProductsPanel.Controls.Add(DiscProd1Txt);
                                    this.ProductsPanel.Controls.Add(DiscProd2Txt);
                                    this.ProductsPanel.Controls.Add(DiscProd3Txt);
                                    this.ProductsPanel.Controls.Add(SalesPriceTxt);
                                    this.ProductsPanel.Controls.Add(SalesDiscTxt);
                                    this.ProdPanel.SendToBack();
                                    {
                                        ValueProdTxt.TextChanged += (object sender6, EventArgs e6) =>
                                        {
                                            ValueProdTxt.Text = ValueProdTxt.Text.Replace(',', '.');
                                            ValueProdTxt.SelectionStart = ValueProdTxt.Text.Length;
                                        };
                                        DiscProd1Txt.TextChanged += (object sender7, EventArgs e7) =>
                                        {
                                            DiscProd1Txt.Text = DiscProd1Txt.Text.Replace(',', '.');
                                            DiscProd1Txt.SelectionStart = DiscProd1Txt.Text.Length;
                                        };
                                        DiscProd2Txt.TextChanged += (object sender8, EventArgs e8) =>
                                        {
                                            DiscProd2Txt.Text = DiscProd2Txt.Text.Replace(',', '.');
                                            DiscProd2Txt.SelectionStart = DiscProd2Txt.Text.Length;
                                        };
                                        DiscProd3Txt.TextChanged += (object sender9, EventArgs e9) =>
                                        {
                                            DiscProd3Txt.Text = DiscProd3Txt.Text.Replace(',', '.');
                                            DiscProd3Txt.SelectionStart = DiscProd3Txt.Text.Length;
                                        };

                                    }
                                }
                                this.ResumeLayout(false);
                                
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση των προϊόντων των Δελτίων Αποστολής. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                        }
                        sqlcon.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                }
                
                
            }
        }

        private void SearchSupplierBtn_Click(object sender, EventArgs e)
        {
            OrderFindSupplier FindSupplier = new OrderFindSupplier();
            FindSupplier.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindSupplier.ReturnSupplierId()!=null)
                {
                    ClearValues();
                    if (Convert.ToInt16(ProdItemsTxt.Text.ToString())>1)
                    {
                        ClearProds();
                    }
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
                    ClearValues();
                    if (Convert.ToInt16(ProdItemsTxt.Text.ToString()) > 1)
                    {
                        ClearProds();
                    }
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
                
        private void ValueProdTxt1_TextChanged(object sender, EventArgs e)
        {
            ValueProdTxt1.Text = ValueProdTxt1.Text.Replace(',', '.');
            ValueProdTxt1.SelectionStart = ValueProdTxt1.Text.Length;
        }
        
        private void CalcBtn_Click(object sender, EventArgs e)
        {
            bool noerrors = true;
            if (DiscInvoiceTxt.Text == "")
            {
                DiscInvoiceTxt.Text = "0";
            }
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                if (chk.CheckQuant(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) == "")
                {
                    ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().BackColor = SystemColors.Window;
                }
                else
                {
                    ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                if (ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text == "")
                {
                    ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text = "0";
                    ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().BackColor = SystemColors.Window;
                }
                else if (chk.CheckQuant(ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text) != "")
                {
                    ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                else if (Convert.ToDouble(ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text) <= 100)
                {
                    ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().BackColor = SystemColors.Window;
                }
                else
                {
                    ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                if (ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().Text == "")
                {
                    ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().Text = "0";
                    ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().BackColor = SystemColors.Window;
                }
                else if (chk.CheckQuant(ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().Text) != "")
                {
                    ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                else if (Convert.ToDouble(ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().Text) <= 100)
                {
                    ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().BackColor = SystemColors.Window;
                }
                else
                {
                    ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                if (ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().Text == "")
                {
                    ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().Text = "0";
                    ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().BackColor = SystemColors.Window;
                }
                else if (chk.CheckQuant(ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().Text) != "")
                {
                    ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                else if (Convert.ToDouble(ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().Text) <= 100)
                {
                    ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().BackColor = SystemColors.Window;
                }
                else
                {
                    ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
            }
            if (IdSupplierTxt.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε προμηθευτή.");
            }
            else if (IdProdTxt1.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον Δελτίο Αποστολής.");
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
            else if (noerrors==false)
            {
                MessageBox.Show("Θα πρέπει να διορθώσετε τα στοιχεία των προϊόντων.");
            }
            else
            {
                DisLs.Clear();
                AddBtn.Visible = true;
                CorrectBtn.Visible = true;
                CalcBtn.Visible = false;
                ClearBtn.Visible = false;
                IdInvoiceTxt.ReadOnly = true;
                SeriesInvoiceTxt.ReadOnly = true;
                DiscInvoiceTxt.ReadOnly = true;
                PriceInvoiceTxt.Enabled = true;
                PayInvoiceCmb.Enabled = false;
                DisNoteBtn.Enabled = false;
                AddSupplierBtn.Enabled = false;
                SearchSupplierBtn.Enabled = false;
                double priceInv = 0;

                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        try
                        {
                            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                            {
                                priceInv += Math.Round(Convert.ToDouble(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) * Convert.ToDouble(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) * (1 - Convert.ToDouble((ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text == "" ? "0" : ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text)) / 100) * (1 - Convert.ToDouble(DiscInvoiceTxt.Text) / 100) * 1.24, 2);
                                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First()).ReadOnly = true;
                                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First()).ReadOnly = true;
                                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First()).ReadOnly = true;
                                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First()).ReadOnly = true;

                                SqlDataAdapter SearchAdapt = new SqlDataAdapter("select dbo.ProfitNewPrice(@value,@disc1,@disc2,@disc3,(select ProfitPerc from Products where Id=@id)),dbo.ProfitNewDisc(@value,@disc1,@disc2,@disc3,(select ProfitPerc from Products where Id=@id))", sqlcon);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"value", ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"disc1", ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text == "" ? "0" : ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"disc2", ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().Text == "" ? "0" : ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().Text);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"disc3", ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().Text == "" ? "0" : ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().Text);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                DataTable dt = new DataTable();
                                SearchAdapt.Fill(dt);
                                if (dt.Rows.Count == 1)
                                {
                                    ProductsPanel.Controls.Find("SalesPriceTxt" + i, true).First().Text = dt.Rows[0][0].ToString();
                                    ProductsPanel.Controls.Find("SalesDiscTxt" + i, true).First().Text = dt.Rows[0][1].ToString();
                                }
                                if (DisLs.Contains(ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text) == false)
                                {
                                    DisLs.Add(ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text);
                                }
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
                if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                {
                    AfterDebitSupplierTxt.Text = (priceInv + Convert.ToDouble(PrevDebitSupplierTxt.Text)).ToString();
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
            DisNoteBtn.Enabled = true;
            AddSupplierBtn.Enabled = true;
            SearchSupplierBtn.Enabled = true;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First()).ReadOnly = false;
                ProductsPanel.Controls.Find("SalesPriceTxt" + i, true).First().Text = "";
                ProductsPanel.Controls.Find("SalesDiscTxt" + i, true).First().Text = "";
            }
            PriceInvoiceTxt.Value = 0;
            PriceInvoiceTxt.Text = "";
            AfterDebitSupplierTxt.Text = "";
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearValues();
            ClearProds();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            bool noerrors = true;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                if (chk.CheckPrice(ProductsPanel.Controls.Find("SalesPriceTxt" + i, true).First().Text) != "")
                {
                    ProductsPanel.Controls.Find("SalesPriceTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                else
                {
                    ProductsPanel.Controls.Find("SalesPriceTxt" + i, true).First().BackColor = SystemColors.Window;
                    noerrors = true;
                }

                if (chk.CheckPrice(ProductsPanel.Controls.Find("SalesDiscTxt" + i, true).First().Text) != "")
                {
                    ProductsPanel.Controls.Find("SalesDiscTxt" + i, true).First().BackColor = Color.Red;
                    noerrors = false;
                }
                else
                {
                    ProductsPanel.Controls.Find("SalesDiscTxt" + i, true).First().BackColor = SystemColors.Window;
                    noerrors = true;
                }
            }
            if (noerrors == false)
            {
                MessageBox.Show("Θα πρέπει να διορθώσετε τα στοιχεία πώλησης.");
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from SupplierInvoice where SupplierId=@supid and InvoiceId=@invid and InvoiceSeries=@invser", sqlcon);
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
                                string DisNotes = "";
                                foreach (var item in DisLs)
                                {
                                    if (DisNotes == "")
                                    {
                                        DisNotes += item;
                                    }
                                    else
                                    {
                                        DisNotes += ", " + item;
                                    }
                                }
                                foreach (var item in ls)
                                {
                                    SqlCommand InsCmd5 = sqlcon.CreateCommand();
                                    InsCmd5.Connection = sqlcon;
                                    InsCmd5.Transaction = InsTrans;
                                    InsCmd5.CommandText = "update SupplierDisNote set Invoice='1' where Id=@id";
                                    InsCmd5.Parameters.AddWithValue("@id", item);
                                    InsCmd5.ExecuteNonQuery();
                                }
                                InsCmd1.CommandText = "insert into SupplierInvoice (Id, SupplierId, InvoiceId, InvoiceSeries, InvoiceDate, InvoicePayment, InvoiceDisc, InvoicePrice,InvoiceDisNotes) values((select dbo.nvl(Max(Id)+1,0) from dbo.SupplierInvoice), @supid, @invid, @ser, @date, @pay, @disc, @price,@note)";
                                InsCmd1.Parameters.AddWithValue("@supid", IdSupplierTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@invid", IdInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@ser", SeriesInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                                InsCmd1.Parameters.AddWithValue("@pay", PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem));
                                InsCmd1.Parameters.AddWithValue("@disc", DiscInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@price", PriceInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@note", DisNotes);
                                InsCmd1.ExecuteNonQuery();

                                if (dt2.Rows.Count == 1)
                                {
                                    decimal oldDebit = Convert.ToDecimal(dt2.Rows[0][0].ToString());
                                    decimal newDebit = oldDebit + Convert.ToDecimal(PriceInvoiceTxt.Text);
                                    InsCmd2.CommandText = "update Suppliers set Debit= @debit where Id=@id";
                                    InsCmd2.Parameters.AddWithValue("@debit", newDebit);
                                    InsCmd2.Parameters.AddWithValue("@id", IdSupplierTxt.Text);
                                    InsCmd2.ExecuteNonQuery();
                                }

                                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                                {
                                    decimal DiscProd = 100-Math.Round((1 - Convert.ToDecimal(ProductsPanel.Controls.Find("DiscProd1Txt" + i, true).First().Text) / 100) * (1 - Convert.ToDecimal(ProductsPanel.Controls.Find("DiscProd2Txt" + i, true).First().Text) / 100) * (1 - Convert.ToDecimal(ProductsPanel.Controls.Find("DiscProd3Txt" + i, true).First().Text) / 100) * 100, 2);
                                    SqlCommand InsCmd3 = sqlcon.CreateCommand();
                                    InsCmd3.Connection = sqlcon;
                                    SqlCommand InsCmd4 = sqlcon.CreateCommand();
                                    InsCmd4.Connection = sqlcon;
                                    InsCmd3.Transaction = InsTrans;
                                    InsCmd4.Transaction = InsTrans;
                                    InsCmd3.CommandText = "insert into SupplierInvoiceProducts (Id,SupplierInvoiceId,ProductId,ProductQuant,ProductPrice,ProductDisc) values ((select dbo.nvl(Max(Id)+1,0) from SupplierInvoiceProducts),(select dbo.nvl(Max(Id),0) from dbo.SupplierInvoice),@prodid,@prodquant,@prodprice,@proddisc)";
                                    InsCmd3.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@prodquant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@prodprice", ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@proddisc", DiscProd);
                                    InsCmd3.ExecuteNonQuery();
                                    InsCmd4.CommandText = "update ProductsReserve set Price=@prodprice, Disc=@proddisc, SupplierDisc=@supdisc,SalesPrice=@sprice, SalesDisc=@sdisc where DocId=@id and ProductId=@prodid and SupplierId=@supid";
                                    InsCmd4.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@prodprice", ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@proddisc", DiscProd);
                                    InsCmd4.Parameters.AddWithValue("@sprice", ProductsPanel.Controls.Find("SalesPriceTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@sdisc", ProductsPanel.Controls.Find("SalesDiscTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@supdisc", DiscInvoiceTxt.Text);
                                    InsCmd4.Parameters.AddWithValue("@id", ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@supid", IdSupplierTxt.Text);
                                    InsCmd4.ExecuteNonQuery();
                                }

                                InsTrans.Commit();
                                MessageBox.Show("Το τιμολόγιο του προμηθευτή καταχωρήθηκε με επιτυχία.");
                                this.SuspendLayout();
                                ClearProds();
                                ClearValues();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του τιμολογίου του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                try
                                {
                                    InsTrans.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του τιμολογίου του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                }
                            }
                        }
                        sqlcon.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    this.ResumeLayout(false);
                }
            }
        }

        private void ClearProds()
        {

            this.SuspendLayout();
            DisNoteProdTxt1.Visible = false;
            IdProdTxt1.Visible = false;
            DescrProdTxt1.Visible = false;
            QuantProdTxt1.Visible = false;
            ValueProdTxt1.Visible = false;
            DiscProd1Txt1.Visible = false;
            DiscProd2Txt1.Visible = false;
            DiscProd3Txt1.Visible = false;
            SalesPriceTxt1.Visible = false;
            SalesDiscTxt1.Visible = false;
            AaLabel.Visible = false;
            AaTxt1.Visible = false;
            DisNoteProdLbl.Visible = false;
            IdProdLbl.Visible = false;
            DescrProdLbl.Visible = false;
            QuantProdLbl.Visible = false;
            ValueProdLbl.Visible = false;
            DiscProd1Lbl.Visible = false;
            DiscProd2Lbl.Visible = false;
            DiscProd3Lbl.Visible = false;
            SalesPriceLbl.Visible = false;
            SalesDiscLbl.Visible = false;
            IdProdTxt1.BackColor = SystemColors.Window;
            DescrProdTxt1.BackColor = SystemColors.Window;
            QuantProdTxt1.BackColor = SystemColors.Window;
            ValueProdTxt1.BackColor = SystemColors.Window;
            DiscProd1Txt1.BackColor = SystemColors.Window;
            DiscProd2Txt1.BackColor = SystemColors.Window;
            DiscProd3Txt1.BackColor = SystemColors.Window;
            SalesPriceTxt1.BackColor = SystemColors.Window;
            SalesDiscTxt1.BackColor = SystemColors.Window;
            this.ProdPanel.Height = 68;
            this.ProductsPanel.Height = 286;
            this.Height = 400;
            if (Convert.ToInt16(ProdItemsTxt.Text)>=2)
            {
                for (int r = 2; r <= Convert.ToInt16(ProdItemsTxt.Text); r++)
                {
                    this.ProductsPanel.Controls.Remove(Controls.Find("AaTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("DisNoteProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("IdProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("DescrProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("QuantProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("ValueProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("DiscProd1Txt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("DiscProd2Txt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("DiscProd3Txt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("SalesPriceTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("SalesDiscTxt" + r, true).First());
                }
                ProdItemsTxt.Text = "1";
            }
            this.ResumeLayout(false);

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
            DateTimeInvoicePicker.Value = System.DateTime.Today.AddDays(2);
            IdProdTxt1.Text = "";
            DescrProdTxt1.Text = "";
            QuantProdTxt1.Text = "";
            ValueProdTxt1.Text = "";
            DiscProd1Txt1.Text = "";
            DiscProd2Txt1.Text = "";
            DiscProd3Txt1.Text = "";
            SalesPriceTxt1.Text = "";
            SalesDiscTxt1.Text = "";
            ValueProdTxt1.ReadOnly = false;
            DiscProd1Txt1.ReadOnly = false;
            DiscProd2Txt1.ReadOnly = false;
            DiscProd3Txt1.ReadOnly = false;
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            IdInvoiceTxt.ReadOnly = false;
            SeriesInvoiceTxt.ReadOnly = false;
            DiscInvoiceTxt.ReadOnly = false;
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            DisNoteBtn.Enabled = true;
            AddSupplierBtn.Enabled = true;
            SearchSupplierBtn.Enabled = true;
            this.ResumeLayout(false);
        }

        private void DisNoteBtn_Click(object sender, EventArgs e)
        {
            OrderFindDisNoteSupplier FindDisNote = new OrderFindDisNoteSupplier(IdSupplierTxt.Text);
            FindDisNote.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindDisNote.ReturnDisNoteIds() != null)
                {
                    ls.Clear();
                    ls = FindDisNote.ReturnDisNoteIds();
                    if (ls.Count > 0)
                    {
                        FillProducts(FindDisNote.ReturnDisNoteIds());
                    }
                }
            };
            FindDisNote.FormClosed += new FormClosedEventHandler(FormClose);
            this.Enabled = false;
            FindDisNote.ShowDialog();
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

        private void DiscProd1Txt1_TextChanged(object sender, EventArgs e)
        {
            DiscProd1Txt1.Text = DiscProd1Txt1.Text.Replace(',', '.');
            DiscProd1Txt1.SelectionStart = DiscProd1Txt1.Text.Length;
        }

        private void DiscProd2Txt1_TextChanged(object sender, EventArgs e)
        {
            DiscProd2Txt1.Text = DiscProd2Txt1.Text.Replace(',', '.');
            DiscProd2Txt1.SelectionStart = DiscProd2Txt1.Text.Length;
        }

        private void DiscProd3Txt1_TextChanged(object sender, EventArgs e)
        {
            DiscProd3Txt1.Text = DiscProd3Txt1.Text.Replace(',', '.');
            DiscProd3Txt1.SelectionStart = DiscProd3Txt1.Text.Length;
        }
    }
}
