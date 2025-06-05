using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechCare
{
    public partial class AddUserForm: Form
    {
        private readonly string _connectionString = "Data Source=DESKTOP-RIOCQT7\\SQLEXPRESS;Initial Catalog=RepairServiceDB;Integrated Security=True;TrustServerCertificate=True";

        public AddUserForm()
        {
            InitializeComponent();
        }

        // Обработчик события щелчка по кнопке "ОК"
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Читаем данные пользователя
            string username = tbUsername.Text.Trim();
            string email = tbEmail.Text.Trim();
            string password = tbPassword.Text.Trim();

            // Проверка на пустоту обязательных полей
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Все поля обязательны для заполнения!");
                return;
            }

            // Проверка правильности email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Некорректный адрес электронной почты!");
                return;
            }

            // Добавляем пользователя в таблицу Users
            AddUser(username, email, password);

            // Проверяем, указаны ли данные сотрудника
            if (!string.IsNullOrWhiteSpace(tbFullName.Text) &&
                !string.IsNullOrWhiteSpace(tbPosition.Text) &&
                !string.IsNullOrWhiteSpace(tbDepartment.Text))
            {
                // Добавляем сотрудника в таблицу Employees
                AddEmployee(tbFullName.Text, tbPosition.Text, tbDepartment.Text);
            }

            // Закрываем форму
            this.Close();
        }

        // Метод добавления пользователя
        private void AddUser(string username, string email, string password)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Users (Username, Email, Password, RoleID) VALUES (@Username, @Email, @Password, 2)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Метод добавления сотрудника
        private void AddEmployee(string fullName, string position, string department)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Employees (FullName, Position, Department) VALUES (@FullName, @Position, @Department)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Position", position);
                    cmd.Parameters.AddWithValue("@Department", department);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Метод проверки адреса электронной почты
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
