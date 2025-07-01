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
        vector<int> v(n);
        for (int &a : v) cin >> a;
        int m = 0;
        for (int i = 1; i < n; i++) {
            int lm = 0;
            for (auto e : v) {
                if (e % i == 0) lm++;
            }
            m = max(lm, m);
        }
        cout << m << endl;
    }
}
