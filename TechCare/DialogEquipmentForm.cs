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
using System.Xml.Linq;

namespace TechCare
{
    public partial class DialogEquipmentForm : Form
    {
        public string EquipmentName { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }

        public DialogEquipmentForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtName.Text = EquipmentName ?? "";
            txtSerialNumber.Text = SerialNumber ?? "";
            txtDescription.Text = Description ?? "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EquipmentName = txtName.Text.Trim();
            SerialNumber = txtSerialNumber.Text.Trim();
            Description = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(EquipmentName) || string.IsNullOrEmpty(SerialNumber))
            {
                MessageBox.Show("Название и серийный номер оборудования обязательны.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
