using MaximovInk.ConsoleGameEngine.Core;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using static MaximovInk.ConsoleGameEngine.Core.NativeMethods;

namespace MaximovInk.ConsoleGameEngine
{
    public class Engine
    {
        public short Width { get; protected set; }
        public short Height { get; protected set; }
        public CharInfo[] buffer;
        protected SmallRect rect;

        public bool isRun = false;

        protected Thread Thread;
        protected float elapsed { get; private set; }
        private Stopwatch sw;

        protected string title;

        protected bool ShowFPS = true;
        protected bool CountFrameRate = true;

        public Engine(short width = 240, short height = 80 , string Title ="Console engine", short fontw = 12, short fonth = 12, bool showFps = true )
        {
            Width = width;
            Height = height;
            SetConsoleCP(437);
            SetConsoleOutputCP(437);
            buffer = new CharInfo[Width * Height];

            rect = new SmallRect() { Left = 0, Top = 0, Right = Width, Bottom = Height };
            Console.CursorVisible = false;
            Console.SetWindowPosition(0, 0);

            CONSOLE_FONT_INFO_EX ConsoleFontInfo = new CONSOLE_FONT_INFO_EX();
            ConsoleFontInfo.cbSize = (uint)Marshal.SizeOf(ConsoleFontInfo);
            ConsoleFontInfo.FaceName = "Lucida Console";
            ConsoleFontInfo.dwFontSize.X = fontw;
            ConsoleFontInfo.dwFontSize.Y = fonth;

            SetCurrentConsoleFontEx(GetStdHandle(STDOUT), false, ref ConsoleFontInfo);

            IntPtr inHandle = GetStdHandle(STDIN);
            uint mode = 0;
            GetConsoleMode(inHandle, ref mode);
            //mode &= ~ENABLE_QUICK_EDIT_MODE;
            mode |= (uint)ConsoleModes.ENABLE_WINDOW_INPUT;
            mode |= (uint)ConsoleModes.ENABLE_MOUSE_INPUT;
            mode &= ~(uint)ConsoleModes.ENABLE_QUICK_EDIT_MODE;
            

            SetConsoleMode(inHandle, mode);

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            ShowFPS = showFps;

            Console.Title = Title;
            title = Title;
        }

        public void StartLoop()
        {
            isRun = true;
            Thread = new Thread(GameThread);
            Thread.Priority = ThreadPriority.Highest;
            sw = new Stopwatch();
            ConsoleListiner.Start();
            ConsoleListiner.KeyEvent += OnKey;
            ConsoleListiner.MouseEvent += OnMouse;
            OnStart();
            Thread.Start();
        }

        private void GameThread()
        {
            while (isRun)
            {
                bool f = CountFrameRate;
                if (f)
                {
                    sw.Start();
                }

                OnUpdate();
                OnDraw();
                if (ShowFPS)
                {
                    Debug.WriteLine("GGG");
                    DrawText(0, 0, "FPS:" + (1 / elapsed), 1);
                }

                if (f)
                {
                    elapsed = (float)sw.Elapsed.TotalSeconds;
                    sw.Stop();
                    sw.Reset();
                }
            }
            ConsoleListiner.Stop();
            ConsoleListiner.KeyEvent -= OnKey;
            ConsoleListiner.MouseEvent -= OnMouse;
        }

        protected virtual void OnKey(KEY_EVENT_RECORD e)
        {
           
        }

        protected virtual void OnMouse(MOUSE_EVENT_RECORD m)
        {

        }

        public void DrawTriangle(short x1, short x2, short x3 , short y1, short y2, short y3, short character, short color)
        {
            DrawLine(x1, y1, x2, y2,character,color);
            DrawLine(x1, y1, x3, y3,character,color);
            DrawLine(x2, y2, x3, y3, character, color);
        }

        public void DrawRect(short x, short y, short width, short height, short character, short color)
        {
            DrawLine(x, y, x, (short)(height + y), character, color);
            DrawLine(x, y, (short)(width + x), y, character, color);
            DrawLine(x, (short)(y+height), (short)(width + x), (short)(height + y), character, color);
            DrawLine((short)(x+width), y, (short)(width + x), (short)(height + y), character, color);
        }

