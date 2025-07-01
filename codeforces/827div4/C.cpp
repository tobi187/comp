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
        string s;
        vector<string> grid(8, "12345678");
        char r = '.';
        for (int j = 0; j < 8; j++) {
            cin >> s;
            r = s[0];
            for (int ii = 0; ii < 8; ii++) {
                if (s[ii]!=r) r='.';
                grid[ii][j] = s[ii];
            }
            if (r!='.') break;
        }
        if (r!='.') {
            std::cout << r << endl;
            continue;
        }
        for (int j = 0; j < 8; j++) {
            r = grid[j][0];
            if (r == 'B') std::cout << grid[j] << endl;  
            for (auto a: grid[j]) 
                if (a != r) r = '.';
            if (r != '.') break;
        }
        std::cout << r << endl;
    }   
}
