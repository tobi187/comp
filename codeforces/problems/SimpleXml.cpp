#include <bits/stdc++.h>

using namespace std;
//https://codeforces.com/problemset/problem/125/B

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    string s;
    cin >> s;
    int depth = 0;
    bool close = false;
    char l = '+'; 
    for (int i = 0; i < s.size(); i++) {
        char c = s[i];
        if (c == '/') c = true;
        
        if (c > 96 && c < 123) {
            string ws(depth*2, ' ');
            cout << ws << "<" << (close ? "/" : "") << s[i] << ">" << endl;
            if (close) 
                if (c == l) depth-=2;
            else 
                depth += 2;
            l = c;
            close = false;
        }
    }
}
