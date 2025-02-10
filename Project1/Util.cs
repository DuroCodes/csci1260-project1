namespace Project1;

public class Util
{
    public static string ColoredText(string text, int ansiCode) => $"\u001b[38;5;{ansiCode}m{text}\u001b[0m";
}