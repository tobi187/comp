public class Test {
    public static void Main() {
        Scanner scanner;
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));
        else
            scanner = new Scanner();

        var t = scanner.NextInt();
        var arr = new int[t];
        for (int i = 0; i < t; i++) {
            arr[i] = scanner.NextInt();
        }

        Array.Sort(arr);

        for (int i = t - 1; i >= 0; i--) {
            if (arr[i] <= t - i) {
                scanner.WriteLine(arr[i]);
                return;
            }
        }
        scanner.WriteLine(0);
    }
}


// https://github.com/epeshk/scanner

class Scanner {
    readonly StreamReader reader;
    readonly StreamWriter writer;
    char[] buffer = new char[4096];
    char currChar = default;

    public Scanner(Stream? fs = null) {
        if (fs == null)
            writer = new(Console.OpenStandardOutput());
        else
            writer = new(File.OpenWrite("out.txt"));
        writer.AutoFlush = true;
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
        if (currChar == '\n')
            return "";
        return reader.ReadLine() ?? "";
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