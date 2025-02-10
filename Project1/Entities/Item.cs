namespace Project1.Entities;

// records would be more concise (and appropriate since items are immutable)
// however, it says to use abstract classes :p
public abstract class Item(string name, string pickupMessage, int x, int y, string symbol) : IEntity
{
    protected string Name { get; } = name;
    public string PickupMessage { get; } = pickupMessage;
    public int X { get; } = x;
    public int Y { get; } = y;
    public string Symbol { get; } = symbol;

    public void Interact(Player player)
    {
    }
}