public class Test
{
    public static void Main()
    {
        Scanner scanner;
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));
        else
            scanner = new Scanner();

        var t = scanner.NextInt();

        for (int _ = 0; _ < t; _++)
        {
            var n = scanner.NextInt();
            var arr = new int[n * 2];
            var ba = new bool[n * 2];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var num = scanner.NextInt();
                    scanner.Debug(i + 2 + j);
                    arr[i + j + 1] = num;
                    ba[num - 1] = true;
                }
            }
            var res = "";
            for (var i = 0; i < ba.Length; i++)
            {
                if (!ba[i])
                {
                    res = $"{i + 1} ";
                    break;
                }
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0)
                {
                    continue;
                }
                res += $"{arr[i]} ";
            }
            scanner.WriteLine(res);
        }
        scanner.Flush();
    }
}


// https://github.com/epeshk/scanner

class Scanner
{
    readonly StreamReader reader;
    readonly StreamWriter writer;
    char[] buffer = new char[4096];
    char currChar = default;

    public Scanner(Stream? fs = null)
    {
        if (fs is null)
        {
            writer = new(Console.OpenStandardOutput());
        }
        else
        {
            writer = new(File.OpenWrite("out.txt"));
        }
        fs ??= Console.OpenStandardInput();
        reader = new(fs, bufferSize: 16384);
    }

    public int NextInt()
    {
        var length = PrepareToken();
        return int.Parse(buffer.AsSpan(0, length));
    }

    public long NextLong()
    {
        var length = PrepareToken();
        return long.Parse(buffer.AsSpan(0, length));
    }

    public double NextDouble()
    {
        var length = PrepareToken();
        return double.Parse(buffer.AsSpan(0, length));
    }

    public string NextString()
    {
        var length = PrepareToken();
        return new string(buffer, 0, length);
    }

    public string NextLine()
    {
        while (currChar == '\r')
            currChar = (char)reader.Read();
        return reader.ReadLine() ?? "";
    }

    private int PrepareToken()
    {
        int length = 0;
        bool readStart = false;
        while (true)
        {
            var cc = reader.Read();
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

    public void Write(object s)
    {
        writer.Write(s);
    }

    public void WriteLine(object s)
    {
        writer.WriteLine(s);
    }

    public void Flush()
    {
        writer.Flush();
    }

    public void Debug(object s)
    {
        Console.Error.WriteLine(s);
    }
}