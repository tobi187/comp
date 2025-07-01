#include <bits/stdc++.h>

using namespace std;

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n;
    cin >> n;
    set<int> s;
    int c = 0;
    for (int i = 0; i < n; i++) {
        int l;
        int e;
        cin >> l;
        for (int j = 0; j < l; j++) {
            cin >> e;
            s.insert(e);
            c++;
        } 
        cout << (s.size() == c ? "YES" : "NO") << endl;
        s.clear();
        c=0;
    }
}
