using System;
using System.Threading;
using static MaximovInk.ConsoleGameEngine.Core.NativeMethods;

namespace MaximovInk.ConsoleGameEngine.Core
{
    public static class ConsoleListiner
    {
        public static event ConsoleMouseEvent MouseEvent;

        public static event ConsoleKeyEvent KeyEvent;

        public static event ConsoleWindowBufferSizeEvent WindowBufferSizeEvent;

        private static bool Run = false;

        public static void Start()
        {
            if (!Run)
            {
                Run = true;
                IntPtr handleIn = GetStdHandle(STD_INPUT_HANDLE);
                new Thread(() =>
                {
                    while (true)
                    {
                        uint numRead = 0;
                        INPUT_RECORD[] record = new INPUT_RECORD[1];
                        record[0] = new INPUT_RECORD();
                        ReadConsoleInput(handleIn, record, 1, ref numRead);
                        if (Run)
                            switch (record[0].EventType)
                            {
                                case INPUT_RECORD.MOUSE_EVENT:
                                    MouseEvent?.Invoke(record[0].MouseEvent);
                                    break;
                                case INPUT_RECORD.KEY_EVENT:
                                    KeyEvent?.Invoke(record[0].KeyEvent);
                                    break;
                                case INPUT_RECORD.WINDOW_BUFFER_SIZE_EVENT:
                                    WindowBufferSizeEvent?.Invoke(record[0].WindowBufferSizeEvent);
                                    break;
                            }
                        else
                        {
                            uint numWritten = 0;
                            WriteConsoleInput(handleIn, record, 1, ref numWritten);
                            return;
                        }
                    }
                }).Start();
            }
        }

        public static void Stop() => Run = false;

        public delegate void ConsoleMouseEvent(MOUSE_EVENT_RECORD r);

        public delegate void ConsoleKeyEvent(KEY_EVENT_RECORD r);

        public delegate void ConsoleWindowBufferSizeEvent(WINDOW_BUFFER_SIZE_RECORD r);

    }
}
