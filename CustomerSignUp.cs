using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeApplianceRentalApplication
{
    public partial class frmCustomerSignUp : Form
    {
        CustomerAccount newAcc;

        public frmCustomerSignUp()
        {
            InitializeComponent();
        }

        private Boolean CheckUnamePattern(string n)
        {
            string pattern = "^[a-zA-Z0-9]*$";
            Match nameMatch = Regex.Match(n, pattern);
            if (nameMatch.Success == true)
            {
                return true;
            }
            return false;
        }

        private Boolean CheckPassPattern(string p)
        {
            string pattern = "^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]*$";
            Match passMatch = Regex.Match(p, pattern);            
            if (passMatch.Success == true)
            {
                return true;
            }
            return false;
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {            
            string username = txtUsername.Text;
            string pass = txtPassword.Text;
            Boolean ret1, ret2;

            if (username == "" && pass == "")
            {
                MessageBox.Show("Username input and password input are expected.");
                return;
            }

            ret1 = CheckUnamePattern(username);
            ret2 = CheckPassPattern(pass);

            if (pass.Length < 8 || pass.Length > 16)
            {
                MessageBox.Show("Password must be between 8 and 16 characters");
                txtPassword.Focus();
                return;
            }

            if (ret1 == true)
            {
                if (ret2 == true)
                {                    
                    newAcc = new CustomerAccount(username, pass);                    
                    frmCustomerSignIn frmCustomerSignIn = new frmCustomerSignIn(newAcc); //Passing an object to Customer sign in form
                    this.Hide();
                    frmCustomerSignIn.Show();
                    MessageBox.Show("The account was successfully created. Logging in is ready.");
                }
                else
                {
                    MessageBox.Show("Password must contain at least one lowercase and one uppercase letter.");
                    txtPassword.Focus();
                }
            }
            else
            {
                MessageBox.Show("Username can only contain letters and numbers");
                txtUsername.Focus();
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            frmCustomerSignIn frmCustomerSignIn = new frmCustomerSignIn(null);
            this.Hide();
            frmCustomerSignIn.Show();
        }
    }
}
