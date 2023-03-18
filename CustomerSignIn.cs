using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeApplianceRentalApplication
{
    public partial class frmCustomerSignIn : Form
    {
        List<CustomerAccount> customers;
        CustomerAccount newCustomer;
        frmAdminSignIn frmAdminSignIn = new frmAdminSignIn(); //For using fields and methods from another form
        int sec = 5;

        public frmCustomerSignIn(CustomerAccount newAcc) //Receiving the object from Customer sign up form
        {
            InitializeComponent();
            this.newCustomer = newAcc;
        }

        private void tmrCustomer_Tick(object sender, EventArgs e)
        {
            sec--;
            if(sec==0)
            {
                tmrCustomer.Stop();
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                cmdLogIn.Enabled = true;
                lblTimer.Text = "";
                frmAdminSignIn.times = 0;
                sec = 5;
                return;
            }
            lblTimer.Text = sec + " seconds remaining to re-enter into the account";
        }

        private void frmCustomerSignIn_Load(object sender, EventArgs e)
        {
            CustomerAccount customer1, customer2;
            customers = new List<CustomerAccount>();

            customer1= new CustomerAccount("customer10", "Customer10");
            customer2= new CustomerAccount("customer20", "Customer20");

            customers.Add(customer1);
            customers.Add(customer2);            
            if (newCustomer != null) //To handle NullReferenceException at line 55 during runtime
            {
                customers.Add(newCustomer);
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            frmUserType frmUserType = new frmUserType();
            this.Hide();
            frmUserType.Show();
        }

        private void cmdLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            Boolean retName, retPass;
            frmAdminSignIn.times++; //Using a field from another form

            if (username == "" && password == "")
            {
                MessageBox.Show("Username input and password input are expected.");
                frmAdminSignIn.CheckTimes(txtUsername,txtPassword,cmdLogIn,lblTimer,tmrCustomer); //Using a method from  another form
                return;
            }

            for (int i = 0; i < customers.Count; i++)
            {
                retName = customers[i].CheckUname(username);
                if (retName == true)
                {
                    retPass = customers[i].CheckPass(password);
                    if (retPass == true)
                    {
                        frmCustomerHome frmCustomerHome = new frmCustomerHome();
                        this.Hide();
                        frmCustomerHome.Show();
                        return;
                    }
                    MessageBox.Show("Password is not correct");
                    frmAdminSignIn.CheckTimes(txtUsername,txtPassword,cmdLogIn,lblTimer,tmrCustomer); //Using a method from another form
                    txtPassword.Focus();
                    return;
                }
            }
            MessageBox.Show("Username is not correct");
            frmAdminSignIn.CheckTimes(txtUsername,txtPassword,cmdLogIn,lblTimer,tmrCustomer); //Using a method from another form
            txtUsername.Focus();
        }

        private void lnkLabelSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCustomerSignUp frmCustomerSignUp = new frmCustomerSignUp();
            this.Hide();
            frmCustomerSignUp.Show();
        }
    }
}
