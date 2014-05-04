namespace BILiteMain
{
    partial class BILiteConnForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SubmitToDBButton = new System.Windows.Forms.Button();
            this.ConnectionNameTextBox = new System.Windows.Forms.TextBox();
            this.ConnectionLocationTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.WinAuthCheckBox = new System.Windows.Forms.CheckBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DatabaseDropDown = new System.Windows.Forms.ComboBox();
            this.ServerConnectButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Establish A New Database Connection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "New Connection Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "New Connection Name";
            // 
            // SubmitToDBButton
            // 
            this.SubmitToDBButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitToDBButton.Location = new System.Drawing.Point(225, 480);
            this.SubmitToDBButton.Name = "SubmitToDBButton";
            this.SubmitToDBButton.Size = new System.Drawing.Size(125, 23);
            this.SubmitToDBButton.TabIndex = 3;
            this.SubmitToDBButton.Text = "Save Connection";
            this.SubmitToDBButton.UseVisualStyleBackColor = true;
            // 
            // ConnectionNameTextBox
            // 
            this.ConnectionNameTextBox.Location = new System.Drawing.Point(163, 73);
            this.ConnectionNameTextBox.Name = "ConnectionNameTextBox";
            this.ConnectionNameTextBox.Size = new System.Drawing.Size(187, 20);
            this.ConnectionNameTextBox.TabIndex = 4;
            // 
            // ConnectionLocationTextBox
            // 
            this.ConnectionLocationTextBox.Location = new System.Drawing.Point(165, 111);
            this.ConnectionLocationTextBox.Name = "ConnectionLocationTextBox";
            this.ConnectionLocationTextBox.Size = new System.Drawing.Size(187, 20);
            this.ConnectionLocationTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(69, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Authentication Type";
            // 
            // WinAuthCheckBox
            // 
            this.WinAuthCheckBox.AutoSize = true;
            this.WinAuthCheckBox.Location = new System.Drawing.Point(109, 190);
            this.WinAuthCheckBox.Name = "WinAuthCheckBox";
            this.WinAuthCheckBox.Size = new System.Drawing.Size(141, 17);
            this.WinAuthCheckBox.TabIndex = 7;
            this.WinAuthCheckBox.Text = "Windows Authentication";
            this.WinAuthCheckBox.UseVisualStyleBackColor = true;
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(164, 263);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.UserNameTextBox.TabIndex = 8;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(164, 292);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(100, 20);
            this.PasswordTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "User name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(100, 292);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Password";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(89, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "SQL Server Authentication";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 388);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Database";
            // 
            // DatabaseDropDown
            // 
            this.DatabaseDropDown.FormattingEnabled = true;
            this.DatabaseDropDown.Location = new System.Drawing.Point(155, 385);
            this.DatabaseDropDown.Name = "DatabaseDropDown";
            this.DatabaseDropDown.Size = new System.Drawing.Size(189, 21);
            this.DatabaseDropDown.TabIndex = 14;
            // 
            // ServerConnectButton
            // 
            this.ServerConnectButton.Location = new System.Drawing.Point(72, 336);
            this.ServerConnectButton.Name = "ServerConnectButton";
            this.ServerConnectButton.Size = new System.Drawing.Size(125, 23);
            this.ServerConnectButton.TabIndex = 15;
            this.ServerConnectButton.Text = "Connect to Server";
            this.ServerConnectButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(12, 480);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(125, 23);
            this.CancelButton.TabIndex = 16;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // BILiteConnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 562);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ServerConnectButton);
            this.Controls.Add(this.DatabaseDropDown);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.WinAuthCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ConnectionLocationTextBox);
            this.Controls.Add(this.ConnectionNameTextBox);
            this.Controls.Add(this.SubmitToDBButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BILiteConnForm";
            this.RightToLeftLayout = true;
            this.Text = "New Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SubmitToDBButton;
        private System.Windows.Forms.TextBox ConnectionNameTextBox;
        private System.Windows.Forms.TextBox ConnectionLocationTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox WinAuthCheckBox;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox DatabaseDropDown;
        private System.Windows.Forms.Button ServerConnectButton;
        private System.Windows.Forms.Button CancelButton;
    }
}

