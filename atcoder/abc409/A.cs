public class Test {
    public static void Main() {
        Scanner scanner;
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));
        else
            scanner = new Scanner();

        var t = scanner.NextInt();

        var l1 = scanner.NextString();
        var l2 = scanner.NextString();
        var op = "No";
        for (int i = 0; i < l1.Length; i++) {
            if (l1[i] == 'o' && l2[i] == 'o') {
                op = "Yes";
                break;
            }
        }
        Console.WriteLine(op);
    }
}

static class Scanner {
    public static int ReadInt() => int.Parse(Console.ReadLine());
}

// https://github.com/epeshk/scanner

class Scanner1 {
    readonly StreamReader reader;
    char[] buffer = new char[4096];
    char currChar = default;

    public Scanner(Stream? fs = null) {
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
        if (currChar == '\n' || currChar == '\r')
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
                if (readStart)
                    break;
                continue;
            }

            readStart = true;
            buffer[length++] = currChar;
        }

        return length;
    }
}