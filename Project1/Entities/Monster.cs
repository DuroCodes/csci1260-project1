namespace Project1.Entities;

public class Monster(int x, int y) : ICharacter
{
    public string Name => "Monster";
    public int Health { get; set; } = 100;
    public int Damage { get; set; } = 10;
    public int X { get; } = x;
    public int Y { get; } = y;
    public string Symbol => Util.ColoredText("M", 1);

    public void Attack(ICharacter target) => target.TakeDamage(Damage);

    public void TakeDamage(int amount) => Health -= amount;

    public void Interact(Player player) => Combat(player);

    private void Combat(Player player)
    {
        while (player.Health > 0 && Health > 0)
        {
            player.Attack(this);
            if (Health <= 0)
            {
                player.LogMessages.Add($"{Name} has died!");
                return;
            }

            Attack(player);
            if (player.Health > 0) continue;

            Console.WriteLine("You died");
            Environment.Exit(1);
            return;
        }
    }
}