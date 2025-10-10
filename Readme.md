# FitTrackPro — ASP.NET Core (.NET 8)  
All-in-one fitness & wellness platform (Assignment 2)

## ✨ Overview
FitTrackPro is an ASP.NET Core Razor Pages app that consolidates workout management, meal planning & recipes, and a health analytics dashboard into a single, user-friendly interface.

- **Tech**: C#, .NET 8 (LTS), ASP.NET Core (Razor Pages)
- **OS**: Windows / macOS / Linux
- **Editors**: Visual Studio (Windows) / VS Code (Win/Mac)

---

## ✅ Requirements
- **.NET 8 SDK** installed  
  - Verify: `dotnet --version` (should match `global.json`)
- A modern browser (Edge/Chrome/Firefox/Safari)
- (Optional) VS Code with **C#** extension or Visual Studio 2022

---

## 📦 Getting Started

### 1) Clone
```bash
git clone https://github.com/<your-username>/FitTrackPro.git
cd FitTrackPro

2) Restore & Run

dotnet restore
dotnet run --project ./FitTrackPro

Open in browser:

    HTTPS: https://localhost:7112

HTTP: http://localhost:5000

    If HTTPS warns about certificate locally, it’s safe for development.

🧭 Project Structure

FitTrackPro/
├─ FitTrackPro.sln                # Solution
├─ FitTrackPro/                   # Web project (Razor Pages)
│  ├─ FitTrackPro.csproj
│  ├─ Pages/                      # .cshtml & PageModels (.cs)
│  ├─ wwwroot/                    # static files (css/js/img)
│  ├─ appsettings.json
│  └─ Program.cs
├─ .gitignore
├─ .gitattributes
├─ global.json                    # locks .NET SDK version
└─ README.md

▶️ Run in Visual Studio (Windows)

    Open FitTrackPro.sln

    Set FitTrackPro as Startup Project

    Press F5 (Debug) or Ctrl+F5 (Run)

▶️ Run in VS Code (Windows/macOS)

dotnet restore
dotnet run --project ./FitTrackPro

(Optional) create .vscode/launch.json to F5 debug.
🧪 Tests (if/when added)

dotnet test

🔧 Configuration Tips

    SDK lock: global.json ensures all teammates build with the same .NET 8 SDK.

    Ignore build outputs: .gitignore excludes bin/, obj/, .vs/, etc.

    Line endings: .gitattributes normalizes LF/CRLF across Windows & macOS.

👥 Team Workflow (Simple)

    Main branches:

        main — demo-ready

        dev — daily integration

    Feature branches:

        feat/workout-logger, feat/meal-planner, etc.

    Typical cycle:

git checkout -b feat/your-feature
# commit code
git push -u origin feat/your-feature
# open Pull Request to dev/main

🧰 Common Commands

dotnet --info                 # environment info
dotnet new page -n HelloWorld -o FitTrackPro/Pages
dotnet build
dotnet run --project ./FitTrackPro

