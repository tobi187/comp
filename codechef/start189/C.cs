using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

class Test {
  public static void Main() {
    Scanner scanner = new Scanner();
    if (File.Exists("in.txt"))
      scanner = new Scanner(File.OpenRead("in.txt"));

    var t = scanner.NextInt();
    while (t-- > 0) {
      var n = scanner.NextInt();
      var val = scanner.NextInt();
      var vits = new int[n];
      var both = new (int, int)[n];
      for (int i = 0; i < n; i++) {
        vits[i] = scanner.NextInt();
      }
      for (int i = 0; i < n; i++) {
        both[i] = (vits[i], scanner.NextInt());
      }

      var d = new Dictionary<int, int>();
      for (int i = 0; i < both.Length; i++) {
        var k = both[i].Item1;
        if (d.ContainsKey(k)) {
          d[k] = Math.Min(d[k], both[i].Item2);
        } else {
          d[k] = both[i].Item2;
        }
      }

      var b = d.Select(x => (x.Key, x.Value)).ToArray();

      Array.Sort(b, new S());

      var cm = 0;
      var costs = 0;
      for (int i = 0; i < b.Length; i++) {
        costs += b[i].Item2;
        var s = val * (i + 1) - costs;
        if (s < cm) {
          break;
        }
        cm = s;
      }
      System.Console.WriteLine(cm);
    }
  }
}

public class S : IComparer<(int, int)> {
  // Call CaseInsensitiveComparer.Compare with the parameters reversed.
  public int Compare((int, int) x, (int, int) y) {
    return x.Item2.CompareTo(y.Item2);
  }
}

class C : EqualityComparer<(int, int)> {
  public override bool Equals((int, int) f, (int, int) s) {
    return f.Item1 == s.Item1;
  }

  public override int GetHashCode((int, int) a) {
    int hCode = a.Item1 * a.Item2;
    return hCode.GetHashCode();
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