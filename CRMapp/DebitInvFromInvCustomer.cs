using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class DebitInvFromInvCustomer : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        List<string> DisLs = new List<string>();
        Dictionary<string, string> dc2 = new Dictionary<string, string>();
        Dictionary<string, string> unt = new Dictionary<string, string>();
        Dictionary<string, string> QuantChk = new Dictionary<string, string>();
        List<string> Chkd = new List<string>();
        Checks chk = new Checks();
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
        string InvPrice="0";
        int proditems = 1;
        double sumquant = 0;
        decimal sumval = 0;
        decimal sumdisc = 0;
        decimal sumvat = 0;
        int pagecount = 1;
        Image InvoiceDrawImg = Properties.Resources.InvDraw;
        Image InvoicePlainImg = Properties.Resources.InvPlain;
        private string CustomerOccupation;
        private string CustomerAddress;
        private string CustomerTk;
        private string CustomerTax_office;
        private string CustomerPhone;
        private string CustomerEmail;
        private string CustomerPhone2;
        private string CustomerRegion;
        string InvIds;

        public DebitInvFromInvCustomer()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetDataProd();
        }


        private void GetDataProd()
        {
            SeriesInvoiceTxt.Text = chk.Return_DebitInvoiceSeries();
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        GC.Collect();
                        string query2 = "select right('00000'+dbo.nvl(Max(right(DebitInvoiceId,5))+1,1),5)  from dbo.CustomerDebitInvoice where DebitInvoiceSeries=@series";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"series", chk.Return_DebitInvoiceSeries());
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        if (dt2.Rows.Count == 1)
                        {
                            IdInvoiceTxt.Text = "ΠΣΤ-" + dt2.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση περιγραφής/κωδικού ή Α/Α Τιμολογίου. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }

        void FormClose(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }
        
        private void FillProducts(List<string> list)
        {
            this.SuspendLayout();
            ClearProds();
            QuantChk.Clear();
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
                            string query = "select InvoiceId+'/'+InvoiceSeries,ProductId,Description,ProductQuant,ProductPrice,ProductDisc,InvoicePrice,InvoiceDisc from CustomerInvoice a,CustomerInvoiceProducts b, Products c where a.Id=b.CustomerInvoiceId and b.ProductId=c.Id and a.Id=@id";
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
                                proditems = j;
                                unt.Add(j.ToString(), dt.Rows[i][5].ToString());
                                if (j == 1)
                                {
                                    DisNoteProdTxt1.Text = dt.Rows[i][0].ToString();
                                    IdProdTxt1.Text = dt.Rows[i][1].ToString();
                                    DescrProdTxt1.Text = dt.Rows[i][2].ToString();
                                    QuantProdTxt1.Text = dt.Rows[i][3].ToString();
                                    ValueProdTxt1.Text = dt.Rows[i][4].ToString();
                                    DiscProdTxt1.Text = dt.Rows[i][5].ToString();
                                    InvPrice = dt.Rows[i][6].ToString();
                                    DiscInvoiceTxt.Text = dt.Rows[i][7].ToString();
                                    QuantChk.Add("1", dt.Rows[i][3].ToString());
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
                                    QuantProdTxt.ReadOnly = true;
                                    QuantProdTxt.Location = new Point(QuantProdTxt1.Location.X, QuantProdTxt1.Location.Y + 21 * (j - 1));
                                    QuantProdTxt.Size = QuantProdTxt1.Size;
                                    QuantProdTxt.Font = QuantProdTxt1.Font;
                                    QuantChk.Add(j.ToString(), dt.Rows[i][3].ToString());
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

        private void SearchCustomerBtn_Click(object sender, EventArgs e)
        {
            OrderFindCustomer FindCustomer = new OrderFindCustomer();
            FindCustomer.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindCustomer.ReturnCustomerId()!=null)
                {
                    ClearValues();
                    if (Convert.ToInt16(ProdItemsTxt.Text.ToString())>1)
                    {
                        ClearProds();
                    }
                    this.IdCustomerTxt.Text = FindCustomer.ReturnCustomerId();
                    this.NameCustomerTxt.Text = FindCustomer.ReturnCustomerName();
                    this.AfmCustomerTxt.Text = FindCustomer.ReturnCustomerAfm();
                    this.PrevCreditCustomerTxt.Text = FindCustomer.ReturnCustomerCredit();
                    CustomerOccupation = FindCustomer.ReturnCustomerOccupation();
                    CustomerAddress = FindCustomer.ReturnCustomerAddress();
                    CustomerTk = FindCustomer.ReturnCustomerTk();
                    CustomerTax_office = FindCustomer.ReturnCustomerTax_office();
                    CustomerPhone = FindCustomer.ReturnCustomerPhone();
                    CustomerEmail = FindCustomer.ReturnCustomerEmail();
                    CustomerPhone2 = FindCustomer.ReturnCustomerPhone2();
                    CustomerRegion = FindCustomer.ReturnCustomerRegion();
                }
            };
            FindCustomer.FormClosed += new FormClosedEventHandler(FormClose);
            FindCustomer.FormClosed += (object sender3, FormClosedEventArgs e3) =>
            {
                this.SuspendLayout();
                FindCustomer.Dispose();
                this.ResumeLayout(false);
            };
            this.Enabled = false;
            FindCustomer.ShowDialog();
        }

        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            OrderAddCustomer AddCustomer = new OrderAddCustomer();
            AddCustomer.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (AddCustomer.ReturnCustomerId() != null)
                {
                    ClearValues();
                    if (Convert.ToInt16(ProdItemsTxt.Text.ToString()) > 1)
                    {
                        ClearProds();
                    }
                    this.IdCustomerTxt.Text = AddCustomer.ReturnCustomerId();
                    this.NameCustomerTxt.Text = AddCustomer.ReturnCustomerName();
                    this.AfmCustomerTxt.Text = AddCustomer.ReturnCustomerAfm();
                    this.PrevCreditCustomerTxt.Text = AddCustomer.ReturnCustomerCredit();
                    CustomerOccupation = AddCustomer.ReturnCustomerOccupation();
                    CustomerAddress = AddCustomer.ReturnCustomerAddress();
                    CustomerTk = AddCustomer.ReturnCustomerTk();
                    CustomerTax_office = AddCustomer.ReturnCustomerTax_office();
                    CustomerPhone = AddCustomer.ReturnCustomerPhone();
                    CustomerEmail = AddCustomer.ReturnCustomerEmail();
                    CustomerPhone2 = AddCustomer.ReturnCustomerPhone2();
                    CustomerRegion = AddCustomer.ReturnCustomerRegion();
                }
            };
            AddCustomer.FormClosed += new FormClosedEventHandler(FormClose);
            AddCustomer.FormClosed += (object sender3, FormClosedEventArgs e3) =>
            {
                this.SuspendLayout();
                AddCustomer.Dispose();
                this.ResumeLayout(false);
            };
            this.Enabled = false;
            AddCustomer.ShowDialog();
        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            string overcome2 = "";
            bool noselection = false;
            Chkd.Clear();
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                CheckBox Chk = new CheckBox();
                Chk = (CheckBox)ProductsPanel.Controls.Find("ChkBx" + i, true).First();
                if (Chk.Checked == true)
                {
                    noselection = true;
                    Chkd.Add(i.ToString());
                    string outcome2;
                    QuantChk.TryGetValue(i.ToString(), out outcome2);
                    if (Convert.ToDecimal(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) > Convert.ToDecimal(outcome2))
                    {
                        ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().BackColor = Color.DarkRed;
                        overcome2 += i + ", ";
                    }

                }

            }
            if (DiscInvoiceTxt.Text == "")
            {
                DiscInvoiceTxt.Text = "0";
            }
            if (IdCustomerTxt.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε πελάτη.");
            }
            else if (IdProdTxt1.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον τιμολόγιο.");
            }
            else if (PayInvoiceCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε τρόπο πληρωμής του τιμολογίου.");
            }
            else if (DateTimeInvoicePicker.Value > System.DateTime.Today.AddDays(1))
            {
                MessageBox.Show("Η ημερομηνία δεν μπορεί να είναι μεταγενέστερη της σημερινής.");
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
                PriceInvoiceTxt.Enabled = true;
                PayInvoiceCmb.Enabled = false;
                InvoiceBtn.Enabled = false;
                AddCustomerBtn.Enabled = false;
                SearchCustomerBtn.Enabled = false;
                NotesTxt.ReadOnly = true;
                CommentsTxt.ReadOnly = true;
                sumquant = 0;
                sumval = 0;
                sumdisc = 0;
                sumvat = 0;
                double priceInv = 0;
                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                {
                    if (Chkd.Contains(i.ToString()))
                    {
                        priceInv += Math.Round(Convert.ToDouble(ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text) * Convert.ToDouble(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) * (1 - Convert.ToDouble((ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text == "" ? "0" : ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text)) / 100) * (1 - Convert.ToDouble(DiscInvoiceTxt.Text) / 100) * 1.24, 2);
                        ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = true;
                        ((System.Windows.Forms.CheckBox)ProductsPanel.Controls.Find("ChkBx" + i, true).First()).Enabled = false;
                        if (DisLs.Contains(ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text) == false)
                        {
                            DisLs.Add(ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text);
                        }
                    }
                }
                if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                {
                    AfterCreditCustomerTxt.Text = (Convert.ToDouble(PrevCreditCustomerTxt.Text) - priceInv).ToString();
                }
                else
                {
                    AfterCreditCustomerTxt.Text = Convert.ToDouble(PrevCreditCustomerTxt.Text).ToString();
                }
                PriceInvoiceTxt.Text = Convert.ToDecimal(priceInv).ToString();

            }
        }

        private void CorrectBtn_Click(object sender, EventArgs e)
        {
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            InvoiceBtn.Enabled = true;
            AddCustomerBtn.Enabled = true;
            SearchCustomerBtn.Enabled = true;
            NotesTxt.ReadOnly = false;
            CommentsTxt.ReadOnly = false;
            PriceInvoiceTxt.Text = "";
            AfterCreditCustomerTxt.Text = "";
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
                        DataTable dt2 = new DataTable();
                        if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                        {
                            string query = "select Credit from Customers where Id=@id";
                            SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", IdCustomerTxt.Text);
                            SearchAdapt2.Fill(dt2);
                        }
                        SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                        SqlCommand InsCmd1 = sqlcon.CreateCommand();
                        InsCmd1.Connection = sqlcon;
                        SqlCommand InsCmd2 = sqlcon.CreateCommand();
                        InsCmd2.Connection = sqlcon;
                        InsCmd1.Transaction = InsTrans;
                        InsCmd2.Transaction = InsTrans;
                        InvIds = "";
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
                        try
                        {
                            InsCmd1.CommandText = "insert into CustomerDebitInvoice (Id, CustomerId, DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, DebitInvoicePayment, DebitInvoiceDisc, DebitInvoicePrice,InvoiceId, Notes, Comments) values((select dbo.nvl(Max(Id)+1,0) from dbo.CustomerDebitInvoice), @supid, @invid, @ser, @date, @pay, @disc, @price,@inv,@not,@comm)";
                            InsCmd1.Parameters.AddWithValue("@supid", IdCustomerTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@invid", IdInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@ser", SeriesInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                            InsCmd1.Parameters.AddWithValue("@pay", PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem));
                            InsCmd1.Parameters.AddWithValue("@disc", DiscInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@price", PriceInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@inv", InvIds);
                            InsCmd1.Parameters.AddWithValue("@not", NotesTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@comm", CommentsTxt.Text);
                            InsCmd1.ExecuteNonQuery();
                                
                            if (dt2.Rows.Count==1)
                            {
                                decimal oldCredit = Convert.ToDecimal(dt2.Rows[0][0].ToString());
                                decimal newCredit = oldCredit - Convert.ToDecimal(PriceInvoiceTxt.Text);
                                InsCmd2.CommandText = "update Customers set Credit= @credit where Id=@id";
                                InsCmd2.Parameters.AddWithValue("@credit", newCredit);
                                InsCmd2.Parameters.AddWithValue("@id", IdCustomerTxt.Text);
                                InsCmd2.ExecuteNonQuery();
                            }

                            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                            {
                                if (Chkd.Contains(i.ToString()))
                                {
                                    SqlCommand InsCmd3 = sqlcon.CreateCommand();
                                    InsCmd3.Connection = sqlcon;
                                    SqlCommand InsCmd4 = sqlcon.CreateCommand();
                                    InsCmd4.Connection = sqlcon;
                                    InsCmd3.Transaction = InsTrans;
                                    InsCmd4.Transaction = InsTrans;
                                    InsCmd3.CommandText = "insert into CustomerDebitInvoiceProducts (Id,CustomerDebitInvoiceId,ProductId,ProductQuant,ProductPrice,ProductDisc) values ((select dbo.nvl(Max(Id)+1,0) from CustomerDebitInvoiceProducts),(select dbo.nvl(Max(Id),0) from dbo.CustomerDebitInvoice),@prodid,@prodquant,@prodprice,@proddisc)";
                                    InsCmd3.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@prodquant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@prodprice", ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@proddisc", ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text);
                                    InsCmd3.ExecuteNonQuery();
                                    InsCmd4.CommandText = "insert into ProductsReserve (Id,DocId,ProductId,Quant,Price,Disc,Date) values ((select dbo.nvl(Max(Id)+1,0) from ProductsReserve),@doc,@prodid,@prodquant,@prodprice,@proddisc,@dat)";
                                    InsCmd4.Parameters.AddWithValue("@doc", IdInvoiceTxt.Text + " - " + SeriesInvoiceTxt.Text);
                                    InsCmd4.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@prodquant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@prodprice", ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@proddisc", ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text);
                                    InsCmd4.Parameters.AddWithValue("@dat", DateTimeInvoicePicker.Value);
                                    InsCmd4.ExecuteNonQuery();
                                }
                            }
                                
                            InsTrans.Commit();
                            MessageBox.Show("Το πιστωτικό τιμολόγιο του πελάτη καταχωρήθηκε με επιτυχία.");
                            Do_Print();
                            this.SuspendLayout();
                            ClearProds();
                            ClearValues();
                            GetDataProd();
                            this.ResumeLayout(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πιστωτικού τιμολογίου του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                        try
                        {
                            InsTrans.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πιστωτικού τιμολογίου του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
            IdCustomerTxt.Text = "";
            NameCustomerTxt.Text = "";
            AfmCustomerTxt.Text = "";
            DiscInvoiceTxt.Text = "";
            PayInvoiceCmb.SelectedIndex = -1;
            PrevCreditCustomerTxt.Text = "";
            AfterCreditCustomerTxt.Text = "";
            PriceInvoiceTxt.Text = "";
            DateTimeInvoicePicker.Value = System.DateTime.Today;
            IdProdTxt1.Text = "";
            DescrProdTxt1.Text = "";
            QuantProdTxt1.Text = "";
            ValueProdTxt1.Text = "";
            DiscProdTxt1.Text = "";
            NotesTxt.Text = "";
            CommentsTxt.Text = "";
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            InvoiceBtn.Enabled = true;
            AddCustomerBtn.Enabled = true;
            SearchCustomerBtn.Enabled = true;
            NotesTxt.ReadOnly = false;
            CommentsTxt.ReadOnly = false;
            sumquant = 0;
            sumval = 0;
            sumdisc = 0;
            sumvat = 0;
            pagecount = 1;
            CustomerOccupation = "";
            CustomerAddress = "";
            CustomerTk = "";
            CustomerTax_office = "";
            CustomerPhone = "";
            CustomerEmail = "";
            CustomerPhone2 = "";
            CustomerRegion = "";
            this.ResumeLayout(false);
        }

        private void PriceInvoiceTxt_ValueChanged(object sender, EventArgs e)
        {
            if (PriceInvoiceTxt.Text != "")
            {
                if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                {
                    AfterCreditCustomerTxt.Text = (Convert.ToDouble(PrevCreditCustomerTxt.Text) - Convert.ToDouble(PriceInvoiceTxt.Text)).ToString();
                }
                else
                {
                    AfterCreditCustomerTxt.Text = Convert.ToDouble(PrevCreditCustomerTxt.Text).ToString();
                }
            }
        }

        private void InvoiceBtn_Click(object sender, EventArgs e)
        {
            OrderFindInvoiceCustomer FindInvoice = new OrderFindInvoiceCustomer(IdCustomerTxt.Text);
            FindInvoice.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindInvoice.ReturnInvoiceIds() != null)
                {
                    ls.Clear();
                    unt.Clear();
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
            e.Graphics.DrawString("Πιστωτικό Τιμολόγιο", new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 140, 250);
            e.Graphics.DrawString(SeriesInvoiceTxt.Text, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 588, 250);
            e.Graphics.DrawString(IdInvoiceTxt.Text, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 690, 250);
            e.Graphics.DrawString(DateTimeInvoicePicker.Value.Day.ToString() + '/' + DateTimeInvoicePicker.Value.Month.ToString() + '/' + DateTimeInvoicePicker.Value.Year.ToString(), new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 821, 250);
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΠΕΛΑΤΗ
            e.Graphics.DrawString(IdCustomerTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 329);
            e.Graphics.DrawString(NameCustomerTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 356);
            e.Graphics.DrawString(CustomerAddress, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 383);
            e.Graphics.DrawString(CustomerRegion, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 410);
            e.Graphics.DrawString(CustomerOccupation, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 438);
            e.Graphics.DrawString(AfmCustomerTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 466);
            e.Graphics.DrawString(CustomerEmail, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 493);
            e.Graphics.DrawString(CustomerPhone.Substring(0, 10), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 329);
            e.Graphics.DrawString(CustomerPhone2.Substring(0, 10), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 356);
            e.Graphics.DrawString(CustomerTk, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 411);
            e.Graphics.DrawString(CustomerTax_office, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 466);
            //ΕΚΤΥΠΩΣΗ ΛΟΙΠΩΝ ΣΤΟΙΧΕΙΩΝ ΤΙΜΟΛΟΓΙΟΥ
            e.Graphics.DrawString(InvIds, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 307);   //SXET PARASTATIKA
            e.Graphics.DrawString("Έδρα πελάτη", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 336);
            e.Graphics.DrawString("Έδρα μας", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 365);
            e.Graphics.DrawString("Επιστροφή", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 394);
            e.Graphics.DrawString(PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 423);
            e.Graphics.DrawString(chk.GrNumber(DiscInvoiceTxt.Text) + " %", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 452);
            //e.Graphics.DrawString(CustomerTax_office, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 487);  METAFORIKO MESO
            //ΕΚΤΥΠΩΣΗ ΠΡΟΙΟΝΤΩΝ
            int rest = proditems;
            int cnt = Convert.ToInt16(ProdItemsTxt.Text) - proditems;
            bool hasmorepages = false;
            decimal vat = chk.Return_Vat(IdCustomerTxt.Text);
            string Unit;
            for (int i = 1; i <= rest; i++)
            {
                if (Chkd.Contains((i + cnt).ToString()))
                {
                    if (i == 19)
                    {
                        hasmorepages = true;
                        break;
                    }
                    string qnt = ProductsPanel.Controls.Find("QuantProdTxt" + (i + cnt), true).First().Text;
                    string val = ProductsPanel.Controls.Find("ValueProdTxt" + (i + cnt), true).First().Text;
                    string disc = ProductsPanel.Controls.Find("DiscProdTxt" + (i + cnt), true).First().Text;
                    decimal totval = Convert.ToDecimal(Math.Round(Convert.ToDouble(qnt) * Convert.ToDouble(val), 2));
                    decimal totdisc = Convert.ToDecimal(Math.Round(totval * Convert.ToDecimal(((disc == "") ? "0" : disc)) / 100, 2));
                    totdisc += Convert.ToDecimal(Math.Round((totval - totdisc) * Convert.ToDecimal(DiscInvoiceTxt.Text) / 100, 2));
                    decimal totdiscval = totval - totdisc;
                    decimal totvat = Convert.ToDecimal(Math.Round(totdiscval * vat / 100, 2));
                    //ΕΥΡΕΣΗ ΜΟΝΑΔΑΣ ΜΕΤΡΗΣΗΣ
                    unt.TryGetValue((i + cnt).ToString(), out Unit);
                    //
                    e.Graphics.DrawString(ProductsPanel.Controls.Find("IdProdTxt" + (i + cnt), true).First().Text, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 33, 558 + i * 25);
                    string descr = ProductsPanel.Controls.Find("DescrProdTxt" + (i + cnt), true).First().Text;
                    if (descr.Length > 60)
                    {
                        descr = descr.Substring(0, 60);
                    }
                    if (Unit.Length > 4)
                    {
                        Unit = Unit.Substring(0, 4);
                    }
                    e.Graphics.DrawString(descr, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 120, 558 + i * 25);
                    e.Graphics.DrawString(Unit, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 520, 558 + i * 25);
                    e.Graphics.DrawString(chk.GrQuant(qnt), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(561, 558 + i * 25, 65, 25), format);
                    e.Graphics.DrawString(chk.GrNumber(val), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(632, 558 + i * 25, 58, 25), format);
                    e.Graphics.DrawString(chk.GrNumber((totval).ToString()), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(696, 558 + i * 25, 72, 25), format);
                    e.Graphics.DrawString(((disc == "0") ? "" : chk.GrNumber(disc.ToString())), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(780, 558 + i * 25, 60, 25), format);
                    e.Graphics.DrawString(((totdisc == 0) ? "" : chk.GrNumber(totdisc.ToString())), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(847, 558 + i * 25, 55, 25), format);
                    e.Graphics.DrawString(chk.GrNumber((totdiscval).ToString()), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(908, 558 + i * 25, 63, 25), format);
                    e.Graphics.DrawString((vat).ToString(), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(976, 558 + i * 25, 24, 25), format);
                    e.Graphics.DrawString(chk.GrNumber((totvat).ToString()), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(1006, 558 + i * 25, 52, 25), format);
                    proditems--;
                    sumquant += Convert.ToDouble(qnt);
                    sumval += Convert.ToDecimal(totval);
                    sumdisc += Convert.ToDecimal(totdisc);
                    sumvat += Convert.ToDecimal(totvat);
                }
            }
            e.Graphics.DrawString(chk.GrNumber(PrevCreditCustomerTxt.Text), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 145, 1052);
            //ΕΚΤΥΠΩΣΗ ΑΡΙΘΜΙΣΗΣ ΣΕΛΙΔΩΝ ΤΙΜΟΛΟΓΙΟΥ
            e.Graphics.DrawString("Σελίδα " + pagecount + " από " + (Convert.ToInt16(Chkd.Count()) / 18 + 1), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 45, 1505);
            pagecount++;
            //ΕΚΤΥΠΩΣΗ ΣΥΝΟΛΩΝ ΤΙΜΟΛΟΓΙΟΥ
            if (hasmorepages == false)
            {
                if (NotesTxt.Text != "")
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
                e.Graphics.DrawString(chk.GrNumber(sumval.ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(924, 1059, 120, 25), format);
                e.Graphics.DrawString(chk.GrNumber(sumdisc.ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(924, 1105, 120, 25), format);
                e.Graphics.DrawString(chk.GrNumber((sumval - sumdisc).ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(924, 1151, 120, 25), format);
                e.Graphics.DrawString(chk.GrNumber(sumvat.ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(924, 1197, 120, 25), format);
                e.Graphics.DrawString(chk.GrNumber((sumval - sumdisc + sumvat).ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Bold), Brushes.Black, new Rectangle(924, 1273, 120, 25), format);
                e.Graphics.DrawString(chk.GrNumber((Convert.ToDecimal(PrevCreditCustomerTxt.Text) + (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση" ? -sumval + sumdisc - sumvat : 0)).ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 391, 1052);
                proditems = Convert.ToInt16(ProdItemsTxt.Text);
                sumquant = 0;
                sumval = 0;
                sumdisc = 0;
                sumvat = 0;
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
    }
}
