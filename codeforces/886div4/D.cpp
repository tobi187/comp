#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int tt, n, k;
    cin >> tt;
    while (tt--) {
        cin >> n >> k;
        vector<int> v(n);
        for (auto &a : v) cin >> a;
        sort(v.begin(), v.end());
        vector<int> r;
        r.push_back(1);
        int l = 0;
        for (int i = 1; i < n; i++) {
            if (v[i] - v[i-1] > k) {
                r.push_back(1);
                l++;
            } else {
                r[l]++;
            }
        }
        int rr = *max_element(r.begin(), r.end());
        cout << n - rr << endl;
    }
}
