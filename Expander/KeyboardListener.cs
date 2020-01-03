using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Forms;
using System.Reflection;
using System.Text;

namespace Expander
{
    public class KeyboardListener
    {
        private const int WH_KEYBOARD = 2;

        const int HC_ACTION = 0;

        const int KF_REPEAT = 0x4000;

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<KeyPressedArgs> OnKeyPressed;

        private HookProc m_oProc;
        private IntPtr m_hHookID = IntPtr.Zero;

        public KeyboardListener()
        {
            m_oProc = HookCallback;
        }

        public void HookKeyboard()
        {
            m_hHookID = SetHook(m_oProc);
        }

        public void UnHookKeyboard()
        {
            UnhookWindowsHookEx(m_hHookID);
        }

        private IntPtr SetHook(HookProc proc)
        {
            return SetWindowsHookEx(WH_KEYBOARD, proc, GetModuleHandle(null), GetCurrentThreadId());
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == HC_ACTION)
            {
                var repeat = (HiWord(lParam) & KF_REPEAT);
                if (repeat == 0)
                {
                    OnKeyPress((uint)wParam);
                }
            }
            return CallNextHookEx(m_hHookID, nCode, wParam, lParam);
        }

        void OnKeyPress(uint keys)
        {
            bool space = IsKeyDown(Keys.Space);
            bool ctrl = IsKeyDown(Keys.LControlKey) || IsKeyDown(Keys.RControlKey);
            bool alt = IsKeyDown(Keys.LMenu) || IsKeyDown(Keys.RMenu);
            if (OnKeyPressed != null)
            {
                OnKeyPressed(this, new KeyPressedArgs(KeyCodeToUnicode(keys), ctrl, alt));
            }
        }
        bool IsKeyDown(Keys keys)
        {
            return (GetKeyState((int)keys) & 0x8000) == 0x8000;
        }

        private static uint HiWord(IntPtr ptr)
        {
            if (((uint)ptr & 0x80000000) == 0x80000000)
                return ((uint)ptr >> 16);
            else
                return ((uint)ptr >> 16) & 0xffff;
        }

        public String KeyCodeToUnicode(uint virtualKeyCode)
        {
            byte[] keyboardState = new byte[255];
            bool keyboardStateStatus = GetKeyboardState(keyboardState);

            if (!keyboardStateStatus)
            {
                return "";
            }

            uint scanCode = MapVirtualKey(virtualKeyCode, 0);
            IntPtr inputLocaleIdentifier = GetKeyboardLayout(0);

            StringBuilder result = new StringBuilder();
            ToUnicodeEx(virtualKeyCode, scanCode, keyboardState, result, (int)5, (uint)0, inputLocaleIdentifier);

            return result.ToString();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
    }

    public class KeyPressedArgs : EventArgs
    {
        public String KeyPressed { get; private set; }
        public bool Ctrl { get; private set; }
        public bool Alt { get; private set; }

        public KeyPressedArgs(String key, bool ctrl, bool alt)
        {
            KeyPressed = key;
            Ctrl = ctrl;
            Alt = alt;
        }
    }
}
