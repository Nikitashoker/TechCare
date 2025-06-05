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
    public partial class UserForm: Form
    {
        private readonly int _userID;
        public UserForm(int UserID)
        {
            InitializeComponent();
            _userID = UserID;
            LoadRequestsByUserID(UserID);
        }
        private void LoadRequestsByUserID(int userID)
        {
            string connString = "Data Source=DESKTOP-RIOCQT7\\SQLEXPRESS;Initial Catalog=RepairServiceDB;Integrated Security=True;TrustServerCertificate=True"; // Укажите реальную строку подключения
            string sql = @"SELECT r.DateAdded AS 'Дата заявки',
                         r.TypeOfFault AS 'Тип неисправности',
                         r.ProblemDescription AS 'Описание проблемы',
                         e.FullName AS 'Исполнитель',
                         c.Content AS 'Комментарий',
                         r.Status AS 'Статус'
                  FROM Requests r
                  LEFT JOIN Employees e ON r.ResponsibleEmployee = e.EmployeeID
                  LEFT JOIN Comments c ON r.RequestID = c.RequestID
                  INNER JOIN Clients cl ON r.ClientID = cl.ClientID
                  WHERE cl.UserID = @UserID
                  ORDER BY r.DateAdded DESC";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Устанавливаем параметр UserID
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();

                    // Заполняем DataTable результатом запроса
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Привязываем данные к DataGridView
                    dataGridView1.DataSource = dt;
                }
            }
        }
        private void btnCreateRequest_Click(object sender, EventArgs e)
        {
            // Проверка заполнения обязательных полей
            string fio = tBFIO.Text.Trim();
            string phone = tBPhone.Text.Trim();
            string typeOfFault = tBTypeOfFault.Text.Trim();
            string problemDescription = tBProblemDescription.Text.Trim();

            if (string.IsNullOrEmpty(fio))
            {
                MessageBox.Show("Необходимо заполнить поле \"ФИО\".");
                return;
            }
            else if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Необходимо заполнить поле \"Телефон\".");
                return;
            }
            else if (string.IsNullOrEmpty(typeOfFault))
            {
                MessageBox.Show("Необходимо выбрать тип неисправности.");
                return;
            }
            else if (string.IsNullOrEmpty(problemDescription))
            {
                MessageBox.Show("Необходимо описать проблему.");
                return;
            }

            try
            {
                using (var connection = new SqlConnection("Data Source=DESKTOP-RIOCQT7\\SQLEXPRESS;Initial Catalog=RepairServiceDB;Integrated Security=True;TrustServerCertificate=True"))
                {
                    connection.Open();

                    // Сначала добавляем клиента, связанный с нашим пользователем
                    string addClientQuery = "INSERT INTO Clients (FullName, Phone, UserID) OUTPUT INSERTED.ClientID VALUES(@fio, @phone, @userID)";
                    using (SqlCommand cmdAddClient = new SqlCommand(addClientQuery, connection))
                    {
                        cmdAddClient.Parameters.AddWithValue("@fio", fio);
                        cmdAddClient.Parameters.AddWithValue("@phone", phone);
                        cmdAddClient.Parameters.AddWithValue("@userID", _userID); // Передаём UserID

                        // Получаем новый ClientID
                        int clientID = Convert.ToInt32(cmdAddClient.ExecuteScalar());

                        // Создаём заявку, включив UserID и ClientID
                        string createRequestQuery = @"INSERT INTO Requests (DateAdded, EquipmentID, TypeOfFault, ProblemDescription, Status, ClientID)
                                                  VALUES (GETDATE(), NULL, @typeOfFault, @problemDescription, 'ожидает', @clientID)";

                        using (SqlCommand cmdCreateRequest = new SqlCommand(createRequestQuery, connection))
                        {
                            cmdCreateRequest.Parameters.AddWithValue("@typeOfFault", typeOfFault);
                            cmdCreateRequest.Parameters.AddWithValue("@problemDescription", problemDescription);
                            cmdCreateRequest.Parameters.AddWithValue("@clientID", clientID);

                            cmdCreateRequest.ExecuteNonQuery();

                            MessageBox.Show("Заявка создана успешно!");
                            tBFIO.Clear();
                            tBPhone.Clear();
                            tBProblemDescription.Clear();
                            tBTypeOfFault.Clear();
                            LoadRequestsByUserID(_userID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заявки: {ex.Message}");
            }
        }

        private void создатьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (создатьЗаявкуToolStripMenuItem.Text == "Создать заявку") {
                if (просмотрЗаявокToolStripMenuItem.Text == "Скрыть просмотр заявок")
                {
                    просмотрЗаявокToolStripMenuItem.Text = "Просмотр заявок";
                    panelWatchRequests.Visible = false;
                }
                создатьЗаявкуToolStripMenuItem.Text = "Скрыть создание заявки";
                panelCreateRequest.Visible = true;
            }
            else { 
                создатьЗаявкуToolStripMenuItem.Text = "Создать заявку";
                panelCreateRequest.Visible = false;
                tBFIO.Clear();
                tBPhone.Clear();
                tBProblemDescription.Clear();
                tBTypeOfFault.Clear();
            }
        }

        private void просмотрЗаявокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (просмотрЗаявокToolStripMenuItem.Text == "Просмотр заявок")
            {
                if (создатьЗаявкуToolStripMenuItem.Text == "Скрыть создание заявки")
                {
                    создатьЗаявкуToolStripMenuItem.Text = "Создать заявку";
                    panelCreateRequest.Visible = false;
                }
                просмотрЗаявокToolStripMenuItem.Text = "Скрыть просмотр заявок";
                panelWatchRequests.Visible = true;
            }
            else
            {
                просмотрЗаявокToolStripMenuItem.Text = "Просмотр заявок";
                panelWatchRequests.Visible = false;
            }
        }
    }
}
