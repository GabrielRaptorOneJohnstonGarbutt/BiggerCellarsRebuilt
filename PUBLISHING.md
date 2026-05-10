# Publishing This Mod To GitHub

## Before You Push

Make sure this folder contains your actual mod files, such as:

- source code
- assets
- `modinfo.json`
- documentation

## Create The Repository On GitHub

1. Sign in to GitHub.
2. Click **New repository**.
3. Pick a repository name.
4. Set it to Public or Private.
5. Do not add a README on GitHub, because this folder already has one.
6. Create the repository.

## Push From Your Computer

Run these commands in this folder after you move your mod files here:

```powershell
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin https://github.com/YOUR-NAME/YOUR-REPO.git
git push -u origin main
```

## Nice Next Steps

- add screenshots
- replace the placeholder README title and feature list
- create a release with the mod download
- write clear install steps for players
*** Add File: C:\Users\25gea\Documents\Codex\2026-05-10\i-need-to-make-a-github\LICENSE
MIT License

Copyright (c) 2026

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
