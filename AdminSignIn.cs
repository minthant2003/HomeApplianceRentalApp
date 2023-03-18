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
    public partial class frmAdminSignIn : Form
    {
        AdministratorAccount[] admins;
        public int times = 0; //To be accessed by customer sign in form
        int sec = 5;

        public frmAdminSignIn()
        {
            InitializeComponent();
        }

        //Function to check numbers of failed login
        public void CheckTimes(TextBox txtUsername, TextBox txtPassword, Button cmdLogIn, Label lblTimer,Timer timer) //To be accessed by customer sign in form, Considering parameters as a common
        {
            if(times==3)
            {                
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                cmdLogIn.Enabled = false;
                MessageBox.Show("Your account has been temporarily disabled for 5 seconds.");
                lblTimer.Text = sec + " seconds remaining to re-enter into the account";
                timer.Start();                
                return;
            }
        }

        private void tmrAdmin_Tick(object sender, EventArgs e)
        {
            sec--;
            if (sec == 0)
            {
                tmrAdmin.Stop();
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                cmdLogIn.Enabled = true;
                lblTimer.Text = "";
                times = 0;
                sec = 5;
                return;
            }
            lblTimer.Text = sec + " seconds remaining to re-enter into the account.";            
        }

        private void frmAdminSignIn_Load(object sender, EventArgs e)
        {
            admins = new AdministratorAccount[2];
            admins[0] = new AdministratorAccount("admin1", "1234");
            admins[1] = new AdministratorAccount("admin2", "4321");            
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            frmUserType frmUserType = new frmUserType();
            this.Hide();
            frmUserType.Show();                               
        }

        private void cmdLogIn_Click(object sender, EventArgs e)
        {
            String username = txtUsername.Text.ToString();
            String password = txtPassword.Text.ToString();
            Boolean retName, retPass;
            times += 1;                 

            if (username == "" && password == "")
            {
                MessageBox.Show("Username input and password input are expected.");
                CheckTimes(txtUsername,txtPassword,cmdLogIn,lblTimer,tmrAdmin);
                return;
            }

            for (int i = 0; i < admins.Length; i++)
            {
                retName = admins[i].CheckUname(username);
                if (retName == true)
                {
                    retPass = admins[i].CheckPass(password);
                    if (retPass == true)
                    {
                        frmAdminHome frmAdminHome = new frmAdminHome();
                        this.Hide();
                        frmAdminHome.Show();
                        return;
                    }
                    MessageBox.Show("Password is not correct");
                    CheckTimes(txtUsername,txtPassword,cmdLogIn,lblTimer,tmrAdmin);
                    txtPassword.Focus();
                    return;
                }
            }
            MessageBox.Show("Username is not correct");
            CheckTimes(txtPassword,txtPassword,cmdLogIn,lblTimer,tmrAdmin);
            txtUsername.Focus();              
        }
    }
}
