using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

public class Scanner {
    List<string> buffer = [];
    bool GimmeOutput { get; }
    StreamReader reader { get; }
    StreamWriter writer { get; }
    bool local = false;
    public Scanner(bool gi = false) {
        GimmeOutput = gi;
        if (File.Exists("in.txt")) {
            reader = new StreamReader("in.txt");
            writer = new StreamWriter("out.txt", false);
            local = true;
        } else {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());
        }
    }

    public string? ReadLine() {
        var line = reader.ReadLine();
        if (GimmeOutput)
            buffer.Add(line);
        return line;
    }

    public void WriteLine(object val) {
        writer.WriteLine(val);
        writer.Flush();
    }

    public void Debug(object val) {
        if (GimmeOutput || local)
            return;
        Console.Error.WriteLine(val);
    }

    public void DumpBuffer() {
        if (buffer.Count == 0)
            return;
        Console.Error.WriteLine(new string('=', 100));
        Console.Error.WriteLine(string.Join("\n", buffer));
        Console.Error.WriteLine(new string('=', 100));
        buffer.Clear();
    }
}

/**
 * Win the water fight by controlling the most territory, or out-soak your opponent!
 **/
public static class Const {
    public static int SmallCover = 1;
    public static int HighCover = 2;
}

public struct Point {
    public Point() {
        X = -1;
        Y = -1;
    }
    public int X { get; set; }
    public int Y { get; set; }

    public void Deconstruct(out int x, out int y) {
        x = X;
        y = Y;
    }

    public bool IsEmpty => X == -1 && Y == -1;

    public static Point New(int x, int y)
        => new Point { X = x, Y = y };

    public static Point New((int, int) p)
        => new Point { X = p.Item1, Y = p.Item2 };

    public Point Update(Point p)
        => New(X + p.X, Y + p.Y);

    public Point Update(int n)
        => New(X + n, Y + n);

    public int ManD(Point p)
        => Math.Abs(X - p.X) + Math.Abs(Y - p.Y);

    public IEnumerable<Point> GetNeighs(bool diag = false) {
        for (int y = Y - 1; y < Y + 1; y++) {
            for (int x = X - 1; x < X + 1; x++) {
                if (X == x && y == Y) continue;
                if (!diag && (x != X && y != Y)) continue;
                yield return New(x, y);
            }
        }
    }

    public static bool IsInRowX(params Point[] p) {
        var r = true;
        if (p.Length < 2)
            return r;
        Func<int, int, bool> comp;
        if (p[0].X > p[1].X)
            comp = (int f, int s) => f > s;
        else
            comp = (int f, int s) => f < s;
        for (int i = 1; i < p.Length - 1; i++) {
            if (!comp(p[i].X, p[i + 1].X)) {
                r = false;
                break;
            }
        }
        return r;
    }

    public static bool IsInRowY(params Point[] p) {
        var r = true;
        if (p.Length < 2)
            return r;
        Func<int, int, bool> comp;
        if (p[0].Y > p[1].Y)
            comp = (int f, int s) => f > s;
        else
            comp = (int f, int s) => f < s;
        for (int i = 1; i < p.Length - 1; i++) {
            if (!comp(p[i].Y, p[i + 1].Y)) {
                r = false;
                break;
            }
        }
        return r;
    }
}

public class Agent {
    public int Id { get; set; }
    public int ShootCooldown { get; set; }
    public int OptimalRange { get; set; }
    public int SoakingPower { get; set; }
    public int SplashBombs { get; set; }
    public Point Point { get; set; }
    public int X => Point.X;
    public int Y => Point.Y;
    public int Wetness { get; set; }

    public string Log(bool? m = null) {
        var p = "";
        if (m != null) p = m.Value ? "Player" : "Enemy";
        var str = $"{p} Id: {Id}\n";
        str += $"Coordinate: {X} {Y}\n";
        str += $"Cooldown: {ShootCooldown}\n";
        str += $"Range: {OptimalRange}\n";
        str += $"Soaking Power: {SoakingPower}\n";
        str += $"Splash Bomb: {SplashBombs}\n";
        str += $"Wetness: {Wetness}\n";

        return str;
    }

