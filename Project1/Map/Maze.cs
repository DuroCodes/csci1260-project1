using Project1.Entities;

namespace Project1.Map;

public class Maze(int width, int height, int maxRooms, int roomMinSize, int roomMaxSize, Random random)
{
    private readonly string[,] _tiles = new string[width, height];
    private readonly List<Room> _rooms = [];
    private Player _player = null!; // <- weird fix to avoid marking as nullable; since Generate() has to be called 
    private readonly Dictionary<(int x, int y), IEntity> _entities = new();

    // https://www.wikiwand.com/en/articles/Binary_space_partitioning
    public void Generate()
    {
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
            _tiles[x, y] = TileType.Wall;

        var h = random.Next(roomMinSize, roomMaxSize);
        for (var i = 0; i < maxRooms; i++)
        {
            var w = random.Next(roomMinSize, roomMaxSize);
            var x = random.Next(1, width - w - 1);
            var y = random.Next(1, height - h - 1);

            var newRoom = new Room(x, y, w, h);

            var intersects = _rooms.Any(otherRoom => newRoom.Intersects(otherRoom));
            if (intersects) continue;

            CreateRoom(newRoom);
            if (_rooms.Count > 0) CreateTunnel(_rooms[^1].Center(), newRoom.Center());
            _rooms.Add(newRoom);
        }

        if (_rooms.Count < 2) return;
        var startRoom = _rooms[random.Next(_rooms.Count)];
        var exitRoom = _rooms[random.Next(_rooms.Count)];

        while (exitRoom == startRoom) exitRoom = _rooms[random.Next(_rooms.Count)];

        _player = new Player(startRoom.Center().x, startRoom.Center().y);
        PlaceEntity(_player);
        PlaceEntity(new Exit(exitRoom.Center().x, exitRoom.Center().y));
    }

    private void CreateRoom(Room room)
    {
        for (var x = room.X; x < room.X + room.Width; x++)
        for (var y = room.Y; y < room.Y + room.Height; y++)
            _tiles[x, y] = TileType.Floor;
    }

    // https://www.wikiwand.com/en/articles/Bresenham%27s_line_algorithm
    private void CreateTunnel((int x, int y) start, (int x, int y) end)
    {
        var (x1, y1) = start;
        var (x2, y2) = end;

        var horizontalFirst = random.Next(2) == 0;

        if (horizontalFirst)
        {
            for (var x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++) _tiles[x, y1] = TileType.Floor;
            for (var y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++) _tiles[x2, y] = TileType.Floor;
        }
        else
        {
            for (var y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++) _tiles[x1, y] = TileType.Floor;
            for (var x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++) _tiles[x, y2] = TileType.Floor;
        }
    }

    public void PopulateEntities(List<(Func<int, int, IEntity> factory, int count)> entityData)
    {
        var validPositions = Enumerable.Range(0, width)
            .SelectMany(x => Enumerable.Range(0, height)
                .Where(y => _tiles[x, y] == TileType.Floor && !_entities.ContainsKey((x, y)))
                .Select(y => (x, y))
            )
            .OrderBy(_ => random.Next())
            .ToList();

        foreach (var (factory, count) in entityData)
        {
            for (var i = 0; i < count && validPositions.Count > 0; i++)
            {
                var (x, y) = validPositions[^1];
                validPositions.RemoveAt(validPositions.Count - 1);
                PlaceEntity(factory(x, y));
            }
        }
    }

    private void PlaceEntity(IEntity entity) => _entities[(entity.X, entity.Y)] = entity;

    public void MovePlayer(int dx, int dy)
    {
        var (x, y) = (_player.X + dx, _player.Y + dy);
        if (_tiles[x, y] == TileType.Wall) return;

        if (_entities.TryGetValue((x, y), out var entity))
        {
            if (entity is Item item) _player.LogMessages.Add(item.PickupMessage);
            entity.Interact(_player);
            _player.LogMessages.Add("");
        }

        _entities.Remove((_player.X, _player.Y));
        _player.X = x;
        _player.Y = y;
        PlaceEntity(_player);
    }

    public void Display()
    {
        var mazeLines = Enumerable.Range(0, height)
            .Select(y => string.Concat(Enumerable.Range(0, width)
                .Select(x => _entities.TryGetValue((x, y), out var entity) ? entity.Symbol : _tiles[x, y])
                .ToArray()));

        var logLines = _player.LogMessages
            .Concat(Enumerable.Repeat("", Math.Max(0, height - _player.LogMessages.Count)))
            .TakeLast(height);

        mazeLines.Zip(logLines, (maze, log) => $"{maze} {log}").ToList().ForEach(Console.WriteLine);

        Console.WriteLine($"Health: {_player.Health}");
        Console.WriteLine("Use WASD to move, Q to quit");
    }
}