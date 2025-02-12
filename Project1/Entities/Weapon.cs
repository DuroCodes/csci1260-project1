namespace Project1.Entities;

public class Weapon(int x, int y, int damage)
    : Item("Weapon", $"You picked up a weapon ({damage} damage).", x, y, Util.ColoredText("/", 4)), IEntity
{
    private int Damage { get; } = damage;

    public new void Interact(Player player)
    {
        player.Damage = Math.Max(player.Damage, Damage);
        if (player.Damage == Damage) player.LogMessages.Add($"{Name} equips {Name}!");
    }
}