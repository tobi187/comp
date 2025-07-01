#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n;
    cin >> n;

    for (int i = 0; i < n; i++) {
        int a,b,c;
        cin >> a >> b >> c;
        if (b>a) swap(a,b);
        if (c>a) swap(a,c);
        cout << (b+c==a ? "YES" : "NO") << endl; 
    }
}
