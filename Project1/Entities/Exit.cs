namespace Project1.Entities;

public class Exit(int x, int y) : IEntity
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public string Symbol => Util.ColoredText("E", 10);

    public void Interact(Player player)
    {
        Console.WriteLine("You have reached the exit! You win!");
        Environment.Exit(0);
    }
}