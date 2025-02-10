namespace Project1.Entities;

public interface ICharacter : IEntity
{
    string Name { get; }
    int Health { get; set; }
    int Damage { get; set; }

    void Attack(ICharacter target);
    void TakeDamage(int amount);
}