#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("in.txt", "r", stdin);
    freopen("out.txt", "w", stdout);
    #endif

    int t;
    cin >> t;
    for (int a = 0; a < t; a++) {
        int n;
        cin >> n;
        vector<char> s = vector<char>(n);
        for (int i = 0; i < n; i++) cin >> s[i];
        for (int i = 1; i < n; i++) {
            if (s[i - 1] > s[i]) {
                char e = s[i - 1];
                int j = i;
                for (j = i; j < n; j++) {
                    if (s[j] <= e) {
                        s[j - 1] = s[j];
                    } else {
                        break;
                    }
                }
                s[--j] = e;
                break;
            }
        }
        for (int i = 0; i < n; i++) cout << s[i];
        cout << '\n';
    }
}