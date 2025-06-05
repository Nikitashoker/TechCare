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
    public partial class EmployeeForm : Form
    {
        private readonly int _userID;
        private readonly string _connectionString;

        public EmployeeForm(int userID)
        {
            InitializeComponent();
            _userID = userID;
            _connectionString = @"Data Source=DESKTOP-RIOCQT7\SQLEXPRESS;Initial Catalog=RepairServiceDB;Integrated Security=True;TrustServerCertificate=True";
            RefreshRequestList();
        }

        // Загрузка списка заявок
        private void RefreshRequestList()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = @"
                SELECT r.RequestID, r.DateAdded, r.TypeOfFault, r.ProblemDescription, r.Status,
                       eq.Model AS НазваниеОборудования, c.Content AS СодержимоеКомментария
                FROM Requests r
                INNER JOIN Equipment eq ON r.EquipmentID = eq.ID
                LEFT JOIN Comments c ON r.RequestID = c.RequestID
                WHERE r.ResponsibleEmployee = @UserID";

                using (var command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@UserID", _userID);
                    conn.Open();
                    var reader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(reader);

                    // Локализуем названия столбцов
                    dt.Columns["RequestID"].ColumnName = "Номер заявки";
                    dt.Columns["DateAdded"].ColumnName = "Дата подачи";
                    dt.Columns["TypeOfFault"].ColumnName = "Тип неисправности";
                    dt.Columns["ProblemDescription"].ColumnName = "Описание проблемы";
                    dt.Columns["Status"].ColumnName = "Статус";
                    dt.Columns["НазваниеОборудования"].ColumnName = "Модель оборудования";
                    dt.Columns["СодержимоеКомментария"].ColumnName = "Комментарий";

                    // Отключаем возможность прямого редактирования
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;

                    dataGridView1.DataSource = dt;

                    // Обновляем состояние кнопок
                    UpdateButtonStates();
                }
            }
        }

        // Полностью отключаем кнопки
        private void ResetButtonStates()
        {
            btnComment.Enabled = false;
            btnEquipment.Enabled = false;
            btnEditStatus.Enabled = false;
        }

        // Метод обновления состояния кнопок при выборе строки
        private void UpdateButtonStates()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];

                // Проверяем наличие комментария
                bool hasComment = false;
                object commentCellValue = selectedRow.Cells["Комментарий"]?.Value;
                if (commentCellValue != DBNull.Value && commentCellValue != null)
                    hasComment = !string.IsNullOrEmpty(commentCellValue.ToString());

                // Проверяем наличие оборудования
                bool hasEquipment = false;
                object equipmentCellValue = selectedRow.Cells["Модель оборудования"]?.Value;
                if (equipmentCellValue != DBNull.Value && equipmentCellValue != null)
                    hasEquipment = !string.IsNullOrEmpty(equipmentCellValue.ToString());

                // Настраиваем доступность кнопок
                btnComment.Enabled = true; // Всегда включаем кнопку комментариев
                btnEquipment.Enabled = true; // Включаем кнопку оборудования
                btnEditStatus.Enabled = selectedRow.Cells["Статус"].Value?.ToString().Trim() == "ожидает" ||
                                        selectedRow.Cells["Статус"].Value?.ToString().Trim() == "в работе";

                // Адаптируем надписи кнопок динамически
                if (hasComment)
                    btnComment.Text = "Изменить комментарий";
                else
                    btnComment.Text = "Добавить комментарий";

                if (hasEquipment)
                    btnEquipment.Text = "Изменить оборудование";
                else
                    btnEquipment.Text = "Добавить оборудование";

                if (btnEditStatus.Enabled)
                    btnEditStatus.Text = "Изменить статус";
                else
                    btnEditStatus.Text = "Нет возможности изменить статус";
            }
            else
            {
                // Если строк нет, отключаем все кнопки
                ResetButtonStates();
            }
        }

        // Обработчики кликов по кнопкам
        private void btnComment_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridView1.SelectedRows[0];
            var requestID = Convert.ToInt32(selectedRow.Cells["Номер заявки"].Value);
            bool hasComment = false;
            object cellValue = selectedRow.Cells["Комментарий"]?.Value;
            if (cellValue != DBNull.Value && cellValue != null)
                hasComment = !string.IsNullOrEmpty(cellValue.ToString());

            if (!hasComment)
            {
                AddComment(requestID);
            }
            else
            {
                EditComment(requestID);
            }
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridView1.SelectedRows[0];
            var requestID = Convert.ToInt32(selectedRow.Cells["Номер заявки"].Value);

            // Проверяем наличие оборудования
            bool hasEquipment = false;
            object cellValue = selectedRow.Cells["Модель оборудования"]?.Value;
            if (cellValue != DBNull.Value && cellValue != null)
                hasEquipment = !string.IsNullOrEmpty(cellValue.ToString());

            if (!hasEquipment)
            {
                AddEquipment(requestID);
            }
            else
            {
                EditEquipment(requestID);
            }
        }

        private void btnEditStatus_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridView1.SelectedRows[0];
            var currentStatus = Convert.ToString(selectedRow.Cells["Статус"].Value);
            UpdateRequestStatus(currentStatus);
            RefreshRequestList();
        }

        // Логика добавления комментария
        private void AddComment(int requestID)
        {
            using (var form = new DialogCommentForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(form.CommentText))
                {
                    InsertComment(requestID, form.CommentText);
                    RefreshRequestList(); // Обновляем список после добавления комментария
                }
            }
        }

        // Логика редактирования комментария
        private void EditComment(int requestID)
        {
            using (var form = new DialogCommentForm())
            {
                form.CommentText = GetCurrentComment(requestID);
                if (form.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(form.CommentText))
                {
                    UpdateComment(requestID, form.CommentText);
                    RefreshRequestList(); // Обновляем список после редактирования комментария
                }
            }
        }

        // Логика добавления оборудования
        private void AddEquipment(int requestID)
        {
            using (var form = new DialogEquipmentForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // Создаем новое оборудование
                    int equipmentID = InsertEquipment(form.EquipmentName, form.SerialNumber, form.Description);

                    // Привязываем оборудование к заявке
                    LinkEquipmentToRequest(requestID, equipmentID);

                    // Обновляем интерфейс
                    RefreshRequestList();
                }
            }
        }

        // Логика редактирования оборудования
        private void EditEquipment(int requestID)
        {
            var currentEquipment = GetCurrentEquipment(requestID);

            using (var form = new DialogEquipmentForm())
            {
                form.EquipmentName = currentEquipment.Name;
                form.SerialNumber = currentEquipment.SerialNumber;
                form.Description = currentEquipment.Description;

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // Обновляем данные оборудования
                    UpdateEquipment(currentEquipment.ID, form.EquipmentName, form.SerialNumber, form.Description);

                    // Обновляем интерфейс
                    RefreshRequestList();
                }
            }
        }

        // Изменяет статус заявки
        private void UpdateRequestStatus(string currentStatus)
        {
            switch (currentStatus)
            {
                case "ожидает":
                    SetNewStatus("в работе");
                    break;
                case "в работе":
                    SetNewStatus("завершено");
                    break;
                default:
                    SetNewStatus("ожидает");
                    break;
            }
        }

        // Устанваливает новый статус заявки
        private void SetNewStatus(string newStatus)
        {
            var selectedRow = dataGridView1.SelectedRows[0];
            var requestID = Convert.ToInt32(selectedRow.Cells["Номер заявки"].Value);

            using (var conn = new SqlConnection(_connectionString))
            {
                var updateCmd = $"UPDATE Requests SET Status=@newStatus WHERE RequestID=@requestID";
                using (var cmd = new SqlCommand(updateCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@newStatus", newStatus);
                    cmd.Parameters.AddWithValue("@requestID", requestID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Вставка комментария
        private void InsertComment(int requestID, string content)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var insertCmd = $"INSERT INTO Comments(RequestID, Content) VALUES(@requestID, @content)";
                using (var cmd = new SqlCommand(insertCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@requestID", requestID);
                    cmd.Parameters.AddWithValue("@content", content);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Обновление комментария
        private void UpdateComment(int requestID, string content)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var updateCmd = $"UPDATE Comments SET Content=@content WHERE RequestID=@requestID";
                using (var cmd = new SqlCommand(updateCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@requestID", requestID);
                    cmd.Parameters.AddWithValue("@content", content);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        // Получение текущего комментария
        private string GetCurrentComment(int requestID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var selectCmd = $"SELECT Content FROM Comments WHERE RequestID=@requestID";
                using (var cmd = new SqlCommand(selectCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@requestID", requestID);
                    conn.Open();
                    return Convert.ToString(cmd.ExecuteScalar()) ?? "";
                }
            }
        }

        // Вставка нового оборудования и возврат его ID
        private int InsertEquipment(string model, string serialNumber, string description)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var insertCmd = @"INSERT INTO Equipment(Model, SerialNumber, Description) OUTPUT INSERTED.ID VALUES(@model, @serialNumber, @description)";
                using (var cmd = new SqlCommand(insertCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                    cmd.Parameters.AddWithValue("@description", description);
                    conn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        // Привязка оборудования к заявке
        private void LinkEquipmentToRequest(int requestID, int equipmentID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var updateCmd = @"UPDATE Requests SET EquipmentID=@equipmentID WHERE RequestID=@requestID";
                using (var cmd = new SqlCommand(updateCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@equipmentID", equipmentID);
                    cmd.Parameters.AddWithValue("@requestID", requestID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Обновление существующего оборудования
        private void UpdateEquipment(int equipmentID, string model, string serialNumber, string description)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var updateCmd = @"UPDATE Equipment SET Model=@model, SerialNumber=@serialNumber, Description=@description WHERE ID=@equipmentID";
                using (var cmd = new SqlCommand(updateCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@equipmentID", equipmentID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Получение текущего оборудования по номеру заявки
        private EquipmentRecord GetCurrentEquipment(int requestID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var selectCmd = @"SELECT E.* FROM Equipment E INNER JOIN Requests R ON E.ID=R.EquipmentID WHERE R.RequestID=@requestID";
                using (var cmd = new SqlCommand(selectCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@requestID", requestID);
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new EquipmentRecord
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Model"].ToString(),
                            SerialNumber = reader["SerialNumber"].ToString(),
                            Description = reader["Description"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        // Обработчик события выбора строки
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates(); // Обновляем состояние кнопок при выборе строки
        }
        internal class EquipmentRecord
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string SerialNumber { get; set; }
            public string Description { get; set; }
        }
    }
}
