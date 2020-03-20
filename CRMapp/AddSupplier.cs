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
    public partial class AddSupplier : UserControl
    {
        Checks chk = new Checks();
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();

        public AddSupplier()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            string errorMessages = "";
            if (NameTxt.Text == "" || OccupationTxt.Text == "" || AfmTxt.Text == "" || AddressTxt.Text == "" || RegionTxt.Text == "" || TkTxt.Text == "" || PhoneTxt.Text == "" || Tax_officeTxt.Text == "")
            {
                errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα πεδία με αστερίσκο (*).\n"; ;
            }
            else
            {
                errorMessages += chk.CheckAfm(AfmTxt.Text);
                errorMessages += chk.CheckTk(TkTxt.Text);
                errorMessages += chk.CheckPhone(PhoneTxt.Text);
                if (Phone2Txt.Visible == true)
                {
                    errorMessages += chk.CheckPhone(Phone2Txt.Text);
                }
                if (DebitTxt.Text != "")
                {
                    errorMessages += chk.CheckDebit(DebitTxt.Text);
                }
                if (MaxDebitTxt.Text != "")
                {
                    errorMessages += chk.CheckMaxDebit(MaxDebitTxt.Text);
                }
                if (EmailTxt.Text != "")
                {
                    errorMessages += chk.CheckEmail(EmailTxt.Text);
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
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from Suppliers where Afm=@parameter", sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"parameter", AfmTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Υπάρχει ήδη καταχωρημένος προμηθευτής με το Α.Φ.Μ. που έχετε εισάγει.");
                        }
                        else
                        {
                            SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                            SqlCommand InsCmd1 = sqlcon.CreateCommand();
                            InsCmd1.Connection = sqlcon;
                            InsCmd1.Transaction = InsTrans;
                            try
                            {
                                InsCmd1.CommandText = "insert into Suppliers(Id, Name, Occupation, Address, Tk, Afm, Tax_office,Phone,Email, Debit,Region,Manager,Phone2) values((select dbo.nvl(Max(Id)+1,1000) from dbo.Suppliers), @name, @occup, @addr, @tk, @afm, @to,@phone,@mail, @debit, @reg, @man, @phone2)";
                                InsCmd1.Parameters.AddWithValue("@name", NameTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@occup", OccupationTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@addr", AddressTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@tk", TkTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@afm", AfmTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@to", Tax_officeTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@phone", PhoneTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@mail", EmailTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@debit", ((DebitTxt.Text == "") ? "0" : DebitTxt.Text));
                                InsCmd1.Parameters.AddWithValue("@reg", RegionTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@man", ManagerTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@phone2", Phone2Txt.Text);
                                InsCmd1.ExecuteNonQuery();
                                if (MaxDebitTxt.Text != "")
                                {
                                    InsCmd1.CommandText = "insert into SuppliersMaxDebit (Id,SupplierId,MaxDebit) values ((select dbo.nvl(Max(Id)+1,0) from SuppliersMaxDebit),(select dbo.nvl(Max(Id),1000) from dbo.Suppliers),@maxdebit)";
                                    InsCmd1.Parameters.AddWithValue("@maxdebit", MaxDebitTxt.Text);
                                    InsCmd1.ExecuteNonQuery();
                                }
                                InsTrans.Commit();
                                MessageBox.Show("Ο Προμηθευτής προστέθηκε με επιτυχία.");
                                ClearValues();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                try
                                {
                                    InsTrans.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
            Tax_officeTxt.Text = "";
            DebitTxt.Text = "";
            MaxDebitTxt.Text = "";
            PhoneTxt.Text = "";
            RegionTxt.Text = "";
            ManagerTxt.Text = "";
            Phone2Txt.Text = "";
            EmailTxt.Text = "";
        }

        private void DebitTxt_TextChanged(object sender, EventArgs e)
        {
            DebitTxt.Text = DebitTxt.Text.Replace(',', '.');
            DebitTxt.SelectionStart = DebitTxt.Text.Length;
        }

        private void MaxDebitTxt_TextChanged(object sender, EventArgs e)
        {
            MaxDebitTxt.Text = MaxDebitTxt.Text.Replace(',', '.');
            MaxDebitTxt.SelectionStart = MaxDebitTxt.Text.Length;
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
    }
}
