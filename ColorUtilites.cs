using System;
using System.Drawing;

namespace MaximovInk.ConsoleGameEngine
{
    public static class ColorUtilites
    {
        public static COLOR GetConsoleColor16(this Color color)
        {
            if (color.GetSaturation() < 0.5)
            {

                float satur = color.GetBrightness() * 3.5f;

                if (satur == 0)
                {
                    return COLOR.FG_BLACK;
                }

            }
            int hue = (int)Math.Round(color.GetHue() / 60, MidpointRounding.AwayFromZero);
            if (color.GetBrightness() < 0.4)
            {
                switch (hue)
                {
                    case 1: return COLOR.FG_DARK_YELLOW;
                    case 2: return COLOR.FG_DARK_GREEN;
                    case 3: return COLOR.FG_DARK_CYAN;
                    case 4: return COLOR.FG_DARK_BLUE;
                    case 5: return COLOR.FG_DARK_MAGENTA;
                    default: return COLOR.FG_DARK_RED;
                }
            }
            switch (hue)
            {
                case 1: return COLOR.FG_YELLOW;
                case 2: return COLOR.FG_GREEN;
                case 3: return COLOR.FG_CYAN;
                case 4: return COLOR.FG_BLUE;
                case 5: return COLOR.FG_MAGENTA;
                default: return COLOR.FG_RED;
            }
        }

