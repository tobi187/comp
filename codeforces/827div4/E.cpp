#include <bits/stdc++.h>

// https://stackoverflow.com/questions/6553970/find-the-first-element-in-a-sorted-array-that-is-greater-than-the-target

using namespace std;

#define ll long long int

int main() {
    #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    #endif

    int n,s,q;
    cin >> n;

    for (int i = 0; i < n; i++) {
        cin >> s >> q;
        vector<int> steps(s);
        vector<ll> computed(s);

        int qq,ss;
        for (int ii=0; ii < s; ii++) {
            cin >> ss;
            steps[ii] = ss;
            if (ii>0) computed[ii] = ss + computed[ii-1];
            else computed[ii] = ss; 
        }    
        for (int j= 0; j < q; j++) {
            cin >> qq;
            int start = 0;
            int end = s-1;
            while (start<=end)
            {
                int mid = (start+end) / 2;
                if (steps[mid] <= qq) start = mid + 1;
                else end = mid - 1;
            }
            cout << (end<0 ? 0 : computed[end]) << " ";
        }
        cout << endl;
    }
}
