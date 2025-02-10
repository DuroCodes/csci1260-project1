namespace Project1.Map;

// was going to use an enum, but enums just get turned into ints
// so using static class instead
public static class TileType
{
    public static readonly string Wall = Util.ColoredText("#", 7);
    public static readonly string Floor = Util.ColoredText(".", 8); 
}