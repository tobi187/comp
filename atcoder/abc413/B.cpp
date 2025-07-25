#include <bits/stdc++.h>

using namespace std;

void solve() {
  int n;
  cin >> n;
}

int main() {
#ifndef ONLINE_JUDGE
  freopen("in.txt", "r", stdin);
  freopen("out.txt", "w", stdout);
#endif

  int t;
  cin >> t;
  vector<string> v(t);
  unordered_set<string> hs{};

  for (int i = 0; i < t; i++) {
    string s;
    cin >> s;
    for (int j = 0; j < i; j++) {
      hs.insert(s + v[j]);
      hs.insert(v[j] + s);
    }
    v[i] = s;
  }

  cout << hs.size() << endl;
}
