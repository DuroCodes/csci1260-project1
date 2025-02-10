namespace Project1.Map;

public record Room(int X, int Y, int Width, int Height)
{
    public (int x, int y) Center() => ((X + X + Width) / 2, (Y + Y + Height) / 2);

    public bool Intersects(Room room) =>
        X < room.X + room.Width && X + Width > room.X &&
        Y < room.Y + room.Height && Y + Height > room.Y;
}