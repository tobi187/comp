#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n,tt, s, r;
    cin >> tt;
    for (int tz = 0; tz < tt; tz++) {
        cin >> n >> s >> r;
        vector<int> res(n, 1);
        int hn = s-r;
        int cur = n-1 + hn;
        res[n-1] = hn;
        
        for (int i = 0; i < n-1; i++) {
            for (int j = hn; j > 0; j--) {
                if (cur + j-1 <= s) {
                    cur += j-1;
                    res[i] = j;
                    break;
                }
            }
            if (cur == s) break;
        }
        

        for (auto a : res) cout << a << " ";
        cout << endl; 
    }
}
