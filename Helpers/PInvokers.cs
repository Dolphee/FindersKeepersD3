using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows;
using FindersKeepers;
using System.Security.Principal;
using System.Reflection;

namespace FindersKeepers.Helpers
{
    public class Administrator
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        private const int TOKEN_ASSIGN_PRIMARY = 0x1;
        private const int TOKEN_DUPLICATE = 0x2;
        private const int TOKEN_IMPERSONATE = 0x4;
        private const int TOKEN_QUERY = 0x8;
        private const int TOKEN_QUERY_SOURCE = 0x10;
        private const int TOKEN_ADJUST_GROUPS = 0x40;
        private const int TOKEN_ADJUST_PRIVILEGES = 0x20;
        private const int TOKEN_ADJUST_SESSIONID = 0x100;
        private const int TOKEN_ADJUST_DEFAULT = 0x80;
        private const int TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE | TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_SESSIONID | TOKEN_ADJUST_DEFAULT);

        public static bool IsProcessOwnerAdmin(Process proc)
        {
            IntPtr ph = IntPtr.Zero;

            try
            {
                OpenProcessToken(proc.Handle, TOKEN_ALL_ACCESS, out ph);

                WindowsIdentity iden = new WindowsIdentity(ph);

                bool result = false;

                foreach (IdentityReference role in iden.Groups)
                {
                    if (role.IsValidTargetType(typeof(SecurityIdentifier)))
                    {
                        SecurityIdentifier sid = role as SecurityIdentifier;

                        if (sid.IsWellKnown(WellKnownSidType.AccountAdministratorSid) || sid.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid))
                        {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }

            catch (Exception e)
            {
                return true;
            }

            finally {
                CloseHandle(ph);
            }
        }
    }


    public class PInvokers
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
        public static Process[] GetDiabloIII()
        {
            return Process.GetProcessesByName("Diablo III");
        }

        public static Process GetDiabloIIIFromPID(int ID)
        {
            return Process.GetProcessById(ID);
        }

        public static void FlashWindow(IntPtr windowHandle)
        {
            FLASHWINFO fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.dwFlags = 2;
            fInfo.dwTimeout = 0;
            fInfo.hwnd = windowHandle;
            fInfo.uCount = 3;

            FlashWindowEx(ref fInfo);
        }


        public static bool DiabloActive()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);

            return (PInvokers.GetDiabloIIIFromPID((int)pid).ProcessName == "Diablo III");
        }

        public static Process CurrentWindow()
        {
            try
            {
                IntPtr hwnd = GetForegroundWindow();
                uint pid;
                GetWindowThreadProcessId(hwnd, out pid);

                if (FindersKeepers.Controller.GameManager.Instance.Accounts == null)
                    return null;

                if (!FindersKeepers.Controller.GameManager.Instance.Accounts.Exists(x => x.DiabloIII.Process.Id == pid))
                    return null;

                Process p = FindersKeepers.Controller.GameManager.Instance.Accounts.Single(x => x.DiabloIII.Process.Id == pid).DiabloIII.Process;

                if (p == null)
                    return null;

                return p;
            }

            catch (Exception e)
            {
              return null;
            }
        }

        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        public static bool CheckLibrary(string fileName)
        {
            return LoadLibrary(fileName) == IntPtr.Zero;
        }
    
        [DllImport("user32.dll")]
        public static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        public static extern int ShowWindow(int hwnd, int command);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 1;

        public static Int32Rect GetClientRect(IntPtr windowHandle)
        {
            Int32Rect clientRect;
            GetClientRect(windowHandle, out clientRect);
            ClientToScreen(windowHandle, ref clientRect);
            return clientRect;
        }
      
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct RECTS
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }

        public const uint GW_OWNER = 4;
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern bool SetWindowPos(
             int hWnd,             // Window handle
             int hWndInsertAfter,  // Placement-order handle
             int X,                // Horizontal position
             int Y,                // Vertical position
             int cx,               // Width
             int cy,               // Height
             uint uFlags);         // Window positioning flags

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Int32Rect lpRect);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Int32Rect lpPoint);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern short VkKeyScanEx(char ch, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(uint idThread);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);


        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool BlockInput([In, MarshalAs(UnmanagedType.Bool)] bool fBlockIt);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

       // [DllImport("user32.dll")]
        //public static extern uint MapVirtualKey(Keys uCode, uint uMapType);

        [DllImport("User32.dll")]
        public static extern IntPtr SetForegroundWindow(IntPtr handle);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(int bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

       /* [DllImport("user32.dll", SetLastError = true)]
        internal static extern int SetWindowLong(IntPtr windowHandle, int index, IntPtr newStyle);*/
        
        [DllImport("user32.dll", SetLastError = true)]
         internal static extern int SetWindowLong(IntPtr windowHandle, int index, uint newStyle);
        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Int32Rect pMargins);


    }
}