    public int GetDamage(Point p) {
        var d = p.ManD(Point);
        if (d <= OptimalRange)
            return d;
        if (d <= OptimalRange * 2)
            return d / 2;
        return 0;
    }
}

public class Enemy : Agent { }
public class Player : Agent {
    public Point Cover { get; set; }
    public int Shoot { get; set; }

    public string Command() {
        var str = $"{Id}";
        if (!Cover.IsEmpty)
            str += $";MOVE {Cover.X} {Cover.Y}";
        if (Shoot > 0)
            str += $";SHOOT {Shoot}";
        return str;
    }
}

public class Grid {
    public int Width { get; set; }
    public int Height { get; set; }
    int[][] map;
    Dictionary<int, Player> winners { get; set; }
    Dictionary<int, Enemy> losers { get; set; }

    public void InitMap() {
        map = new int[Height][];
        for (int i = 0; i < Height; i++) {
            map[i] = new int[Width];
        }
    }

    // flip x y bec of convenience
    public int this[int x, int y] {
        get {
            if (!IsInBounds(x, y)) {
                return -1;
            }
            return map[y][x];
        }
        set {
            if (IsInBounds(x, y)) {
                map[y][x] = value;
            }
        }
    }

    public int this[Point p] {
        get => this[p.X, p.Y];
        set => this[p.X, p.Y] = value;
    }

    public void UpdatePlayers(IEnumerable<Player> players, IEnumerable<Enemy> enemies) {
        winners = [];
        losers = [];
        foreach (var p in players) {
            winners[p.Id] = p;
            this[p.X, p.Y] = p.Id;
        }
        foreach (var p in enemies) {
            losers[p.Id] = p;
            this[p.X, p.Y] = p.Id;
        }
    }

    public Agent GetAgent(int id) {
        if (winners.TryGetValue(id, out var p)) {
            return p;
        }
        if (losers.TryGetValue(id, out var e)) {
            return e;
        }
        throw new ArgumentException("Agent not found");
    }

    public bool IsInBounds(int x, int y)
        => x >= 0 && x < Width && y >= 0 && y < Height;
    public bool IsInBounds(Point p) => IsInBounds(p.X, p.Y);

    IEnumerable<(Point p, int val)> LoopRange(Agent ag, int diff)
        => LoopRange(ag.Point.Update(-diff), ag.Point.Update(diff));

    IEnumerable<(Point p, int val)> LoopRange(Point s, Point e) {
        for (int y = s.Y; y < e.Y; y++)
            for (int x = s.X; x < e.X; x++)
                if (IsInBounds(x, y))
                    yield return (Point.New(x, y), this[x, y]);
    }

    public void GetBestShoot(int agId) {
        var agent = winners[agId];
        var mostD = 0;
        foreach (var (_, enem) in losers) {
            var dmg = agent.GetDamage(enem.Point);
            if (dmg > mostD) {
                mostD = dmg;
                agent.Shoot = enem.Id;
            }
        }
    }

    public void GetBestCover(int agId) {
        var agent = winners[agId];

        double maxCover = 0;
        foreach (var (cov, el) in LoopRange(agent, 1)) {
            if (el == Const.SmallCover || el == Const.HighCover) {
                var mult = el == Const.SmallCover ? .5 : .75;
                foreach (var myP in cov.GetNeighs()) {
                    if (!IsInBounds(myP) || cov.ManD(myP) > 1)
                        continue;
                    double localMax = 0;
                    foreach (var (_, v) in losers) {
                        if (v.Point.ManD(cov) == 1)
                            continue;
                        if (agent.X == cov.X && Point.IsInRowY(myP, cov, v.Point)
                            || agent.Y == cov.Y && Point.IsInRowX(myP, cov, v.Point)) {
                            localMax += mult * v.GetDamage(myP);
                        }
                    }
                    if (localMax > maxCover) {
                        maxCover = localMax;
                        agent.Cover = myP;
                    }
                }
            }
        }
    }
}

