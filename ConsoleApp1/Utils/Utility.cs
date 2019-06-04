using Decal.Adapter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;


namespace Defiance.Utils
{
    class Utility
    {
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
    
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr wparam, UIntPtr lparam);
        [DllImport("Decal.dll")]
        private static extern int DispatchOnChatCommand(ref IntPtr str, [MarshalAs(UnmanagedType.U4)] int target);

        private static bool Decal_DispatchOnChatCommand(string cmd)
        {
            IntPtr ptr = Marshal.StringToBSTR(cmd);
            bool result;
            try
            {
                bool flag = (DispatchOnChatCommand(ref ptr, 1) & 1) > 0;
                result = flag;
            }
            finally
            {
                Marshal.FreeBSTR(ptr);
            }
            return result;
        }

        public static void DispatchChatToBoxWithPluginIntercept(string cmd)
        {
            bool flag = !Decal_DispatchOnChatCommand(cmd);
            if (flag)
            {
                CoreManager.Current.Actions.InvokeChatParser(cmd);
            }
        }

        public static void ClickYes()
        {
            RECT rect = new RECT();

            GetWindowRect(CoreManager.Current.Decal.Hwnd, ref rect);
            SendMouseClick2(rect.Width / 2 - 80, rect.Height / 2 + 18);
            SendMouseClick2(rect.Width / 2 - 80, rect.Height / 2 + 25);
            SendMouseClick2(rect.Width / 2 - 80, rect.Height / 2 + 31);
        }

        public static void SendMouseClick(int x, int y)
        {
            SendMouseClickCheck(CoreManager.Current.Decal.Hwnd, (short)x, (short)y);
        }

        public static void SendMouseClickCheck(IntPtr wnd, short x, short y)
        {
            int num = (int)y << 16 | ((int)x & 65535);
            PostMessage(wnd, 512u, (IntPtr)0, (UIntPtr)((ulong)((long)num)));
            PostMessage(wnd, 513u, (IntPtr)1, (UIntPtr)((ulong)((long)num)));
            PostMessage(wnd, 514u, (IntPtr)0, (UIntPtr)((ulong)((long)num)));
        }

        public static void SendMouseClick2(int x, int y)
        {
            int loc = (y * 0x10000) + x;

            PostMessage(CoreManager.Current.Decal.Hwnd, WM_MOUSEMOVE, (IntPtr)0x00000000, (UIntPtr)loc);
            PostMessage(CoreManager.Current.Decal.Hwnd, WM_LBUTTONDOWN, (IntPtr)0x00000001, (UIntPtr)loc);
            PostMessage(CoreManager.Current.Decal.Hwnd, WM_LBUTTONUP, (IntPtr)0x00000000, (UIntPtr)loc);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public int Width { get { return Right - Left; } }
            public int Height { get { return Bottom - Top; } }
        }

        public static void AddChatText(string Message, int color)
        {
            try
            {
                CoreManager.Current.Actions.AddChatText(string.Format("[Defiance]: {0}", Message), 6);
            }
            catch (Exception ex)
            {
                Repo.RecordException(ex);
            }
        }
        public static void InvokeTextF(string Message)
        {
            try
            {
                CoreManager.Current.Actions.InvokeChatParser(string.Format("/f [Defiance]: {0}", Message));
            }
            catch (Exception ex)
            {
                Repo.RecordException(ex);
            }
        }

        public static void InvokeTextA(string Message)
        {
            try
            {
                CoreManager.Current.Actions.InvokeChatParser(string.Format("/a [Defiance]: {0}", Message));
            }
            catch (Exception ex)
            {
                Repo.RecordException(ex);
            }
        }

        public static void AddWindowText(string str)
        {
            try
            {
                WindowText(string.Format("[Defiance]: {0}", str));
            }
            catch (Exception ex)
            {
                Repo.RecordException(ex);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetWindowText(IntPtr hwnd, string lpString);
        internal static void WindowText(string str)
        {
            SetWindowText(CoreManager.Current.Decal.Hwnd, str);
        }
    
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public static void ActivateWindow()
        {
            Process p = Process.GetCurrentProcess();
            SendMessage(p.MainWindowHandle, 0x0006, IntPtr.Zero, IntPtr.Zero);
        }
    }
}