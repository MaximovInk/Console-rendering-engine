using System;
using System.Runtime.InteropServices;

namespace MaximovInk.ConsoleGameEngine.Core
{
    public static class NativeMethods
    {
        public const int STDIN = -10;
        public const int STDOUT = -11;
        public const int STDERR = -12;
        public static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public struct COORD
        {
            public short X;
            public short Y;

            public COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT_RECORD
        {
            public const ushort KEY_EVENT = 0x0001,
                MOUSE_EVENT = 0x0002,
                WINDOW_BUFFER_SIZE_EVENT = 0x0004; //more

            [FieldOffset(0)]
            public ushort EventType;
            [FieldOffset(4)]
            public KEY_EVENT_RECORD KeyEvent;
            [FieldOffset(4)]
            public MOUSE_EVENT_RECORD MouseEvent;
            [FieldOffset(4)]
            public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;
            /*
            and:
             MENU_EVENT_RECORD MenuEvent;
             FOCUS_EVENT_RECORD FocusEvent;
             */
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_SCREEN_BUFFER_INFO
        {
            public COORD dwSize;
            public COORD dwCursorPosition;
            public short wAttributes;
            public SmallRect srWindow;
            public COORD dwMaximumWindowSize;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)]
            internal char UnicodeChar;
            [FieldOffset(0)]
            internal short AsciiChar;
            [FieldOffset(2)]
            internal short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFO_EX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFOEX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        public struct MOUSE_EVENT_RECORD
        {
            public COORD dwMousePosition;

            public const uint FROM_LEFT_1ST_BUTTON_PRESSED = 0x0001,
                FROM_LEFT_2ND_BUTTON_PRESSED = 0x0004,
                FROM_LEFT_3RD_BUTTON_PRESSED = 0x0008,
                FROM_LEFT_4TH_BUTTON_PRESSED = 0x0010,
                RIGHTMOST_BUTTON_PRESSED = 0x0002;
            public uint dwButtonState;

            public const int CAPSLOCK_ON = 0x0080,
                ENHANCED_KEY = 0x0100,
                LEFT_ALT_PRESSED = 0x0002,
                LEFT_CTRL_PRESSED = 0x0008,
                NUMLOCK_ON = 0x0020,
                RIGHT_ALT_PRESSED = 0x0001,
                RIGHT_CTRL_PRESSED = 0x0004,
                SCROLLLOCK_ON = 0x0040,
                SHIFT_PRESSED = 0x0010;
            public uint dwControlKeyState;

            public const int DOUBLE_CLICK = 0x0002,
                MOUSE_HWHEELED = 0x0008,
                MOUSE_MOVED = 0x0001,
                MOUSE_WHEELED = 0x0004;
            public uint dwEventFlags;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct KEY_EVENT_RECORD
        {
            [FieldOffset(0)]
            public bool bKeyDown;
            [FieldOffset(4)]
            public ushort wRepeatCount;
            [FieldOffset(6)]
            public ushort wVirtualKeyCode;
            [FieldOffset(8)]
            public ushort wVirtualScanCode;
            [FieldOffset(10)]
            public char UnicodeChar;
            [FieldOffset(10)]
            public byte AsciiChar;

            public const int CAPSLOCK_ON = 0x0080,
                ENHANCED_KEY = 0x0100,
                LEFT_ALT_PRESSED = 0x0002,
                LEFT_CTRL_PRESSED = 0x0008,
                NUMLOCK_ON = 0x0020,
                RIGHT_ALT_PRESSED = 0x0001,
                RIGHT_CTRL_PRESSED = 0x0004,
                SCROLLLOCK_ON = 0x0040,
                SHIFT_PRESSED = 0x0010;
            [FieldOffset(12)]
            public uint dwControlKeyState;
        }

        public struct WINDOW_BUFFER_SIZE_RECORD
        {
            public COORD dwSize;
        }

        public const uint STD_INPUT_HANDLE = unchecked((uint)-10),
            STD_OUTPUT_HANDLE = unchecked((uint)-11),
            STD_ERROR_HANDLE = unchecked((uint)-12);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetStdHandle(uint nStdHandle);

        [Flags]
        public enum ConsoleModes : uint
        {
            ENABLE_PROCESSED_INPUT = 0x0001,
            ENABLE_LINE_INPUT = 0x0002,
            ENABLE_ECHO_INPUT = 0x0004,
            ENABLE_WINDOW_INPUT = 0x0008,
            ENABLE_MOUSE_INPUT = 0x0010,
            ENABLE_INSERT_MODE = 0x0020,
            ENABLE_QUICK_EDIT_MODE = 0x0040,
            ENABLE_EXTENDED_FLAGS = 0x0080,
            ENABLE_AUTO_POSITION = 0x0100,

            ENABLE_PROCESSED_OUTPUT = 0x0001,
            ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002,
            ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004,
            DISABLE_NEWLINE_AUTO_RETURN = 0x0008,
            ENABLE_LVB_GRID_WORLDWIDE = 0x0010
        }

        /* public const uint ENABLE_MOUSE_INPUT = 0x0010,
             ENABLE_QUICK_EDIT_MODE = 0x0040,
             ENABLE_EXTENDED_FLAGS = 0x0080,
             ENABLE_ECHO_INPUT = 0x0004,
             ENABLE_WINDOW_INPUT = 0x0008; //more*/

        [DllImport("kernel32.dll")]
        public static extern bool GetConsoleMode(IntPtr hConsoleInput, ref uint lpMode);

        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleMode(IntPtr hConsoleInput, uint dwMode);


        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ReadConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, uint nLength, ref uint lpNumberOfEventsRead);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool WriteConsoleInput(IntPtr hConsoleInput, INPUT_RECORD[] lpBuffer, uint nLength, ref uint lpNumberOfEventsWritten);


    }
}
