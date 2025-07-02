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
      var f = scanner.NextInt();
      var s = scanner.NextInt();

      if (f % 2 != 0 && s % 2 != 0) {
        if (CD(f, s + 1) || CD(f + 1, s)) {
          scanner.WriteLine(1);
        } else {
          scanner.WriteLine(2);
        }
      } else if (f % 2 == 0 && s % 2 == 0) {
        scanner.WriteLine(0);
      } else if (CD(f, s)) {
        scanner.WriteLine(0);
      } else {
        scanner.WriteLine(1);
      }
    }

    scanner.Flush();
  }
  static bool CD(int one, int two) {
    if (Math.Min(one, two) == 1) {
      return false;
    }
    if (Math.Max(one, two) % Math.Min(one, two) == 0) {
      return true;
    }
    for (int i = 2; i < Math.Min(one, two) / 2; i++) {
      if (one % i == 0 && two % i == 0) {
        return true;
      }
    }
    return false;
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