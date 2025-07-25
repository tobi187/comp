using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
/**
 * Win the water fight by controlling the most territory, or out-soak your opponent!
 **/
public class Agent
{
    public int agentId { get; set; }
    public int shootCooldown { get; set; }
    public int optimalRange { get; set; }
    public int weaponDamage { get; set; }
    public int splashBombs { get; set; }
    public int hp { get; set; }
    public (int, int) position { get; set; }
}
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        int myId = int.Parse(Console.ReadLine()); // Meine ID
        int SpielerAnzahl = int.Parse(Console.ReadLine()); // Anzahl meiner Spieler
        Console.Error.WriteLine(SpielerAnzahl);
        List<Agent> MyAgents = new List<Agent>();
        List<Agent> OpponentAgents = new List<Agent>();
        for (int i = 0; i < SpielerAnzahl; i++)
        {
            Agent agent = new Agent();
            bool myPlayer = true;
            inputs = Console.ReadLine().Split(' ');
            agent.agentId = int.Parse(inputs[0]); // Unique identifier for this agent
            agent.shootCooldown = int.Parse(inputs[2]); // Number of turns between each of this agent's shots
            agent.optimalRange = int.Parse(inputs[3]); // Maximum manhattan distance for greatest damage output
            agent.weaponDamage = int.Parse(inputs[4]); // Damage output within optimal conditions
            agent.splashBombs = int.Parse(inputs[5]); // Menge an Bomben
            if (int.Parse(inputs[1]) != myId) myPlayer = false; // Player id of this agent
            if (myPlayer)
                MyAgents.Add(agent);
            else
                OpponentAgents.Add(agent);
        }

        inputs = Console.ReadLine().Split(' ');
        int width = int.Parse(inputs[0]); // Width of the game map
        int height = int.Parse(inputs[1]); // Height of the game map
        for (int i = 0; i < height; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            for (int j = 0; j < width; j++)
            {
                int x = int.Parse(inputs[3 * j]);// X coordinate, 0 is left edge
                int y = int.Parse(inputs[3 * j + 1]);// Y coordinate, 0 is top edge
                int tileType = int.Parse(inputs[3 * j + 2]);
            }
        }
        // game loop
        while (true)
        {
            SpielerAnzahl = int.Parse(Console.ReadLine());
            for (int i = 0; i < SpielerAnzahl; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int agentId = int.Parse(inputs[0]);

                Agent currentAgent = new Agent();
                foreach (var c in MyAgents)
                    if (c.agentId == agentId)
                    {
                        currentAgent = c;
                        if (currentAgent.hp >= 100) MyAgents.Remove(c);
                    }
                foreach (var c in OpponentAgents)
                    if (c.agentId == agentId)
                    {
                        currentAgent = c;
                        if (currentAgent.hp >= 100) OpponentAgents.Remove(c);
                    }
                int x = int.Parse(inputs[1]);
                int y = int.Parse(inputs[2]);
                currentAgent.position = (x, y);
                int cooldown = int.Parse(inputs[3]); // Number of turns before this agent can shoot
                currentAgent.shootCooldown = cooldown;
                int splashBombs = int.Parse(inputs[4]);
                currentAgent.splashBombs = splashBombs;
                int hp = int.Parse(inputs[5]); // Damage (0-100) this agent has taken
                currentAgent.hp = hp;
            }
            int myAgentCount = int.Parse(Console.ReadLine()); // Number of alive agents controlled by you

            foreach (var agent in MyAgents)
            {
                var possibleEnemies = new Dictionary<int, double>();
                (int, int) myPos = agent.position;
                foreach (var enemies in OpponentAgents)
                {
                    double dist = (myPos.Item1 - enemies.position.Item1) + (myPos.Item2 - enemies.position.Item2);
                    if (dist <= (double)agent.optimalRange) possibleEnemies.Add(enemies.agentId, enemies.hp);
                }

                int shootAt = -1;
                int mostHP = 0;
                int mosthpID = 0;
                foreach (var kvp in possibleEnemies)
                {
                    if (kvp.Value + agent.weaponDamage >= 100)
                    {
                        shootAt = kvp.Key;
                        possibleEnemies.Remove(kvp.Key);
                        break;
                    }
                    else if (mostHP < (int)kvp.Value)
                    {
                        mostHP = (int)kvp.Value;
                        mosthpID = kvp.Key;
                    }
                }
                if (myAgentCount != MyAgents.Count) Console.Error.WriteLine("Fehler: Anzahl eigener Agents unterscheidet sich zur Anzahl der Agents in der Liste!");
                if (shootAt == -1) shootAt = mosthpID;
                // One line per agent: <agentId>;<action1;action2;...> actions are "MOVE x y | SHOOT id | THROW x y | HUNKER_DOWN | MESSAGE text"
                Console.WriteLine($"{agent.agentId};SHOOT {shootAt}");
            }
        }
    }
}