#include <bits/stdc++.h>

using namespace std;

// k=2, n = 5
// 5, 4 -> 6 6
//

void solve() {
  int n, k;
  cin >> n >> k;
  vector<int> sol;
  sol.reserve(n);
  while (k--) {
    sol.push_back(n--);
  }
  for (int i = 0; i < n; i++) {
    sol.push_back(i + 1);
  }
  for (int i = 0; i < sol.size(); i++) {
    cout << sol[i] << " ";
  }
  cout << endl;
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
