#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif
    string pi = "314159265358979323846264338327";
    int n;
    cin >> n;
    for (int i = 0; i < n; i++) {
        string s;
        cin >> s;
        int count = 0;
        for (int i = 0; i < min(30, (int)s.size()); i++) {
            if (s[i] == pi[i]) count++;
            else break;
        }
        cout << count << endl;
    }
}
