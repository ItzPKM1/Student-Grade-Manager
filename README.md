# Student Grade Manager

A console application in C# for managing students, their courses and their grades. It was built for a Data Structures and Algorithms course at LaSalle College (Montréal). The menu and messages are in French.

The point of the project was to build the main data structures and algorithms by hand, so the student list is a hand-built doubly linked list, and the sorting and the linear search are written out in full.

New here? [Tutorial.md](Tutorial.md) walks through a first session with the program.

## What it does

- Add a student with an ID, name, email, phone number and 1 to 5 courses
- Each course has a code, title, hours, credits, three grades and a weight for each grade
- List, search, update and delete students
- Sort the students by ID
- Show the lowest and highest final grade, and the class average
- Save the students to a file and load them back later

## What it looks like

The program clears the screen between menus. It opens on a start screen, then shows the menu with a cyan "Menu:" heading and cyan option numbers, and each option opens on its own screen with its own banner. The prompt is gray and your answer is cyan. Each student is shown with a bold ID and name and a cyan final grade. Errors are shown in red and notices (such as "student not found") in yellow. After each action the program waits for a key, so the result stays on screen until you have read it.

## Menu

```
1. Ajouter un étudiant
2. Afficher les étudiants
3. Rechercher un étudiant par ID
4. Trier les étudiants par ID
5. Supprimer un étudiant
6. Trouver et afficher la note minimale et maximale
7. Afficher la moyenne de la classe
8. Sauvegarder les étudiants dans un fichier
9. Lire les étudiants du fichier
10. Modifier un étudiant
11. Quitter
```

## How the grades are calculated

A course average is the weighted sum of its three grades, so the three weights have to add up to 1 (for example 0.3, 0.3 and 0.4). A student's final grade is the average of their course averages, weighted by each course's credits. Averages and final grades are shown rounded to two decimals.

## Data structures and algorithms

| What | Where | Notes |
| --- | --- | --- |
| Doubly linked list | `ListeDoubleEtudiants.cs`, `NoeudEtudiant.cs` | Holds the students in the order they were added, or in ID order after sorting |
| Linear search | `ListeDoubleEtudiants.RechercheParId`, `GestionEtudiants.RechercheLineaireParID` | Used by the search, update and delete options |
| Hash table | `GestionEtudiants` | A `Dictionary<int, Etudiant>` used to reject duplicate IDs |
| Quicksort | `GestionEtudiants.QuickSortByID` | Written by hand, and the stored list is rebuilt in sorted order |
| Min, max, average | `GestionEtudiants` | One pass through the list |

## Project structure

| File | Purpose |
| --- | --- |
| `Program.cs` | The menu, all user input, and the screen display (clearing, colors, banners) |
| `GestionEtudiants.cs` | Ties the list and the dictionary together, plus sorting and statistics |
| `Etudiant.cs` | A student and their final grade |
| `Cours.cs` | A course, its three grades and weights |
| `ListeDoubleEtudiants.cs` | The linked list |
| `NoeudEtudiant.cs` | One node of the list |
| `FichierEtudiants.cs` | Saving to and loading from `etudiants.txt` |
| `etudiants.txt` | A sample save file with one made-up student |
| `Tutorial.md` | A walkthrough of the program |

## Running it

You need the [.NET 8 SDK](https://dotnet.microsoft.com/download). Run it from a real terminal (Terminal on macOS, Windows Terminal or PowerShell on Windows), because the program clears the screen and waits for single key presses.

```
git clone https://github.com/ItzPKM1/student-grade-checker.git
cd student-grade-checker
dotnet run
```

If you run it from an IDE console that crashes when the program waits for a key, open `Program.cs` and change `DEVELOPMENT` to `true`. In that mode the program does not clear the screen and waits for Enter instead of a key press.

## The save file

Students are saved to `etudiants.txt` in the folder you run the program from, one student per line, with the fields separated by `;`:

```
id;prenom;nom;email;telephone;nombreDeCours; then, for each course: code;titre;heures;credits;note1;poids1;note2;poids2;note3;poids3
```

A sample file with one made-up student is included. Loading adds the students to the current list and skips any ID that is already there. A line that can't be read is reported and skipped. Fields can't contain a `;`.

## History

Commits stopped after April 20 because our group stopped using Git (we ran into merge conflicts), and the rest of the work was done in separate local copies. The "Submitted version for DSA project" commit is that local code, added to the repository in one go afterwards.

After the course ended, a later commit revised the submitted code:

- Fixed a bug where a typing mistake while entering a course duplicated the earlier courses
- Fixed a bug where invalid course weights saved a student with missing courses
- IDs of 0 or below are rejected
- The program no longer loops forever when its input ends
- Sorting now reorders the stored list, and no longer prints a "sorted" message for an empty list
- Loading students from the file now works, and the save format was changed so it can be read back (see above)
- Search by ID now uses the linear search through the list (it used to look the student up in the dictionary)
- Added updating a student, email and phone validation, and credit-weighted final grades
- Averages and final grades are now rounded when shown
- "Quitter" moved to the end of the menu (it is now option 11), and `FicherEtudiants.cs` was renamed `FichierEtudiants.cs`

A further commit added the console display described above: the start screen, a banner for each option, screen clearing, red and yellow messages, the pause after each action, the 3-second goodbye and the `DEVELOPMENT` switch.

Another commit changed the main menu and the student display to look like the Student Grade Manager card on my portfolio website: a plain cyan "Menu:" heading and numbers, a gray prompt with a cyan answer, a cyan student count, a bold ID line and a cyan final grade.

## Limitations

- Students can only be sorted by ID, not by name or grade
- The hash table is .NET's `Dictionary`, not one written for this project
- Everything is kept in memory, so changes are lost unless you save
- The banners use block and box-drawing characters, so they need a terminal that supports UTF-8 to display correctly

## Acknowledgements

The console display (clearing the screen, colors, ASCII banners, the `DEVELOPMENT` switch and the "Continuer..." pause) was inspired by Shayan Delbari's [factoid project](https://github.com/shayandelbari/factoid-project), which Shayan suggested as a model.

## Authors

Group project by Akshay Kheterpal and Mime Uddin Reba. Revised afterwards by Akshay Kheterpal.
