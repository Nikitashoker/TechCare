namespace TechCare
{
    partial class UserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.заявкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьЗаявкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрЗаявокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCreateRequest = new System.Windows.Forms.Panel();
            this.btnCreateRequest = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tBProblemDescription = new System.Windows.Forms.TextBox();
            this.tBTypeOfFault = new System.Windows.Forms.TextBox();
            this.tBPhone = new System.Windows.Forms.TextBox();
            this.tBFIO = new System.Windows.Forms.TextBox();
            this.panelWatchRequests = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.panelCreateRequest.SuspendLayout();
            this.panelWatchRequests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заявкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1199, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // заявкаToolStripMenuItem
            // 
            this.заявкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьЗаявкуToolStripMenuItem,
            this.просмотрЗаявокToolStripMenuItem});
            this.заявкаToolStripMenuItem.Name = "заявкаToolStripMenuItem";
            this.заявкаToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.заявкаToolStripMenuItem.Text = "Заявка";
            // 
            // создатьЗаявкуToolStripMenuItem
            // 
            this.создатьЗаявкуToolStripMenuItem.Name = "создатьЗаявкуToolStripMenuItem";
            this.создатьЗаявкуToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.создатьЗаявкуToolStripMenuItem.Text = "Создать заявку";
            this.создатьЗаявкуToolStripMenuItem.Click += new System.EventHandler(this.создатьЗаявкуToolStripMenuItem_Click);
            // 
            // просмотрЗаявокToolStripMenuItem
            // 
            this.просмотрЗаявокToolStripMenuItem.Name = "просмотрЗаявокToolStripMenuItem";
            this.просмотрЗаявокToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.просмотрЗаявокToolStripMenuItem.Text = "Просмотр заявок";
            this.просмотрЗаявокToolStripMenuItem.Click += new System.EventHandler(this.просмотрЗаявокToolStripMenuItem_Click);
            // 
            // panelCreateRequest
            // 
            this.panelCreateRequest.Controls.Add(this.btnCreateRequest);
            this.panelCreateRequest.Controls.Add(this.label4);
            this.panelCreateRequest.Controls.Add(this.label3);
            this.panelCreateRequest.Controls.Add(this.label2);
            this.panelCreateRequest.Controls.Add(this.label1);
            this.panelCreateRequest.Controls.Add(this.tBProblemDescription);
            this.panelCreateRequest.Controls.Add(this.tBTypeOfFault);
            this.panelCreateRequest.Controls.Add(this.tBPhone);
            this.panelCreateRequest.Controls.Add(this.tBFIO);
            this.panelCreateRequest.Location = new System.Drawing.Point(13, 31);
            this.panelCreateRequest.Name = "panelCreateRequest";
            this.panelCreateRequest.Size = new System.Drawing.Size(1175, 407);
            this.panelCreateRequest.TabIndex = 1;
            this.panelCreateRequest.Visible = false;
            // 
            // btnCreateRequest
            // 
            this.btnCreateRequest.Location = new System.Drawing.Point(506, 363);
            this.btnCreateRequest.Name = "btnCreateRequest";
            this.btnCreateRequest.Size = new System.Drawing.Size(170, 29);
            this.btnCreateRequest.TabIndex = 8;
            this.btnCreateRequest.Text = "Отправить заявку";
            this.btnCreateRequest.UseVisualStyleBackColor = true;
            this.btnCreateRequest.Click += new System.EventHandler(this.btnCreateRequest_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(227, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Описание проблемы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(227, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Тип неисправности";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(227, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Телефон";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(227, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Фио";
            // 
            // tBProblemDescription
            // 
            this.tBProblemDescription.Location = new System.Drawing.Point(437, 176);
            this.tBProblemDescription.Multiline = true;
            this.tBProblemDescription.Name = "tBProblemDescription";
            this.tBProblemDescription.Size = new System.Drawing.Size(546, 161);
            this.tBProblemDescription.TabIndex = 3;
            // 
            // tBTypeOfFault
            // 
            this.tBTypeOfFault.Location = new System.Drawing.Point(437, 130);
            this.tBTypeOfFault.Name = "tBTypeOfFault";
            this.tBTypeOfFault.Size = new System.Drawing.Size(546, 22);
            this.tBTypeOfFault.TabIndex = 2;
            // 
            // tBPhone
            // 
            this.tBPhone.Location = new System.Drawing.Point(437, 84);
            this.tBPhone.Name = "tBPhone";
            this.tBPhone.Size = new System.Drawing.Size(546, 22);
            this.tBPhone.TabIndex = 1;
            // 
            // tBFIO
            // 
            this.tBFIO.Location = new System.Drawing.Point(437, 40);
            this.tBFIO.Name = "tBFIO";
            this.tBFIO.Size = new System.Drawing.Size(546, 22);
            this.tBFIO.TabIndex = 0;
            // 
            // panelWatchRequests
            // 
            this.panelWatchRequests.Controls.Add(this.dataGridView1);
            this.panelWatchRequests.Location = new System.Drawing.Point(13, 31);
            this.panelWatchRequests.Name = "panelWatchRequests";
            this.panelWatchRequests.Size = new System.Drawing.Size(1175, 407);
            this.panelWatchRequests.TabIndex = 9;
            this.panelWatchRequests.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1169, 353);
            this.dataGridView1.TabIndex = 0;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 450);
            this.Controls.Add(this.panelWatchRequests);
            this.Controls.Add(this.panelCreateRequest);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechCare";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelCreateRequest.ResumeLayout(false);
            this.panelCreateRequest.PerformLayout();
            this.panelWatchRequests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem заявкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьЗаявкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотрЗаявокToolStripMenuItem;
        private System.Windows.Forms.Panel panelCreateRequest;
        private System.Windows.Forms.TextBox tBProblemDescription;
        private System.Windows.Forms.TextBox tBTypeOfFault;
        private System.Windows.Forms.TextBox tBPhone;
        private System.Windows.Forms.TextBox tBFIO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateRequest;
        private System.Windows.Forms.Panel panelWatchRequests;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}