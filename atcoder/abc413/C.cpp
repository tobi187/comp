#include <bits/stdc++.h>

using namespace std;

void solve() {}

int main() {
#ifndef ONLINE_JUDGE
  freopen("in.txt", "r", stdin);
  freopen("out.txt", "w", stdout);
#endif

  int t;
  cin >> t;

  int a, b, c;
  deque<pair<long long, long long>> v = {};
  while (t--) {
    cin >> a;
    cin >> b;
    if (a == 1) {
      cin >> c;
      v.push_back(make_pair(b, c));
      continue;
    }
    long long s = 0;
    while (true) {
      pair<long long, long long>& f = v.front();
      if (f.first < b) {
        s += f.first * f.second;
        b -= f.first;
        v.pop_front();
        continue;
      }
      if (b == f.first) {
        v.pop_front();
      } else {
        f.first -= b;
      }
      s += b * f.second;
      break;
    }
    cout << s << endl;
  }
}
