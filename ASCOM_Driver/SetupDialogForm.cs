/*
 * SetupDialogForm.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.Utilities;

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.DarkSkyGeek
{
    // Form not registered for COM!
    [ComVisible(false)]

    public partial class SetupDialogForm : Form
    {
        // Holder for a reference to the driver's trace logger
        readonly VirtualFocuser virtualFocuserInstance;

        public SetupDialogForm(VirtualFocuser virtualFocuserInstance)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            this.virtualFocuserInstance = virtualFocuserInstance;
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            Profile profile = new Profile();

            // Populate focuser device list...
            ArrayList focuserDevices = profile.RegisteredDevices("Focuser");
            foreach (KeyValuePair kv in focuserDevices)
            {
                // Don't include the virtual focuser in the list, for obvious reasons...
                if (kv.Key != VirtualFocuser.driverID)
                {
                    ComboboxItem item = new ComboboxItem
                    {
                        Text = kv.Value,
                        Value = kv.Key
                    };
                    int index = focuserSelectorComboBox.Items.Add(item);
                    // Select newly added item if it matches the value stored in the profile.
                    if (kv.Key == virtualFocuserInstance.focuserId)
                    {
                        focuserSelectorComboBox.SelectedIndex = index;
                    }
                }
            }

            // Populate observing conditions device list...
            ArrayList observingConditionsDevices = profile.RegisteredDevices("ObservingConditions");
            foreach (KeyValuePair kv in observingConditionsDevices)
            {
                ComboboxItem item = new ComboboxItem
                {
                    Text = kv.Value,
                    Value = kv.Key
                };
                int index = observingConditionsDeviceComboBox.Items.Add(item);
                // Select newly added item if it matches the value stored in the profile.
                if (kv.Key == virtualFocuserInstance.observingConditionsDeviceId)
                {
                    observingConditionsDeviceComboBox.SelectedIndex = index;
                }
            }

            useFocuserForTemperatureRadioBtn.Checked = (virtualFocuserInstance.temperatureSource == TemperatureSource.FOCUSER);
            useObservingConditionsDeviceForTemperatureRadioBtn.Checked = (virtualFocuserInstance.temperatureSource == TemperatureSource.OBSERVING_CONDITIONS_DEVICE);

            chkTrace.Checked = virtualFocuserInstance.tl.Enabled;
            positionToleranceNumericUpDown.Value = virtualFocuserInstance.positionTolerance;
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            if (focuserSelectorComboBox.SelectedItem != null)
            {
                virtualFocuserInstance.focuserId = (focuserSelectorComboBox.SelectedItem as ComboboxItem).Value;
            }

            if (useObservingConditionsDeviceForTemperatureRadioBtn.Checked && observingConditionsDeviceComboBox.SelectedItem != null)
            {
                virtualFocuserInstance.temperatureSource = TemperatureSource.OBSERVING_CONDITIONS_DEVICE;
                virtualFocuserInstance.observingConditionsDeviceId = (observingConditionsDeviceComboBox.SelectedItem as ComboboxItem).Value;
            }
            else
            {
                virtualFocuserInstance.temperatureSource = TemperatureSource.FOCUSER;
            }

            virtualFocuserInstance.tl.Enabled = chkTrace.Checked;
            virtualFocuserInstance.positionTolerance = (uint) positionToleranceNumericUpDown.Value;
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void BrowseToHomepage(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/jlecomte/ascom-virtual-focuser");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void useObservingConditionsDeviceRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            observingConditionsDeviceComboBox.Enabled = (sender as RadioButton).Checked;
        }

    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}