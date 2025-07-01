using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

public class Test {
    public static void Main() {
        Scanner scanner;
        if (File.Exists("in.txt"))
            scanner = new Scanner(File.OpenRead("in.txt"));
        else
            scanner = new Scanner();

        var q = scanner.NextInt();
        var t = scanner.NextInt();
        var arr = new bool[q]; // white false

        var s = 0;

        bool GetColOrDef(int i, bool d) {
            if (i < 0 || i > q - 1) {
                return d;
            }
            return arr[i];
        }

        for (int a = 0; a < t; a++) {
            var p = scanner.NextInt() - 1;
            arr[p] = !arr[p];
            if (arr[p]) {
                if (!GetColOrDef(p - 1, false) && !GetColOrDef(p + 1, false)) {
                    ++s;
                } else if (GetColOrDef(p - 1, false) && GetColOrDef(p + 1, false)) {
                    --s;
                }
            } else {
                if (p == 0) {
                    if (!GetColOrDef(p + 1, false)) --s;
                } else if (p == q - 1) {
                    if (!GetColOrDef(p - 1, false)) --s;
                } else if (arr[p - 1] && arr[p + 1]) {
                    ++s;
                }
            }
            // [b,w,b]
            // for (int i = 0; i < arr.Length; i++) {
            //     if (arr[i]) {
            //         if (!b) {
            //             b = true;
            //         }
            //     } else {
            //         if (b) {
            //             ++s;
            //             b = false;
            //         }
            //     }
            // }
            // if (b) ++s;
            scanner.WriteLine(s);
        }

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