# BattleshipLite

A simple, console-based implementation of the classic Battleship game in C#.

## 📝 Description

BattleshipLite is a two-player console application where each player places ships on a grid and takes turns guessing the location of the opponent’s ships. The first player to sink all enemy ships wins!

## 🎯 Core Learning Concepts

- **Object-Oriented Programming (OOP):** Use of classes and models to represent players, ships, and the game grid.
- **Collections:** Managing lists for ship locations and shots.
- **Input Validation:** Ensuring valid user input for ship placement and shots.
- **Game Logic:** Turn-based play, win condition checks, and result display.
- **Separation of Concerns:** Dividing responsibilities between the UI (console) and game logic layers.

## 📸 Screenshot

![Game Screenshot](screenshot.jpg)


## 🚀 Getting Started

### Requirements
- .NET Framework 4.7.2 or compatible (for Visual Studio)

### Build and Run

#### Using Visual Studio (Recommended)
1. Open the solution `BattleshipLiteApp.sln` in Visual Studio.
2. Build the solution.
3. Run the `BattleshipLite` project.

#### Using Command Line (Windows with .NET Framework)
This project targets .NET Framework 4.7.2 and is not directly compatible with `dotnet run` on macOS/Linux. To run from the command line, use MSBuild and run the generated `.exe` on Windows.

```
msbuild BattleshipLiteApp.sln
cd BattleshipLite/bin/Debug/
BattleshipLite.exe
```

*Note: To use `dotnet run`, the project must target .NET Core or .NET 5+.*

---

Let me know if you want to add installation instructions, usage examples, or more details!