namespace Project1.Entities;

public class Potion(int x, int y)
    : Item("Potion", "You picked up a potion.", x, y, Util.ColoredText("P", 13)), IEntity
{
    private static int Healing => 10;

    public new void Interact(Player player)
    {
        player.Health += Healing;
        player.LogMessages.Add($"{player.Name} drinks {Name} and heals {Healing} health!");
    }
}