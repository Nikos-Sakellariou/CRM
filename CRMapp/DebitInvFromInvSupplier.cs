using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class DebitInvFromInvSupplier : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        List<string> ls2 = new List<string>();
        List<string> supdisls = new List<string>();
        List<string> DisLs = new List<string>();
        Dictionary<string, string> dc2 = new Dictionary<string, string>();
        Dictionary<string, string> QuantChk = new Dictionary<string, string>();
        List<string> Chkd = new List<string>();
        Checks chk = new Checks();
        string InvPrice="0";

        public DebitInvFromInvSupplier()
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
                            string query2 = "select ProductId,sum(CAST (Quant As decimal)) from Products a,ProductsReserve b where a.Id=b.ProductId and b.SupplierId=@supid group by ProductId,SupplierDescr order by ProductId,SupplierDescr";
                            SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                            SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"supid", IdSupplierTxt.Text);
                            DataTable dt2 = new DataTable();
                            SearchAdapt2.Fill(dt2);
                            dc2.Clear();
                            QuantChk.Clear();
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                dc2.Add(dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString());
                            }
                            string query = "select InvoiceId+'/'+InvoiceSeries,ProductId,SupplierDescr,ProductQuant,ProductPrice,ProductDisc,InvoicePrice,InvoiceDisc from SupplierInvoice a,SupplierInvoiceProducts b, Products c where a.Id=b.SupplierInvoiceId and b.ProductId=c.Id and a.Id=@id";
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
                                    QuantChk.Add("1",dt.Rows[i][3].ToString());
                                    ValueProdTxt1.Text = dt.Rows[i][4].ToString();
                                    DiscProdTxt1.Text = dt.Rows[i][5].ToString();
                                    InvPrice = dt.Rows[i][6].ToString();
                                    DiscInvoiceTxt.Text = dt.Rows[i][7].ToString();
                                    DisNoteProdTxt1.Visible = true;
                                    IdProdTxt1.Visible = true;
                                    DescrProdTxt1.Visible = true;
                                    QuantProdTxt1.Visible = true;
                                    ValueProdTxt1.Visible = true;
                                    DiscProdTxt1.Visible = true;
                                    ChkBx1.Visible = true;
                                    AaLabel.Visible = true;
                                    AaTxt1.Visible = true;
                                    DisNoteProdLbl.Visible = true;
                                    IdProdLbl.Visible = true;
                                    DescrProdLbl.Visible = true;
                                    QuantProdLbl.Visible = true;
                                    ValueProdLbl.Visible = true;
                                    DiscProdLbl.Visible = true;
                                }
                                else
                                {
                                    System.Windows.Forms.Label AaTxt = new System.Windows.Forms.Label();
                                    System.Windows.Forms.TextBox DisNoteProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox IdProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox DescrProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox QuantProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox ValueProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox DiscProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.CheckBox ChkBx = new System.Windows.Forms.CheckBox();
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
                                    QuantChk.Add(j.ToString(), dt.Rows[i][3].ToString());
                                    QuantProdTxt.Location = new Point(QuantProdTxt1.Location.X, QuantProdTxt1.Location.Y + 21 * (j - 1));
                                    QuantProdTxt.Size = QuantProdTxt1.Size;
                                    QuantProdTxt.Font = QuantProdTxt1.Font;
                                    ValueProdTxt.Name = "ValueProdTxt" + j;
                                    ValueProdTxt.Text = dt.Rows[i][4].ToString();
                                    ValueProdTxt.ReadOnly = true;
                                    ValueProdTxt.Location = new Point(ValueProdTxt1.Location.X, ValueProdTxt1.Location.Y + 21 * (j - 1));
                                    ValueProdTxt.Size = ValueProdTxt1.Size;
                                    ValueProdTxt.Font = ValueProdTxt1.Font;
                                    DiscProdTxt.Name = "DiscProdTxt" + j;
                                    DiscProdTxt.Text = dt.Rows[i][5].ToString();
                                    DiscProdTxt.ReadOnly = true;
                                    DiscProdTxt.Location = new Point(DiscProdTxt1.Location.X, DiscProdTxt1.Location.Y + 21 * (j - 1));
                                    DiscProdTxt.MaxLength = 5;
                                    DiscProdTxt.Size = DiscProdTxt1.Size;
                                    DiscProdTxt.Font = DiscProdTxt1.Font;
                                    ChkBx.Name = "ChkBx" + j;
                                    ChkBx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                                    ChkBx.Location = new Point(ChkBx1.Location.X, ChkBx1.Location.Y + 21 * (j - 1));
                                    this.ProductsPanel.Controls.Add(AaTxt);
                                    this.ProductsPanel.Controls.Add(DisNoteProdTxt);
                                    this.ProductsPanel.Controls.Add(IdProdTxt);
                                    this.ProductsPanel.Controls.Add(DescrProdTxt);
                                    this.ProductsPanel.Controls.Add(QuantProdTxt);
                                    this.ProductsPanel.Controls.Add(ValueProdTxt);
                                    this.ProductsPanel.Controls.Add(DiscProdTxt);
                                    this.ProductsPanel.Controls.Add(ChkBx);
                                    this.ProdPanel.SendToBack();
                                }
                                this.ResumeLayout(false);
                                
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση των προϊόντων των Τιμολογίων. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            string overcome = "";
            string overcome2 = "";
            string storageAvail = "";
            bool noselection = false;
            Chkd.Clear();
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                CheckBox Chk = new CheckBox();
                Chk = (CheckBox)ProductsPanel.Controls.Find("ChkBx" + i, true).First();
                if (Chk.Checked== true)
                {
                    noselection = true;
                    Chkd.Add(i.ToString());
                    if (chk.CheckQuant(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) == "")
                    {
                        string outcome;
                        dc2.TryGetValue(ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text, out outcome);
                        if (Convert.ToDecimal(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) > Convert.ToDecimal(outcome))
                        {
                            ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = Color.DarkRed;
                            overcome += i + ", ";
                            storageAvail += "A/A " + i + " :   " + outcome + "\n";
                        }
                        string outcome2;
                        QuantChk.TryGetValue(i.ToString(), out outcome2);
                        if (Convert.ToDecimal(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) > Convert.ToDecimal(outcome2))
                        {
                            ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = Color.DarkRed;
                            overcome2 += i + ", ";
                        }

                    }

                }
                
            }
            if (DiscInvoiceTxt.Text == "")
            {
                DiscInvoiceTxt.Text = "0";
            }
            if (IdSupplierTxt.Text == "")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε προμηθευτή.");
            }
            else if (ls2.Count()==0)
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον Δελτίο Αποστολής.");
            }
            else if (IdProdTxt1.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον τιμολόγιο.");
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
            else if (overcome != "")
            {
                System.Windows.MessageBox.Show("Τα προϊόντα με Α/Α: " + overcome + " υπερβαίνουν το σύνολο των διαθέσιμων προϊόντων της αποθήκης.\n Διαθέσιμα Αποθήκης \n" + storageAvail);
            }
            else if (overcome2 != "")
            {
                MessageBox.Show("Τα προϊόντα με Α/Α: " + overcome2 + " υπερβαίνουν την αρχική ποσότητα του Τιμολογίου.");
            }
            else if (noselection == false)
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον προϊόν.");
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
                PriceInvoiceTxt.Enabled = true;
                PayInvoiceCmb.Enabled = false;
                InvoiceBtn.Enabled = false;
                DisNoteBtn.Enabled = false;
                AddSupplierBtn.Enabled = false;
                SearchSupplierBtn.Enabled = false;
                double priceInv = 0;
                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                {
                    if (Chkd.Contains(i.ToString()))
                    {
                        priceInv += Math.Round(Convert.ToDouble(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) * Convert.ToDouble(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) * (1 - Convert.ToDouble((ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text == "" ? "0" : ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text)) / 100) * (1 - Convert.ToDouble(DiscInvoiceTxt.Text) / 100) * 1.24, 2);
                        ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = true;
                        ((System.Windows.Forms.CheckBox)ProductsPanel.Controls.Find("ChkBx" + i, true).First()).Enabled = false;
                        if (DisLs.Contains(ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text)==false)
                        {
                            DisLs.Add(ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text);
                        }
                    }
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
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            InvoiceBtn.Enabled = true;
            DisNoteBtn.Enabled = true;
            AddSupplierBtn.Enabled = true;
            SearchSupplierBtn.Enabled = true;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.CheckBox)ProductsPanel.Controls.Find("ChkBx" + i, true).First()).Enabled = true;
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
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        try
                        {
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from SupplierDebitInvoice where SupplierId=@supid and DebitInvoiceId=@invid and DebitInvoiceSeries=@invser", sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"supid", IdSupplierTxt.Text);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"invid", IdInvoiceTxt.Text);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"invser", SeriesInvoiceTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Υπάρχει ήδη καταχωρημένο πιστωτικό τιμολόγιο με τον αριθμό, τη σειρά και τον προμηθευτή που έχετε εισάγει.");
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
                            string InvIds = "";
                            foreach (var item in DisLs)
                            {
                                if (InvIds == "")
                                {
                                    InvIds += item;
                                }
                                else
                                {
                                    InvIds += ", " + item;
                                }
                            }
                            InsCmd1.CommandText = "insert into SupplierDebitInvoice (Id, SupplierId, DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, DebitInvoicePayment, DebitInvoiceDisc, DebitInvoicePrice,InvoiceId) values((select dbo.nvl(Max(Id)+1,0) from dbo.SupplierDebitInvoice), @supid, @invid, @ser, @date, @pay, @disc, @price,@inv)";
                            InsCmd1.Parameters.AddWithValue("@supid", IdSupplierTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@invid", IdInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@ser", SeriesInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                            InsCmd1.Parameters.AddWithValue("@pay", PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem));
                            InsCmd1.Parameters.AddWithValue("@disc", DiscInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@price", PriceInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@inv", InvIds);
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
                                if (Chkd.Contains(i.ToString()))
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
                                    InsCmd4.Parameters.AddWithValue("@disc", (ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text == "" ? "0" : ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text));
                                    InsCmd4.Parameters.AddWithValue("@supplierid", IdSupplierTxt.Text);
                                    InsCmd4.Parameters.AddWithValue("@supplierdisc", DiscInvoiceTxt.Text);
                                    InsCmd4.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                                    InsCmd4.ExecuteNonQuery();
                                }
                            }
                            foreach (var item in ls2)
                            {
                                SqlCommand InsCmd5 = sqlcon.CreateCommand();
                                InsCmd5.Connection = sqlcon;
                                InsCmd5.Transaction = InsTrans;
                                InsCmd5.CommandText = "update SupplierReturnDisNote set DebitInvoice='1' where Id=@id";
                                InsCmd5.Parameters.AddWithValue("@id", item);
                                InsCmd5.ExecuteNonQuery();
                            }
                            InsTrans.Commit();
                                MessageBox.Show("Το πιστωτικό τιμολόγιο του προμηθευτή καταχωρήθηκε με επιτυχία.");
                                this.SuspendLayout();
                                ClearProds();
                                ClearValues();
                                this.ResumeLayout(false);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πιστωτικού τιμολογίου. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
            DiscProdTxt1.Visible = false;
            ChkBx1.Visible = false;
            AaLabel.Visible = false;
            AaTxt1.Visible = false;
            DisNoteProdLbl.Visible = false;
            IdProdLbl.Visible = false;
            DescrProdLbl.Visible = false;
            QuantProdLbl.Visible = false;
            ValueProdLbl.Visible = false;
            DiscProdLbl.Visible = false;
            IdProdTxt1.Visible = false;
            DescrProdTxt1.Visible = false;
            QuantProdTxt1.Visible = false;
            ValueProdTxt1.Visible = false;
            DiscProdTxt1.Visible = false;
            IdProdTxt1.BackColor = SystemColors.Window;
            DescrProdTxt1.BackColor = SystemColors.Window;
            QuantProdTxt1.BackColor = SystemColors.Window;
            ValueProdTxt1.BackColor = SystemColors.Window;
            DiscProdTxt1.BackColor = SystemColors.Window;
            this.ProdPanel.Height = 68;
            this.ProductsPanel.Height = 286;
            this.Height = 400;
            if (Convert.ToInt16(ProdItemsTxt.Text) >= 2)
            {
                for (int r = 2; r <= Convert.ToInt16(ProdItemsTxt.Text); r++)
                {
                    this.ProductsPanel.Controls.Remove(Controls.Find("AaTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("DisNoteProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("IdProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("DescrProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("QuantProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("ValueProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("DiscProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("ChkBx" + r, true).First());
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
            PriceInvoiceTxt.Text = "";
            PriceInvoiceTxt.Value = 0;
            DateTimeInvoicePicker.Value = System.DateTime.Today.AddDays(2);
            IdProdTxt1.Text = "";
            DescrProdTxt1.Text = "";
            QuantProdTxt1.Text = "";
            ValueProdTxt1.Text = "";
            DiscProdTxt1.Text = "";
            DisNoteTxt.Text = "";
            ls2.Clear();
            ChkBx1.Checked = false;
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            IdInvoiceTxt.ReadOnly = false;
            SeriesInvoiceTxt.ReadOnly = false;
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            InvoiceBtn.Enabled = true;
            DisNoteBtn.Enabled = true;
            AddSupplierBtn.Enabled = true;
            SearchSupplierBtn.Enabled = true;
            this.ResumeLayout(false);
        }

        private void PriceInvoiceTxt_ValueChanged(object sender, EventArgs e)
        {
            if (PriceInvoiceTxt.Value != 0 && PriceInvoiceTxt.Text != "")
            {
                if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                {
                    AfterDebitSupplierTxt.Text = ( Convert.ToDouble(PrevDebitSupplierTxt.Text) - Convert.ToDouble(PriceInvoiceTxt.Value)).ToString();
                }
                else
                {
                    AfterDebitSupplierTxt.Text = Convert.ToDouble(PrevDebitSupplierTxt.Text).ToString();
                }
            }
        }

        private void InvoiceBtn_Click(object sender, EventArgs e)
        {
            OrderFindInvoice FindInvoice = new OrderFindInvoice(IdSupplierTxt.Text);
            FindInvoice.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindInvoice.ReturnInvoiceIds() != null)
                {
                    ls.Clear();
                    ls = FindInvoice.ReturnInvoiceIds();
                    if (ls.Count>0)
                    {
                        FillProducts(FindInvoice.ReturnInvoiceIds());
                    }
                }
            };
            FindInvoice.FormClosed += new FormClosedEventHandler(FormClose);
            this.Enabled = false;
            FindInvoice.ShowDialog();
        }

        private void DisNoteBtn_Click(object sender, EventArgs e)
        {
            OrderFindDisNoteReturnSupplier FindDisNoteReturnSupplier = new OrderFindDisNoteReturnSupplier(IdSupplierTxt.Text);
            FindDisNoteReturnSupplier.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindDisNoteReturnSupplier.ReturnDisNoteIds() != null)
                {
                    ls2.Clear();
                    ls2 = FindDisNoteReturnSupplier.ReturnDisNoteIds();
                    DisNoteTxt.Text = FindDisNoteReturnSupplier.ReturnDisNotes();
                }
            };
            FindDisNoteReturnSupplier.FormClosed += new FormClosedEventHandler(FormClose);
            this.Enabled = false;
            FindDisNoteReturnSupplier.ShowDialog();
        }
    }
}
