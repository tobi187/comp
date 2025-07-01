#include <bits/stdc++.h>

using namespace std;

// https://codeforces.com/contest/1790/problem/C

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n,tt;
    cin >> tt;
    for (int tz = 0; tz < tt; tz++) {
        cin >> n;
        vector<vector<int>> v(n);
        for (int i = 0; i < n; i++) {
            vector<int> vv(n-1);
            for (auto &a : vv) cin >> a;
            v[i] = vv;
        }

        vector<int> r(n, -1);
        while (r[n-1] == -1) {
            pair<int,int> p = make_pair<int,int>(v[0][0], v[0][1]);
            for (int i = 1; i < n; i++) {
                
            }
        }
    }
    
}
