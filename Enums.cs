namespace MaximovInk.ConsoleGameEngine
{
    public enum Character : short
    {
        Null = 0x0000,
        Full = 0x00DB,
        Dark = 0x00B2,
        Medium = 0x00B1,
        Light = 0x00B0,
    }

    public enum COLOR : short
    {
        FG_BLACK = 0,
        FG_DARK_BLUE = 1,
        FG_DARK_GREEN = 2,
        FG_DARK_CYAN = 3,
        FG_DARK_RED = 4,
        FG_DARK_MAGENTA = 5,
        FG_DARK_YELLOW = 6,
        FG_GREY = 7,
        FG_DARK_GREY = 8,
        FG_BLUE = 9,
        FG_GREEN = 10,
        FG_CYAN = 11,
        FG_RED = 12,
        FG_MAGENTA = 13,
        FG_YELLOW = 14,
        FG_WHITE = 15,
        BG_BLACK = 0,
        BG_DARK_BLUE = 16,
        BG_DARK_GREEN = 32,
        BG_DARK_CYAN = 48,
        BG_DARK_RED = 64,
        BG_DARK_MAGENTA = 80,
        BG_DARK_YELLOW = 96,
        BG_GREY = 112,
        BG_DARK_GREY = 128,
        BG_BLUE = 144,
        BG_GREEN = 160,
        BG_CYAN = 176,
        BG_RED = 192,
        BG_MAGENTA = 208,
        BG_YELLOW = 224,
        BG_WHITE = 240,
    };
}
