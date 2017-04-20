using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medusa.Forms
{
    public partial class MainForm : Form
    {
        private Employee _employee = new Employee();
        private Person _person = new Person();
        private Medusa_v2Entities dbContext = new Medusa_v2Entities();

        public MainForm(Employee employee)
        {
            InitializeComponent();

            _employee = employee;

            _person = dbContext.Person.Where(em => em.IdPerson == employee.idPersonEmployee).First();

            NameTB.Text = _person.Name;
            SurnameTB.Text = _person.Surname;
            AddressTB.Text = _person.Address;
            BirthdayTB.Text = _person.Birthday.ToShortDateString();
            PhoneTB.Text = _person.Phone;
            EmailTB.Text = _person.Email;

            PositionLbl.Text = _employee.Position1.PositionDescription;
            DepartmentLbl.Text = _employee.Position1.Department1.Name;
            StartWorkAtLbl.Text = _employee.StartWorkAt.Date.ToShortDateString();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditBtn.Hide();
            SaveBtn.Show();

            NameTB.ReadOnly = false;
            SurnameTB.ReadOnly = false;
            AddressTB.ReadOnly = false;
            BirthdayTB.ReadOnly = false;
            PhoneTB.ReadOnly = false;
            EmailTB.ReadOnly = false;

            
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var phoneRegEx = new Regex("^(\\+380|0)([0-9]{9})$");
                if (!phoneRegEx.IsMatch(PhoneTB.Text))
                {
                    MessageBox.Show("Невіроно введений номер телефону, номер поивнен бути у форматі +380*********");
                    return;
                }
                else
                {
                    _person.Phone = PhoneTB.Text;
                }
                try
                {
                    MailAddress m = new MailAddress(EmailTB.Text);
                    _person.Email = EmailTB.Text;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Перевірте коректність введеного Email");
                    return;
                }

                _person.Name = NameTB.Text;
                _person.Surname = SurnameTB.Text;
                _person.Address = AddressTB.Text;
                try
                {
                    _person.Birthday = DateTime.Parse(BirthdayTB.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Перевірте правильність введеня дати");
                    return;
                }
                dbContext.Entry(_person).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception) {
                MessageBox.Show("Сталася помилка при збереженні");
            } finally
            {
                NameTB.ReadOnly = true;
                SurnameTB.ReadOnly = true;
                AddressTB.ReadOnly = true;
                BirthdayTB.ReadOnly = true;
                PhoneTB.ReadOnly = true;
                EmailTB.ReadOnly = true;

                SaveBtn.Hide();
                EditBtn.Show();

                MessageBox.Show("Дані успішно збережені");
            }
        }
    }
}
