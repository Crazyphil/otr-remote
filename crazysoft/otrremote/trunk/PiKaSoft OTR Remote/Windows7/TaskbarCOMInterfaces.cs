// Copyright (c) Microsoft Corporation.  All rights reserved.
// This file is part of the Windows® API Code Pack for Microsoft® .NET Framework
// http://code.msdn.microsoft.com/WindowsAPICodePack

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Crazysoft.OTRRemote.Windows7
{
    /// <summary>
    /// HRESULT Wrapper
    /// This is intended for Library Internal use only.
    /// </summary>
    [CLSCompliant(false)]
    public enum HRESULT : uint
    {
        /// <summary>
        /// S_FALSE
        /// </summary>
        S_FALSE = 0x0001,

        /// <summary>
        /// S_OK
        /// </summary>
        S_OK = 0x0000,

        /// <summary>
        /// E_INVALIDARG
        /// </summary>
        E_INVALIDARG = 0x80070057,

        /// <summary>
        /// E_OUTOFMEMORY
        /// </summary>
        E_OUTOFMEMORY = 0x8007000E,

        /// <summary>
        /// E_NOINTERFACE
        /// </summary>
        E_NOINTERFACE = 0x80004002,

        /// <summary>
        /// E_FAIL
        /// </summary>
        E_FAIL = 0x80004005,

        /// <summary>
        /// E_ELEMENTNOTFOUND
        /// </summary>
        E_ELEMENTNOTFOUND = 0x80070490,

        /// <summary>
        /// TYPE_E_ELEMENTNOTFOUND
        /// </summary>
        TYPE_E_ELEMENTNOTFOUND = 0x8002802B,

        /// <summary>
        /// Win32 Error code: ERROR_CANCELLED
        /// </summary>
        ERROR_CANCELLED = 1223,
    }

    internal enum TBPFLAG
    {
        TBPF_NOPROGRESS = 0,
        TBPF_INDETERMINATE = 0x1,
        TBPF_NORMAL = 0x2,
        TBPF_ERROR = 0x4,
        TBPF_PAUSED = 0x8
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    internal struct THUMBBUTTON
    {
        /// <summary>
        /// WPARAM value for a THUMBBUTTON being clicked.
        /// </summary>
        internal const int THBN_CLICKED = 0x1800;

        [MarshalAs(UnmanagedType.U4)]
        internal THBMASK dwMask;
        internal uint iId;
        internal uint iBitmap;
        internal IntPtr hIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        internal string szTip;
        [MarshalAs(UnmanagedType.U4)]
        internal THBFLAGS dwFlags;
    }

    internal enum THBMASK
    {
        THB_BITMAP = 0x1,
        THB_ICON = 0x2,
        THB_TOOLTIP = 0x4,
        THB_FLAGS = 0x8
    }

    [Flags]
    internal enum THBFLAGS
    {
        THBF_ENABLED = 0x00000000,
        THBF_DISABLED = 0x00000001,
        THBF_DISMISSONCLICK = 0x00000002,
        THBF_NOBACKGROUND = 0x00000004,
        THBF_HIDDEN = 0x00000008,
        THBF_NONINTERACTIVE = 0x00000010
    }

    /// <summary>
    /// A Wrapper for a RECT struct
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        /// <summary>
        /// Position of left edge
        /// </summary>
        public int left;
        /// <summary>
        /// Position of top edge
        /// </summary>
        public int top;
        /// <summary>
        /// Position of right edge
        /// </summary>
        public int right;
        /// <summary>
        /// Position of bottom edge
        /// </summary>
        public int bottom;
    };

    internal enum STPFLAG
    {
        STPF_NONE = 0x0,
        STPF_USEAPPTHUMBNAILALWAYS = 0x1,
        STPF_USEAPPTHUMBNAILWHENACTIVE = 0x2,
        STPF_USEAPPPEEKALWAYS = 0x4,
        STPF_USEAPPPEEKWHENACTIVE = 0x8
    }

    /// <summary>
    /// Represents the thumbnail progress bar state.
    /// </summary>
    public enum TaskbarButtonProgressState
    {
        /// <summary>
        /// No progress is displayed.
        /// </summary>
        NoProgress = 0,

        /// <summary>
        /// The progress is indeterminate (marquee).
        /// </summary>
        Indeterminate = 0x1,

        /// <summary>
        /// Normal progress is displayed.
        /// </summary>
        Normal = 0x2,

        /// <summary>
        /// An error occurred (red).
        /// </summary>
        Error = 0x4,

        /// <summary>
        /// The operation is paused (yellow).
        /// </summary>
        Paused = 0x8
    }

    [ComImportAttribute()]
    [GuidAttribute("c43dc798-95d1-4bea-9030-bb99e2983a1a")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList4
    {
        // ITaskbarList
        [PreserveSig]
        void HrInit();
        [PreserveSig]
        void AddTab(IntPtr hwnd);
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);

        // ITaskbarList2
        [PreserveSig]
        void MarkFullscreenWindow(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

        // ITaskbarList3
        [PreserveSig]
        void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
        [PreserveSig]
        void SetProgressState(IntPtr hwnd, TBPFLAG tbpFlags);
        [PreserveSig]
        void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);
        [PreserveSig]
        void UnregisterTab(IntPtr hwndTab);
        [PreserveSig]
        void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);
        [PreserveSig]
        void SetTabActive(IntPtr hwndTab, IntPtr hwndMDI, uint dwReserved);
        [PreserveSig]
        HRESULT ThumbBarAddButtons(
            IntPtr hwnd,
            uint cButtons,
            [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
        [PreserveSig]
        HRESULT ThumbBarUpdateButtons(
            IntPtr hwnd,
            uint cButtons,
            [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
        [PreserveSig]
        void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
        [PreserveSig]
        void SetOverlayIcon(
          IntPtr hwnd,
          IntPtr hIcon,
          [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
        [PreserveSig]
        void SetThumbnailTooltip(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPWStr)] string pszTip);
        [PreserveSig]
        void SetThumbnailClip(
            IntPtr hwnd,
            ref RECT prcClip);

        // ITaskbarList4
        void SetTabProperties(IntPtr hwndTab, STPFLAG stpFlags);
    }

    [GuidAttribute("56FDF344-FD6D-11d0-958A-006097C9A090")]
    [ClassInterfaceAttribute(ClassInterfaceType.None)]
    [ComImportAttribute()]
    internal class CTaskbarList { }
}
