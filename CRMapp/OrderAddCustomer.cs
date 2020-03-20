using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class OrderAddCustomer : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        private string CustomerId;
        private string CustomerName;
        private string CustomerAfm;
        private string CustomerCredit;
        private string CustomerOccupation;
        private string CustomerAddress;
        private string CustomerTk;
        private string CustomerTax_office;
        private string CustomerPhone;
        private string CustomerEmail;
        private string CustomerPhone2;
        private string CustomerRegion;
        private string CustomerRetail;

        public OrderAddCustomer()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
        }
        public string ReturnCustomerId()
        {
            return CustomerId;
        }
        public string ReturnCustomerName()
        {
            return CustomerName;
        }
        public string ReturnCustomerAfm()
        {
            return CustomerAfm;
        }
        public string ReturnCustomerCredit()
        {
            return CustomerCredit;
        }
        public string ReturnCustomerOccupation()
        {
            return CustomerOccupation;
        }
        public string ReturnCustomerAddress()
        {
            return CustomerAddress;
        }
        public string ReturnCustomerTk()
        {
            return CustomerTk;
        }
        public string ReturnCustomerTax_office()
        {
            return CustomerTax_office;
        }
        public string ReturnCustomerPhone()
        {
            return CustomerPhone;
        }
        public string ReturnCustomerEmail()
        {
            return CustomerEmail;
        }
        public string ReturnCustomerPhone2()
        {
            return CustomerPhone2;
        }
        public string ReturnCustomerRegion()
        {
            return CustomerRegion;
        }
        public string ReturnCustomerRetail()
        {
            return CustomerRetail;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            string errorMessages = "";
            if (NameTxt.Text == "" || OccupationTxt.Text == "" || AfmTxt.Text == "" || AddressTxt.Text == "" || RegionTxt.Text == "" ||  PhoneTxt.Text == "" || Tax_officeTxt.Text == "")
            {
                errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα πεδία με αστερίσκο (*).\n"; ;
            }
            else
            {
                errorMessages += chk.CheckAfm(AfmTxt.Text);
                errorMessages += chk.CheckPhone(PhoneTxt.Text);
                if (Phone2Txt.Visible == true)
                {
                    errorMessages += chk.CheckPhone(Phone2Txt.Text);
                }
                if (TkTxt.Text != "")
                {
                    errorMessages += chk.CheckTk(TkTxt.Text);
                }
                if (VatTxt.Text != "")
                {
                    errorMessages += chk.CheckVat(VatTxt.Text);
                }
                if (EmailTxt.Text != "")
                {
                    errorMessages += chk.CheckEmail(EmailTxt.Text);
                }
                if (CreditTxt.Text != "")
                {
                    errorMessages += chk.CheckCredit(CreditTxt.Text);
                }
                if (MaxCreditTxt.Text != "")
                {
                    errorMessages += chk.CheckMaxCredit(MaxCreditTxt.Text);
                }
            }
            if (errorMessages != "")
            {
                MessageBox.Show(errorMessages);
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from Customers where Afm=@parameter", sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"parameter", AfmTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Υπάρχει ήδη καταχωρημένος πελάτης με το Α.Φ.Μ. που έχετε εισάγει.");
                        }
                        else
                        {
                            SqlDataAdapter SearchAdapt2 = new SqlDataAdapter("select dbo.nvl(Max(Id)+1,1000) from Customers", sqlcon);
                            DataTable dt2 = new DataTable();
                            SearchAdapt2.Fill(dt2);
                            if (dt2.Rows.Count == 1)
                            {
                                CustomerId = dt2.Rows[0][0].ToString();
                            }
                            SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                            SqlCommand InsCmd1 = sqlcon.CreateCommand();
                            InsCmd1.Connection = sqlcon;
                            SqlCommand InsCmd2 = sqlcon.CreateCommand();
                            InsCmd2.Connection = sqlcon;
                            SqlCommand InsCmd3 = sqlcon.CreateCommand();
                            InsCmd3.Connection = sqlcon;
                            SqlCommand InsCmd4 = sqlcon.CreateCommand();
                            InsCmd4.Connection = sqlcon;
                            InsCmd1.Transaction = InsTrans;
                            InsCmd2.Transaction = InsTrans;
                            InsCmd3.Transaction = InsTrans;
                            InsCmd4.Transaction = InsTrans;
                            try
                            {
                                InsCmd1.CommandText = "insert into Customers(Id, Name, Occupation, Address, Tk, Afm, Tax_office, Phone, Email, Credit, Retail, Region) values(@id, @name, @occup, @addr, @tk, @afm, @to, @phone, @email, @credit, @ret, @reg)";
                                InsCmd1.Parameters.AddWithValue("@id", CustomerId);
                                InsCmd1.Parameters.AddWithValue("@name", NameTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@occup", OccupationTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@addr", AddressTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@tk", TkTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@afm", AfmTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@to", Tax_officeTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@phone", PhoneTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@email", EmailTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@credit", ((CreditTxt.Text == "") ? "0" : CreditTxt.Text));
                                InsCmd1.Parameters.AddWithValue("@ret", (RetailBox.Checked ? "1" : "0"));
                                InsCmd1.Parameters.AddWithValue("@reg", RegionTxt.Text);
                                InsCmd1.ExecuteNonQuery();
                                if (Phone2Txt.Text != "")
                                {
                                    InsCmd2.CommandText = "insert into CustomersPhone2 (Id, CustomerId, Phone2) values((select dbo.nvl(Max(Id) + 1, 0) from CustomersPhone2), (select dbo.nvl(Max(Id),1000) from dbo.Customers), @phone2)";
                                    InsCmd2.Parameters.AddWithValue("@phone2", Phone2Txt.Text);
                                    InsCmd2.ExecuteNonQuery();
                                }

                                if (VatTxt.Text != "")
                                {
                                    InsCmd3.CommandText = "insert into CustomersVat (Id,CustomerId,Vat) values ((select dbo.nvl(Max(Id)+1,0) from CustomersVat),(select dbo.nvl(Max(Id),1000) from dbo.Customers),@vat)";
                                    InsCmd3.Parameters.AddWithValue("@vat", VatTxt.Text);
                                    InsCmd3.ExecuteNonQuery();
                                }
                                if (MaxCreditTxt.Text != "")
                                {
                                    InsCmd4.CommandText = "insert into CustomersMaxCredit (Id,CustomerId,MaxCredit) values ((select dbo.nvl(Max(Id)+1,0) from CustomersMaxCredit),(select dbo.nvl(Max(Id),1000) from dbo.Customers),@maxcredit)";
                                    InsCmd4.Parameters.AddWithValue("@maxcredit", MaxCreditTxt.Text);
                                    InsCmd4.ExecuteNonQuery();
                                }
                                InsTrans.Commit();
                                MessageBox.Show("Ο Πελάτης προστέθηκε με επιτυχία.");
                                CustomerName = NameTxt.Text;
                                CustomerOccupation = OccupationTxt.Text;
                                CustomerAddress = AddressTxt.Text;
                                CustomerTk = TkTxt.Text;
                                CustomerTax_office = Tax_officeTxt.Text;
                                CustomerPhone = PhoneTxt.Text;
                                CustomerPhone2 = Phone2Txt.Text;
                                CustomerEmail = EmailTxt.Text;
                                CustomerAfm = AfmTxt.Text;
                                CustomerRegion = RegionTxt.Text;
                                CustomerCredit = ((CreditTxt.Text == "") ? "0" : CreditTxt.Text);
                                CustomerRetail = (RetailBox.Checked ? "1" : "0");
                                ClearValues();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                try
                                {
                                    InsTrans.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
        }

        private void ClearValues()
        {
            NameTxt.Text = "";
            OccupationTxt.Text = "";
            AfmTxt.Text = "";
            AddressTxt.Text = "";
            TkTxt.Text = "";
            VatTxt.Text = "";
            PhoneTxt.Text = "";
            Phone2Txt.Text = "";
            EmailTxt.Text = "";
            Tax_officeTxt.Text = "";
            CreditTxt.Text = "";
            MaxCreditTxt.Text = "";
            RegionTxt.Text = "";
            RetailBox.Checked = false;
        }

        private void AddPhoneBtn_Click(object sender, EventArgs e)
        {
            if (Phone2Txt.Visible == true)
            {
                Phone2Txt.Visible = false;
                Phone2Txt.Text = "";
            }
            else
            {
                Phone2Txt.Visible = true;
            }
        }

        private void MaxCreditTxt_TextChanged(object sender, EventArgs e)
        {
            MaxCreditTxt.Text = MaxCreditTxt.Text.Replace(',', '.');
            MaxCreditTxt.SelectionStart = MaxCreditTxt.Text.Length;
        }

        private void CreditTxt_TextChanged(object sender, EventArgs e)
        {
            CreditTxt.Text = CreditTxt.Text.Replace(',', '.');
            CreditTxt.SelectionStart = CreditTxt.Text.Length;
        }
        
    }
}
