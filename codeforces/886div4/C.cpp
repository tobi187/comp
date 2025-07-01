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
        string s = "";
        char c;
        for (int y = 0; y < 8 * 8; y++) {
            cin >> c;
            if (c != '.') s += c;
        }
        cout << s << endl;
    }
}
