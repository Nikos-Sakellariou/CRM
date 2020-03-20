using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace CRMapp
{
    public partial class Main : Form
    {

        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Thread Alerts;
        Thread Notes;

        public Main()
        {
            GC.AddMemoryPressure(102400000);
            con = new Checks().Initiallize_con();
            InitializeComponent();
            HeaderPanel.Focus();
            BackgroundNotesLoader_Load(null, null);
            NotesToolTip.SetToolTip(NotesBox, "Σημειώσεις");
            AlertsToolTip.SetToolTip(AlertsProdCustSupBox, "Ειδοποιήσεις Αποθήκης-Προμηθευτών-Πελατών");
            AlertDocsToolTip.SetToolTip(AlertsDocsBox, "Ειδοποιήσεις Παραστατικών");
            SettingsToolTip.SetToolTip(SettingsBox, "Ρυθμίσεις");
            NotesControlPanel_ControlAdded(null, null);
            //AlertsPanel_ControlAdded(null, null); // mhpws einai peritto?
            Notes = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                while (true)
                {
                    ThreadHelperClass.SetText(this, TimeTxtBox, System.DateTime.Now.ToShortTimeString());
                    Thread.Sleep(1000);
                }
            });
            Notes.Start();
            Alerts = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                while (true)
                {
                    if (AlertsPanel.Visible == false)
                    {
                        Alerts_Loader1();
                    }
                    if (AlertsDocsPanel.Visible == false)
                    {
                        Alerts_Loader2();
                    }
                    Thread.Sleep(60000);
                }
            });
            Alerts.Start();
        }
        
        public static class ThreadHelperClass
        {
            delegate void SetTextCallback(Form f, Control ctrl, string text);

            public static void SetText(Form form, Control ctrl, string text)
            {
                if (ctrl.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    form.Invoke(d, new object[] { form, ctrl, text });
                }
                else
                {
                    ctrl.Text = text;
                }
            }

            delegate void ClearControlsCallback(Form f, Control ctrl);

            public static void ClearControls(Form form, Control ctrl)
            {
                if (ctrl.InvokeRequired)
                {
                    ClearControlsCallback d = new ClearControlsCallback(ClearControls);
                    form.Invoke(d, new object[] { form, ctrl });
                }
                else
                {
                    ctrl.Controls.Clear();
                }
            }

            delegate void AddControlCallback(Form f, Control ctrl, UserControl userctrl);

            public static void AddControl(Form form, Control ctrl, UserControl userctr)
            {
                if (ctrl.InvokeRequired)
                {
                    AddControlCallback d = new AddControlCallback(AddControl);
                    form.Invoke(d, new object[] { form, ctrl, userctr });
                }
                else
                {
                    ctrl.Controls.Add(userctr);
                }
            }

            delegate void SetViewDockCallback(Form f, Control ctrl, DockStyle dock);

            public static void SetViewDock(Form form, Control ctrl, DockStyle dock)
            {
                if (ctrl.InvokeRequired)
                {
                    SetViewDockCallback d = new SetViewDockCallback(SetViewDock);
                    form.Invoke(d, new object[] { form, ctrl, dock });
                }
                else
                {
                    ctrl.Dock = dock;
                }
            }
        }

        void FormClose(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
            BackgroundNotesLoader_Load(null, null);
        }



        private void NotesBox_Click(object sender, EventArgs e)
        {
            AlertsPanel.Visible = false;
            if (NotesPanel.Visible == true)
            {
                NotesPanel.Visible = false;
            }
            else
            {
                NotesPanel.Visible = true;
            }
        }

        private void AlertsBox_Click(object sender, EventArgs e)
        {
            NotesPanel.Visible = false;
            AlertsDocsPanel.Visible = false;
            if (AlertsPanel.Visible == true)
            {
                AlertsPanel.Visible = false;
            }
            else
            {
                Alerts_Loader1();
                if (AlertsPanel.Controls.Count>0)
                {
                    AlertsPanel.Visible = true;
                }
            }
        }

        private void AlertsDocsBox_Click(object sender, EventArgs e)
        {
            NotesPanel.Visible = false;
            AlertsPanel.Visible = false;
            if (AlertsDocsPanel.Visible == true)
            {
                AlertsDocsPanel.Visible = false;
            }
            else
            {
                Alerts_Loader2();
                if (AlertsDocsPanel.Controls.Count > 0)
                {
                    AlertsDocsPanel.Visible = true;
                }
            }
        }

        private void SettingsBox_Click(object sender, EventArgs e)
        {
            AlertsPanel.Visible = false;
            NotesPanel.Visible = false;
            Settings SettingsControl = new Settings();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(SettingsControl);
            HeaderLbl.Text = "Ρυθμίσεις";
        }

        private void ExitBox_Click(object sender, EventArgs e)
        {
            Notes.Abort();
            Alerts.Abort();
            System.Windows.Forms.Application.Exit();
        }

        private void MaximizeBox_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void MinimizeBox_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void DocumentButton_Click(object sender, EventArgs e)
        {
            if (SalesButton.Visible == false)
            {
                PrintButton.Visible = true;
                OrdersButton.Visible = true;
                SalesButton.Visible = true;
            }
            else
            {
                SalesButton.Visible = false;
                OrdersButton.Visible = false;
                PrintButton.Visible = false;
            }
            AddCustomerButton.Visible = false;
            EditCustomerButton.Visible = false;
            CardCustomerBtn.Visible = false;
            CardSupplierBtn.Visible = false;
            AddProductButton.Visible = false;
            EditProductButton.Visible = false;
            AddSupplierButton.Visible = false;
            EditSupplierButton.Visible = false;
        }

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            SalesButton.Visible = false;
            OrdersButton.Visible = false;
            PrintButton.Visible = false;
            AddCustomerButton.Visible = false;
            EditCustomerButton.Visible = false;
            AddProductButton.Visible = false;
            EditProductButton.Visible = false;
            AddSupplierButton.Visible = false;
            EditSupplierButton.Visible = false;
            CardCustomerBtn.Visible = false;
            CardSupplierBtn.Visible = false;
        }

        private void CustomersButton_Click(object sender, EventArgs e)
        {
            if (AddCustomerButton.Visible == false)
            {
                CardCustomerBtn.Visible = true;
                EditCustomerButton.Visible = true;
                AddCustomerButton.Visible = true;
            }
            else
            {
                AddCustomerButton.Visible = false;
                EditCustomerButton.Visible = false;
                CardCustomerBtn.Visible = false;
            }
            SalesButton.Visible = false;
            OrdersButton.Visible = false;
            PrintButton.Visible = false;
            AddProductButton.Visible = false;
            EditProductButton.Visible = false;
            AddSupplierButton.Visible = false;
            EditSupplierButton.Visible = false;
            CardSupplierBtn.Visible = false;

        }

        private void ProductsButton_Click(object sender, EventArgs e)
        {
            if (AddProductButton.Visible == false)
            {
                EditProductButton.Visible = true;
                AddProductButton.Visible = true;
            }
            else
            {
                AddProductButton.Visible = false;
                EditProductButton.Visible = false;
            }
            SalesButton.Visible = false;
            OrdersButton.Visible = false;
            PrintButton.Visible = false;
            AddCustomerButton.Visible = false;
            EditCustomerButton.Visible = false;
            AddSupplierButton.Visible = false;
            EditSupplierButton.Visible = false;
            CardCustomerBtn.Visible = false;
            CardSupplierBtn.Visible = false;
        }

        private void SuppliersButton_Click(object sender, EventArgs e)
        {
            if (AddSupplierButton.Visible == false)
            {
                CardSupplierBtn.Visible = true;
                EditSupplierButton.Visible = true;
                AddSupplierButton.Visible = true;
            }
            else
            {
                EditSupplierButton.Visible = false;
                AddSupplierButton.Visible = false;
                CardSupplierBtn.Visible = false;
            }
            SalesButton.Visible = false;
            OrdersButton.Visible = false;
            PrintButton.Visible = false;
            AddCustomerButton.Visible = false;
            EditCustomerButton.Visible = false;
            AddProductButton.Visible = false;
            EditProductButton.Visible = false;
            CardCustomerBtn.Visible = false;
        }

        private void AddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomer AddCustomerControl = new AddCustomer();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(AddCustomerControl);
            HeaderLbl.Text="Προσθήκη Πελάτη";
        }

        private void EditCustomerButton_Click(object sender, EventArgs e)
        {
            EditCustomer EditCustomerControl = new EditCustomer();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(EditCustomerControl);
            HeaderLbl.Text = "Επεξεργασία Πελάτη";
        }

        private void CardCustomerBtn_Click(object sender, EventArgs e)
        {
            CardCustomer CardCustomerControl = new CardCustomer();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(CardCustomerControl);
            HeaderLbl.Text = "Καρτέλα Κινήσεων Πελάτη";
        }


        private void AddProductButton_Click(object sender, EventArgs e)
        {
            AddProduct AddProductControl = new AddProduct();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(AddProductControl);
            HeaderLbl.Text = "Προσθήκη Προϊόντος";
        }

        private void EditProductButton_Click(object sender, EventArgs e)
        {
            EditProduct EditProductControl = new EditProduct();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(EditProductControl);
            HeaderLbl.Text = "Επεξεργασία Προϊόντος";
        }

        private void AddSupplierButton_Click(object sender, EventArgs e)
        {
            AddSupplier AddSupplierControl = new AddSupplier();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(AddSupplierControl);
            HeaderLbl.Text = "Προσθήκη Προμηθευτή";
        }

        private void EditSupplierButton_Click(object sender, EventArgs e)
        {
            EditSupplier EditSupplierControl = new EditSupplier();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(EditSupplierControl);
            HeaderLbl.Text = "Επεξεργασία Προμηθευτή";
        }

        private void CardSupplierBtn_Click(object sender, EventArgs e)
        {
            CardSupplier CardSupplierControl = new CardSupplier();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(CardSupplierControl);
            HeaderLbl.Text = "Καρτέλα Κινήσεων Προμηθευτή";
        }


        private void AddNoteBtn_Click(object sender, EventArgs e)
        {
            AddNoteForm AddNoteF = new AddNoteForm();
            AddNoteF.FormClosed += new FormClosedEventHandler(FormClose);
            this.Enabled = false;
            NotesPanel.Visible = false;
            AddNoteF.ShowDialog();
        }

        private void ToolsPanel_Paint(object sender, PaintEventArgs e)
        {
            PointF[] pta = new PointF[4];
            pta[0] = new Point(0, 36);
            pta[1] = new Point(30, 4);
            pta[2] = new Point(170, 4);
            pta[3] = new Point(200, 36);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillPolygon(SystemBrushes.ActiveCaption, pta);      
        }
        

        private void BackgroundNotesLoader_Load(object sender, DoWorkEventArgs e)
        {
            NotesControlPanel.Controls.Clear();
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select Id from Notes where Date<=Convert(date,GETDATE())";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                NotificationView Noteview = new NotificationView(dt.Rows[i][0].ToString());
                                NotesControlPanel.Controls.Add(Noteview);
                                Noteview.Dock = DockStyle.Top;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση σημείωσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }

        private void Alerts_Loader1()
        {
            this.SuspendLayout();
            ThreadHelperClass.ClearControls(this, AlertsPanel);
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        //Υπέρβαση Συνολικού Ορίου Πίστωσης
                        string query0 = "select dbo.MaxTotalDebit_Alert_Proc()";
                        SqlDataAdapter SearchAdapt0 = new SqlDataAdapter(query0, sqlcon);
                        DataTable dt0 = new DataTable();
                        SearchAdapt0.Fill(dt0);
                        if (dt0.Rows.Count == 1)
                        {
                            if (dt0.Rows[0][0].ToString() == "1")
                            {
                                AlertView Alertview = new AlertView("Υπέρβαση Ορίου Πίστωσης", "Έχετε υπερβεί το μέγιστο όριο πίστωσης στους προμηθευτές σας.", System.Drawing.Color.LightGray);
                                ThreadHelperClass.AddControl(this, AlertsPanel, Alertview);
                                ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                            }
                        }

                        //Υπέρβαση Συνολικού Ορίου Χρέωσης
                        string query1 = "select dbo.MaxTotalCredit_Alert_Proc()";
                        SqlDataAdapter SearchAdapt1 = new SqlDataAdapter(query1, sqlcon);
                        DataTable dt1 = new DataTable();
                        SearchAdapt1.Fill(dt1);
                        if (dt1.Rows.Count == 1)
                        {
                            if (dt1.Rows[0][0].ToString() == "1")
                            {
                                AlertView Alertview = new AlertView("Υπέρβαση Ορίου Χρέωσης", "Έχετε υπερβεί το μέγιστο όριο παροχής πίστωσης στους πελάτες σας.", System.Drawing.Color.LightGray);
                                ThreadHelperClass.AddControl(this, AlertsPanel, Alertview);
                                ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                            }
                        }
                        
                        //Ελάχιστο stock προϊόντων
                        string query3 = "select Description,ProductId,MinStock from ProductsMinStock a,ProductsReserveView b where a.ProductId=b.Id and Cast(MinStock As decimal (18,5))> Cast(b.Quant As decimal(18,5))";
                        SqlDataAdapter SearchAdapt3 = new SqlDataAdapter(query3, sqlcon);
                        DataTable dt3 = new DataTable();
                        SearchAdapt3.Fill(dt3);
                        if (dt3.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt3.Rows.Count; i++)
                            {
                                AlertView Alertview = new AlertView("Ελάχιστο Stock Προϊόντος", "Το προϊόν: ", System.Drawing.Color.LightCyan);
                                Alertview.AppendBoldText(dt3.Rows[i][0].ToString());
                                Alertview.AppendText(" με κωδικό: ");
                                Alertview.AppendBoldText(dt3.Rows[i][1].ToString());
                                Alertview.AppendText(" βρίσκεται κάτω του ελαχίστου.");
                                ThreadHelperClass.AddControl(this, AlertsPanel, Alertview);
                                ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                            }
                        }

                        //Υπέρβαση Μέγιστου Ορίου Πίστωσης Πελάτη
                        string query4 = "select Name,Id from CustomerOverCredit";
                        SqlDataAdapter SearchAdapt4 = new SqlDataAdapter(query4, sqlcon);
                        DataTable dt4 = new DataTable();
                        SearchAdapt4.Fill(dt4);
                        if (dt4.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt4.Rows.Count; i++)
                            {
                                AlertView Alertview = new AlertView("Υπέρβαση Πίστωσης Πελάτη", "Ο πελάτης: ", System.Drawing.Color.Aquamarine);
                                Alertview.AppendBoldText(dt4.Rows[i][0].ToString());
                                Alertview.AppendText(" με κωδικό: ");
                                Alertview.AppendBoldText(dt4.Rows[i][1].ToString());
                                Alertview.AppendText(" έχει υπερβεί το όριο πίστωσης.");
                                ThreadHelperClass.AddControl(this, AlertsPanel, Alertview);
                                ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                            }
                        }

                        //Υπέρβαση Μέγιστου Ορίου Πίστωσης Προμηθευτή
                        string query5 = "select Name,Id from SupplierOverDebit";
                        SqlDataAdapter SearchAdapt5 = new SqlDataAdapter(query5, sqlcon);
                        DataTable dt5 = new DataTable();
                        SearchAdapt5.Fill(dt5);
                        if (dt5.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt5.Rows.Count; i++)
                            {
                                AlertView Alertview = new AlertView("Υπέρβαση Πίστωσης Προμηθευτή", "Ο προμηθευτής: ", System.Drawing.Color.AntiqueWhite);
                                Alertview.AppendText(" με κωδικό: ");
                                Alertview.AppendBoldText(dt5.Rows[i][1].ToString());
                                Alertview.AppendText(" έχει υπερβεί το όριο πίστωσης.");
                                ThreadHelperClass.AddControl(this, AlertsPanel, Alertview);
                                ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                            }
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση των ειδοποιήσεων. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            this.ResumeLayout(false);
        }
        private void Alerts_Loader2()
        {
            this.SuspendLayout();
            ThreadHelperClass.ClearControls(this, AlertsDocsPanel);
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        //Δελτία Αποστολής Προμηθευτών σε Εκκρεμότητα
                        string query2 = "select DisNoteId,DisNoteSeries,DisNoteDate,Name from dbo.SupplierDisNote a, dbo.Suppliers b where Invoice='0' and a.SupplierId=b.Id";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                DateTime dat = Convert.ToDateTime(dt2.Rows[i][2].ToString());
                                if (dat.Month < System.DateTime.Today.Month || dat.Year < System.DateTime.Today.Year)
                                {
                                    AlertView Alertview = new AlertView("Δ.Α. Προμηθευτή σε Εκκρεμότητα", "Δεν έχει εκδοθεί Τιμολόγιο για το Δελτίο Αποστολής με αριθμό: ", System.Drawing.Color.LightCoral);
                                    Alertview.AppendBoldText(dt2.Rows[i][0].ToString());
                                    Alertview.AppendText(" και σειρά: ");
                                    Alertview.AppendBoldText(dt2.Rows[i][1].ToString());
                                    Alertview.AppendText(" του προμηθευτή: ");
                                    Alertview.AppendBoldText(dt2.Rows[i][3].ToString());
                                    Alertview.AppendText(".");
                                    ThreadHelperClass.AddControl(this, AlertsDocsPanel, Alertview);
                                    ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                                }
                                else if (System.DateTime.Today.Day >= 25)
                                {
                                    AlertView Alertview = new AlertView("Δ.Α. Προμηθευτή σε Εκκρεμότητα", "Δεν έχει εκδοθεί Τιμολόγιο για το Δελτίο Αποστολής με αριθμό: ", System.Drawing.Color.LightGoldenrodYellow);
                                    Alertview.AppendBoldText(dt2.Rows[i][0].ToString());
                                    Alertview.AppendText(" και σειρά: ");
                                    Alertview.AppendBoldText(dt2.Rows[i][1].ToString());
                                    Alertview.AppendText(" του προμηθευτή: ");
                                    Alertview.AppendBoldText(dt2.Rows[i][3].ToString());
                                    Alertview.AppendText(".");
                                    ThreadHelperClass.AddControl(this, AlertsDocsPanel, Alertview);
                                    ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                                }
                            }
                        }

                        //Δελτία Αποστολής Επιστροφής Προμηθευτών σε Εκκρεμότητα
                        string query3 = "select DisNoteId,DisNoteSeries,DisNoteDate,Name from dbo.SupplierReturnDisNote a, dbo.Suppliers b where DebitInvoice='0' and a.SupplierId=b.Id";
                        SqlDataAdapter SearchAdapt3 = new SqlDataAdapter(query3, sqlcon);
                        DataTable dt3 = new DataTable();
                        SearchAdapt3.Fill(dt3);
                        if (dt3.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt3.Rows.Count; i++)
                            {
                                DateTime dat = Convert.ToDateTime(dt3.Rows[i][2].ToString());
                                if (dat.Month < System.DateTime.Today.Month || dat.Year < System.DateTime.Today.Year)
                                {
                                    AlertView Alertview = new AlertView("Δ.Α. Προμηθευτή σε Εκκρεμότητα", "Δεν έχει εκδοθεί Πιστωτικό Τιμολόγιο για το Δελτίο Αποστολής με αριθμό: ", System.Drawing.Color.LightCoral);
                                    Alertview.AppendBoldText(dt3.Rows[i][0].ToString());
                                    Alertview.AppendText(" και σειρά: ");
                                    Alertview.AppendBoldText(dt3.Rows[i][1].ToString());
                                    Alertview.AppendText(" του προμηθευτή: ");
                                    Alertview.AppendBoldText(dt3.Rows[i][3].ToString());
                                    Alertview.AppendText(".");
                                    ThreadHelperClass.AddControl(this, AlertsDocsPanel, Alertview);
                                    ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                                }
                                else if (System.DateTime.Today.Day >= 25)
                                {
                                    AlertView Alertview = new AlertView("Δ.Α. Προμηθευτή σε Εκκρεμότητα", "Δεν έχει εκδοθεί Πιστωτικό Τιμολόγιο για το Δελτίο Αποστολής με αριθμό: ", System.Drawing.Color.LightGoldenrodYellow);
                                    Alertview.AppendBoldText(dt3.Rows[i][0].ToString());
                                    Alertview.AppendText(" και σειρά: ");
                                    Alertview.AppendBoldText(dt3.Rows[i][1].ToString());
                                    Alertview.AppendText(" του προμηθευτή: ");
                                    Alertview.AppendBoldText(dt3.Rows[i][3].ToString());
                                    Alertview.AppendText(".");
                                    ThreadHelperClass.AddControl(this, AlertsDocsPanel, Alertview);
                                    ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                                }
                            }
                        }

                        //Δελτία Αποστολής Πελατών σε Εκκρεμότητα
                        string query6 = "select DisNoteId,DisNoteSeries,DisNoteDate,Name from dbo.CustomerDisNote a, dbo.Customers b where Invoice='0' and a.CustomerId=b.Id";
                        SqlDataAdapter SearchAdapt6 = new SqlDataAdapter(query6, sqlcon);
                        DataTable dt6 = new DataTable();
                        SearchAdapt6.Fill(dt6);
                        if (dt6.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt6.Rows.Count; i++)
                            {
                                DateTime dat = Convert.ToDateTime(dt6.Rows[i][2].ToString());
                                if (dat.Month < System.DateTime.Today.Month || dat.Year < System.DateTime.Today.Year)
                                {
                                    AlertView Alertview = new AlertView("Δ.Α. Πελάτη σε Εκκρεμότητα", "Δεν έχει εκδοθεί Τιμολόγιο για το Δελτίο Αποστολής με αριθμό: ", System.Drawing.Color.LightCoral);
                                    Alertview.AppendBoldText(dt6.Rows[i][0].ToString());
                                    Alertview.AppendText(" και σειρά: ");
                                    Alertview.AppendBoldText(dt6.Rows[i][1].ToString());
                                    Alertview.AppendText(" του πελάτη: ");
                                    Alertview.AppendBoldText(dt6.Rows[i][3].ToString());
                                    Alertview.AppendText(".");
                                    ThreadHelperClass.AddControl(this, AlertsDocsPanel, Alertview);
                                    ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                                }
                                else if (System.DateTime.Today.Day >= 25)
                                {
                                    AlertView Alertview = new AlertView("Δ.Α. Πελάτη σε Εκκρεμότητα", "Δεν έχει εκδοθεί Τιμολόγιο για το Δελτίο Αποστολής με αριθμό: ", System.Drawing.Color.LightGoldenrodYellow);
                                    Alertview.AppendBoldText(dt6.Rows[i][0].ToString());
                                    Alertview.AppendText(" και σειρά: ");
                                    Alertview.AppendBoldText(dt6.Rows[i][1].ToString());
                                    Alertview.AppendText(" του πελάτη: ");
                                    Alertview.AppendBoldText(dt6.Rows[i][3].ToString());
                                    Alertview.AppendText(".");
                                    ThreadHelperClass.AddControl(this, AlertsDocsPanel, Alertview);
                                    ThreadHelperClass.SetViewDock(this, Alertview, DockStyle.Top);
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση των ειδοποιήσεων. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            this.ResumeLayout(false);
        }

        private void NotesControlPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            NotesLbl.Text = ((NotesControlPanel.Controls.Count == 0) ? "" : NotesControlPanel.Controls.Count.ToString());
            NotesLbl.Parent = NotesBox;
            if (NotesControlPanel.Controls.Count>9)
            {
                NotesControlPanel.Height = 200;
                NotesLbl.Location = new Point(11, 0);
                NotesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NotesBox.Refresh();
            }
            else
            {
                if (NotesControlPanel.Controls.Count == 1)
                {
                    NotesControlPanel.Height = 62;
                }
                else if (NotesControlPanel.Controls.Count == 2)
                {
                    NotesControlPanel.Height = 124;
                }
                else if (NotesControlPanel.Controls.Count == 3)
                {
                    NotesControlPanel.Height = 186;
                }
                else
                {
                    NotesControlPanel.Height = 200;
                }
                NotesLbl.Location = new Point(17, 0);
                NotesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NotesBox.Refresh();
            }
        }

        private void NotesControlPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            NotesLbl.Text = ((NotesControlPanel.Controls.Count == 0) ? "" : NotesControlPanel.Controls.Count.ToString());
            NotesLbl.Parent = NotesBox;
            if (NotesControlPanel.Controls.Count > 9)
            {
                NotesControlPanel.Height = 200;
                NotesLbl.Location = new Point(11, 0);
                NotesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NotesBox.Refresh();
            }
            else
            {
                if (NotesControlPanel.Controls.Count == 1)
                {
                    NotesControlPanel.Height = 62;
                }
                else if (NotesControlPanel.Controls.Count == 2)
                {
                    NotesControlPanel.Height = 124;
                }
                else if (NotesControlPanel.Controls.Count == 3)
                {
                    NotesControlPanel.Height = 186;
                }
                else
                {
                    NotesControlPanel.Height = 200;
                }
                NotesLbl.Location = new Point(17, 0);
                NotesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NotesBox.Refresh();
            }
        }

        private void AlertsPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            AlertsLbl.Text = ((AlertsPanel.Controls.Count == 0) ? "" : AlertsPanel.Controls.Count.ToString());
            AlertsLbl.Parent = AlertsProdCustSupBox;
            if (AlertsPanel.Controls.Count > 9)
            {
                AlertsPanel.Height = 260;
                AlertsLbl.Location = new Point(11, 0);
                AlertsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AlertsProdCustSupBox.Refresh();
            }
            else
            {
                if (AlertsPanel.Controls.Count == 1)
                {
                    AlertsPanel.Height = 69;
                }
                else if (AlertsPanel.Controls.Count == 2)
                {
                    AlertsPanel.Height = 138;
                }
                else if (AlertsPanel.Controls.Count == 3)
                {
                    AlertsPanel.Height = 207;
                }
                else
                {
                    AlertsPanel.Height = 260;
                }
                AlertsLbl.Location = new Point(17, 0);
                AlertsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AlertsProdCustSupBox.Refresh();
            }
        }

        private void AlertsPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            AlertsLbl.Text = ((AlertsPanel.Controls.Count == 0) ? "" : AlertsPanel.Controls.Count.ToString());
            AlertsLbl.Parent = AlertsProdCustSupBox;
            if (AlertsPanel.Controls.Count > 9)
            {
                AlertsPanel.Height = 260;
                AlertsLbl.Location = new Point(11, 0);
                AlertsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AlertsProdCustSupBox.Refresh();
            }
            else
            {
                if (AlertsPanel.Controls.Count == 1)
                {
                    AlertsPanel.Height = 69;
                }
                else if (AlertsPanel.Controls.Count == 2)
                {
                    AlertsPanel.Height = 138;
                }
                else if (AlertsPanel.Controls.Count == 3)
                {
                    AlertsPanel.Height = 207;
                }
                else
                {
                    AlertsPanel.Height = 260;
                }
                AlertsLbl.Location = new Point(17, 0);
                AlertsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AlertsProdCustSupBox.Refresh();
            }
        }
        
        private void AlertsDocsPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            AlertsDocsLbl.Text = ((AlertsDocsPanel.Controls.Count == 0) ? "" : AlertsDocsPanel.Controls.Count.ToString());
            AlertsDocsLbl.Parent = AlertsDocsBox;
            if (AlertsDocsPanel.Controls.Count > 9)
            {
                AlertsDocsPanel.Height = 260;
                AlertsDocsLbl.Location = new Point(11, 0);
                AlertsDocsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AlertsDocsBox.Refresh();
            }
            else
            {
                if (AlertsDocsPanel.Controls.Count == 1)
                {
                    AlertsDocsPanel.Height = 69;
                }
                else if (AlertsDocsPanel.Controls.Count == 2)
                {
                    AlertsDocsPanel.Height = 138;
                }
                else if (AlertsDocsPanel.Controls.Count == 3)
                {
                    AlertsDocsPanel.Height = 207;
                }
                else
                {
                    AlertsDocsPanel.Height = 260;
                }
                AlertsDocsLbl.Location = new Point(17, 0);
                AlertsDocsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AlertsDocsBox.Refresh();
            }
        }

        private void AlertsDocsPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            AlertsDocsLbl.Text = ((AlertsDocsPanel.Controls.Count == 0) ? "" : AlertsDocsPanel.Controls.Count.ToString());
            AlertsDocsLbl.Parent = AlertsDocsBox;
            if (AlertsDocsPanel.Controls.Count > 9)
            {
                AlertsDocsPanel.Height = 260;
                AlertsDocsLbl.Location = new Point(11, 0);
                AlertsDocsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AlertsDocsBox.Refresh();
            }
            else
            {
                if (AlertsDocsPanel.Controls.Count == 1)
                {
                    AlertsDocsPanel.Height = 69;
                }
                else if (AlertsDocsPanel.Controls.Count == 2)
                {
                    AlertsDocsPanel.Height = 138;
                }
                else if (AlertsDocsPanel.Controls.Count == 3)
                {
                    AlertsDocsPanel.Height = 207;
                }
                else
                {
                    AlertsDocsPanel.Height = 260;
                }
                AlertsDocsLbl.Location = new Point(17, 0);
                AlertsDocsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AlertsDocsBox.Refresh();
            }
        }

        private void NotesBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (NotesControlPanel.Controls.Count > 9)
            {
                e.Graphics.FillEllipse(System.Drawing.Brushes.DarkRed, new Rectangle(new Point(13, 0), new Size(15, 15)));
            }
            else if (NotesControlPanel.Controls.Count != 0)
            {
                e.Graphics.FillEllipse(System.Drawing.Brushes.DarkRed, new Rectangle(new Point(17, 0), new Size(13, 13)));
            }
        }
        
        private void AlertsBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (AlertsPanel.Controls.Count > 9)
            {
                e.Graphics.FillEllipse(System.Drawing.Brushes.DarkRed, new Rectangle(new Point(13, 0), new Size(15, 15)));
            }
            else if (AlertsPanel.Controls.Count!=0)
            {
                e.Graphics.FillEllipse(System.Drawing.Brushes.DarkRed, new Rectangle(new Point(17, 0), new Size(13, 13)));
            }
        }

        private void AlertsDocsBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (AlertsDocsPanel.Controls.Count > 9)
            {
                e.Graphics.FillEllipse(System.Drawing.Brushes.DarkRed, new Rectangle(new Point(13, 0), new Size(15, 15)));
            }
            else if (AlertsDocsPanel.Controls.Count != 0)
            {
                e.Graphics.FillEllipse(System.Drawing.Brushes.DarkRed, new Rectangle(new Point(17, 0), new Size(13, 13)));
            }
        }

        private void SalesButton_Click(object sender, EventArgs e)
        {
            SaleKindCustomer SaleKindCustomerControl = new SaleKindCustomer();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(SaleKindCustomerControl);
            SaleKindCustomerControl.Controls.Find("InvoiceBtn", false).First().Click += (sender0, e0) =>
            {
                InvoiceCustomer InvoiceCustomerControl = new InvoiceCustomer();
                MainPanel.Controls.Add(InvoiceCustomerControl);
                InvoiceCustomerControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender00, e00) =>
                {
                    SaleKindCustomerControl.Visible = true;
                    InvoiceCustomerControl.Dispose();
                    GC.Collect(0);
                    HeaderLbl.Text = "Πελάτες";
                };
                HeaderLbl.Text = "Τιμολόγιο-Δ.Α. Πελάτη";
                SaleKindCustomerControl.Visible = false;
            };
            SaleKindCustomerControl.Controls.Find("ReceiptBtn", false).First().Click += (sender1, e1) =>
            {
                ReceiptCustomer ReceiptCustomerControl = new ReceiptCustomer();
                MainPanel.Controls.Add(ReceiptCustomerControl);
                ReceiptCustomerControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender11, e11) =>
                {
                    SaleKindCustomerControl.Visible = true;
                    ReceiptCustomerControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Πελάτες";
                };
                HeaderLbl.Text = "Απόδειξη Είσπραξης";
                SaleKindCustomerControl.Visible = false;
            };
            SaleKindCustomerControl.Controls.Find("DispatchNoteBtn", false).First().Click += (sender3, e3) =>
            {
                DisNoteCustomer DisNoteCustomerControl = new DisNoteCustomer();
                MainPanel.Controls.Add(DisNoteCustomerControl);
                DisNoteCustomerControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender33, e33) =>
                {
                    SaleKindCustomerControl.Visible = true;
                    DisNoteCustomerControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Πελάτες";
                };
                HeaderLbl.Text = "Δελτίο Αποστολής Πελάτη";
                SaleKindCustomerControl.Visible = false;
            };
            SaleKindCustomerControl.Controls.Find("InvoiceToDispNoteBtn", false).First().Click += (sender4, e4) =>
            {
                InvoiceDisNoteCustomer InvoiceDisNoteCustomerControl = new InvoiceDisNoteCustomer();
                MainPanel.Controls.Add(InvoiceDisNoteCustomerControl);
                InvoiceDisNoteCustomerControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender44, e44) =>
                {
                    SaleKindCustomerControl.Visible = true;
                    InvoiceDisNoteCustomerControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Πελάτες";
                };
                HeaderLbl.Text = "Τιμολόγηση Δ.Α. Πελάτη";
                SaleKindCustomerControl.Visible = false;
            };
            SaleKindCustomerControl.Controls.Find("DebitInvoiceNewBtn", false).First().Click += (sender5, e5) =>
            {
                DebitInvoiceCustomer DebitInvoiceCustomerControl = new DebitInvoiceCustomer();
                MainPanel.Controls.Add(DebitInvoiceCustomerControl);
                DebitInvoiceCustomerControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender55, e55) =>
                {
                    SaleKindCustomerControl.Visible = true;
                    DebitInvoiceCustomerControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Πελάτες";
                };
                HeaderLbl.Text = "Πιστωτικό Τιμολόγιο Πελάτη";
                SaleKindCustomerControl.Visible = false;
            };
            SaleKindCustomerControl.Controls.Find("DebitInvoiceOldBtn", false).First().Click += (sender6, e6) =>
            {
                DebitInvFromInvCustomer DebitInvFromInvCustomerControl = new DebitInvFromInvCustomer();
                MainPanel.Controls.Add(DebitInvFromInvCustomerControl);
                DebitInvFromInvCustomerControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender55, e55) =>
                {
                    SaleKindCustomerControl.Visible = true;
                    DebitInvFromInvCustomerControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Πελάτες";
                };
                HeaderLbl.Text = "Πιστωτικό Τιμολόγιο Πελάτη";
                SaleKindCustomerControl.Visible = false;
            };
            HeaderLbl.Text = "Πελάτες";
        }

        private void OrdersButton_Click(object sender, EventArgs e)
        {
            OrderKindSupplier OrderKindSupplierControl = new OrderKindSupplier();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(OrderKindSupplierControl);
            OrderKindSupplierControl.Controls.Find("InvoiceBtn", false).First().Click += (sender1, e1) =>
            {
                InvoiceSupplier InvoiceSupplierControl = new InvoiceSupplier();
                MainPanel.Controls.Add(InvoiceSupplierControl);
                InvoiceSupplierControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender11, e11) =>
                {
                    OrderKindSupplierControl.Visible = true;
                    InvoiceSupplierControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Προμηθευτές";
                };
                HeaderLbl.Text = "Τιμολόγιο-Δ.Α. Προμηθευτή";
                OrderKindSupplierControl.Visible = false;
            };
            OrderKindSupplierControl.Controls.Find("ReceiptBtn", false).First().Click += (sender2, e2) =>
            {
                ReceiptSupplier ReceiptSupplierControl = new ReceiptSupplier();
                MainPanel.Controls.Add(ReceiptSupplierControl);
                ReceiptSupplierControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender22, e22) =>
                {
                    OrderKindSupplierControl.Visible = true;
                    ReceiptSupplierControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Προμηθευτές";
                };
                HeaderLbl.Text = "Απόδειξη Πληρωμής";
                OrderKindSupplierControl.Visible = false;
            };
            OrderKindSupplierControl.Controls.Find("DispatchNoteBtn", false).First().Click += (sender3, e3) =>
            {
                DisNoteSupplier DisNoteSupplierControl = new DisNoteSupplier();
                MainPanel.Controls.Add(DisNoteSupplierControl);
                DisNoteSupplierControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender33, e33) =>
                {
                    OrderKindSupplierControl.Visible = true;
                    DisNoteSupplierControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Προμηθευτές";
                };
                HeaderLbl.Text = "Δελτίο Αποστολής Προμηθευτή";
                OrderKindSupplierControl.Visible = false;
            };
            OrderKindSupplierControl.Controls.Find("DispatchNoteToSupplierBtn", false).First().Click += (sender3, e3) =>
            {
                DisNoteReturnSupplier DisNoteSupplierControl = new DisNoteReturnSupplier();
                MainPanel.Controls.Add(DisNoteSupplierControl);
                DisNoteSupplierControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender33, e33) =>
                {
                    OrderKindSupplierControl.Visible = true;
                    DisNoteSupplierControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Προμηθευτές";
                };
                HeaderLbl.Text = "Δελτίο Αποστολής σε Προμηθευτή";
                OrderKindSupplierControl.Visible = false;
            };
            OrderKindSupplierControl.Controls.Find("InvoiceToDispNoteBtn", false).First().Click += (sender4, e4) =>
            {
                InvoiceDisNoteSupplier InvoiceDisNoteSupplierControl = new InvoiceDisNoteSupplier();
                MainPanel.Controls.Add(InvoiceDisNoteSupplierControl);
                InvoiceDisNoteSupplierControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender44, e44) =>
                {
                    OrderKindSupplierControl.Visible = true;
                    InvoiceDisNoteSupplierControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Προμηθευτές";
                };
                HeaderLbl.Text = "Τιμολόγηση Δ.Α. Προμηθευτή";
                OrderKindSupplierControl.Visible = false;
            };
            OrderKindSupplierControl.Controls.Find("DebitInvoiceNewBtn", false).First().Click += (sender5, e5) =>
            {
                DebitInvoiceSupplier DebitInvoiceSupplierControl = new DebitInvoiceSupplier();
                MainPanel.Controls.Add(DebitInvoiceSupplierControl);
                DebitInvoiceSupplierControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender55, e55) =>
                {
                    OrderKindSupplierControl.Visible = true;
                    DebitInvoiceSupplierControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Προμηθευτές";
                };
                HeaderLbl.Text = "Πιστωτικό Τιμολόγιο Προμηθευτή";
                OrderKindSupplierControl.Visible = false;
            };
            OrderKindSupplierControl.Controls.Find("DebitInvoiceOldBtn", false).First().Click += (sender5, e5) =>
            {
                DebitInvFromInvSupplier DebitInvFromInvSupplierControl = new DebitInvFromInvSupplier();
                MainPanel.Controls.Add(DebitInvFromInvSupplierControl);
                DebitInvFromInvSupplierControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender55, e55) =>
                {
                    OrderKindSupplierControl.Visible = true;
                    DebitInvFromInvSupplierControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Προμηθευτές";
                };
                HeaderLbl.Text = "Πιστωτικό Τιμολόγιο Προμηθευτή";
                OrderKindSupplierControl.Visible = false;
            };
            OrderKindSupplierControl.Controls.Find("ReceiptToSupplierBtn", false).First().Click += (sender6, e6) =>
            {
                ReceiptToSupplier ReceiptToSupplierControl = new ReceiptToSupplier();
                MainPanel.Controls.Add(ReceiptToSupplierControl);
                ReceiptToSupplierControl.Controls.Find("ProductsPanel", false).First().Controls.Find("FooterPanel", false).First().Controls.Find("ReturnBtn", false).First().Click += (sender66, e66) =>
                {
                    OrderKindSupplierControl.Visible = true;
                    ReceiptToSupplierControl.Dispose();
                    GC.Collect();
                    HeaderLbl.Text = "Προμηθευτές";
                };
                HeaderLbl.Text = "Απόδειξη Πληρωμής";
                OrderKindSupplierControl.Visible = false;
            };


            HeaderLbl.Text = "Προμηθευτές";
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            SalesPrint SalesPrintControl = new SalesPrint();
            MainPanel.Controls.Clear();
            GC.Collect();
            MainPanel.Controls.Add(SalesPrintControl);
            HeaderLbl.Text = "Εκτύπωση Παραστατικού";
        }
    }
}
