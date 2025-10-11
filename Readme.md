# FitTrackPro — ASP.NET Core (.NET 8) 🏋️‍♂️


<br>
<br>
<br>


## ✨ Overview

FitTrackPro is an ASP.NET Core Razor Pages app that consolidates workout management, meal planning & recipes, and a health analytics dashboard into a single, user-friendly interface.

-   **Tech**: C#, .NET 8 (LTS), ASP.NET Core (Razor Pages)
-   **OS**: Windows / macOS / Linux
-   **Editors**: Visual Studio (Windows) / VS Code (Win/Mac)

<br>
<br>

## ✅ Requirements

-   **.NET 8 SDK** must be installed.
    -   Verify with `dotnet --version` (this should match the version in `global.json`).
-   A **modern browser** (Edge/Chrome/Firefox/Safari).
-   (Optional) **Visual Studio 2022** or **VS Code** with the C# extension.

<br>
<br>

## 🚀 Getting Started

### 1. Clone the Repository

```shell
git clone https://github.com/KIRAK26/FitTrackPro
cd FitTrackPro
```


### 2. Restore Dependencies & Run the App

```shell
dotnet restore
dotnet run --project ./FitTrackPro
```

The application will be running at:
-   **HTTPS**: `https://localhost:7178`




<br>



## 🔧 Local Development Setup
1.Install EF Core Tools (One-Time Setup)
This project uses Entity Framework Core for its database. You must install the specific command-line tools version that matches our project's SDK. You only need to do this once on your machine.

```shell
dotnet tool install --global dotnet-ef --version 8.0.0
```


2.Restore Project Dependencies
This command reads the .csproj file and downloads all the necessary packages defined for the project.


```shell
dotnet restore
```

3.Create the Local Database
This command finds the migration files that exist in the repository and runs them to create the fittrack.db database file on your local machine. You do not need to create migrations, only apply them.


```shell
dotnet ef database update --project FitTrackPro
```

After these steps, your local environment will be fully configured and ready. You can now run the application.


## 🧭 Project Structure

The repository is structured to separate concerns and maintain clarity.

```
FitTrackPro/
├─ FitTrackPro.sln          # Visual Studio Solution File
├─ FitTrackPro/             # Main Web Project (Razor Pages)
│  ├─ FitTrackPro.csproj
│  ├─ Pages/               # Razor Pages (.cshtml) and their PageModels (.cs)
│  ├─ wwwroot/             # Static assets (CSS, JavaScript, images)
│  ├─ appsettings.json
│  └─ Program.cs
├─ .gitignore               # Specifies intentionally untracked files to ignore
├─ .gitattributes           # Defines attributes per path
├─ global.json              # Locks the .NET SDK version for consistency
└─ README.md                # You are here!
```
<br>
<br>


## ▶️ Running the Application

### Using Visual Studio 2022 (Windows)
1.  Open the `FitTrackPro.sln` file.
2.  Set `FitTrackPro` as the startup project.
3.  Press **F5** (to debug) or **Ctrl+F5** (to run without debugging).

### Using VS Code or Terminal (Windows/macOS/Linux)
1. Open the project folder in your terminal.
2. Run the following command:
   ```shell
   dotnet run --project ./FitTrackPro
   ```



## 🔧 Development Workflow

### Key Configuration Files
-   **`global.json`**: Ensures all teammates build with the exact same .NET 8 SDK, preventing "works on my machine" issues.
-   **`.gitignore`**: Excludes temporary build files (`bin/`, `obj/`), user-specific settings (`.vs/`), and secrets from source control.
-   **`.gitattributes`**: Normalizes line endings (`LF`/`CRLF`) across different operating systems (Windows, macOS) to prevent formatting conflicts.

### Git Workflow
The project uses a simple feature-branch workflow.

1.  **Create a new feature branch** from `dev`:
    ```shell
    git checkout dev
    git pull
    git checkout -b feat/your-cool-feature
    ```

2.  **Commit your changes** and **push the branch**:
    ```shell
    # ...do your work and commit...
    git push -u origin feat/your-cool-feature
    ```

3.  **Open a Pull Request** on GitHub from `feat/your-cool-feature` to `dev`.

<br>
<br>

## 🧰 Common Commands

Here are some useful `dotnet` commands for this project.

| Command                                      | Description                                            |
| -------------------------------------------- | ------------------------------------------------------ |
| `dotnet run --project ./FitTrackPro`         | Runs the web application.                              |
| `dotnet build`                               | Compiles the project without running it.               |
| `dotnet watch --project ./FitTrackPro`       | Runs the app and auto-reloads on file changes.         |
| `dotnet new page -n NewPage -o FitTrackPro/Pages` | Creates a new Razor Page named "NewPage".              |
| `dotnet --info`                              | Displays detailed .NET environment information.        |
