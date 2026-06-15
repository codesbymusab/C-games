# C-games 🎮

A small collection of games built during the early semesters of my Computer Science degree. These aren't polished products — they're a snapshot of how I learned core programming concepts: data structures, OOP, file I/O, console graphics, and eventually a full desktop GUI application.

Each folder is a self-contained project from a different point in that journey.

## Projects at a glance

| Project | Language | Type | What it's really about |
|---|---|---|---|
| [2048](./2048) | C++ | Graphical (cppgraphics) | 2D arrays, merge logic, file-based save/score system |
| [Solitaire](./Solitaire) | C++ | Console | Hand-rolled doubly linked list & stack (templates) instead of STL |
| [Flappy Copter](./Flappy%20Copter) | C# (.NET 8, WinForms) | Desktop GUI | Event-driven UI, custom physics, multi-screen navigation, audio |

> **Note:** All three projects target Windows (they rely on `windows.h`, the Windows Console API, or Windows Forms), so they're best built and run on a Windows machine with Visual Studio.

---

## 🧩 2048

A from-scratch implementation of the classic sliding-tile puzzle, rendered with the lightweight `cppgraphics` library.

**Features**
- 4x4 grid with row/column merging and scoring logic
- Save/load game state (`saves.txt`) and high scores (`scores.txt`, `save score.txt`)
- Simple tile rendering and a basic game loop using `cppgraphics`

**Build & run**
```bash
# 1. Install/configure the cppgraphics library
#    (copy its resource files to your Public Documents folder)

# 2. Compile
g++ main.cpp -o Cgames

# 3. Run
./Cgames
```

---

## 🃏 Solitaire

A terminal-based Klondike-style solitaire game, written to practice data structures and console I/O.

**Features**
- Custom-built generic `List` (doubly linked list) and `Stack`, written from scratch instead of using `std::list` / `std::stack`
- Colored suit symbols (♥ ♦ ♣ ♠) rendered via the Windows Console API
- Command-based gameplay: draw cards, move cards between piles, and undo previous moves
- Card/Game/Command classes separate display, state, and input handling

**Build & run**
- Open `Solitaire.sln` in Visual Studio and run, **or**
```bash
g++ Solitaire.cpp -o Solitaire
./Solitaire
```

---

## 🚁 Flappy Copter

A Flappy Bird–style desktop game built with .NET 8 and Windows Forms — the most "complete" project in this set, with a real UI flow instead of a single game loop.

**Features**
- Multiple screens with smooth navigation: Splash → Menu → Difficulty Select → Playing → Pause/Game Over → High Scores
- Easy/Medium/Hard difficulty modes
- Custom `Player`, `Obstacle`, and `Heart` models (extending `PictureBox`) with gravity, flap velocity, and collision behavior
- Sound effects and menu music via **NAudio**
- Material-style UI components via **MaterialSkin.2**
- Persisted high scores (`scores.txt`)

**Build & run**
- Open `Flappy Copter.sln` in Visual Studio (2022+, with .NET 8 SDK) and run, **or**
```bash
cd "Flappy Copter"
dotnet run
```

---

## Repository structure
```
C-games/
├── 2048/            # C++ console/graphical game
├── Solitaire/       # C++ console game with custom data structures
└── Flappy Copter/   # C# WinForms desktop game
```

<img width="799" height="799" alt="20483" src="https://github.com/user-attachments/assets/f6ac8c2a-98a5-43d8-a112-1fb03aefc572" />
<img width="1045" height="611" alt="Screenshot 2025-10-12 204113" src="https://github.com/user-attachments/assets/204d32f3-6e8b-4ebc-b5ff-233ad350d6f7" />


## Why this repo exists

This is mainly an archive — a record of early projects from when I was first getting comfortable with C++ and C#. Looking back, the jump from "raw arrays and a graphics header" (2048) to "hand-built linked lists" (Solitaire) to "a full event-driven desktop app with audio and multiple screens" (Flappy Copter) is a decent picture of what those first couple of semesters looked like.

## License

MIT — feel free to use any of this for learning purposes.