        public void DrawCircle(short x0, short y0, short radius, short character, short color)
        {
            int x = 0, y = radius;
            int d = 3 - 2 * radius;

            DrawCircle(x0, (short)x, y0, (short)y, character, color);

            while (y >= x)
            {
                x++;
                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                    d = d + 4 * x + 6;

                DrawCircle(x0, (short)x, y0, (short)y, character, color);
            }
            
        }

        private void DrawCircle(short x0, short x, short y0, short y , short character , short color)
        {
            DrawPixel((short)(x0 + x), (short)(y0 + y), character, color);
            DrawPixel((short)(x0 - x), (short)(y0 + y), character, color);
            DrawPixel((short)(x0 + x), (short)(y0 - y), character, color);
            DrawPixel((short)(x0 - x), (short)(y0 - y), character, color);
            DrawPixel((short)(x0 + y), (short)(y0 + x), character, color);
            DrawPixel((short)(x0 - y), (short)(y0 + x), character, color);
            DrawPixel((short)(x0 + y), (short)(y0 - x), character, color);
            DrawPixel((short)(x0 - y), (short)(y0 - x), character, color);
        }

        public void DrawLineGradient(short x, short y, short x2, short y2, short color1, short color2)
        {
            int w = x2 - x;
            int h = y2 - y;
            short dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;

            Color one = ((COLOR)color1).GetRGB();
            Color two = ((COLOR)color2).GetRGB();

            for (int i = 0; i <= longest; i++)
            {
                COLOR cl;
                Character ch;

                ColorUtilites.CombineTwoColors(((COLOR)color1), ((COLOR)color2), (float)i / longest, out ch, out cl);

                DrawPixel(x, y, (short)ch, (short)cl);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }

            }
        }

        public void DrawLine(short x, short y, short x2, short y2, short character, short color)
        {
            int w = x2 - x;
            int h = y2 - y;
            short dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                DrawPixel(x, y, character, color);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        public void DrawPixel(short x, short y, short symbol, short color)
        {
            DrawChar(x, y, (char)symbol, color);
        }

        public void DrawText(short x, short y, string text, short color)
        {
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                DrawChar((short)(x + i), y, chars[i], color);
            }
        }

        public void DrawChar(short x, short y, char @char, short color)
        {
            SetPixel(x, y, new CharInfo() { Attributes = color, UnicodeChar = @char });
        }

        public void FillRect(short x, short y, short width, short height, short character, short color)
        {
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    DrawPixel((short)(x + w), (short)(y + h), character, color);
                }
            }
        }

        protected void SetPixel(short x, short y, CharInfo @char)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                buffer[y * Width + x] = @char;
            }
        }

        public CharInfo GetPixel(short x, short y)
        {
            x = (short)MathF.Clamp(x, 0, Width);
            y = (short)MathF.Clamp(y, 0, Height);
            return buffer[y * Width + x];
        }

        protected void Apply()
        {
            WriteConsoleOutput(GetStdHandle(STDOUT), buffer, new COORD(Width, Height), new COORD(0, 0), ref rect);
        }

        protected void Clear()
        {
            CharInfo ch = new CharInfo();

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = ch;
            }
        }

        protected virtual void OnStart()
        {

        }
        protected virtual void OnUpdate()
        {

        }
        protected virtual void OnDraw()
        {

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD size);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleWindowInfo(IntPtr hConsoleOutput, bool bAbsolute, SmallRect rect);

        [DllImport("kernel32.dll",SetLastError =true)]
        static extern bool SetConsoleOutputCP(uint CodePageId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleCP(uint CodePageId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, CharInfo[] buffer, COORD bufferSize, COORD bufferCoord, ref SmallRect region);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int handle);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out CONSOLE_SCREEN_BUFFER_INFO buffer);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int SetCurrentConsoleFontEx(IntPtr ConsoleOutput,bool MaximumWindow, ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        extern static bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput,bool bMaximumWindow,ref CONSOLE_FONT_INFOEX info);


    }
}
