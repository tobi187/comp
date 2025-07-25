#include <bits/stdc++.h>

using namespace std;

void solve() {
  int n;
  cin >> n;
  vector<int> v(n);
  for (auto& a : v) cin >> a;
  sort(v.begin(), v.end(), greater<int>());

  auto d = v[0] / (float)v[1];
  string ans = "Yes";
  for (int i = n - 2; i >= 0; i--) {
    if (round(d * v[i + 1]) != v[i]) {
      ans = "No";
      break;
    }
  }
  cout << ans << endl;
}

int main() {
#ifndef ONLINE_JUDGE
  freopen("in.txt", "r", stdin);
  freopen("out.txt", "w", stdout);
#endif

  int t;
  cin >> t;

  while (t--) {
    solve();
  }
}
