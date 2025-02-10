namespace Project1.Entities;

public class Player(int x, int y) : ICharacter
{
    public string Name { get; set; } = "Player";
    public int Health { get; set; } = 100;
    public int Damage { get; set; } = 10;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public string Symbol => Util.ColoredText("@", 11);
    
    public List<Item> Inventory = [];
    public readonly List<string> LogMessages = [];

    public void Attack(ICharacter target)
    {
        target.TakeDamage(Damage);
        LogMessages.Add($"{Name} attacks {target.Name} for {Damage} damage!");
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        LogMessages.Add($"{Name} takes {amount} damage! ({Health} HP)");
    }

    // this is redundant, but it's here because it's required by IEntity
    public void Interact(Player player) => Console.WriteLine($"{Name} interacts with {player.Name}!");
}