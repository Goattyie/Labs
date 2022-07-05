namespace Operations_Table
{
    partial class MainForm
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
            this.OpenFile = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.OpenSource = new System.Windows.Forms.OpenFileDialog();
            this.OperationsList = new System.Windows.Forms.ComboBox();
            this.OperationsLabel = new System.Windows.Forms.Label();
            this.LinesList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenFile
            // 
            this.OpenFile.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.OpenFile.Location = new System.Drawing.Point(273, 389);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(219, 39);
            this.OpenFile.TabIndex = 0;
            this.OpenFile.Text = "Выбрать файл для поиска";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(947, 172);
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Size = new System.Drawing.Size(24, 22);
            this.FilePath.TabIndex = 1;
            // 
            // OperationsList
            // 
            this.OperationsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperationsList.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.OperationsList.FormattingEnabled = true;
            this.OperationsList.Location = new System.Drawing.Point(132, 395);
            this.OperationsList.Name = "OperationsList";
            this.OperationsList.Size = new System.Drawing.Size(135, 28);
            this.OperationsList.TabIndex = 3;
            this.OperationsList.SelectedIndexChanged += new System.EventHandler(this.OperationsList_SelectedIndexChanged);
            // 
            // OperationsLabel
            // 
            this.OperationsLabel.AutoSize = true;
            this.OperationsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.OperationsLabel.Location = new System.Drawing.Point(12, 398);
            this.OperationsLabel.Name = "OperationsLabel";
            this.OperationsLabel.Size = new System.Drawing.Size(114, 20);
            this.OperationsLabel.TabIndex = 4;
            this.OperationsLabel.Text = "Отсоритровать";
            // 
            // LinesList
            // 
            this.LinesList.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.LinesList.FormattingEnabled = true;
            this.LinesList.HorizontalScrollbar = true;
            this.LinesList.ItemHeight = 18;
            this.LinesList.Location = new System.Drawing.Point(12, 37);
            this.LinesList.Name = "LinesList";
            this.LinesList.ScrollAlwaysVisible = true;
            this.LinesList.Size = new System.Drawing.Size(480, 346);
            this.LinesList.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(178, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Таблица меток";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 434);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LinesList);
            this.Controls.Add(this.OperationsLabel);
            this.Controls.Add(this.OperationsList);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.OpenFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.OpenFileDialog OpenSource;
        private System.Windows.Forms.ComboBox OperationsList;
        private System.Windows.Forms.Label OperationsLabel;
        private System.Windows.Forms.ListBox LinesList;
        private System.Windows.Forms.Label label1;
    }
}

