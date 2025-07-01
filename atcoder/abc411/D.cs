using System.Security.Cryptography.X509Certificates;
using System.Text;

public class Test {
    public static void Main() {
        Scanner scanner;
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));
        else
            scanner = new Scanner();

        var t = scanner.NextInt();
        var q = scanner.NextInt();

        int buff = (int)Math.Pow(10, 6);
        var pcs = new char[t][];
        var pl = new int[t];
        var server = new char[buff];
        var sl = 0;
        Array.Fill(pcs, new char[buff]);
        char[] c = new char[0];
        for (int i = 0; i < q; i++) {
            var f = scanner.NextInt();
            var s = scanner.NextInt() - 1;

            if (f == 2)
                c = scanner.NextLine().TrimStart().ToArray();
            if (f == 1) {
                Array.Copy(server, pcs[s], sl);
                pl[s] = sl;
            } else if (f == 2) {
                Array.Copy(c, 0, pcs[s], pl[s], c.Length);
                pl[s] += c.Length;
            } else {
                Array.Copy(pcs[s], server, pl[s]);
                sl = pl[s];
            }
        }
        scanner.WriteLine(string.Concat(server[..sl]));

        scanner.Flush();
    }
}


// https://github.com/epeshk/scanner

class Scanner {
    readonly StreamReader reader;
    readonly StreamWriter writer;
    char[] buffer = new char[4096];

    public Scanner(Stream? fs = null) {
        if (fs is null) {
            writer = new(Console.OpenStandardOutput());
        } else {
            writer = new(File.Create("out.txt"));
        }
        fs ??= Console.OpenStandardInput();
        reader = new(fs, bufferSize: 16384);
    }

    public int NextInt() {
        var length = PrepareToken();
        return int.Parse(buffer.AsSpan(0, length));
    }

    public long NextLong() {
        var length = PrepareToken();
        return long.Parse(buffer.AsSpan(0, length));
    }

    public double NextDouble() {
        var length = PrepareToken();
        return double.Parse(buffer.AsSpan(0, length));
    }

    public string NextString() {
        var length = PrepareToken();
        return new string(buffer, 0, length);
    }

    public string NextLine() {
        while (reader.Peek() == '\r' || reader.Peek() == '\n') {
            reader.Read();
        }
        return reader.ReadLine() ?? "";
    }

    private int PrepareToken() {
        int length = 0;
        bool readStart = false;
        while (true) {
            var cc = reader.Read();
            if (cc == -1)
                break;
            var currChar = (char)cc;

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