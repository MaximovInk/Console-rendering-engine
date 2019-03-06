using System.Drawing;

namespace MaximovInk.ConsoleGameEngine
{
    public class Primitives : Engine
    {
        public Primitives(short width = 182, short height = 53, string Title = "Console engine", short fontw = 16, short fonth = 16, bool showFps = true) : base(width, height, Title, fontw, fonth, showFps)
        {
        }

        protected override void OnStart()
        {
            //Draw symbol from char
            DrawChar(0, 0, '#', 15);
            //Draw symbol from short
            DrawPixel(2, 0, (short)Character.Medium, (short) (COLOR.FG_BLUE | COLOR.BG_RED));
            //Draw line
            DrawLine(1, 2, 3, 20, (short)Character.Full, (short)COLOR.FG_CYAN);
            //Draw line gradient
            DrawLineGradient(3, 2, 5, 20, (short)COLOR.FG_RED, (short)COLOR.FG_BLUE);
            //Draw text
            DrawText(4, 0, "This is text ..", (short)(COLOR.BG_YELLOW | COLOR.FG_BLUE));
            /*
             Convert rgb color to ConsoleColor!
             */
            COLOR cl;
            Character ch;
            ColorUtilites.GetConsoleColor(Color.Crimson, out ch, out cl);
            DrawRect(6, 2, 3, 3, (short)ch, (short)cl);
            //Draw rect
            DrawRect(11, 2, 4, 4, (short)Character.Full, (short)COLOR.FG_GREEN);
            //Draw cicrle
            DrawCircle(13, 15, 6,(short)'*', 12);
            //Draw triangle
            DrawTriangle(21, 41, 25, 0, 0, 25, (short)Character.Dark, (short)COLOR.FG_GREEN);

            FillRect(43, 0, 20, 20, (short)Character.Medium, (short)(COLOR.FG_YELLOW | COLOR.BG_BLUE));

            Apply();
        }

    }
}
