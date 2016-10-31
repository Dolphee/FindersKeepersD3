using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;

namespace FindersKeepers.Helpers
{
    public class Hotkey
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static HashSet<IntPtr> Handle = new HashSet<IntPtr>();
        public static IntPtr Main_Handle = IntPtr.Zero;
        public const int WM_HOTKEY = 0x0312;

        public static void GetHandle()
        {
            Main_Handle = Controller.GameManager.Instance.GManager.GList.MainAccount.DiabloIII.Process.MainWindowHandle;
        }
        
        public static void Register(IntPtr _MainHandle)
        {
            //GetHandle();
            Main_Handle = _MainHandle;
            RegisterHotKey(Main_Handle, 1, 0, Config.Get<FKConfig>().General.Macros.Hotkey);
        }

        public static void Unregister()
        {
            UnregisterHotKey(Main_Handle, 1);
        }
    }

    public class VirtualInput : Hotkey
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int X, int Y);


        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        private const int MOUSE_MOVE = 0x0200;
        private const int MOUSE_LEFTDOWN = 0x0201;
        private const int MOUSE_LEFTUP = 0x0202;
        private const int MOUSE_RIGHTDOWN = 0x0204;
        private const int MOUSE_RIGHTUP = 0x0205;
        private const int MOUSE_MIDDLEDOWN = 0x0020;
        private const int MOUSE_MIDDLEUP = 0x0040;
        private const int MOUSE_ABSOLUTE = 0x8000;

        public static int CreatelParam(Point Point)
        {
            return ((int)Point.Y << 16) | ((int)Point.X & 0xFFFF);
        }

        public static void OnThread(Action Callback)
        {
            Task.Factory.StartNew(
                () => Callback.Invoke()
            );
        }

        public static void MoveCursor(Point Point)
        {
            SendMessage(Main_Handle, MOUSE_ABSOLUTE | MOUSE_MOVE, 0, CreatelParam(Point));
        }

        public static void KeyClick(IntPtr Handle, Point Point)
        {
            OnThread(() =>
            {
                Random Random = new Random();
                PostMessage(Handle, MOUSE_MOVE, IntPtr.Zero, (IntPtr)CreatelParam(Point));
                PostMessage(Handle, WM_KEYDOWN, (IntPtr)Config.Get<FKConfig>().General.Macros.ForceMoveHotkey, IntPtr.Zero);
               // System.Threading.Thread.Sleep(Random.Next(300, 600));
                PostMessage(Handle, WM_KEYUP, (IntPtr)Config.Get<FKConfig>().General.Macros.ForceMoveHotkey, IntPtr.Zero);
            });
        }

        public static void LeftClick(IntPtr Handle, Point Point)
        {
            OnThread(() =>
            {
                Random Random = new Random();

                SendMessage(Handle, MOUSE_LEFTDOWN, 0, CreatelParam(Point));
                System.Threading.Thread.Sleep(Random.Next(300, 600));
                SendMessage(Handle, MOUSE_LEFTUP, 0, CreatelParam(Point));
            });
        }

        public static void RightClick(System.Windows.Point Pos)
        {
           // mouse_event(MOUSEEVENTF_RIGHTDOWN, (int)Pos.X, (int)Pos.Y, 0, 0);
           // mouse_event(MOUSEEVENTF_RIGHTUP, (int)Pos.X, (int)Pos.Y, 0, 0);
        }
    }
}
