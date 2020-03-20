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

    public partial class DebitInvoiceCustomer : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        Dictionary<string, string> dc = new Dictionary<string, string>();
        Dictionary<string, string> dscr = new Dictionary<string, string>();
        Dictionary<string, string> unt = new Dictionary<string, string>();
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
        Checks chk = new Checks();
        ToolTip Tool1 = new ToolTip();
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

        public DebitInvoiceCustomer()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetData();
        }


        private void GetData()
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


        private void GetDataProd()
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select distinct ProductId,Description,LongDescr,Unit from Products a,CustomerInvoice b,CustomerInvoiceProducts c where a.Id=c.ProductId and b.CustomerId=@cusid and b.Id=c.CustomerInvoiceId order by ProductId,Description";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"cusid", IdCustomerTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        ls.Clear();
                        dc.Clear();
                        dscr.Clear();
                        unt.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ls.Add(dt.Rows[i][1].ToString());
                            dc.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            dscr.Add(dt.Rows[i][0].ToString(), dt.Rows[i][2].ToString());
                            unt.Add(dt.Rows[i][0].ToString(), dt.Rows[i][3].ToString());
                            IdProdTxt1.AutoCompleteCustomSource.Add(dt.Rows[i][0].ToString());
                        }
                        ls.Sort();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση περιγραφής/κωδικού. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
            proditems = i;
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
            ToolTip Tool = new ToolTip();
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
            OrderFindCustomer FindCustomer = new OrderFindCustomer();
            FindCustomer.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindCustomer.ReturnCustomerId() != null)
                {
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
                    GetDataProd();
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
                    GetDataProd();
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
            this.ProductsPanel.Controls.Remove(Controls.Find("ValueProdTxt" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("DiscProdTxt" + i, true).First());
            i--;
            proditems = i;
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
            }
            if (IdCustomerTxt.Text=="")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε πελάτη.");
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
            else
            {
                AddBtn.Visible = true;
                CorrectBtn.Visible = true;
                CalcBtn.Visible = false;
                ClearBtn.Visible = false;
                DiscInvoiceTxt.ReadOnly = true;
                PayInvoiceCmb.Enabled = false;
                AddProdBtn1.Enabled = false;
                RemoveProdBtn1.Enabled = false;
                AddCustomerBtn.Enabled = false;
                SearchCustomerBtn.Enabled = false;
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
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First()).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First()).ReadOnly = true;
                }
                decimal priceInv = -price;
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
                PriceInvoiceTxt.Text = Convert.ToDecimal(-priceInv).ToString();

            }
        }

        private void CorrectBtn_Click(object sender, EventArgs e)
        {
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            DiscInvoiceTxt.ReadOnly = false;
            PayInvoiceCmb.Enabled = true;
            AddProdBtn1.Enabled = true;
            RemoveProdBtn1.Enabled = true;
            AddCustomerBtn.Enabled = true;
            SearchCustomerBtn.Enabled = true;
            for (int i = 1; i <= Convert.ToInt16(ProdItemsTxt.Text); i++)
            {
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("IdProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DescrProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("QuantProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("ValueProdTxt" + i, true).First()).ReadOnly = false;
                ((System.Windows.Forms.TextBox)ProductsPanel.Controls.Find("DiscProdTxt" + i, true).First()).ReadOnly = false;
            }
            PriceInvoiceTxt.Text = "";
            AfterCreditCustomerTxt.Text = "";
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
                            try
                            {
                                InsCmd1.CommandText = "insert into CustomerDebitInvoice (Id, CustomerId, DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, DebitInvoicePayment, DebitInvoiceDisc, DebitInvoicePrice) values((select dbo.nvl(Max(Id)+1,0) from dbo.CustomerDebitInvoice), @supid, @invid, @ser, @date, @pay, @disc, @price)";
                                InsCmd1.Parameters.AddWithValue("@supid", IdCustomerTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@invid", IdInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@ser", SeriesInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@date", DateTimeInvoicePicker.Value);
                                InsCmd1.Parameters.AddWithValue("@pay", PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem));
                                InsCmd1.Parameters.AddWithValue("@disc", DiscInvoiceTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@price", PriceInvoiceTxt.Text);
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
                                
                                InsTrans.Commit();
                                MessageBox.Show("Το πιστωτικό τιμολόγιο του πελάτη καταχωρήθηκε με επιτυχία.");
                                Do_Print();
                                ClearValues();
                                GetData();
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
            AddCustomerBtn.Enabled = true;
            SearchCustomerBtn.Enabled = true;
            for (int i = Convert.ToInt16(ProdItemsTxt.Text); i >= 2; i--)
            {
                RemoveProdBtn1_Click(null, null);
            }
            AddBtn.Visible = false;
            CorrectBtn.Visible = false;
            CalcBtn.Visible = true;
            ClearBtn.Visible = true;
            DiscInvoiceTxt.ReadOnly = false;
            PayInvoiceCmb.Enabled = true;
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
                    AfterCreditCustomerTxt.Text = (-Convert.ToDouble(PriceInvoiceTxt.Text) + Convert.ToDouble(PrevCreditCustomerTxt.Text)).ToString();
                }
                else
                {
                    AfterCreditCustomerTxt.Text = Convert.ToDouble(PrevCreditCustomerTxt.Text).ToString();
                }
            }
        }

        private void SearchNameListBox1_Leave(object sender, EventArgs e)
        {
            SearchNameListBox1.Visible = false;
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
            //e.Graphics.DrawString(null, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 307);   SXET PARASTATIKA
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
                unt.TryGetValue(ProductsPanel.Controls.Find("IdProdTxt" + (i + cnt), true).First().Text, out Unit);
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
