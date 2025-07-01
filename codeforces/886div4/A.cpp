#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n;
    cin >> n;
    while (n--) {
        int a, b, c;
        cin >> a >> b >> c;
        if (a + b >= 10 || a+ c >= 10 || b + c >= 10) {
            cout << "YES" << endl;
        } else {
            cout << "NO" << endl;
        }
    }
}
