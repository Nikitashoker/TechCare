using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechCare
{
    public partial class DialogCommentForm : Form
    {
        public string CommentText { get; set; }

        public DialogCommentForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            textBoxComment.Text = CommentText ?? ""; // Инициализируем текстовое поле
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CommentText = textBoxComment.Text.Trim(); // Сохраняем введенный комментарий
            this.DialogResult = DialogResult.OK; // Важно установить DialogResult
            Close();
        }
    }
}
