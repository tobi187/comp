public class Test {
    public static void Main() {
        Scanner scanner;
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));
        else
            scanner = new Scanner();

        var t = scanner.NextInt();

        var s = new HashSet<int>();
        while (--t >= 0) {
            int n = scanner.NextInt();
            s.Add(n);
        }
        scanner.WriteLine(s.Contains(scanner.NextInt()) ? "Yes" : "No");

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

    public string EndLine() {
        return reader.ReadLine();
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