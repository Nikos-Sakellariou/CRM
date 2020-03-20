using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class InvoiceDisNoteCustomer : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        List<string> DisLs = new List<string>();
        Dictionary<string, string> unt = new Dictionary<string, string>();
        Dictionary<string, string> LastPrc = new Dictionary<string, string>();
        Dictionary<string, string> LastDisc = new Dictionary<string, string>();
        Dictionary<string, string> LastDate = new Dictionary<string, string>();
        Dictionary<string, string> OrderValue = new Dictionary<string, string>();
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
        string DisNotes;
        Checks chk = new Checks();
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

        public InvoiceDisNoteCustomer()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetDataProd();
        }


        private void GetDataProd()
        {
            SeriesInvoiceTxt.Text = chk.Return_InvoiceSeries();
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        GC.Collect();
                        ls.Clear();
                        unt.Clear();
                        OrderValue.Clear();
                        string query2 = "select right('00000'+dbo.nvl(Max(right(InvoiceId,5))+1,1),5)  from dbo.CustomerInvoice where InvoiceSeries=@series";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"series", chk.Return_InvoiceSeries());
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        if (dt2.Rows.Count == 1)
                        {
                            IdInvoiceTxt.Text = "TIM-" + dt2.Rows[0][0].ToString();
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

        private void GetCustomerLastValues(string Id)
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        GC.Collect();
                        LastPrc.Clear();
                        LastDisc.Clear();
                        LastDate.Clear();
                        string query = @"select a.ProductId,ProductPrice,ProductDisc, InvoiceDate from
                        (select ProductId,Max(Id) Mid from CustomerInvoiceProducts group by ProductId) a,
                        CustomerInvoiceProducts b, CustomerInvoice c where a.Mid=b.Id and c.Id=b.CustomerInvoiceId and CustomerId=@id";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", Id);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            LastPrc.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            LastDisc.Add(dt.Rows[i][0].ToString(), dt.Rows[i][2].ToString());
                            LastDate.Add(dt.Rows[i][0].ToString(), dt.Rows[i][3].ToString());
                        }
                        string query2 = "select dbo.nvl(InvoiceDisc,'') from CustomerInvoice where Id=(select Max(Id) from CustomerInvoice where CustomerId=@id)";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", Id);
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        if (dt2.Rows.Count == 1)
                        {
                            LastDiscInvoiceTxt.Text = dt2.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση ιστορικών στοιχείων Τιμολογίου. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
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
                            string query = "select DisNoteId+'/'+DisNoteSeries,c.Id,c.Description,ProductQuant,ProductSalePrice,ProductSaleDisc,ProductCostPrice,Unit from CustomerDisNote a,CustomerDisNoteProducts b, Products c where a.Id=b.CustomerDisNoteId and b.ProductId=c.Id and a.Id=@id";
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
                                unt.Add(j.ToString(), dt.Rows[i][7].ToString());
                                OrderValue.Add(j.ToString(), dt.Rows[i][6].ToString());
                                string outcome1;
                                string outcome2;
                                string outcome3;
                                LastPrc.TryGetValue(dt.Rows[i][1].ToString(), out outcome1);
                                LastDisc.TryGetValue(dt.Rows[i][1].ToString(), out outcome2);
                                LastDate.TryGetValue(dt.Rows[i][1].ToString(), out outcome3);
                                if (j == 1)
                                {

                                    DisNoteProdTxt1.Text = dt.Rows[i][0].ToString();
                                    IdProdTxt1.Text = dt.Rows[i][1].ToString();
                                    DescrProdTxt1.Text = dt.Rows[i][2].ToString();
                                    QuantProdTxt1.Text = dt.Rows[i][3].ToString();
                                    ValueProdTxt1.Text = dt.Rows[i][4].ToString();
                                    DiscProdTxt1.Text = dt.Rows[i][5].ToString();
                                    LastValueProdTxt1.Text = outcome1;
                                    LastDiscProdTxt1.Text = outcome2;
                                    LastDateProdTxt1.Text = (outcome3 == null ? "" : Convert.ToDateTime(outcome3).Day.ToString() + '/' + Convert.ToDateTime(outcome3).Month.ToString() + '/' + Convert.ToDateTime(outcome3).Year.ToString());
                                    DisNoteProdTxt1.Visible = true;
                                    IdProdTxt1.Visible = true;
                                    DescrProdTxt1.Visible = true;
                                    QuantProdTxt1.Visible = true;
                                    ValueProdTxt1.Visible = true;
                                    DiscProdTxt1.Visible = true;
                                    EndPriceTxt1.Visible = true;
                                    AaLabel.Visible = true;
                                    AaTxt1.Visible = true;
                                    DisNoteProdLbl.Visible = true;
                                    IdProdLbl.Visible = true;
                                    DescrProdLbl.Visible = true;
                                    QuantProdLbl.Visible = true;
                                    ValueProdLbl.Visible = true;
                                    DiscProdLbl.Visible = true;
                                    EndPriceLbl.Visible = true;
                                    LastValueProdLbl.Visible = true;
                                    LastDiscProdLbl.Visible = true;
                                    LastDateProdLbl.Visible = true;
                                    LastValueProdTxt1.Visible = true;
                                    LastDiscProdTxt1.Visible = true;
                                    LastDateProdTxt1.Visible = true;
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
                                    System.Windows.Forms.TextBox EndPriceTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox LastValueProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox LastDiscProdTxt = new System.Windows.Forms.TextBox();
                                    System.Windows.Forms.TextBox LastDateProdTxt = new System.Windows.Forms.TextBox();
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
                                    ValueProdTxt.Text = dt.Rows[i][4].ToString();
                                    ValueProdTxt.Location = new Point(ValueProdTxt1.Location.X, ValueProdTxt1.Location.Y + 21 * (j - 1));
                                    ValueProdTxt.Size = ValueProdTxt1.Size;
                                    ValueProdTxt.Font = ValueProdTxt1.Font;
                                    DiscProdTxt.Name = "DiscProdTxt" + j;
                                    DiscProdTxt.Text = dt.Rows[i][5].ToString();
                                    DiscProdTxt.Location = new Point(DiscProdTxt1.Location.X, DiscProdTxt1.Location.Y + 21 * (j - 1));
                                    DiscProdTxt.MaxLength = 5;
                                    DiscProdTxt.Size = DiscProdTxt1.Size;
                                    DiscProdTxt.Font = DiscProdTxt1.Font;
                                    EndPriceTxt.Name = "EndPriceTxt" + j;
                                    EndPriceTxt.Location = new System.Drawing.Point(EndPriceTxt1.Location.X, EndPriceTxt1.Location.Y + 21 * (j - 1));
                                    EndPriceTxt.ReadOnly = true;
                                    EndPriceTxt.Size = EndPriceTxt1.Size;
                                    EndPriceTxt.Font = EndPriceTxt1.Font;
                                    LastValueProdTxt.Name = "LastValueProdTxt" + j;
                                    LastValueProdTxt.Location = new System.Drawing.Point(LastValueProdTxt1.Location.X, LastValueProdTxt1.Location.Y + 21 * (j - 1));
                                    LastValueProdTxt.Size = LastValueProdTxt1.Size;
                                    LastValueProdTxt.Font = LastValueProdTxt1.Font;
                                    LastValueProdTxt.ReadOnly = true;
                                    LastValueProdTxt.Text = outcome1;
                                    LastDiscProdTxt.Name = "LastDiscProdTxt" + j;
                                    LastDiscProdTxt.Location = new System.Drawing.Point(LastDiscProdTxt1.Location.X, LastDiscProdTxt1.Location.Y + 21 * (j - 1));
                                    LastDiscProdTxt.MaxLength = 5;
                                    LastDiscProdTxt.Size = LastDiscProdTxt1.Size;
                                    LastDiscProdTxt.Font = LastDiscProdTxt1.Font;
                                    LastDiscProdTxt.ReadOnly = true;
                                    LastDiscProdTxt.Text = outcome2;
                                    LastDateProdTxt.Name = "LastDateProdTxt" + j;
                                    LastDateProdTxt.Location = new System.Drawing.Point(LastDateProdTxt1.Location.X, LastDateProdTxt1.Location.Y + 21 * (j - 1));
                                    LastDateProdTxt.Size = LastDateProdTxt1.Size;
                                    LastDateProdTxt.Font = LastDateProdTxt1.Font;
                                    LastDateProdTxt.ReadOnly = true;
                                    LastDateProdTxt.Text = Convert.ToDateTime(outcome3).Day.ToString() + '/' + Convert.ToDateTime(outcome3).Month.ToString() + '/' + Convert.ToDateTime(outcome3).Year.ToString();
                                    this.ProductsPanel.Controls.Add(AaTxt);
                                    this.ProductsPanel.Controls.Add(DisNoteProdTxt);
                                    this.ProductsPanel.Controls.Add(IdProdTxt);
                                    this.ProductsPanel.Controls.Add(DescrProdTxt);
                                    this.ProductsPanel.Controls.Add(QuantProdTxt);
                                    this.ProductsPanel.Controls.Add(ValueProdTxt);
                                    this.ProductsPanel.Controls.Add(DiscProdTxt);
                                    this.ProductsPanel.Controls.Add(EndPriceTxt);
                                    this.ProductsPanel.Controls.Add(LastValueProdTxt);
                                    this.ProductsPanel.Controls.Add(LastDiscProdTxt);
                                    this.ProductsPanel.Controls.Add(LastDateProdTxt);
                                    this.ProdPanel.SendToBack();
                                    {
                                        ValueProdTxt.TextChanged += (object sender6, EventArgs e6) =>
                                        {
                                            ValueProdTxt.Text = ValueProdTxt.Text.Replace(',', '.');
                                            ValueProdTxt.SelectionStart = ValueProdTxt.Text.Length;
                                        };
                                        DiscProdTxt.TextChanged += (object sender7, EventArgs e7) =>
                                        {
                                            DiscProdTxt.Text = DiscProdTxt.Text.Replace(',', '.');
                                            DiscProdTxt.SelectionStart = DiscProdTxt.Text.Length;
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
        private void SearchCustomerBtn_Click(object sender, EventArgs e)
        {
            OrderFindCustomer FindCustomer = new OrderFindCustomer();
            FindCustomer.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindCustomer.ReturnCustomerId() != null)
                {
                    ClearValues();
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
                    // ΙΣΤΟΡΙΚΑ ΣΤΟΙΧΕΙΑ ΤΙΜΟΛΟΓΙΟΥ
                    GetCustomerLastValues(IdCustomerTxt.Text);
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
            string belowcost = "";
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
                if (ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text == "")
                {
                    ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().BackColor = SystemColors.Window;
                }
                else if(chk.CheckQuant(ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text) != "")
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
                if (noerrors == true)
                {
                    string orderv;
                    OrderValue.TryGetValue(ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text, out orderv);
                    decimal salev = Math.Round(Convert.ToDecimal(ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text) * (1 - Convert.ToDecimal(ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text) / 100) * (1 - Convert.ToDecimal(DiscInvoiceTxt.Text) / 100), 2);
                    if (orderv != "")
                    {
                        if (Convert.ToDecimal(orderv) >= salev)
                        {
                            ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().BackColor = Color.DarkRed;
                            belowcost += i + ", ";
                        }
                        else
                        {
                            ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().BackColor = System.Drawing.SystemColors.Window;
                        }
                    }
                }
            }
            if (IdCustomerTxt.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε πελάτη.");
            }
            else if (IdProdTxt1.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον Δελτίο Αποστολής.");
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
            else if (belowcost != "")
            {
                System.Windows.MessageBox.Show("Τα προϊόντα με Α/Α: " + belowcost + " πωλούνται κάτω του κόστους.");
            }
            else
            {
                DisLs.Clear();
                AddBtn.Visible = true;
                CorrectBtn.Visible = true;
                CalcBtn.Visible = false;
                ClearBtn.Visible = false;
                DiscInvoiceTxt.ReadOnly = true;
                PriceInvoiceTxt.Enabled = true;
                PayInvoiceCmb.Enabled = false;
                DisNoteBtn.Enabled = false;
                AddCustomerBtn.Enabled = false;
                SearchCustomerBtn.Enabled = false;
                VehicleTxt.ReadOnly = true;
                NotesTxt.ReadOnly = true;
                CommentsTxt.ReadOnly = true;
                decimal price = 0;
                decimal vat = chk.Return_Vat(IdCustomerTxt.Text);
                for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                {
                    string qnt = ProductsPanel.Controls.Find("QuantProdTxt" + (i), true).First().Text;
                    string val = ProductsPanel.Controls.Find("ValueProdTxt" + (i), true).First().Text;
                    string disc = ProductsPanel.Controls.Find("DiscProdTxt" + (i), true).First().Text;
                    decimal totval = Convert.ToDecimal(Math.Round(Convert.ToDouble(qnt) * Convert.ToDouble(val), 2));
                    decimal totdisc = Convert.ToDecimal(Math.Round(totval * Convert.ToDecimal(((disc == "") ? "0" : disc)) / 100, 2));
                    totdisc += Convert.ToDecimal(Math.Round((totval - totdisc) * Convert.ToDecimal(DiscInvoiceTxt.Text) / 100, 2));
                    decimal totdiscval = totval - totdisc;
                    decimal totvat = Convert.ToDecimal(Math.Round(totdiscval * vat / 100, 2));
                    price += totval - totdisc + totvat;
                    ProductsPanel.Controls.Find("EndPriceTxt" + i, true).First().Text = Math.Round(Convert.ToDecimal(val) * (1 - Convert.ToDecimal(disc) / 100) * (1 - Convert.ToDecimal(DiscInvoiceTxt.Text) / 100), 2).ToString();
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First()).ReadOnly = true;
                    if (DisLs.Contains(ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text) == false)
                    {
                        DisLs.Add(ProductsPanel.Controls.Find("DisNoteProdTxt" + i, true).First().Text);
                    }
                }
                decimal priceInv = price;
                sumquant = 0;
                sumval = 0;
                sumdisc = 0;
                sumvat = 0;
                if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                {
                    AfterCreditCustomerTxt.Text = (priceInv + Convert.ToDecimal(PrevCreditCustomerTxt.Text)).ToString();
                }
                else
                {
                    AfterCreditCustomerTxt.Text = Convert.ToDecimal(PrevCreditCustomerTxt.Text).ToString();
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
            DiscInvoiceTxt.ReadOnly = false;
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            DisNoteBtn.Enabled = true;
            AddCustomerBtn.Enabled = true;
            SearchCustomerBtn.Enabled = true;
            VehicleTxt.ReadOnly = false;
            NotesTxt.ReadOnly = false;
            CommentsTxt.ReadOnly = false;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First()).ReadOnly = false;
                ProductsPanel.Controls.Find("EndPriceTxt" + i, true).First().Text = "";
            }
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
                    DataTable dt = new DataTable();
                    if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                    {
                        string query = "select Credit from Customers where Id=@id";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", IdCustomerTxt.Text);
                        SearchAdapt.Fill(dt);
                    }
                    DisNotes = "";
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
                    SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                    SqlCommand InsCmd1 = sqlcon.CreateCommand();
                    InsCmd1.Connection = sqlcon;
                    SqlCommand InsCmd2 = sqlcon.CreateCommand();
                    InsCmd2.Connection = sqlcon;
                    InsCmd1.Transaction = InsTrans;
                    InsCmd2.Transaction = InsTrans;
                    try
                    {
                        foreach (var item in ls)
                        {
                            SqlCommand InsCmd5 = sqlcon.CreateCommand();
                            InsCmd5.Connection = sqlcon;
                            InsCmd5.Transaction = InsTrans;
                            InsCmd5.CommandText = "update CustomerDisNote set Invoice='1' where Id=@id";
                            InsCmd5.Parameters.AddWithValue("@id", item);
                            InsCmd5.ExecuteNonQuery();
                        }
                            InsCmd1.CommandText = "insert into CustomerInvoice (Id, CustomerId, InvoiceId, InvoiceSeries, InvoiceDate, InvoicePayment, InvoiceDisc, InvoicePrice, InvoiceDisNotes, Vehicle, Notes, Comments) values((select dbo.nvl(Max(Id)+1,0) from dbo.CustomerInvoice), @supid, @invid, @ser, @date, @pay, @disc, @price, @note,@veh,@not,@comm)";
                            InsCmd1.Parameters.AddWithValue("@supid", IdCustomerTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@invid", IdInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@ser", SeriesInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                            InsCmd1.Parameters.AddWithValue("@pay", PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem));
                            InsCmd1.Parameters.AddWithValue("@disc", DiscInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@price", PriceInvoiceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@note", DisNotes);
                            InsCmd1.Parameters.AddWithValue("@veh", VehicleTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@not", NotesTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@comm", CommentsTxt.Text);
                            InsCmd1.ExecuteNonQuery();

                            if (dt.Rows.Count == 1)
                            {
                                decimal oldDebit = Convert.ToDecimal(dt.Rows[0][0].ToString());
                                decimal newDebit = oldDebit + Convert.ToDecimal(PriceInvoiceTxt.Text);
                                InsCmd2.CommandText = "update Customers set Credit= @credit where Id=@id";
                                InsCmd2.Parameters.AddWithValue("@credit", newDebit);
                                InsCmd2.Parameters.AddWithValue("@id", IdCustomerTxt.Text);
                                InsCmd2.ExecuteNonQuery();
                            }

                            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
                            {
                                SqlCommand InsCmd3 = sqlcon.CreateCommand();
                                InsCmd3.Connection = sqlcon;
                                InsCmd3.Transaction = InsTrans;
                                InsCmd3.CommandText = "insert into CustomerInvoiceProducts (Id,CustomerInvoiceId,ProductId,ProductQuant,ProductPrice,ProductDisc) values ((select dbo.nvl(Max(Id)+1,0) from CustomerInvoiceProducts),(select dbo.nvl(Max(Id),0) from dbo.CustomerInvoice),@prodid,@prodquant,@prodprice,@proddisc)";
                                InsCmd3.Parameters.AddWithValue("@prodid", ProductsPanel.Controls.Find("IdProdTxt" + i, true).First().Text);
                                InsCmd3.Parameters.AddWithValue("@prodquant", ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First().Text);
                                InsCmd3.Parameters.AddWithValue("@prodprice", ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First().Text);
                                InsCmd3.Parameters.AddWithValue("@proddisc", ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First().Text);
                                InsCmd3.ExecuteNonQuery();
                            }

                            InsTrans.Commit();
                            MessageBox.Show("Το τιμολόγιο του πελάτη καταχωρήθηκε με επιτυχία.");
                            Do_Print();
                            this.SuspendLayout();
                            ClearProds();
                            ClearValues();
                            GetDataProd();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του τιμολογίου του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                            try
                            {
                                InsTrans.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του τιμολογίου του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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

        private void ClearProds()
        {

            this.SuspendLayout();
            DisNoteProdTxt1.Visible = false;
            IdProdTxt1.Visible = false;
            DescrProdTxt1.Visible = false;
            QuantProdTxt1.Visible = false;
            ValueProdTxt1.Visible = false;
            DiscProdTxt1.Visible = false;
            EndPriceTxt1.Visible = false;
            AaLabel.Visible = false;
            AaTxt1.Visible = false;
            DisNoteProdLbl.Visible = false;
            IdProdLbl.Visible = false;
            DescrProdLbl.Visible = false;
            QuantProdLbl.Visible = false;
            ValueProdLbl.Visible = false;
            DiscProdLbl.Visible = false;
            EndPriceLbl.Visible = false;
            LastValueProdLbl.Visible = false;
            LastDiscProdLbl.Visible = false;
            LastDateProdLbl.Visible = false;
            LastValueProdTxt1.Visible = false;
            LastDiscProdTxt1.Visible = false;
            LastDateProdTxt1.Visible = false;
            VehicleTxt.ReadOnly = false;
            NotesTxt.ReadOnly = false;
            CommentsTxt.ReadOnly = false;
            IdProdTxt1.BackColor = SystemColors.Window;
            DescrProdTxt1.BackColor = SystemColors.Window;
            QuantProdTxt1.BackColor = SystemColors.Window;
            ValueProdTxt1.BackColor = SystemColors.Window;
            DiscProdTxt1.BackColor = SystemColors.Window;
            LastValueProdTxt1.BackColor = SystemColors.Window;
            LastDiscProdTxt1.BackColor = SystemColors.Window;
            LastDateProdTxt1.BackColor = SystemColors.Window;
            this.ProdPanel.Height = 68;
            this.ProductsPanel.Height = 256;
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
                    this.ProductsPanel.Controls.Remove(Controls.Find("DiscProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("EndPriceTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("LastValueProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("LastDiscProdTxt" + r, true).First());
                    this.ProductsPanel.Controls.Remove(Controls.Find("LastDateProdTxt" + r, true).First());
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
            EndPriceTxt1.Text = "";
            LastValueProdTxt1.Text = "";
            LastDiscProdTxt1.Text = "";
            LastDateProdTxt1.Text = "";
            VehicleTxt.Text = "";
            NotesTxt.Text = "";
            CommentsTxt.Text = "";
            ValueProdTxt1.ReadOnly = false;
            DiscProdTxt1.ReadOnly = false;
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            DiscInvoiceTxt.ReadOnly = false;
            PriceInvoiceTxt.Enabled = false;
            PayInvoiceCmb.Enabled = true;
            DisNoteBtn.Enabled = true;
            AddCustomerBtn.Enabled = true;
            SearchCustomerBtn.Enabled = true;
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

        private void DisNoteBtn_Click(object sender, EventArgs e)
        {
            OrderFindDisNoteCustomer FindDisNote = new OrderFindDisNoteCustomer(IdCustomerTxt.Text);
            FindDisNote.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindDisNote.ReturnDisNoteIds() != null)
                {
                    ls.Clear();
                    unt.Clear();
                    OrderValue.Clear();
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
            if (PriceInvoiceTxt.Text != "")
            {
                if (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση")
                {
                    AfterCreditCustomerTxt.Text = (Convert.ToDouble(PriceInvoiceTxt.Text) + Convert.ToDouble(PrevCreditCustomerTxt.Text)).ToString();
                }
                else
                {
                    AfterCreditCustomerTxt.Text = Convert.ToDouble(PrevCreditCustomerTxt.Text).ToString();
                }
            }
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
            e.Graphics.DrawString("Τιμολόγιο", new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 230, 250);
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
            e.Graphics.DrawString(DisNotes, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 307);   //SXET PARASTATIKA
            //e.Graphics.DrawString("Έδρα μας", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 336);
            //e.Graphics.DrawString("Έδρα πελάτη", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 365);
            e.Graphics.DrawString("Πώληση", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 394);
            e.Graphics.DrawString(PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 423);
            e.Graphics.DrawString(chk.GrNumber(DiscInvoiceTxt.Text) + " %", new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 452);
            e.Graphics.DrawString(VehicleTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 487);
            //ΕΚΤΥΠΩΣΗ ΠΡΟΙΟΝΤΩΝ
            int rest = proditems;
            int cnt = Convert.ToInt16(ProdItemsTxt.Text) - proditems;
            bool hasmorepages = false;
            decimal vat = chk.Return_Vat(IdCustomerTxt.Text);
            string Unit;
            for (int i = 1; i <= rest; i++)
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
            e.Graphics.DrawString(chk.GrNumber(PrevCreditCustomerTxt.Text), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 145, 1052);
            //ΕΚΤΥΠΩΣΗ ΑΡΙΘΜΙΣΗΣ ΣΕΛΙΔΩΝ ΤΙΜΟΛΟΓΙΟΥ
            e.Graphics.DrawString("Σελίδα " + pagecount + " από " + (Convert.ToInt16(ProdItemsTxt.Text) / 18 + 1), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 45, 1505);
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
                e.Graphics.DrawString(chk.GrNumber((Convert.ToDecimal(PrevCreditCustomerTxt.Text) + (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση" ? sumval - sumdisc + sumvat : 0)).ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 391, 1052);
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
