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
    public partial class SelectEmployeeForm: Form
    {
        private readonly int _requestID;
        private readonly string _connectionString = "Data Source=DESKTOP-RIOCQT7\\SQLEXPRESS;Initial Catalog=RepairServiceDB;Integrated Security=True;TrustServerCertificate=True";

        public SelectEmployeeForm(int requestID)
        {
            InitializeComponent();
            _requestID = requestID;
            LoadEmployees();
        }

        // Загрузка списка сотрудников
        private void LoadEmployees()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = "SELECT EmployeeID, FullName FROM Employees";
                    using (var adapter = new SqlDataAdapter(query, conn))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        listBoxEmployees.DataSource = ds.Tables[0].DefaultView;
                        listBoxEmployees.DisplayMember = "FullName";
                        listBoxEmployees.ValueMember = "EmployeeID";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Ошибка SQL: {sqlEx.Message}\n\nDetails:\n{sqlEx.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Общая ошибка: {ex.Message}\n\nDetails:\n{ex.StackTrace}");
            }
        }

        // Назначение выбранного сотрудника заявке
        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedIndex >= 0)
            {
                var employeeID = Convert.ToInt32(listBoxEmployees.SelectedValue);
                AssignEmployee(employeeID);
                this.Close();
            }
        }

        // Присваивание сотрудника заявке
        private void AssignEmployee(int employeeID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Requests SET ResponsibleEmployee = @employeeID WHERE RequestID = @requestID";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employeeID", employeeID);
                    cmd.Parameters.AddWithValue("@requestID", _requestID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
