using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Test {
  public static void Main() {
    Scanner scanner = new Scanner();
    if (File.Exists("in.txt"))
      scanner = new Scanner(File.OpenRead("in.txt"));

    var t = scanner.NextInt();
    while (t-- > 0) {
      var n = scanner.NextInt();
      var lines = scanner.NextLine();
      Console.WriteLine(lines);
      var res = new bool[n];
      res[0] = lines[0] == '1';
      if (n > 1 && res[0]) res[1] = true;
      for (int i = 1; i < n; i++) {
        if (lines[i] == '1') {
          if (!res[i - 1]) {
            res[i - 1] = true;
          } else {
            if (i + 1 < n) {
              res[i + 1] = true;
            }
          }
          res[i] = true;
        }

      }
      Console.WriteLine(string.Join(", ", res));
      scanner.WriteLine(res.Any(x => !x) ? "No" : "Yes");
    }
    scanner.Flush();
  }
}

class Scanner {
  StreamReader reader;
  StreamWriter writer;
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
    while (reader.Peek() == '\n' || reader.Peek() == '\r') {
      reader.Read();
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