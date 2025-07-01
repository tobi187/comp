#include <bits/stdc++.h>

using namespace std;

bool isCoprime(int a, int b) {
    if (b>a) swap(a,b);
    while (true)
    {
        if (a%b==0) return b==1;
        a = a / b;
        swap(a,b);
    }
}

struct PairSort
{
    inline bool operator() (const pair<int,int> &a, pair<int,int> &b) {
        return (a.first+a.second>b.first+b.second);
    }
};


int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n, l;
    cin >> n;

    for (int i = 0; i < n; i++) {
        cin >> l;
        vector<int> v(l);
        vector<pair<int,int>> indices; 
        indices.reserve(pow(l, 2));
        for (auto &a : v) cin >> a;
        for(int j=0;j<l;j++) 
            for (int jj=0;jj<l;jj++)
                indices.push_back(make_pair(j,jj));
        bool f = false;
        sort(indices.begin(), indices.end(), PairSort());
        for (auto p : indices) {
            if (isCoprime(v[p.first], v[p.second])){
                cout << p.first+1 + p.second+1 << endl;
                f=true;
                break;
            }
        }
        if (!f) cout << -1 << endl;
    }
}