        public static void GetConsoleColor(this Color color, out Character ch, out COLOR cl)
        {
            ch = Character.Null;
            cl = COLOR.BG_BLACK;

            float satur = color.GetSaturation();
            float light = color.GetBrightness();
            float hue = color.GetHue();

            if (satur < 0.1f | light > 0.8f)
            {
                ch = Character.Full;
                cl = COLOR.BG_BLACK;

                ch = light < 0.2 ? Character.Light : light < 0.5 ? Character.Medium : light < 0.8 ? Character.Dark : Character.Full;

                if (light == 0)
                {
                    cl = COLOR.FG_BLACK | COLOR.BG_BLACK;
                    ch = Character.Medium;
                }
                else if (light <= 0.1)
                {
                    cl = COLOR.FG_DARK_GREY | COLOR.BG_BLACK;
                    ch = Character.Medium;
                }
                else if (light <= 0.3)
                {
                    cl = COLOR.FG_DARK_GREY;
                    ch = Character.Full;
                }
                else if (light <= 0.5)
                {
                    cl = COLOR.FG_DARK_GREY | COLOR.BG_GREY;
                    ch = Character.Medium;
                }
                else if (light <= 0.6)
                {
                    cl = COLOR.FG_GREY;
                    ch = Character.Full;
                }
                else if (light <= 0.8)
                {
                    cl = COLOR.FG_GREY | COLOR.BG_WHITE;
                    ch = Character.Medium;
                }
                else
                {
                    cl = COLOR.FG_WHITE;
                    ch = Character.Full;
                }
            }
            else
            {
                if (hue <= 10)
                {
                    if (light <= 0.4)
                    {
                        cl = COLOR.FG_DARK_RED;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_RED;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_RED | COLOR.BG_WHITE;
                        ch = Character.Medium;
                    }
                    else
                    {
                        cl = COLOR.FG_RED | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 30)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_RED | COLOR.BG_YELLOW;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.4)
                    {
                        cl = COLOR.FG_DARK_RED | COLOR.BG_YELLOW;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_RED | COLOR.BG_YELLOW;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_RED | COLOR.BG_WHITE;
                        ch = Character.Medium;
                    }
                    else
                    {
                        cl = COLOR.FG_RED | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 45)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_YELLOW | COLOR.BG_DARK_RED;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.6)
                    {
                        cl = COLOR.FG_YELLOW | COLOR.BG_RED;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_YELLOW | COLOR.BG_RED;
                        ch = Character.Light;
                    }
                    else
                    {
                        cl = COLOR.FG_YELLOW | COLOR.BG_RED;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 65)
                {
                    if (light <= 0.4)
                    {
                        cl = COLOR.FG_DARK_YELLOW;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_YELLOW;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.6)
                    {
                        cl = COLOR.FG_YELLOW | COLOR.BG_YELLOW;
                        ch = Character.Light;
                    }
                    else
                    {
                        cl = COLOR.FG_YELLOW | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 96)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_GREEN | COLOR.BG_DARK_YELLOW;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_GREEN | COLOR.BG_DARK_YELLOW;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.6)
                    {
                        cl = COLOR.FG_GREEN | COLOR.BG_YELLOW;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_GREEN | COLOR.BG_YELLOW;
                        ch = Character.Light;
                    }
                    else
                    {
                        cl = COLOR.FG_GREEN | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 150)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_GREEN;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_GREEN;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_GREEN;
                        ch = Character.Dark;
                    }
                    else
                    {
                        cl = COLOR.FG_GREEN | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 150)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_CYAN | COLOR.BG_DARK_GREEN;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_CYAN | COLOR.BG_DARK_GREEN;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.6)
                    {
                        cl = COLOR.FG_CYAN | COLOR.BG_GREEN;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_CYAN | COLOR.BG_GREEN;
                        ch = Character.Light;
                    }
                    else
                    {
                        cl = COLOR.FG_CYAN | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 190)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_CYAN;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_CYAN;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_CYAN;
                        ch = Character.Dark;
                    }
                    else
                    {
                        cl = COLOR.FG_CYAN | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 220)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_CYAN | COLOR.BG_DARK_BLUE;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_CYAN | COLOR.BG_DARK_BLUE;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_CYAN | COLOR.BG_BLUE;
                        ch = Character.Medium;
                    }
                    else
                    {
                        cl = COLOR.FG_CYAN | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 240)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_BLUE;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_BLUE;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_BLUE;
                        ch = Character.Dark;
                    }
                    else
                    {
                        cl = COLOR.FG_BLUE | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 275)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_BLUE | COLOR.BG_DARK_MAGENTA;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_BLUE | COLOR.BG_DARK_MAGENTA;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.6)
                    {
                        cl = COLOR.FG_BLUE | COLOR.BG_MAGENTA;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_BLUE | COLOR.BG_MAGENTA;
                        ch = Character.Light;
                    }
                    else
                    {
                        cl = COLOR.FG_BLUE | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else if (hue <= 320)
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_MAGENTA;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_MAGENTA;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_MAGENTA;
                        ch = Character.Dark;
                    }
                    else
                    {
                        cl = COLOR.FG_MAGENTA | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }
                else
                {
                    if (light <= 0.3)
                    {
                        cl = COLOR.FG_DARK_MAGENTA | COLOR.BG_DARK_RED;
                        ch = Character.Dark;
                    }
                    else if (light <= 0.5)
                    {
                        cl = COLOR.FG_DARK_MAGENTA | COLOR.BG_DARK_RED;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.6)
                    {
                        cl = COLOR.FG_MAGENTA | COLOR.BG_RED;
                        ch = Character.Medium;
                    }
                    else if (light <= 0.7)
                    {
                        cl = COLOR.FG_MAGENTA | COLOR.BG_RED;
                        ch = Character.Light;
                    }
                    else
                    {
                        cl = COLOR.FG_MAGENTA | COLOR.BG_WHITE;
                        ch = Character.Light;
                    }
                }

            }

        }

        public static Color GetRGB(this COLOR color)
        {
            switch (color)
            {
                case COLOR.FG_DARK_BLUE:
                    return Color.FromArgb(0, 0, 128);
                case COLOR.FG_DARK_GREEN:
                    return Color.FromArgb(0, 128, 0);
                case COLOR.FG_DARK_CYAN:
                    return Color.FromArgb(0, 128, 128);
                case COLOR.FG_DARK_RED:
                    return Color.FromArgb(128, 0, 0);
                case COLOR.FG_DARK_MAGENTA:
                    return Color.FromArgb(128, 0, 128);
                case COLOR.FG_DARK_YELLOW:
                    return Color.FromArgb(128, 128, 0);
                case COLOR.FG_GREY:
                    return Color.FromArgb(128, 128, 128);
                case COLOR.FG_DARK_GREY:
                    return Color.FromArgb(192, 192, 192);
                case COLOR.FG_BLUE:
                    return Color.FromArgb(0, 0, 255);
                case COLOR.FG_GREEN:
                    return Color.FromArgb(0, 255, 0);
                case COLOR.FG_CYAN:
                    return Color.FromArgb(0, 255, 255);
                case COLOR.FG_RED:
                    return Color.FromArgb(255, 0, 0);
                case COLOR.FG_MAGENTA:
                    return Color.FromArgb(255, 0,255);
                case COLOR.FG_YELLOW:
                    return Color.FromArgb(255, 255, 0);
                case COLOR.FG_WHITE:
                    return Color.FromArgb(255, 255, 255);
                default:
                    return Color.FromArgb(0, 0, 0);
            }
        }

        public static COLOR InvertFBG(COLOR color)
        {
            switch (color)
            {
                case COLOR.FG_BLACK:
                    return COLOR.BG_BLACK;
                case COLOR.FG_DARK_BLUE:
                    return COLOR.BG_DARK_GREEN;
                case COLOR.FG_DARK_GREEN:
                    return COLOR.BG_DARK_GREEN;
                case COLOR.FG_DARK_CYAN:
                    return COLOR.BG_DARK_CYAN;
                case COLOR.FG_DARK_RED:
                    return COLOR.BG_DARK_RED;
                case COLOR.FG_DARK_MAGENTA:
                    return COLOR.BG_DARK_MAGENTA;
                case COLOR.FG_DARK_YELLOW:
                    return COLOR.BG_DARK_YELLOW;
                case COLOR.FG_GREY:
                    return COLOR.BG_GREY;
                case COLOR.FG_DARK_GREY:
                    return COLOR.BG_DARK_GREY;
                case COLOR.FG_BLUE:
                    return COLOR.BG_BLUE;
                case COLOR.FG_GREEN:
                    return COLOR.BG_GREEN;
                case COLOR.FG_CYAN:
                    return COLOR.BG_CYAN;
                case COLOR.FG_RED:
                    return COLOR.BG_RED;
                case COLOR.FG_MAGENTA:
                    return COLOR.BG_MAGENTA;
                case COLOR.FG_YELLOW:
                    return COLOR.BG_YELLOW;
                case COLOR.FG_WHITE:
                    return COLOR.BG_WHITE;
                case COLOR.BG_DARK_BLUE:
                    return COLOR.FG_DARK_BLUE;
                case COLOR.BG_DARK_GREEN:
                    return COLOR.FG_DARK_GREEN;
                case COLOR.BG_DARK_CYAN:
                    return COLOR.FG_DARK_CYAN;
                case COLOR.BG_DARK_RED:
                    return COLOR.FG_DARK_RED;
                case COLOR.BG_DARK_MAGENTA:
                    return COLOR.FG_DARK_MAGENTA;
                case COLOR.BG_DARK_YELLOW:
                    return COLOR.FG_DARK_YELLOW;
                case COLOR.BG_GREY:
                    return COLOR.FG_GREY;
                case COLOR.BG_DARK_GREY:
                    return COLOR.FG_DARK_GREY;
                case COLOR.BG_BLUE:
                    return COLOR.FG_BLUE;
                case COLOR.BG_GREEN:
                    return COLOR.FG_GREEN;
                case COLOR.BG_CYAN:
                    return COLOR.FG_CYAN;
                case COLOR.BG_RED:
                    return COLOR.FG_RED;
                case COLOR.BG_MAGENTA:
                    return COLOR.BG_MAGENTA;
                case COLOR.BG_YELLOW:
                    return COLOR.FG_YELLOW;
                case COLOR.BG_WHITE:
                    return COLOR.FG_WHITE;
                default:
                    return COLOR.FG_BLACK;
            }
        }

        public static void CombineTwoColors(COLOR one, COLOR two, float mix, out Character ch, out COLOR cl)
        {
            ch = Character.Null;
            cl = COLOR.BG_BLACK;

            if (mix < 0.05f)
            {
                //full
                ch = Character.Full;
                cl = one | InvertFBG(two);
            }
            else if (mix < 0.4f)
            {
                //dark
                ch = Character.Dark;
                cl = one | InvertFBG(two);
            }
            else if (mix < 0.6f)
            {
                //medium
                ch = Character.Medium;
                cl = one | InvertFBG(two);
            }
            else if (mix <= 0.95f)
            {
                //dark
                ch = Character.Dark;
                cl = two | InvertFBG(one);
            }
            else
            {
                //full
                ch = Character.Full;
                cl = two | InvertFBG(one);
            }
        }
    }
}
