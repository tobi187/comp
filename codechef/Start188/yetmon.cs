using System;
using System.IO;
using System.Text;

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
            var arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = scanner.NextInt();

            Array.Sort(arr);

            for (int i = n - 1; i >= 0; i--)
            {
                if (arr[i] < arr.Length)
                {
                    
                }
            }
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
            currChar = (char)input.Read();
            if (currChar == -1)
                break;

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