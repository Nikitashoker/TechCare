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
    public partial class AdminForm: Form
    {
        private readonly string _connectionString = "Data Source=DESKTOP-RIOCQT7\\SQLEXPRESS;Initial Catalog=RepairServiceDB;Integrated Security=True;TrustServerCertificate=True";

        public AdminForm()
        {
            InitializeComponent();
            LoadDataGridView();
            LoadDataGridViewUsers();
        }

        // Загрузка данных в DataGridView
        private void LoadDataGridView()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    const string query = @"
                SELECT 
                    req.RequestID AS 'Номер заявки',
                    req.TypeOfFault AS 'Тип неисправности',
                    req.ProblemDescription AS 'Описание проблемы',
                    req.Status AS 'Статус',
                    emp.FullName AS 'ФИО исполнителя',
                    emp.Position AS 'Должность',
                    emp.Department AS 'Отдел'
                FROM Requests req
                LEFT JOIN Employees emp ON req.ResponsibleEmployee = emp.EmployeeID
                ORDER BY req.RequestID ASC";

                    conn.Open();

                    using (var adapter = new SqlDataAdapter(query, conn))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        dataGridViewRequests.DataSource = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}\n\nDetails:\n{ex.StackTrace}");
            }
        }

        // Обработчик события изменения выделенной строки
        private void dataGridViewRequests_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count > 0)
            {
                var row = dataGridViewRequests.SelectedRows[0];

                // Активируем кнопки
                btnEditStatus.Enabled = true;
                comboBoxStatus.Visible = true;
                btnEmployee.Enabled = true;
                btnDelRequest.Enabled = true; // <--- добавляем сюда

                // Определяем, назначен ли сотрудник
                object responsibleEmployeeObj = row.Cells["ФИО исполнителя"].Value;
                bool hasEmployeeAssigned = !(responsibleEmployeeObj is DBNull) && responsibleEmployeeObj != null;

                // Меняем подпись кнопки в зависимости от наличия исполнителя
                if (!hasEmployeeAssigned)
                {
                    btnEmployee.Text = "Назначить исполнителя";
                }
                else
                {
                    btnEmployee.Text = "Изменить исполнителя";
                }
            }
            else
            {
                // Скрываем и блокируем элементы управления
                btnEditStatus.Enabled = false;
                comboBoxStatus.Visible = false;
                btnEmployee.Enabled = false;
                btnDelRequest.Enabled = false; // <--- добавляем сюда
            }
        }

        // Обработка изменения статуса
        private void btnEditStatus_Click(object sender, EventArgs e)
        {
            // Проверяем, что строка выбрана
            if (dataGridViewRequests.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Выберите заявку для изменения статуса.");
                return;
            }

            // Получаем текущую строку
            var row = dataGridViewRequests.SelectedRows[0];

            // Проверяем корректность значений
            if (!(row.Cells["Номер заявки"].Value is int requestID))
            {
                MessageBox.Show("Ошибка в номере заявки.");
                return;
            }

            // Проверяем наличие выбранного статуса
            if (comboBoxStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите новый статус.");
                return;
            }

            // Новое значение статуса
            var newStatus = comboBoxStatus.SelectedItem.ToString();

            // Применяем изменение статуса
            ChangeRequestStatus(requestID, newStatus);

            // Обновляем таблицу
            LoadDataGridView();
        }


        // Обработка назначения исполнителя
        private void btnEmployee_Click(object sender, EventArgs e)
        {
            // Проверяем, что строка выбрана
            if (dataGridViewRequests.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Выберите заявку для назначения исполнителя.");
                return;
            }

            // Получаем текущую строку
            var row = dataGridViewRequests.SelectedRows[0];

            // Проверяем корректность значения RequestID
            if (!(row.Cells["Номер заявки"].Value is int requestID))
            {
                MessageBox.Show("Ошибка в номере заявки.");
                return;
            }

            // Открываем форму выбора сотрудника
            using (var employeeForm = new SelectEmployeeForm(requestID))
            {
                employeeForm.ShowDialog();
            }

            // Обновляем данные после возврата из формы
            LoadDataGridView();
        }

        // Изменение статуса заявки
        private void ChangeRequestStatus(int requestID, string newStatus)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Requests SET Status = @newStatus WHERE RequestID = @requestID";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@newStatus", newStatus);
                    cmd.Parameters.AddWithValue("@requestID", requestID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnDelRequest_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridViewRequests.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Выберите заявку для удаления.");
                return;
            }

            // Получаем выбранную строку
            var row = dataGridViewRequests.SelectedRows[0];

            // Проверяем корректность значения RequestID
            if (!(row.Cells["Номер заявки"].Value is int requestID))
            {
                MessageBox.Show("Ошибка в номере заявки.");
                return;
            }

            // Спрашиваем подтверждение удаления
            var result = MessageBox.Show(
                "Вы уверены, что хотите удалить данную заявку?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                return; // отмена удаления
            }

            // Производим удаление
            DeleteRequest(requestID);

            // Обновляем таблицу
            LoadDataGridView();

        }


        // Метод удаления заявки
        private void DeleteRequest(int requestID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM Requests WHERE RequestID = @requestID";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@requestID", requestID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void dataGridViewUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                var row = dataGridViewUsers.SelectedRows[0];
                var roleID = Convert.ToInt32(row.Cells["RoleID"].Value);

                // Активация кнопки удаления
                btnDelete.Enabled = true;

                // Меняем подпись кнопки в зависимости от роли
                if (roleID == 1 || roleID == 2)
                {
                    btnDelete.Text = "Удалить пользователя";
                }
                else
                {
                    btnDelete.Text = "Удалить исполнителя";
                }
            }
            else
            {
                // Скрываем и блокируем кнопку удаления
                btnDelete.Enabled = false;
                btnDelete.Text = "Удалить"; // нейтральная подпись по умолчанию
            }
        }

        private void btnEAEmployee_Click(object sender, EventArgs e)
        {
            // Открываем форму добавления пользователя
            using (var addUserForm = new AddUserForm())
            {
                addUserForm.ShowDialog();

                // После добавления обновляем таблицу
                LoadDataGridViewUsers();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                var row = dataGridViewUsers.SelectedRows[0];
                var userID = Convert.ToInt32(row.Cells["UserID"].Value);
                var roleID = Convert.ToInt32(row.Cells["RoleID"].Value);

                // Проверяем право на удаление
                if (roleID != 1) // если роль НЕ равна 1 (администратор)
                {
                    // Подтверждаем удаление
                    var result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        DeleteUser(userID);
                        LoadDataGridViewUsers(); // обновляем таблицу
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя удалить Администратора (роль 1)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Метод удаления пользователя
        private void DeleteUser(int userID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM Users WHERE UserID = @userID";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void LoadDataGridViewUsers()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = @"
            SELECT 
                UserID, 
                Username, 
                Email, 
                RoleID 
            FROM Users";

                using (var adapter = new SqlDataAdapter(query, conn))
                {
                    var ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridViewUsers.DataSource = ds.Tables[0];
                }
            }
        }
        
        private void просмотрЗаявокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (просмотрЗаявокToolStripMenuItem.Text == "Просмотр заявок")
            {
                if (просмотрПользователейToolStripMenuItem.Text == "Скрыть просмотр пользователей")
                {
                    просмотрПользователейToolStripMenuItem.Text = "Просмотр пользователей";
                    panelUsers.Visible = false;
                }
                просмотрЗаявокToolStripMenuItem.Text = "Скрыть просмотр заявки";
                panelWatchRequests.Visible = true;
            }
            else
            {
                просмотрЗаявокToolStripMenuItem.Text = "Просмотр заявок";
                panelWatchRequests.Visible = false;
                btnDelete.Enabled = false;
            }
        }

        private void просмотрПользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {if (просмотрПользователейToolStripMenuItem.Text == "Просмотр пользователей")
            {
                if (просмотрЗаявокToolStripMenuItem.Text == "Скрыть просмотр заявки")
                {
                    просмотрЗаявокToolStripMenuItem.Text = "Просмотр заявок";
                    panelWatchRequests.Visible = false;
                    btnDelete.Enabled = false;
                }
                просмотрПользователейToolStripMenuItem.Text = "Скрыть просмотр пользователей";
                panelUsers.Visible = true;
            }
            else
            {
                просмотрПользователейToolStripMenuItem.Text = "Просмотр пользователей";
                panelUsers.Visible = false;
                btnDelRequest.Enabled = false;
                btnEditStatus.Enabled = false;
                btnEmployee.Enabled = false;
            }

        }
    }
}
