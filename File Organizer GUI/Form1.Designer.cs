namespace File_Organizer_GUI
{
    partial class Form1
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
            this.moveFilesBtn = new System.Windows.Forms.Button();
            this.undoBtn = new System.Windows.Forms.Button();
            this.arrangeFilesBtn = new System.Windows.Forms.Button();
            this.pathNameLbl = new System.Windows.Forms.Label();
            this.currentPathTxt = new System.Windows.Forms.TextBox();
            this.infoLogLbl = new System.Windows.Forms.Label();
            this.logsList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // moveFilesBtn
            // 
            this.moveFilesBtn.Location = new System.Drawing.Point(12, 338);
            this.moveFilesBtn.Name = "moveFilesBtn";
            this.moveFilesBtn.Size = new System.Drawing.Size(140, 40);
            this.moveFilesBtn.TabIndex = 0;
            this.moveFilesBtn.Text = "Move Files";
            this.moveFilesBtn.UseVisualStyleBackColor = true;
            this.moveFilesBtn.Click += new System.EventHandler(this.moveFilesBtn_Click);
            // 
            // undoBtn
            // 
            this.undoBtn.Location = new System.Drawing.Point(619, 338);
            this.undoBtn.Name = "undoBtn";
            this.undoBtn.Size = new System.Drawing.Size(140, 40);
            this.undoBtn.TabIndex = 1;
            this.undoBtn.Text = "Undo";
            this.undoBtn.UseVisualStyleBackColor = true;
            this.undoBtn.Click += new System.EventHandler(this.undoBtn_Click);
            // 
            // arrangeFilesBtn
            // 
            this.arrangeFilesBtn.Location = new System.Drawing.Point(332, 338);
            this.arrangeFilesBtn.Name = "arrangeFilesBtn";
            this.arrangeFilesBtn.Size = new System.Drawing.Size(140, 40);
            this.arrangeFilesBtn.TabIndex = 2;
            this.arrangeFilesBtn.Text = "Arrange Files";
            this.arrangeFilesBtn.UseVisualStyleBackColor = true;
            this.arrangeFilesBtn.Click += new System.EventHandler(this.arrangeFilesBtn_Click);
            // 
            // pathNameLbl
            // 
            this.pathNameLbl.AutoSize = true;
            this.pathNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathNameLbl.Location = new System.Drawing.Point(9, 41);
            this.pathNameLbl.Name = "pathNameLbl";
            this.pathNameLbl.Size = new System.Drawing.Size(88, 15);
            this.pathNameLbl.TabIndex = 3;
            this.pathNameLbl.Text = "Current Folder:";
            // 
            // currentPathTxt
            // 
            this.currentPathTxt.Location = new System.Drawing.Point(103, 40);
            this.currentPathTxt.Name = "currentPathTxt";
            this.currentPathTxt.Size = new System.Drawing.Size(410, 20);
            this.currentPathTxt.TabIndex = 4;
            // 
            // infoLogLbl
            // 
            this.infoLogLbl.AutoSize = true;
            this.infoLogLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLogLbl.Location = new System.Drawing.Point(12, 80);
            this.infoLogLbl.Name = "infoLogLbl";
            this.infoLogLbl.Size = new System.Drawing.Size(54, 15);
            this.infoLogLbl.TabIndex = 5;
            this.infoLogLbl.Text = "Info Log:";
            // 
            // logsList
            // 
            this.logsList.FormattingEnabled = true;
            this.logsList.Location = new System.Drawing.Point(12, 111);
            this.logsList.Name = "logsList";
            this.logsList.Size = new System.Drawing.Size(747, 199);
            this.logsList.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 390);
            this.Controls.Add(this.logsList);
            this.Controls.Add(this.infoLogLbl);
            this.Controls.Add(this.currentPathTxt);
            this.Controls.Add(this.pathNameLbl);
            this.Controls.Add(this.arrangeFilesBtn);
            this.Controls.Add(this.undoBtn);
            this.Controls.Add(this.moveFilesBtn);
            this.Name = "Form1";
            this.Text = "File Organizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button moveFilesBtn;
        private System.Windows.Forms.Button undoBtn;
        private System.Windows.Forms.Button arrangeFilesBtn;
        private System.Windows.Forms.Label pathNameLbl;
        private System.Windows.Forms.TextBox currentPathTxt;
        private System.Windows.Forms.Label infoLogLbl;
        private System.Windows.Forms.ListBox logsList;
    }
}