class Downie {
    static void Main(string[] args) {
        string[] inputs;
        var scanner = new Scanner(true);
        int myId = int.Parse(scanner.ReadLine()); // Your player id (0 or 1)
        int agentDataCount = int.Parse(scanner.ReadLine()); // Total number of agents in the game
        var kings = new List<Player>();
        var idiots = new List<Enemy>();
        var grid = new Grid();
        for (int i = 0; i < agentDataCount; i++) {
            inputs = scanner.ReadLine().Split(' ');
            int agentId = int.Parse(inputs[0]); // Unique identifier for this agent
            int player = int.Parse(inputs[1]); // Player id of this agent
            int shootCooldown = int.Parse(inputs[2]); // Number of turns between each of this agent's shots
            int optimalRange = int.Parse(inputs[3]); // Maximum manhattan distance for greatest damage output
            int soakingPower = int.Parse(inputs[4]); // Damage output within optimal conditions
            int splashBombs = int.Parse(inputs[5]); // Number of splash bombs this can throw this game
            Agent pl = player == myId ? new Player() : new Enemy();
            pl.Id = agentId;
            pl.ShootCooldown = shootCooldown;
            pl.OptimalRange = optimalRange;
            pl.SoakingPower = soakingPower;
            pl.SplashBombs = splashBombs;
            if (player == myId)
                kings.Add((Player)pl);
            else
                idiots.Add((Enemy)pl);
        }
        inputs = scanner.ReadLine().Split(' ');
        int width = int.Parse(inputs[0]); // Width of the game map
        int height = int.Parse(inputs[1]); // Height of the game map
        grid.Width = width;
        grid.Height = height;
        grid.InitMap();
        for (int i = 0; i < height; i++) {
            inputs = scanner.ReadLine().Split(' ');
            for (int j = 0; j < width; j++) {
                int x = int.Parse(inputs[3 * j]);// X coordinate, 0 is left edge
                int y = int.Parse(inputs[3 * j + 1]);// Y coordinate, 0 is top edge
                int tileType = int.Parse(inputs[3 * j + 2]);
                if (tileType > 0) {
                    grid[x, y] = tileType;
                }
            }
        }
        grid.UpdatePlayers(kings, idiots);

        // game loop
        while (true) {
            kings.Clear();
            idiots.Clear();
            int agentCount = int.Parse(scanner.ReadLine()); // Total number of agents still in the game
            for (int i = 0; i < agentCount; i++) {
                inputs = scanner.ReadLine().Split(' ');
                int agentId = int.Parse(inputs[0]);
                var agent = grid.GetAgent(agentId);

                var xx = int.Parse(inputs[1]);
                var yy = int.Parse(inputs[2]);
                agent.Point = Point.New(xx, yy);
                // int cooldown = int.Parse(inputs[3]); // Number of turns before this agent can shoot
                agent.ShootCooldown = int.Parse(inputs[3]); // Number of turns before this agent can shoot
                // int splashBombs = int.Parse(inputs[4]);
                agent.SplashBombs = int.Parse(inputs[4]);
                // int wetness = int.Parse(inputs[5]); // Damage (0-100) this agent has taken
                agent.SoakingPower = int.Parse(inputs[5]); // Damage (0-100) this agent has taken
                if (agent is Enemy e)
                    idiots.Add(e);
                else if (agent is Player p)
                    kings.Add(p);
                else throw new Exception("Unknown Type");
                scanner.Debug(agent.Log(agent is Player));
            }
            grid.UpdatePlayers(kings, idiots);
            int myAgentCount = int.Parse(scanner.ReadLine()); // Number of alive agents controlled by you
            var enem = idiots.MaxBy(x => x.Wetness);
            for (int i = 0; i < myAgentCount; i++) {

                // Write an action using scanner.WriteLine()
                // To debug: scanner.Error.WriteLine("Debug messages...");
                var player = kings[i];
                grid.GetBestCover(player.Id);
                grid.GetBestShoot(player.Id);
                scanner.WriteLine(player.Command());

                // One line per agent: <agentId>;<action1;action2;...> actions are "MOVE x y | SHOOT id | THROW x y | HUNKER_DOWN | MESSAGE text"
            }
            scanner.DumpBuffer();
        }
    }
}