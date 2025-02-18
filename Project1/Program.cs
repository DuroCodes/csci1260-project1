using Project1.Entities;
using Project1.Map;

var random = new Random();
var maze = new Maze(40, 20, 6, 5, 10, random);
maze.Generate();
maze.PopulateEntities([
    ((x, y) => new Monster(x, y), 5),
    ((x, y) => new Potion(x, y), 3),
    ((x, y) => new Weapon(x, y, random.Next(5, 20)), 2)
]);

maze.Display();

while (true)
{
    var key = Console.ReadKey(true).Key;
    if (key == ConsoleKey.Q) break;

    var (dx, dy) = key switch
    {
        ConsoleKey.W or ConsoleKey.UpArrow => (0, -1),
        ConsoleKey.A or ConsoleKey.LeftArrow => (-1, 0),
        ConsoleKey.S or ConsoleKey.DownArrow => (0, 1),
        ConsoleKey.D or ConsoleKey.RightArrow => (1, 0),
        _ => (0, 0)
    };

    maze.MovePlayer(dx, dy);
    Console.Clear();
    maze.Display();
}