using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class Test
{
    public static void Main()
    {
        Scanner scanner = new Scanner();
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));

        var t = scanner.NextInt();

        for (int _ = 0; _ < t; _++)
        {
            var l = scanner.NextInt();
            var zero = scanner.NextInt();
            var one = scanner.NextInt();
            var zeroOne = scanner.NextInt();
            var oneZero = scanner.NextInt();

            int max = 0;
            for (int i = 0; i <= l; i++)
            {
                // var rest = l - i;
                // var v1 = zero * i + one * i + zeroOne * i * rest;
                // var v2 = zero * i + one * i + oneZero * i * rest;

                // var lMAx = Math.Max(v1, v2);
                // max = Math.Max(lMAx, max);
                var rest = l - i;
                var score = i * one + rest * zero + i * rest * (oneZero + zeroOne);
                max = Math.Max(score, max);
            }
            
            System.Console.WriteLine(max);
        }
    }
}


class Scanner
{
    readonly StreamReader input;
    char[] buffer = new char[4096];
    char currChar = '\0';

    public Scanner(Stream fs = null)
    {
        fs = fs ?? Console.OpenStandardInput();
        input = new StreamReader(fs, Encoding.Default, true, 16384);
    }

    public int NextInt()
    {
        var length = PrepareToken();
        return int.Parse(new string(buffer, 0, length));
    }

    public long NextLong()
    {
        var length = PrepareToken();
        return long.Parse(new string(buffer, 0, length));
    }

    public double NextDouble()
    {
        var length = PrepareToken();
        return double.Parse(new string(buffer, 0, length));
    }

    public string NextString()
    {
        var length = PrepareToken();
        return new string(buffer, 0, length);
    }

    public string NextLine()
    {
        if (currChar == '\n') // || currChar == '\r')
            return null;
        return input.ReadLine();
    }

    private int PrepareToken()
    {
        int length = 0;
        bool readStart = false;
        while (true)
        {
            var cc = input.Read();
            if (cc == -1)
                break;

            currChar = (char)cc;
            if (char.IsWhiteSpace(currChar))
            {
                if (readStart) break;
                continue;
            }

            readStart = true;
            buffer[length++] = currChar;
        }

        return length;
    }
}
