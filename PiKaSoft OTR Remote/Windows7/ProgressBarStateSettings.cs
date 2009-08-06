// Copyright (c) Microsoft Corporation.  All rights reserved.
// This file was part of the Windows® API Code Pack for Microsoft® .NET Framework
// http://code.msdn.microsoft.com/WindowsAPICodePack

using System;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;

namespace Crazysoft.OTRRemote.Windows7
{
    internal class ProgressBarStateSettings
    {
        // Best practice recommends defining a private object to lock on
        private static Object syncLock = new Object();

        internal readonly int DefaultMaxValue = 100;
        internal readonly int DefaultMinValue = 0;

        /// <summary>
        /// Represents a collection of name/value pairs for each HWND and it’s 
        /// current progress bar value.
        /// </summary>
        internal IDictionary<IntPtr, int> CurrentValues;

        /// <summary>
        /// Represents a collection of name/value pairs for each HWND and it’s 
        /// current progress bar max values
        /// </summary>
        internal IDictionary<IntPtr, int> MaxValues;

        /// <summary>
        /// Represents a collection of name/value pairs for each HWND and it’s 
        /// current progress bar state.
        /// </summary>
        internal IDictionary<IntPtr, TaskbarButtonProgressState> States;

        internal ProgressBarStateSettings()
        {
            CurrentValues = new Dictionary<IntPtr, int>();
            MaxValues = new Dictionary<IntPtr, int>();
            States = new Dictionary<IntPtr, TaskbarButtonProgressState>();
        }

        private static ProgressBarStateSettings instance;
        /// <summary>
        /// Returns a singleton instance of the ProgressBarStateSettings class
        /// </summary>
        internal static ProgressBarStateSettings Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProgressBarStateSettings();

                return instance;
            }
        }

        private static IntPtr defaultHandle;
        /// <summary>
        /// Represents the HWND for the application or default window. 
        /// </summary>
        internal IntPtr DefaultHandle
        {
            get
            {
                if (defaultHandle == IntPtr.Zero)
                {
                    if (Application.OpenForms.Count > 0)
                        defaultHandle = Application.OpenForms[0].Handle;
                    else
                        throw new InvalidOperationException("A valid active Window is needed to update the Taskbar");
                }

                return defaultHandle;
            }
        }

        /// <summary>
        /// Refreshes the native taskbar with the current progressbar values for the given HWND
        /// </summary>
        /// <param name="hwnd">Current window handle</param>
        /// <param name="currentValue">Current progress bar value</param>
        /// <param name="maxValue">Current progress bar max value</param>
        internal void RefreshValue(IntPtr hwnd, int currentValue, int maxValue)
        {
            TaskbarList.SetProgressValue(hwnd, (ulong)currentValue, (ulong)maxValue);
        }

        /// <summary>
        /// Refreshes the native taskbar with the current progressbar state for the given HWND
        /// </summary>
        /// <param name="hwnd">Current window handle</param>
        /// <param name="state">Current progress bar state</param>
        internal void RefreshState(IntPtr hwnd, TaskbarButtonProgressState state)
        {
            TaskbarList.SetProgressState(hwnd, (TBPFLAG)state);
        }

        // Internal implemenation of ITaskbarList4 interface
        private static ITaskbarList4 taskbarList;
        internal static ITaskbarList4 TaskbarList
        {
            get
            {
                if (taskbarList == null)
                {
                    // Create a new instance of ITaskbarList3
                    lock (syncLock)
                    {
                        if (taskbarList == null)
                        {
                            taskbarList = (ITaskbarList4)new CTaskbarList();
                            taskbarList.HrInit();
                        }
                    }
                }

                return taskbarList;
            }
        }
    }
}
