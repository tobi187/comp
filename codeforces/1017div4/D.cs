public class Test {
    public static void Main() {
        Scanner scanner;
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));
        else
            scanner = new Scanner();

        var t = scanner.NextInt();

        for (int _ = 0; _ < t; _++) {
            var f = scanner.NextLine();
            var s = scanner.NextLine();
            var cc = f[0];
            var p1 = 0;
            var p2 = 0;
            var l1 = 0;
            var l2 = 0;
            bool can = true;

            while (true) {
                while (p1 < f.Length && f[p1] == cc) ++p1;
                while (p2 < s.Length && s[p2] == cc) ++p2;
                // check

                var a1 = p1 - l1;
                var a2 = p2 - l2;
                if (a1 > a2) {
                    can = false;
                    break;
                }
                if (a2 > a1 * 2) {
                    can = false;
                    break;
                }
                if (p1 >= f.Length && p2 >= s.Length)
                    break;
                if (p1 >= f.Length || p2 >= s.Length) {
                    can = false;
                    break;
                }
                l1 = p1;
                l2 = p2;
                cc = s[p2];
            }
            scanner.WriteLine(can ? "YES" : "NO");
        }
        scanner.Flush();
    }
}


// https://github.com/epeshk/scanner

class Scanner {
    readonly StreamReader reader;
    readonly StreamWriter writer;
    char[] buffer = new char[4096];
    char currChar = default;

    public Scanner(Stream? fs = null) {
        if (fs is null) {
            writer = new(Console.OpenStandardOutput());
        } else {
            writer = new(File.OpenWrite("out.txt"));
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
        while (currChar == '\r')
            currChar = (char)reader.Read();
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