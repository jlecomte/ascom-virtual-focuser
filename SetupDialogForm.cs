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
        TraceLogger tl;

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;
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
                    ComboboxItem item = new ComboboxItem();
                    item.Text = kv.Value;
                    item.Value = kv.Key;
                    int index = focuserSelectorComboBox.Items.Add(item);
                    // Select newly added item if it matches the value stored in the profile.
                    if (kv.Key == VirtualFocuser.focuserId)
                    {
                        focuserSelectorComboBox.SelectedIndex = index;
                    }
                }
            }

            chkTrace.Checked = tl.Enabled;
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            if (focuserSelectorComboBox.SelectedItem != null)
            {
                VirtualFocuser.focuserId = (focuserSelectorComboBox.SelectedItem as ComboboxItem).Value;
            }

            tl.Enabled = chkTrace.Checked;
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