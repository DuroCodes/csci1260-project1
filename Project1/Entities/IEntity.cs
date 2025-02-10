namespace Project1.Entities;

// mainly adding this interface for entity detection on the map
// that way it's easier to interact with entities
// rather than just checking the map for the symbol
public interface IEntity
{
    public int X { get; }
    public int Y { get; }
    public string Symbol { get; }

    public void Interact(Player player);
}