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
            var n = scanner.NextInt();
            var x = scanner.NextInt();
            var d = new int[n];
            var dd = new int[n];
            var p = new int[n];
            for (int i = 0; i < n; i++) {
                d[i] = scanner.NextInt();
            }
            for (int i = 0; i < n; i++) {
                p[i] = scanner.NextInt();
            }
            // dd[n - 1] = int.MinValue;
            // for (int i = dd.Length - 2; i >= 0; i--) {
            //     dd[i] = Math.Max(d[i + 1], dd[i + 1]);
            // }
            int ps = 0;
            // for (int i = 0; i < n; i++) {
            //     if (x >= p[i] && x > dd[i]) {
            //         ++ps;
            //         --x;
            //     }
            // }

            for (int i = 0; i < n; i++) {
                if (x < d[i]) {
                    var diff = d[i] - x;
                    if (ps >= diff) {
                        ps -= diff;
                        x += diff;
                    }
                } else if (x >= p[i]) {
                    --x;
                    ++ps;
                }
            }
            scanner.WriteLine(ps);
        }
        scanner.Flush();
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
            writer = new StreamWriter(File.Create("out.txt"));
        }
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
