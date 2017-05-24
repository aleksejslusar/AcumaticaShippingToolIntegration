using System.Windows.Forms;

namespace SmartShipment.UI.Forms
{
    partial class SmartShipmentSettingsDialog
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
            this.groupBoxAdvanced = new System.Windows.Forms.GroupBox();
            this.groupBoxAcumatica = new System.Windows.Forms.GroupBox();
            this.textBoxAcumaticaDefaultBoxId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAcumaticaConfirmShipment = new System.Windows.Forms.CheckBox();
            this.groupBoxFedex = new System.Windows.Forms.GroupBox();
            this.checkBoxFedexAddUpdateAddressBook = new System.Windows.Forms.CheckBox();
            this.groupBoxUps = new System.Windows.Forms.GroupBox();
            this.checkBoxUpsAddUpdateAddressBook = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBoxCredentials = new System.Windows.Forms.GroupBox();
            this.labelBaseUrl = new System.Windows.Forms.Label();
            this.textBoxBaseUrl = new System.Windows.Forms.TextBox();
            this.btnAcumaticaTestLogin = new System.Windows.Forms.Button();
            this.labelCompany = new System.Windows.Forms.Label();
            this.textAcumaticaCompany = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textAcumaticaPassword = new System.Windows.Forms.TextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.textAcumaticaLogin = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxAdvanced.SuspendLayout();
            this.groupBoxAcumatica.SuspendLayout();
            this.groupBoxFedex.SuspendLayout();
            this.groupBoxUps.SuspendLayout();
            this.groupBoxCredentials.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAdvanced
            // 
            this.groupBoxAdvanced.Controls.Add(this.groupBoxAcumatica);
            this.groupBoxAdvanced.Controls.Add(this.groupBoxFedex);
            this.groupBoxAdvanced.Controls.Add(this.groupBoxUps);
            this.groupBoxAdvanced.Location = new System.Drawing.Point(11, 159);
            this.groupBoxAdvanced.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxAdvanced.Name = "groupBoxAdvanced";
            this.groupBoxAdvanced.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxAdvanced.Size = new System.Drawing.Size(354, 226);
            this.groupBoxAdvanced.TabIndex = 2;
            this.groupBoxAdvanced.TabStop = false;
            this.groupBoxAdvanced.Text = "Advanced Settings";
            // 
            // groupBoxAcumatica
            // 
            this.groupBoxAcumatica.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAcumatica.Controls.Add(this.textBoxAcumaticaDefaultBoxId);
            this.groupBoxAcumatica.Controls.Add(this.label1);
            this.groupBoxAcumatica.Controls.Add(this.checkBoxAcumaticaConfirmShipment);
            this.groupBoxAcumatica.Location = new System.Drawing.Point(11, 17);
            this.groupBoxAcumatica.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxAcumatica.Name = "groupBoxAcumatica";
            this.groupBoxAcumatica.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxAcumatica.Size = new System.Drawing.Size(334, 84);
            this.groupBoxAcumatica.TabIndex = 6;
            this.groupBoxAcumatica.TabStop = false;
            this.groupBoxAcumatica.Text = "Acumatica";
            // 
            // textBoxAcumaticaDefaultBoxId
            // 
            this.textBoxAcumaticaDefaultBoxId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAcumaticaDefaultBoxId.Location = new System.Drawing.Point(136, 45);
            this.textBoxAcumaticaDefaultBoxId.Name = "textBoxAcumaticaDefaultBoxId";
            this.textBoxAcumaticaDefaultBoxId.Size = new System.Drawing.Size(193, 20);
            this.textBoxAcumaticaDefaultBoxId.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Default Package Box ID*";
            // 
            // checkBoxAcumaticaConfirmShipment
            // 
            this.checkBoxAcumaticaConfirmShipment.AutoSize = true;
            this.checkBoxAcumaticaConfirmShipment.Location = new System.Drawing.Point(11, 21);
            this.checkBoxAcumaticaConfirmShipment.Name = "checkBoxAcumaticaConfirmShipment";
            this.checkBoxAcumaticaConfirmShipment.Size = new System.Drawing.Size(108, 17);
            this.checkBoxAcumaticaConfirmShipment.TabIndex = 5;
            this.checkBoxAcumaticaConfirmShipment.Text = "Confirm Shipment";
            this.checkBoxAcumaticaConfirmShipment.UseVisualStyleBackColor = true;
            // 
            // groupBoxFedex
            // 
            this.groupBoxFedex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFedex.Controls.Add(this.checkBoxFedexAddUpdateAddressBook);
            this.groupBoxFedex.Location = new System.Drawing.Point(11, 161);
            this.groupBoxFedex.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxFedex.Name = "groupBoxFedex";
            this.groupBoxFedex.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxFedex.Size = new System.Drawing.Size(334, 53);
            this.groupBoxFedex.TabIndex = 8;
            this.groupBoxFedex.TabStop = false;
            this.groupBoxFedex.Text = "FedEx Ship Manager";
            // 
            // checkBoxFedexAddUpdateAddressBook
            // 
            this.checkBoxFedexAddUpdateAddressBook.AutoSize = true;
            this.checkBoxFedexAddUpdateAddressBook.Location = new System.Drawing.Point(11, 20);
            this.checkBoxFedexAddUpdateAddressBook.Name = "checkBoxFedexAddUpdateAddressBook";
            this.checkBoxFedexAddUpdateAddressBook.Size = new System.Drawing.Size(154, 17);
            this.checkBoxFedexAddUpdateAddressBook.TabIndex = 8;
            this.checkBoxFedexAddUpdateAddressBook.Text = "Add/Update Address Book";
            this.checkBoxFedexAddUpdateAddressBook.UseVisualStyleBackColor = true;
            // 
            // groupBoxUps
            // 
            this.groupBoxUps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUps.Controls.Add(this.checkBoxUpsAddUpdateAddressBook);
            this.groupBoxUps.Location = new System.Drawing.Point(11, 105);
            this.groupBoxUps.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxUps.Name = "groupBoxUps";
            this.groupBoxUps.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxUps.Size = new System.Drawing.Size(334, 53);
            this.groupBoxUps.TabIndex = 7;
            this.groupBoxUps.TabStop = false;
            this.groupBoxUps.Text = "UPS WorldShip";
            // 
            // checkBoxUpsAddUpdateAddressBook
            // 
            this.checkBoxUpsAddUpdateAddressBook.AutoSize = true;
            this.checkBoxUpsAddUpdateAddressBook.Location = new System.Drawing.Point(11, 20);
            this.checkBoxUpsAddUpdateAddressBook.Name = "checkBoxUpsAddUpdateAddressBook";
            this.checkBoxUpsAddUpdateAddressBook.Size = new System.Drawing.Size(154, 17);
            this.checkBoxUpsAddUpdateAddressBook.TabIndex = 7;
            this.checkBoxUpsAddUpdateAddressBook.Text = "Add/Update Address Book";
            this.checkBoxUpsAddUpdateAddressBook.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(87, 393);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 24);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // groupBoxCredentials
            // 
            this.groupBoxCredentials.Controls.Add(this.labelBaseUrl);
            this.groupBoxCredentials.Controls.Add(this.textBoxBaseUrl);
            this.groupBoxCredentials.Controls.Add(this.btnAcumaticaTestLogin);
            this.groupBoxCredentials.Controls.Add(this.labelCompany);
            this.groupBoxCredentials.Controls.Add(this.textAcumaticaCompany);
            this.groupBoxCredentials.Controls.Add(this.labelPassword);
            this.groupBoxCredentials.Controls.Add(this.textAcumaticaPassword);
            this.groupBoxCredentials.Controls.Add(this.labelLogin);
            this.groupBoxCredentials.Controls.Add(this.textAcumaticaLogin);
            this.groupBoxCredentials.Location = new System.Drawing.Point(11, 11);
            this.groupBoxCredentials.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxCredentials.Name = "groupBoxCredentials";
            this.groupBoxCredentials.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxCredentials.Size = new System.Drawing.Size(354, 145);
            this.groupBoxCredentials.TabIndex = 0;
            this.groupBoxCredentials.TabStop = false;
            this.groupBoxCredentials.Text = "Credentials ";
            // 
            // labelBaseUrl
            // 
            this.labelBaseUrl.AutoSize = true;
            this.labelBaseUrl.Location = new System.Drawing.Point(8, 19);
            this.labelBaseUrl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBaseUrl.Name = "labelBaseUrl";
            this.labelBaseUrl.Size = new System.Drawing.Size(77, 13);
            this.labelBaseUrl.TabIndex = 9;
            this.labelBaseUrl.Text = "Acumatica Url*";
            // 
            // textBoxBaseUrl
            // 
            this.textBoxBaseUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBaseUrl.Location = new System.Drawing.Point(89, 16);
            this.textBoxBaseUrl.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxBaseUrl.Name = "textBoxBaseUrl";
            this.textBoxBaseUrl.Size = new System.Drawing.Size(251, 20);
            this.textBoxBaseUrl.TabIndex = 0;
            // 
            // btnAcumaticaTestLogin
            // 
            this.btnAcumaticaTestLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAcumaticaTestLogin.Location = new System.Drawing.Point(240, 112);
            this.btnAcumaticaTestLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnAcumaticaTestLogin.Name = "btnAcumaticaTestLogin";
            this.btnAcumaticaTestLogin.Size = new System.Drawing.Size(100, 24);
            this.btnAcumaticaTestLogin.TabIndex = 0;
            this.btnAcumaticaTestLogin.TabStop = false;
            this.btnAcumaticaTestLogin.Text = "Test";
            // 
            // labelCompany
            // 
            this.labelCompany.AutoSize = true;
            this.labelCompany.Location = new System.Drawing.Point(8, 88);
            this.labelCompany.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(51, 13);
            this.labelCompany.TabIndex = 7;
            this.labelCompany.Text = "Company";
            // 
            // textAcumaticaCompany
            // 
            this.textAcumaticaCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAcumaticaCompany.Location = new System.Drawing.Point(89, 85);
            this.textAcumaticaCompany.Margin = new System.Windows.Forms.Padding(2);
            this.textAcumaticaCompany.Name = "textAcumaticaCompany";
            this.textAcumaticaCompany.Size = new System.Drawing.Size(251, 20);
            this.textAcumaticaCompany.TabIndex = 4;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(8, 65);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(57, 13);
            this.labelPassword.TabIndex = 5;
            this.labelPassword.Text = "Password*";
            // 
            // textAcumaticaPassword
            // 
            this.textAcumaticaPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAcumaticaPassword.Location = new System.Drawing.Point(89, 62);
            this.textAcumaticaPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textAcumaticaPassword.Name = "textAcumaticaPassword";
            this.textAcumaticaPassword.PasswordChar = '*';
            this.textAcumaticaPassword.Size = new System.Drawing.Size(251, 20);
            this.textAcumaticaPassword.TabIndex = 3;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(8, 42);
            this.labelLogin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(37, 13);
            this.labelLogin.TabIndex = 3;
            this.labelLogin.Text = "Login*";
            // 
            // textAcumaticaLogin
            // 
            this.textAcumaticaLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAcumaticaLogin.Location = new System.Drawing.Point(89, 39);
            this.textAcumaticaLogin.Margin = new System.Windows.Forms.Padding(2);
            this.textAcumaticaLogin.Name = "textAcumaticaLogin";
            this.textAcumaticaLogin.Size = new System.Drawing.Size(251, 20);
            this.textAcumaticaLogin.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(191, 393);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // SmartShipmentSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 424);
            this.Controls.Add(this.groupBoxAdvanced);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBoxCredentials);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmartShipmentSettingsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{applicationTitle} Settings | v.{version}";
            this.TopMost = true;
            this.groupBoxAdvanced.ResumeLayout(false);
            this.groupBoxAcumatica.ResumeLayout(false);
            this.groupBoxAcumatica.PerformLayout();
            this.groupBoxFedex.ResumeLayout(false);
            this.groupBoxFedex.PerformLayout();
            this.groupBoxUps.ResumeLayout(false);
            this.groupBoxUps.PerformLayout();
            this.groupBoxCredentials.ResumeLayout(false);
            this.groupBoxCredentials.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBoxAdvanced;
        private System.Windows.Forms.Button btnAcumaticaTestLogin;
        private System.Windows.Forms.GroupBox groupBoxCredentials;
        private System.Windows.Forms.Label labelPassword;        
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.GroupBox groupBoxAcumatica;
        private System.Windows.Forms.Label labelCompany;
        private System.Windows.Forms.TextBox textAcumaticaLogin;
        private System.Windows.Forms.TextBox textAcumaticaPassword;
        private System.Windows.Forms.TextBox textAcumaticaCompany;
        private System.Windows.Forms.CheckBox checkBoxAcumaticaConfirmShipment;
        private System.Windows.Forms.GroupBox groupBoxFedex;
        private System.Windows.Forms.CheckBox checkBoxFedexAddUpdateAddressBook;
        private System.Windows.Forms.GroupBox groupBoxUps;
        private System.Windows.Forms.CheckBox checkBoxUpsAddUpdateAddressBook;
        private Label labelBaseUrl;
        private TextBox textBoxBaseUrl;
        private TextBox textBoxAcumaticaDefaultBoxId;
        private Label label1;
    }
}