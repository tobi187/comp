using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

class Test {
  public static void Main() {
    Scanner scanner = new Scanner();
    if (File.Exists("in.txt"))
      scanner = new Scanner(File.OpenRead("in.txt"));

    var t = scanner.NextInt();
    while (t-- > 0) {
      var n = scanner.NextInt();
      var q = scanner.NextInt();
      var arr = new int[n];
      for (int i = 0; i < n; i++) {
        arr[i] = scanner.NextInt();
      }
      for (int iii = 0; iii < q; iii++) {
        var ind = scanner.NextInt();
        var val = scanner.NextInt();
        arr[ind - 1] = val;

        var l = arr.ToList();
        int s = 0;
        while (l.Count > 1) {
          int m = 0;
          int ii = 0;
          for (int i = 1; i < l.Count; i++) {
            var min = Math.Min(l[i], l[i - 1]);
            if (min > m) {
              m = min;
              ii = i;
            }
          }
          if (l[ii] < l[ii - 1]) {
            s += l[ii];
            l.RemoveAt(ii - 1);
          } else {
            s += l[ii - 1];
            l.RemoveAt(ii);
          }
        }
        scanner.WriteLine(s);
      }
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