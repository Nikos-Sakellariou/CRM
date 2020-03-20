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

namespace CRMapp
{
    public partial class AddCustomer : UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();

        public AddCustomer()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            string errorMessages = "";
            if (NameTxt.Text == "" || OccupationTxt.Text == "" || AfmTxt.Text == "" || AddressTxt.Text == "" || RegionTxt.Text == "" || PhoneTxt.Text == "" || Tax_officeTxt.Text == "")
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
                if (EmailTxt.Text!="")
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
            if (errorMessages!="")
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
                                InsCmd1.CommandText = "insert into Customers(Id, Name, Occupation, Address, Tk, Afm, Tax_office, Phone, Email, Credit, Retail, Region) values((select dbo.nvl(Max(Id)+1,1000) from dbo.Customers), @name, @occup, @addr, @tk, @afm, @to, @phone, @email, @credit, @ret, @reg)";
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

        private void CreditTxt_TextChanged(object sender, EventArgs e)
        {
            if (CreditTxt.Focused)
            {
                CreditTxt.Text = CreditTxt.Text.Replace(',', '.');
                CreditTxt.SelectionStart = CreditTxt.Text.Length;
            }
        }

        private void MaxCreditTxt_TextChanged(object sender, EventArgs e)
        {
            if (MaxCreditTxt.Focused)
            {
                MaxCreditTxt.Text = MaxCreditTxt.Text.Replace(',', '.');
                MaxCreditTxt.SelectionStart = MaxCreditTxt.Text.Length;
            }
        }
        
    }
}
