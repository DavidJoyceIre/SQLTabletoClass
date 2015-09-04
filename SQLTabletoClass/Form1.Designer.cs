namespace SQLTabletoClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetTables = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTables = new System.Windows.Forms.ComboBox();
            this.btnConvertToClass = new System.Windows.Forms.Button();
            this.txtClass = new System.Windows.Forms.RichTextBox();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnConvertToVBClass = new System.Windows.Forms.Button();
            this.btnDatabasetoVB = new System.Windows.Forms.Button();
            this.btnDatabaseToC = new System.Windows.Forms.Button();
            this.cboIS = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(7, 23);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(222, 22);
            this.txtServer.TabIndex = 0;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(7, 69);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(222, 22);
            this.txtDatabase.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(7, 115);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(222, 22);
            this.txtUserID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "User ID";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(7, 161);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(222, 22);
            this.txtPassword.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // btnGetTables
            // 
            this.btnGetTables.Location = new System.Drawing.Point(7, 250);
            this.btnGetTables.Name = "btnGetTables";
            this.btnGetTables.Size = new System.Drawing.Size(222, 39);
            this.btnGetTables.TabIndex = 4;
            this.btnGetTables.Text = "Get Tables";
            this.btnGetTables.UseVisualStyleBackColor = true;
            this.btnGetTables.Click += new System.EventHandler(this.btnGetTables_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tables";
            // 
            // cboTables
            // 
            this.cboTables.DisplayMember = "TABLE_NAME";
            this.cboTables.FormattingEnabled = true;
            this.cboTables.Location = new System.Drawing.Point(7, 313);
            this.cboTables.Name = "cboTables";
            this.cboTables.Size = new System.Drawing.Size(222, 24);
            this.cboTables.TabIndex = 10;
            this.cboTables.ValueMember = "TABLE_NAME";
            // 
            // btnConvertToClass
            // 
            this.btnConvertToClass.Location = new System.Drawing.Point(7, 341);
            this.btnConvertToClass.Name = "btnConvertToClass";
            this.btnConvertToClass.Size = new System.Drawing.Size(222, 39);
            this.btnConvertToClass.TabIndex = 5;
            this.btnConvertToClass.Text = "Convert to C# Class";
            this.btnConvertToClass.UseVisualStyleBackColor = true;
            this.btnConvertToClass.Click += new System.EventHandler(this.btnConvertToClass_Click);
            // 
            // txtClass
            // 
            this.txtClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClass.Location = new System.Drawing.Point(235, 3);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(727, 576);
            this.txtClass.TabIndex = 12;
            this.txtClass.Text = "";
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveToFile.Location = new System.Drawing.Point(7, 540);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(222, 39);
            this.btnSaveToFile.TabIndex = 9;
            this.btnSaveToFile.Text = "Save to File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnConvertToVBClass
            // 
            this.btnConvertToVBClass.Location = new System.Drawing.Point(7, 384);
            this.btnConvertToVBClass.Name = "btnConvertToVBClass";
            this.btnConvertToVBClass.Size = new System.Drawing.Size(222, 39);
            this.btnConvertToVBClass.TabIndex = 6;
            this.btnConvertToVBClass.Text = "Convert to VB Class";
            this.btnConvertToVBClass.UseVisualStyleBackColor = true;
            this.btnConvertToVBClass.Click += new System.EventHandler(this.btnConvertToVBClass_Click);
            // 
            // btnDatabasetoVB
            // 
            this.btnDatabasetoVB.Location = new System.Drawing.Point(7, 470);
            this.btnDatabasetoVB.Name = "btnDatabasetoVB";
            this.btnDatabasetoVB.Size = new System.Drawing.Size(222, 39);
            this.btnDatabasetoVB.TabIndex = 8;
            this.btnDatabasetoVB.Text = "Convert Database to VB";
            this.btnDatabasetoVB.UseVisualStyleBackColor = true;
            this.btnDatabasetoVB.Click += new System.EventHandler(this.btnDatabasetoVB_Click);
            // 
            // btnDatabaseToC
            // 
            this.btnDatabaseToC.Location = new System.Drawing.Point(7, 427);
            this.btnDatabaseToC.Name = "btnDatabaseToC";
            this.btnDatabaseToC.Size = new System.Drawing.Size(222, 39);
            this.btnDatabaseToC.TabIndex = 7;
            this.btnDatabaseToC.Text = "Convert Database to C#";
            this.btnDatabaseToC.UseVisualStyleBackColor = true;
            this.btnDatabaseToC.Click += new System.EventHandler(this.btnDatabaseToC_Click);
            // 
            // cboIS
            // 
            this.cboIS.DisplayMember = "TABLE_NAME";
            this.cboIS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIS.FormattingEnabled = true;
            this.cboIS.Items.AddRange(new object[] {
            "SSPI",
            "False"});
            this.cboIS.Location = new System.Drawing.Point(7, 207);
            this.cboIS.Name = "cboIS";
            this.cboIS.Size = new System.Drawing.Size(222, 24);
            this.cboIS.TabIndex = 4;
            this.cboIS.ValueMember = "TABLE_NAME";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Integrated Security";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 581);
            this.Controls.Add(this.cboIS);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnDatabaseToC);
            this.Controls.Add(this.btnDatabasetoVB);
            this.Controls.Add(this.btnConvertToVBClass);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.btnConvertToClass);
            this.Controls.Add(this.cboTables);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGetTables);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Converts an SQL table to a Class";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGetTables;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboTables;
        private System.Windows.Forms.Button btnConvertToClass;
        private System.Windows.Forms.RichTextBox txtClass;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnConvertToVBClass;
        private System.Windows.Forms.Button btnDatabasetoVB;
        private System.Windows.Forms.Button btnDatabaseToC;
        private System.Windows.Forms.ComboBox cboIS;
        private System.Windows.Forms.Label label6;
    }
}

