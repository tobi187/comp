using System;
using System.IO;
using System.Text;

public class Test {
    public static void Main() {
        Scanner scanner;
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));
        else
            scanner = new Scanner();

        var t = scanner.NextInt();

        for (int _ = 0; _ < t; _++) {

        }
    }
}


class Scanner {
    readonly StreamReader reader;
    readonly StreamWriter writer;
    char[] buffer = new char[4096];
    char currChar = '\0';

    public Scanner(Stream fs = null) {
        if (fs == null) {
            writer = new StreamWriter(Console.OpenStandardOutput());
        } else {
            writer = new StreamWriter(File.OpenWrite("out.txt"));
        }
        writer.AutoFlush = true;
        fs = fs ?? Console.OpenStandardInput();
        reader = new StreamReader(fs, Encoding.Default, true, 16384);
    }

    public int NextInt() {
        var length = PrepareToken();
        return int.Parse(new string(buffer, 0, length));
    }

    public long NextLong() {
        var length = PrepareToken();
        return long.Parse(new string(buffer, 0, length));
    }

    public double NextDouble() {
        var length = PrepareToken();
        return double.Parse(new string(buffer, 0, length));
    }

    public string NextString() {
        var length = PrepareToken();
        return new string(buffer, 0, length);
    }

    public string NextLine() {
        if (currChar == '\n') // || currChar == '\r')
            return "";
        return reader.ReadLine();
    }

    private int PrepareToken() {
        int length = 0;
        bool readStart = false;
        while (true) {
            var cc = reader.Read();
            if (cc == -1)
                break;

            currChar = (char)cc;
            if (char.IsWhiteSpace(currChar)) {
                if (readStart) break;
                continue;
            }

            readStart = true;
            buffer[length++] = currChar;
        }

        return length;
    }

    public void Write(object s) {
        writer.Write(s);
    }

    public void WriteLine(object s) {
        writer.WriteLine(s);
    }

    public void Flush() {
        writer.Flush();
    }
}
