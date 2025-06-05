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
    public partial class Form1: Form
    {
        Form1 fr;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Вход";
        }

        private void linkLabelToAutorizz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
            tBRegEmail.Clear();
            tBRegLogin.Clear();
            tBRegPass.Clear();
            this.Text = "Вход";
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            // Шаг 1: получаем данные из TextBox
            string username = tBRegLogin.Text.Trim();
            string password = tBRegPass.Text.Trim(); // Пароль хранится открытым текстом (это плохо!)
            string email = tBRegEmail.Text.Trim();

            // Шаг 2: проверка введённых данных
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Необходимо ввести логин!");
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Необходимо ввести пароль!");
                return;
            }
            else if (!IsValidEmail(email)) // Используем метод проверки email
            {
                MessageBox.Show("Некорректный адрес электронной почты!");
                return;
            }

            try
            {
                using (var connection = new SqlConnection("Data Source=DESKTOP-RIOCQT7\\SQLEXPRESS;Initial Catalog=RepairServiceDB;User ID=nefor;Password=27062006nik;TrustServerCertificate=True\""))
                {
                    connection.Open();

                    // Проверяем уникальность имени пользователя и email
                    var checkQuery = $"SELECT COUNT(*) FROM Users WHERE Username='{username}' OR Email='{email}'";
                    int count = Convert.ToInt32(new SqlCommand(checkQuery, connection).ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Такой логин или электронная почта уже существует.");
                        return;
                    }

                    // Шаг 3: добавляем нового пользователя
                    var insertQuery = @"INSERT INTO Users (Username, Password, Email, RoleID) 
                           VALUES (@Username, @Password, @Email, 2)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password); // Хранение открытого текста опасно!
                        cmd.Parameters.AddWithValue("@Email", email);

                        cmd.ExecuteNonQuery();
                    }
                    panel2.Visible = false;
                    panel1.Visible = true;
                    tBRegEmail.Clear();
                    tBRegLogin.Clear();
                    tBRegPass.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}");
            }
        }

        // Метод проверки правильности формата email
        public bool IsValidEmail(string email)
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

        private void btnLog_Click(object sender, EventArgs e)
        {
            // Получаем введённые логин и пароль
            string login = tBLogin.Text.Trim();
            string password = tBPass.Text.Trim();

            // Базовая проверка на пустые поля
            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введите логин");
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            try
            {
                using (var connection = new SqlConnection("Data Source=DESKTOP-RIOCQT7\\SQLEXPRESS;Initial Catalog=RepairServiceDB;User ID=nefor;Password=27062006nik;TrustServerCertificate=True"))
                {
                    connection.Open();

                    // Проверяем существование пользователя и извлекаем его RoleID и UserID
                    string sql = "SELECT RoleID, UserID FROM Users WHERE Username=@login AND Password=@password";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Получаем RoleID и UserID
                                    int roleID = Convert.ToInt32(reader["RoleID"]);
                                    int userID = Convert.ToInt32(reader["UserID"]);

                                    // В зависимости от роли переходим на нужную форму
                                    switch (roleID)
                                    {
                                        case 1:
                                            OpenAdminForm(userID); // Переход на форму администратора
                                            break;
                                        case 2:
                                            OpenUserForm(userID); // Переход на форму пользователя
                                            break;
                                        case 3:
                                            OpenEmployeeForm(userID); // Переход на форму сотрудника
                                            break;
                                        default:
                                            MessageBox.Show("Неизвестная роль пользователя");
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Диагностика ошибок
                MessageBox.Show($"Ошибка входа: {ex.Message}\n\nПодробнее:\n{ex.StackTrace}");
            }
        }

        // Методы для открытия нужных форм
        void OpenAdminForm(int userID)
        {
            Hide(); // Скрываем текущую форму
            AdminForm form = new AdminForm(); // Конструктор формы должен получать UserID
            form.FormClosed += delegate { Application.Exit(); }; // Завершаем программу при закрытии формы
            form.ShowDialog();
        }

        void OpenUserForm(int userID)
        {
            Hide();
            UserForm form = new UserForm(userID);
            form.FormClosed += delegate { Application.Exit(); };
            form.ShowDialog();
        }

        void OpenEmployeeForm(int userID)
        {
            Hide();
            EmployeeForm form = new EmployeeForm(userID);
            form.FormClosed += delegate { Application.Exit(); };
            form.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            tBLogin.Clear();
            tBPass.Clear();
            this.Text = "Регистрация";
        }
    }
}
