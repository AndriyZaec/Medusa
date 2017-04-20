using Medusa.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Form

namespace Medusa
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {

            Medusa_v2Entities dbContext = new Medusa_v2Entities();
            if (LoginTB.Text != "" && PasswordTB.Text != "")
            {
                var employee = dbContext.Employee.Where(em => em.Login == LoginTB.Text).First();
                if (PasswordTB.Text == employee.Password)
                {
                    MainForm mf = new MainForm(employee);
                    mf.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Було введено невірний пароль!");
                }
            }
            else {
                MessageBox.Show("Перевірте правильність логіну");
            }
        }
    }
}
