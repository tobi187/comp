#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n, t;
    cin >> t;
    for (int _=0; _< 300; _++) {
        cin >> n;
        int c = 0;
        for (int i = 1; i < n+1; i++) {
            if (i | n) c++;
        }
        if (n != c)
            cout << c << " " << n << endl;
    }
}
