/*
 * FocuserDriver.cs
 * Copyright (C) 2022 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.DarkSkyGeek.Properties;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace ASCOM.DarkSkyGeek
{
    //
    // Your driver's DeviceID is ASCOM.DarkSkyGeek.VirtualFocuser
    //

    /// <summary>
    /// Virtual ASCOM Focuser Driver for DarkSkyGeek.
    /// </summary>
    [Guid("4447c452-a343-49ff-9b3a-46d0d8b83e1b")]
    [ClassInterface(ClassInterfaceType.None)]
    public class VirtualFocuser : IFocuserV3
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.DarkSkyGeek.VirtualFocuser";

        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "DarkSkyGeek’s Virtual Focuser ASCOM Driver";

        // Constants used for Profile persistence
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        internal static string focuserIdProfileName = "Focuser ID";
        internal static string focuserIdDefault = string.Empty;

        // Variables to hold the current device configuration
        internal static string focuserId = string.Empty;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal TraceLogger tl;

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        /// Private variable to hold a reference to the real focuser we're controlling
        /// </summary>
        private ASCOM.DriverAccess.Focuser focuser;

        /// <summary>
        /// Private variable to hold the temperature values we were able to obtain in the last 60 seconds
        /// (the queue length is therefore <= 60)
        /// </summary>
        private Queue<TemperatureReading> temperatures = new Queue<TemperatureReading>();

        /// <summary>
        /// Private variable used for thread synchronization
        /// </summary>
        private readonly object _temperaturesLockObject = new object();

        /// <summary>
        /// Constant defining how many seconds to consider when computing the average temperature value.
        /// </summary>
        public const int TEMPERATURE_WINDOW_IN_SECONDS = 120;
        private int? _lastRequestedPosition = null;

        public VirtualFocuser()
        {
            tl = new TraceLogger("", "DarkSkyGeek.VirtualFocuser");

            ReadProfile();

            tl.LogMessage("VirtualFocuser", "Starting initialization");

            connectedState = false;

            ThreadStart threadDelegate = new ThreadStart(readCurrentTemperature);
            Thread readTemperatureThread = new Thread(threadDelegate);
            readTemperatureThread.Start();

            tl.LogMessage("VirtualFocuser", "Completed initialization");
        }

        private void readCurrentTemperature()
        {
            for (; ; ) {
                lock (_temperaturesLockObject)
                {
                    long now = DateTimeOffset.Now.ToUnixTimeSeconds();

                    // Remove any temperature reading older than 60 seconds.
                    temperatures = new Queue<TemperatureReading>(temperatures.Where(x => x.Timestamp >= now - TEMPERATURE_WINDOW_IN_SECONDS));

                    // Add new temperature reading, if we are connected to the focuser.
                    if (connectedState)
                    {
                        try
                        {
                            TemperatureReading temperatureReading = new TemperatureReading();
                            // This will throw if the focuser cannot report the temperature, hence the try ... catch!
                            temperatureReading.Temperature = focuser.Temperature;
                            temperatureReading.Timestamp = now;
                            temperatures.Enqueue(temperatureReading);
                        }
                        catch (Exception)
                        {
                            // ignore
                        }
                    }
                }

                Thread.Sleep(1000);
            }
        }

        //
        // PUBLIC COM INTERFACE IFocuserV3 IMPLEMENTATION
        //

        #region Common properties and methods.

        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm(tl))
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                ArrayList supportedActions = new ArrayList();
                if (connectedState)
                {
                    ArrayList focuserActions = focuser.SupportedActions;
                    if (focuserActions != null)
                    {
                        supportedActions.AddRange(focuserActions);
                    }
                }
                return supportedActions;
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            // TODO: If an action does not require the focuser to be connected,
            // i.e., it is a "virtual" action, you will need to update this code.
            CheckConnected("Action");
            return focuser.Action(actionName, actionParameters);
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            focuser.CommandBlind(command, raw);
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            return focuser.CommandBool(command, raw);
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            return focuser.CommandString(command, raw);
        }

        public void Dispose()
        {
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
        }

        public bool Connected
        {
            get
            {
                LogMessage("Connected", "Get {0}", IsConnected);
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected", "Set {0}", value);
                if (value == IsConnected)
                    return;

                if (value)
                {
                    if (string.IsNullOrEmpty(focuserId))
                    {
                        throw new ASCOM.InvalidValueException("You have not specified which focuser to connect to");
                    }

                    lock (_temperaturesLockObject)
                    {
                        try
                        {
                            focuser = new ASCOM.DriverAccess.Focuser(focuserId);
                            focuser.Connected = true;

                            connectedState = true;
                        }
                        catch (Exception)
                        {
                            throw new ASCOM.DriverException("Cannot connect to " + focuserId);
                        }
                    }
                }
                else
                {
                    lock (_temperaturesLockObject)
                    {
                        connectedState = false;

                        focuser.Connected = false;
                        focuser.Dispose();
                        focuser = null;
                    }
                }
            }
        }

        public string Description
        {
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = driverDescription + " Version " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "3");
                return Convert.ToInt16("3");
            }
        }

        public string Name
        {
            get
            {
                tl.LogMessage("Name Get", driverDescription);
                return driverDescription;
            }
        }

        #endregion

        #region IFocuser Implementation

        public bool Absolute
        {
            get
            {
                CheckConnected("Absolute");
                return focuser.Absolute;
            }
        }

        public void Halt()
        {
            CheckConnected("Halt");
            focuser.Halt();
        }

        public bool IsMoving
        {
            get
            {
                CheckConnected("IsMoving");
                return focuser.IsMoving;
            }
        }

        public bool Link
        {
            get
            {
                CheckConnected("Link");
                return focuser.Link;
            }
            set
            {
                CheckConnected("Link");
                focuser.Link = value;
            }
        }

        public int MaxIncrement
        {
            get
            {
                CheckConnected("MaxIncrement");
                return focuser.MaxIncrement;
            }
        }

        public int MaxStep
        {
            get
            {
                CheckConnected("MaxStep");
                return focuser.MaxStep;
            }
        }

        public void Move(int Position)
        {
            CheckConnected("Move");
            focuser.Move(Position);
            _lastRequestedPosition = Position;
        }

        public int Position
        {
            get
            {
                CheckConnected("Position");
                if (this.IsMoving == false && _lastRequestedPosition != null)
                {
                    var goalPosition = _lastRequestedPosition ?? 0;
                    if (Math.Abs(focuser.Position - goalPosition) < Settings.Default.AcceptedOffsetSteps)
                    {
                        return goalPosition;
                    }
                    else
                    {
                        return focuser.Position;
                    }
                }   
                else
                {
                    return focuser.Position;

                }
            }
        }

        public double StepSize
        {
            get
            {
                CheckConnected("StepSize");
                return focuser.StepSize;
            }
        }

        public bool TempComp
        {
            get
            {
                CheckConnected("TempComp");
                return focuser.TempComp;
            }
            set
            {
                CheckConnected("TempComp");
                focuser.TempComp = value;
            }
        }

        public bool TempCompAvailable
        {
            get
            {
                CheckConnected("TempCompAvailable");
                return focuser.TempCompAvailable;
            }
        }

        public double Temperature
        {
            get
            {
                lock (_temperaturesLockObject)
                {
                    return temperatures.Select(x => x.Temperature).Average();
                }
            }
        }

        #endregion

        #region Private properties and methods

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Focuser";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        private bool IsConnected
        {
            get
            {
                return connectedState;
            }
        }

        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Focuser";

                try
                {
                    tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                    focuserId = driverProfile.GetValue(driverID, focuserIdProfileName, string.Empty, focuserIdDefault);
                }
                catch (Exception e)
                {
                    tl.LogMessage("VirtualFocuser", "ReadProfile: Exception handled: " + e.Message);
                }
            }
        }

        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Focuser";
                driverProfile.WriteValue(driverID, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(driverID, focuserIdProfileName, focuserId);
            }
        }

        internal void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }

        #endregion
    }

    public class TemperatureReading
    {
        public double Temperature { get; set; }
        public long Timestamp { get; set; }

        public override string ToString()
        {
            return Temperature + "�C [Time: " + Timestamp + "]";
        }
    }
}
