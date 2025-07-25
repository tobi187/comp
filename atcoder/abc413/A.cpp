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

  int t, m;
  cin >> t >> m;

  int sum = 0;
  while (t--) {
    int a;
    cin >> a;
    sum += a;
  }
  cout << (sum > m ? "No" : "Yes") << endl;
}
