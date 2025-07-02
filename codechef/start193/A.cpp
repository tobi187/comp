#if defined(_MSC_VER)
#include <__msvc_all_public_headers.hpp>
#elif defined(__GNUC__)
#include <bits/stdc++.h>
#else
#error "Unsupported compiler"
#endif

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

  int rl, rb, q;
  cin >> rl >> rb >> q;

  cout << (rl * rb == q * q ? "yes" : "no") << endl;
}
