

public class Test {
    public static void Main() {
        var scanner = new Scanner();
        var t = scanner.NextInt();

        for (int _ = 0; _ < t; _++) {
            var n = scanner.NextInt();
            var s = scanner.NextString().ToArray();
            for (int i = 1; i < n; i++) {
                if (s[i - 1] > s[i]) {
                    var e = s[i - 1];
                    var j = i;
                    for (j = i; j < n; j++) {
                        if (s[j] <= e) {
                            s[j - 1] = s[j];
                        } else {
                            break;
                        }
                    }
                    s[--j] = e;
                    break;
                }
            }
            Console.WriteLine(string.Concat(s));
        }
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
            writer = new(File.Create("out.txt"));
        }
        writer.AutoFlush = false;
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

    public void WriteArr(char[] arr) {
        foreach (var c in arr) {
            writer.Write(c);
        }
        writer.Write(writer.NewLine);
    }
}