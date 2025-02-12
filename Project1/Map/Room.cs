namespace Project1.Map;

public record Room(int X, int Y, int Width, int Height)
{
    public (int x, int y) Center() => ((X + X + Width) / 2, (Y + Y + Height) / 2);

    public bool Intersects(Room other) =>
        X < other.X + other.Width && X + Width > other.X &&
        Y < other.Y + other.Height && Y + Height > other.Y;
}