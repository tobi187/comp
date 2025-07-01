#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int tt, n;
    cin >> tt;
    while (tt--) {
        cin >> n;
        int mv = 0;
        int ind = 0;
        int l, q;
        for (int i = 0; i < n; i++) {
            cin >> l >> q;
            if (l <= 10) {
                mv = max(q, mv);
                if (mv == q) ind = i; 
            }
        }
        cout << ++ind << endl;
    }
}
