import os
import sys
import shutil
import argparse

parser = argparse.ArgumentParser()

parser.add_argument('site', choices=['cc', 'ac', 'cf', 'lc', 'random'])
parser.add_argument('name')
parser.add_argument('-l', '--lang', default='cs', choices=['cs', 'cpp'])
parser.add_argument('-a', '--amount', type=int, default=6)

args = parser.parse_args()

locs = {'cc': 'codechef', 'ac': 'atcoder', 'cf': 'codeforces', 'lc': 'leetcode', 'random': 'random'}

dir = os.path.join(locs[args.site], args.name)

os.makedirs(dir, exist_ok=True)

t_folder = ""
template = ""
suffix = ""
if args.lang == 'cpp':
    t_folder = "cpp_helper"
    template = "template.cpp"
    suffix = "cpp"
elif args.lang == 'cs':
    t_folder = "cs_helper"
    suffix = "cs"
    template = "DotnetCoreTemplate.cs"
    if args.site == "cc":
        template = "DotnetFrameworkTemplate.cs"

def cf(file):
    path = os.path.join(dir, file)
    with open(path, "w", encoding="utf-8") as f:
        f.write("")

cf("in.txt")
cf("out.txt")

sp = os.path.join(t_folder, template) 
for i in range(args.amount):
    num = chr(ord('A') + i) 
    target = os.path.join(dir, f'{num}.{suffix}')
    shutil.copy(sp, target)

if suffix == 'cs':
    shutil.copy(os.path.join(t_folder, '.editorconfig'), os.path.join(dir, '.editorconfig'))