namespace TechCare
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.работаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрЗаявокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрПользователейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelWatchRequests = new System.Windows.Forms.Panel();
            this.btnDelRequest = new System.Windows.Forms.Button();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.btnEditStatus = new System.Windows.Forms.Button();
            this.btnEmployee = new System.Windows.Forms.Button();
            this.dataGridViewRequests = new System.Windows.Forms.DataGridView();
            this.panelUsers = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEAEmployee = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.panelWatchRequests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).BeginInit();
            this.panelUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.работаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1137, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // работаToolStripMenuItem
            // 
            this.работаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.просмотрЗаявокToolStripMenuItem,
            this.просмотрПользователейToolStripMenuItem});
            this.работаToolStripMenuItem.Name = "работаToolStripMenuItem";
            this.работаToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.работаToolStripMenuItem.Text = "Работа";
            // 
            // просмотрЗаявокToolStripMenuItem
            // 
            this.просмотрЗаявокToolStripMenuItem.Name = "просмотрЗаявокToolStripMenuItem";
            this.просмотрЗаявокToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.просмотрЗаявокToolStripMenuItem.Text = "Просмотр заявок";
            this.просмотрЗаявокToolStripMenuItem.Click += new System.EventHandler(this.просмотрЗаявокToolStripMenuItem_Click);
            // 
            // просмотрПользователейToolStripMenuItem
            // 
            this.просмотрПользователейToolStripMenuItem.Name = "просмотрПользователейToolStripMenuItem";
            this.просмотрПользователейToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.просмотрПользователейToolStripMenuItem.Text = "Просмотр пользователей";
            this.просмотрПользователейToolStripMenuItem.Click += new System.EventHandler(this.просмотрПользователейToolStripMenuItem_Click);
            // 
            // panelWatchRequests
            // 
            this.panelWatchRequests.Controls.Add(this.btnDelRequest);
            this.panelWatchRequests.Controls.Add(this.comboBoxStatus);
            this.panelWatchRequests.Controls.Add(this.btnEditStatus);
            this.panelWatchRequests.Controls.Add(this.btnEmployee);
            this.panelWatchRequests.Controls.Add(this.dataGridViewRequests);
            this.panelWatchRequests.Location = new System.Drawing.Point(12, 37);
            this.panelWatchRequests.Name = "panelWatchRequests";
            this.panelWatchRequests.Size = new System.Drawing.Size(1113, 398);
            this.panelWatchRequests.TabIndex = 1;
            this.panelWatchRequests.Visible = false;
            // 
            // btnDelRequest
            // 
            this.btnDelRequest.Enabled = false;
            this.btnDelRequest.Location = new System.Drawing.Point(14, 305);
            this.btnDelRequest.Name = "btnDelRequest";
            this.btnDelRequest.Size = new System.Drawing.Size(207, 34);
            this.btnDelRequest.TabIndex = 4;
            this.btnDelRequest.Text = "Удалить заявку";
            this.btnDelRequest.UseVisualStyleBackColor = true;
            this.btnDelRequest.Click += new System.EventHandler(this.btnDelRequest_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "ожидает",
            "в работе",
            "завершено"});
            this.comboBoxStatus.Location = new System.Drawing.Point(242, 271);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(147, 24);
            this.comboBoxStatus.TabIndex = 3;
            this.comboBoxStatus.Visible = false;
            // 
            // btnEditStatus
            // 
            this.btnEditStatus.Location = new System.Drawing.Point(14, 265);
            this.btnEditStatus.Name = "btnEditStatus";
            this.btnEditStatus.Size = new System.Drawing.Size(207, 34);
            this.btnEditStatus.TabIndex = 2;
            this.btnEditStatus.Text = "Изменить статус";
            this.btnEditStatus.UseVisualStyleBackColor = true;
            this.btnEditStatus.Click += new System.EventHandler(this.btnEditStatus_Click);
            // 
            // btnEmployee
            // 
            this.btnEmployee.Location = new System.Drawing.Point(14, 225);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(207, 34);
            this.btnEmployee.TabIndex = 1;
            this.btnEmployee.Text = "Назначить исполнителя";
            this.btnEmployee.UseVisualStyleBackColor = true;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // dataGridViewRequests
            // 
            this.dataGridViewRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRequests.Location = new System.Drawing.Point(4, 4);
            this.dataGridViewRequests.Name = "dataGridViewRequests";
            this.dataGridViewRequests.RowHeadersWidth = 51;
            this.dataGridViewRequests.RowTemplate.Height = 24;
            this.dataGridViewRequests.Size = new System.Drawing.Size(1106, 214);
            this.dataGridViewRequests.TabIndex = 0;
            this.dataGridViewRequests.SelectionChanged += new System.EventHandler(this.dataGridViewRequests_SelectionChanged);
            // 
            // panelUsers
            // 
            this.panelUsers.Controls.Add(this.btnDelete);
            this.panelUsers.Controls.Add(this.btnEAEmployee);
            this.panelUsers.Controls.Add(this.dataGridViewUsers);
            this.panelUsers.Location = new System.Drawing.Point(12, 37);
            this.panelUsers.Name = "panelUsers";
            this.panelUsers.Size = new System.Drawing.Size(1113, 398);
            this.panelUsers.TabIndex = 5;
            this.panelUsers.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(16, 276);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(207, 41);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEAEmployee
            // 
            this.btnEAEmployee.Location = new System.Drawing.Point(14, 225);
            this.btnEAEmployee.Name = "btnEAEmployee";
            this.btnEAEmployee.Size = new System.Drawing.Size(207, 45);
            this.btnEAEmployee.TabIndex = 1;
            this.btnEAEmployee.Text = "Добавить исполнителя";
            this.btnEAEmployee.UseVisualStyleBackColor = true;
            this.btnEAEmployee.Click += new System.EventHandler(this.btnEAEmployee_Click);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(4, 4);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.RowHeadersWidth = 51;
            this.dataGridViewUsers.RowTemplate.Height = 24;
            this.dataGridViewUsers.Size = new System.Drawing.Size(1106, 214);
            this.dataGridViewUsers.TabIndex = 0;
            this.dataGridViewUsers.SelectionChanged += new System.EventHandler(this.dataGridViewUsers_SelectionChanged);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 450);
            this.Controls.Add(this.panelUsers);
            this.Controls.Add(this.panelWatchRequests);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechCare";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelWatchRequests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).EndInit();
            this.panelUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem работаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотрЗаявокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотрПользователейToolStripMenuItem;
        private System.Windows.Forms.Panel panelWatchRequests;
        private System.Windows.Forms.Button btnEditStatus;
        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.DataGridView dataGridViewRequests;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Button btnDelRequest;
        private System.Windows.Forms.Panel panelUsers;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEAEmployee;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
    }
}