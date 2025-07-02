using System;
using System.IO;
using System.Text;

class Test {
  public static void Main() {
    Scanner scanner = new Scanner();
    if (File.Exists("in.txt"))
      scanner = new Scanner(File.OpenRead("in.txt"));

    var t = scanner.NextInt();
    while (t-- > 0) {
      var n = scanner.NextInt();
      int last = -1;
      for (int i = 0; i < n; i++) {
        var next = scanner.NextInt();
        if (next != last && last != -1) {
          System.Console.WriteLine(2);
          scanner.NextLine();
          last = -1;

          break;
        }
        last = next;
      }
      if (last != -1) System.Console.WriteLine(-1);
    }
  }
}

class Scanner {
  StreamReader reader;
  char[] buffer = new char[4096];
  char currChar = '\0';

  public Scanner(Stream fs = null) {
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
    if (currChar == '\n' || currChar == '\r') {
      return "";
    }
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
}