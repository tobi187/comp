#include <bits/stdc++.h>

// https://codeforces.com/problemset/problem/1792/B

using namespace std;

int main() {
#ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
#endif

    int tt, l, a, b, n;
    cin >> tt;
    for (int t = 0; t < tt; t++) {
        cin >> l >> a >> b >> n;
        int bob = l, alice = l, round = l;
        while (min(bob, alice) >= 0 && a + b + n > 0) {
            int dec = 0;
            if (alice < bob && a > 0) {
                dec = min(bob, a);
                a -= dec;
                bob -= dec;
                alice += dec;
            } else if (b > 0) {
                dec = min(alice, b);
                b -= dec;
                alice -= dec;
                bob += dec;
            } else {
                dec = min(n, min(alice, bob));
                n -= dec;
                bob -= dec;
                alice -= dec;
                round += dec;
                if (dec != n) round++;
                break;
            }
            round += dec;
            if ((alice==0&&a==0) || (bob==0&&b==0)) {
                round++;
                break;
            }
        }
        cout << round << endl;
    }
}