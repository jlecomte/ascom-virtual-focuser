namespace ASCOM.DarkSkyGeek
{
    partial class SetupDialogForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.DSGLogo = new System.Windows.Forms.PictureBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.focuserSelectorLabel = new System.Windows.Forms.Label();
            this.focuserSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.positionToleranceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.tempSourceGroupBox = new System.Windows.Forms.GroupBox();
            this.observingConditionsDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.useObservingConditionsDeviceForTemperatureRadioBtn = new System.Windows.Forms.RadioButton();
            this.useFocuserForTemperatureRadioBtn = new System.Windows.Forms.RadioButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DSGLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionToleranceNumericUpDown)).BeginInit();
            this.tempSourceGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_ok_24;
            this.cmdOK.Location = new System.Drawing.Point(368, 350);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 36);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.icon_cancel_24;
            this.cmdCancel.Location = new System.Drawing.Point(433, 350);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 36);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // chkTrace
            // 
            this.chkTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(12, 369);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // DSGLogo
            // 
            this.DSGLogo.Image = global::ASCOM.DarkSkyGeek.Properties.Resources.darkskygeek;
            this.DSGLogo.Location = new System.Drawing.Point(12, 12);
            this.DSGLogo.Name = "DSGLogo";
            this.DSGLogo.Size = new System.Drawing.Size(88, 88);
            this.DSGLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DSGLogo.TabIndex = 7;
            this.DSGLogo.TabStop = false;
            this.DSGLogo.Click += new System.EventHandler(this.BrowseToHomepage);
            this.DSGLogo.DoubleClick += new System.EventHandler(this.BrowseToHomepage);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.Location = new System.Drawing.Point(107, 13);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(385, 87);
            this.descriptionLabel.TabIndex = 8;
            this.descriptionLabel.Text = resources.GetString("descriptionLabel.Text");
            // 
            // focuserSelectorLabel
            // 
            this.focuserSelectorLabel.AutoSize = true;
            this.focuserSelectorLabel.Location = new System.Drawing.Point(13, 123);
            this.focuserSelectorLabel.Name = "focuserSelectorLabel";
            this.focuserSelectorLabel.Size = new System.Drawing.Size(48, 13);
            this.focuserSelectorLabel.TabIndex = 9;
            this.focuserSelectorLabel.Text = "Focuser:";
            // 
            // focuserSelectorComboBox
            // 
            this.focuserSelectorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.focuserSelectorComboBox.FormattingEnabled = true;
            this.focuserSelectorComboBox.Location = new System.Drawing.Point(67, 120);
            this.focuserSelectorComboBox.Name = "focuserSelectorComboBox";
            this.focuserSelectorComboBox.Size = new System.Drawing.Size(424, 21);
            this.focuserSelectorComboBox.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Position Tolerance:";
            // 
            // positionToleranceNumericUpDown
            // 
            this.positionToleranceNumericUpDown.Location = new System.Drawing.Point(117, 155);
            this.positionToleranceNumericUpDown.Name = "positionToleranceNumericUpDown";
            this.positionToleranceNumericUpDown.Size = new System.Drawing.Size(54, 20);
            this.positionToleranceNumericUpDown.TabIndex = 13;
            // 
            // tempSourceGroupBox
            // 
            this.tempSourceGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tempSourceGroupBox.Controls.Add(this.observingConditionsDeviceComboBox);
            this.tempSourceGroupBox.Controls.Add(this.useObservingConditionsDeviceForTemperatureRadioBtn);
            this.tempSourceGroupBox.Controls.Add(this.useFocuserForTemperatureRadioBtn);
            this.tempSourceGroupBox.Location = new System.Drawing.Point(16, 194);
            this.tempSourceGroupBox.Name = "tempSourceGroupBox";
            this.tempSourceGroupBox.Size = new System.Drawing.Size(474, 119);
            this.tempSourceGroupBox.TabIndex = 14;
            this.tempSourceGroupBox.TabStop = false;
            this.tempSourceGroupBox.Text = "Source of temperature readings";
            // 
            // observingConditionsDeviceComboBox
            // 
            this.observingConditionsDeviceComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.observingConditionsDeviceComboBox.Enabled = false;
            this.observingConditionsDeviceComboBox.FormattingEnabled = true;
            this.observingConditionsDeviceComboBox.Location = new System.Drawing.Point(17, 78);
            this.observingConditionsDeviceComboBox.Name = "observingConditionsDeviceComboBox";
            this.observingConditionsDeviceComboBox.Size = new System.Drawing.Size(438, 21);
            this.observingConditionsDeviceComboBox.TabIndex = 2;
            // 
            // useObservingConditionsDeviceForTemperatureRadioBtn
            // 
            this.useObservingConditionsDeviceForTemperatureRadioBtn.AutoSize = true;
            this.useObservingConditionsDeviceForTemperatureRadioBtn.Location = new System.Drawing.Point(17, 47);
            this.useObservingConditionsDeviceForTemperatureRadioBtn.Name = "useObservingConditionsDeviceForTemperatureRadioBtn";
            this.useObservingConditionsDeviceForTemperatureRadioBtn.Size = new System.Drawing.Size(353, 17);
            this.useObservingConditionsDeviceForTemperatureRadioBtn.TabIndex = 1;
            this.useObservingConditionsDeviceForTemperatureRadioBtn.Text = "Use a device able to report observing conditions among the following:";
            this.useObservingConditionsDeviceForTemperatureRadioBtn.UseVisualStyleBackColor = true;
            this.useObservingConditionsDeviceForTemperatureRadioBtn.CheckedChanged += new System.EventHandler(this.useObservingConditionsDeviceRadioBtn_CheckedChanged);
            // 
            // useFocuserForTemperatureRadioBtn
            // 
            this.useFocuserForTemperatureRadioBtn.AutoSize = true;
            this.useFocuserForTemperatureRadioBtn.Checked = true;
            this.useFocuserForTemperatureRadioBtn.Location = new System.Drawing.Point(17, 24);
            this.useFocuserForTemperatureRadioBtn.Name = "useFocuserForTemperatureRadioBtn";
            this.useFocuserForTemperatureRadioBtn.Size = new System.Drawing.Size(158, 17);
            this.useFocuserForTemperatureRadioBtn.TabIndex = 0;
            this.useFocuserForTemperatureRadioBtn.TabStop = true;
            this.useFocuserForTemperatureRadioBtn.Text = "Use focuser selected above";
            this.useFocuserForTemperatureRadioBtn.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 394);
            this.Controls.Add(this.tempSourceGroupBox);
            this.Controls.Add(this.positionToleranceNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.focuserSelectorComboBox);
            this.Controls.Add(this.focuserSelectorLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.DSGLogo);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DarkSkyGeek’s Virtual Focuser ASCOM Driver";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DSGLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionToleranceNumericUpDown)).EndInit();
            this.tempSourceGroupBox.ResumeLayout(false);
            this.tempSourceGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.PictureBox DSGLogo;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label focuserSelectorLabel;
        private System.Windows.Forms.ComboBox focuserSelectorComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown positionToleranceNumericUpDown;
        private System.Windows.Forms.GroupBox tempSourceGroupBox;
        private System.Windows.Forms.ComboBox observingConditionsDeviceComboBox;
        private System.Windows.Forms.RadioButton useObservingConditionsDeviceForTemperatureRadioBtn;
        private System.Windows.Forms.RadioButton useFocuserForTemperatureRadioBtn;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}