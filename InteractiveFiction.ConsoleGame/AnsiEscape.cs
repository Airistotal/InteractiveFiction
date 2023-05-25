namespace InteractiveFiction.ConsoleGame
{
    /// <summary>
    /// If you want more info on ANSI escape codes, look https://tforgione.fr/posts/ansi-escape-codes/
    /// </summary>
    public class AnsiEscape
    {
        public static string RESET = "\x1B[0m";
        public static string BOLD = "\x1B[1m";
        public static string FAINT = "\x1B[2m";
        public static string ITALIC = "\x1B[3m";
        public static string UNDERLINE = "\x1B[4m";
        public static string INVERSE = "\x1B[7m";
        public static string STRIKETHROUGH = "\x1B[9m";

        public static string BLACK = "\x1B[30m";
        public static string RED = "\x1B[31m";
        public static string GREEN = "\x1B[32m";
        public static string YELLOW = "\x1B[33m";
        public static string BLUE = "\x1B[34m";
        public static string PURPLE = "\x1B[35m";
        public static string LIGHTBLUE = "\x1B[36m";
        public static string GREY = "\x1B[90m";
        public static string SALMON = "\x1B[91m";
        public static string LIGHTGREEN = "\x1B[92m";
        public static string LIGHTYELLOW = "\x1B[93m";
        public static string MEDBLUE = "\x1B[94m";
        public static string ROYALPURPLE = "\x1B[95m";
        public static string SKYBLUE = "\x1B[96m";

        public static string REDHIGHLIGHT = "\x1B[41m";
        public static string GREENHIGHLIGHT = "\x1B[42m";
        public static string YELLOWHIGHLIGHT = "\x1B[43m";
        public static string BLUEHIGHLIGHT = "\x1B[44m";
        public static string PURPLEHIGHLIGHT = "\x1B[45m";
        public static string LIGHTBLUEHIGHLIGHT = "\x1B[46m";
        public static string GREYHIGHLIGHT = "\x1B[100m";
        public static string SALMONHIGHLIGHT = "\x1B[101m";
        public static string LIGHTGREENHIGHLIGHT = "\x1B[102m";
        public static string LIGHTYELLOWHIGHLIGHT = "\x1B[103m";
        public static string MEDBLUEHIGHLIGHT = "\x1B[104m";
        public static string ROYALPURPLEHIGHLIGHT = "\x1B[105m";
        public static string SKYBLUEHIGHLIGHT = "\x1B[106m";
        public static string EGGSHELLHIGHLIGHT = "\x1B[107m";
        public static string SPOILER = "\x1B[47m";
    }
}
