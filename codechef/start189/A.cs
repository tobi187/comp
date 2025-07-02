using System;
using System.IO;
using System.Text;

class Test {
  public static void Main() {
    Scanner scanner = new Scanner();
    if (File.Exists("in.txt")) {
      scanner = new Scanner(File.OpenRead("in.txt"));
    }

    var b = (scanner.NextInt() + 1) * 4;
    var g = scanner.NextInt() * 3;
    var s = b + g;
    if (s % 8 == 0) System.Console.WriteLine(s / 8);
    else System.Console.WriteLine(s / 8 + 1);
  }
}

class Scanner {
  StreamReader reader;
  char[] buffer = new char[4096];
  char currChar = '\0';

  public Scanner(Stream fs = null) {
    fs = fs ?? Console.OpenStandardInput();
    reader = new(fs, Encoding.Default, true, 16384);
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
}