namespace DataTranslator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dest_status = new System.Windows.Forms.Label();
            this.src_status = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.import_el = new System.Windows.Forms.Button();
            this.open_el = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.t_dst_status = new System.Windows.Forms.Label();
            this.t_src_status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.import_task = new System.Windows.Forms.Button();
            this.open_task = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ProgressStatus = new System.Windows.Forms.ProgressBar();
            this.debugBox = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(570, 198);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.dest_status);
            this.tabPage1.Controls.Add(this.src_status);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.import_el);
            this.tabPage1.Controls.Add(this.open_el);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(562, 172);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Elements.data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(320, 144);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(236, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Доперевести используя Yandex Translate";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dest_status
            // 
            this.dest_status.AutoSize = true;
            this.dest_status.Location = new System.Drawing.Point(136, 145);
            this.dest_status.Name = "dest_status";
            this.dest_status.Size = new System.Drawing.Size(0, 13);
            this.dest_status.TabIndex = 8;
            // 
            // src_status
            // 
            this.src_status.AutoSize = true;
            this.src_status.Location = new System.Drawing.Point(136, 65);
            this.src_status.Name = "src_status";
            this.src_status.Size = new System.Drawing.Size(0, 13);
            this.src_status.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(20, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Файл в который импортируем";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Файл из которого импортируем";
            // 
            // import_el
            // 
            this.import_el.Enabled = false;
            this.import_el.Location = new System.Drawing.Point(20, 140);
            this.import_el.Name = "import_el";
            this.import_el.Size = new System.Drawing.Size(110, 23);
            this.import_el.TabIndex = 4;
            this.import_el.Text = "Импортировать";
            this.import_el.UseVisualStyleBackColor = true;
            this.import_el.Click += new System.EventHandler(this.import_el_Click);
            // 
            // open_el
            // 
            this.open_el.Location = new System.Drawing.Point(20, 60);
            this.open_el.Name = "open_el";
            this.open_el.Size = new System.Drawing.Size(110, 23);
            this.open_el.TabIndex = 3;
            this.open_el.Text = "Открыть";
            this.open_el.UseVisualStyleBackColor = true;
            this.open_el.Click += new System.EventHandler(this.open_el_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.t_dst_status);
            this.tabPage2.Controls.Add(this.t_src_status);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.import_task);
            this.tabPage2.Controls.Add(this.open_task);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(562, 172);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tasks.data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // t_dst_status
            // 
            this.t_dst_status.AutoSize = true;
            this.t_dst_status.Location = new System.Drawing.Point(136, 145);
            this.t_dst_status.Name = "t_dst_status";
            this.t_dst_status.Size = new System.Drawing.Size(0, 13);
            this.t_dst_status.TabIndex = 13;
            // 
            // t_src_status
            // 
            this.t_src_status.AutoSize = true;
            this.t_src_status.Location = new System.Drawing.Point(136, 65);
            this.t_src_status.Name = "t_src_status";
            this.t_src_status.Size = new System.Drawing.Size(0, 13);
            this.t_src_status.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(20, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(281, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Файл в который импортируем";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(20, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(298, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "Файл из которого импортируем";
            // 
            // import_task
            // 
            this.import_task.Enabled = false;
            this.import_task.Location = new System.Drawing.Point(20, 140);
            this.import_task.Name = "import_task";
            this.import_task.Size = new System.Drawing.Size(110, 23);
            this.import_task.TabIndex = 8;
            this.import_task.Text = "Импортировать";
            this.import_task.UseVisualStyleBackColor = true;
            this.import_task.Click += new System.EventHandler(this.import_task_Click);
            // 
            // open_task
            // 
            this.open_task.Location = new System.Drawing.Point(20, 60);
            this.open_task.Name = "open_task";
            this.open_task.Size = new System.Drawing.Size(110, 23);
            this.open_task.TabIndex = 7;
            this.open_task.Text = "Открыть";
            this.open_task.UseVisualStyleBackColor = true;
            this.open_task.Click += new System.EventHandler(this.open_task_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(562, 172);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Справка";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ProgressStatus
            // 
            this.ProgressStatus.Location = new System.Drawing.Point(12, 381);
            this.ProgressStatus.Name = "ProgressStatus";
            this.ProgressStatus.Size = new System.Drawing.Size(570, 23);
            this.ProgressStatus.TabIndex = 11;
            // 
            // debugBox
            // 
            this.debugBox.Location = new System.Drawing.Point(12, 212);
            this.debugBox.Name = "debugBox";
            this.debugBox.ReadOnly = true;
            this.debugBox.Size = new System.Drawing.Size(566, 163);
            this.debugBox.TabIndex = 12;
            this.debugBox.Text = "";
            this.debugBox.TextChanged += new System.EventHandler(this.debugBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 416);
            this.Controls.Add(this.debugBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ProgressStatus);
            this.MaximumSize = new System.Drawing.Size(610, 455);
            this.MinimumSize = new System.Drawing.Size(610, 455);
            this.Name = "Form1";
            this.Text = "DataTranslator";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button import_el;
        private System.Windows.Forms.Button open_el;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dest_status;
        private System.Windows.Forms.Label src_status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button import_task;
        private System.Windows.Forms.Button open_task;
        private System.Windows.Forms.ProgressBar ProgressStatus;
        private System.Windows.Forms.Label t_dst_status;
        private System.Windows.Forms.Label t_src_status;
        private System.Windows.Forms.RichTextBox debugBox;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

