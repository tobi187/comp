#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n;
    string s;
    cin >> n;
    for (int i = 0; i < n; i++) {
        cin >> s;
        int ss = s.size();
        if (ss > 10) cout << s[0] << ss - 2 << s[ss-1] << endl;
        else cout << s << endl;
    }
}
