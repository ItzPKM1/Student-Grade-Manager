# Tutorial: a first session

This walks through the Student Grade Manager from start to finish, using the sample file that comes with the project. The program is in French, so the prompts below are in French too.

## 1. Start the program

You need the [.NET 8 SDK](https://dotnet.microsoft.com/download). In a terminal, from the project folder:

```
dotnet run
```

You'll see a start screen. Press any key to reach the menu. The program clears the screen between menus, so each step below starts on a fresh screen, and after each action it waits for a key so you can read the result.

## 2. Load the sample student

Type `9` and press Enter ("Lire les étudiants du fichier"). The program reads `etudiants.txt`, which has one made-up student. It confirms each student it adds, then prints how many it loaded:

```
Étudiant ajouté avec succès.
Nombre d'étudiants chargés: 1
```

Loading adds students to the current list. If you load the same file twice, the second time every ID is already there, so those lines are skipped and the program tells you.

## 3. Look at the list

Type `2` ("Afficher les étudiants"). You get the student's details, their courses and their final grade:

```
Nombre d'étudiants: 1
ID: 1042 - Alex Martin
   Email:     alex.martin@example.com
   Téléphone: 514-555-0100
   Cours:
      Code        Titre            Heures  Crédits  Notes             Moyenne
      402-AA      Math             50      3        80, 84, 82             82
      402-AB      Français         50      3        74, 82, 78             78
      402-AC      Programmation    60      3        90, 92, 91             91
   Note finale:                                                         83.67
─────────────────────────────────────────────────────────────────────────────
```

Here is where those numbers come from. Each course has three grades with a weight each, and the weights add up to 1. For Math the weights are 0.3, 0.3 and 0.4, so the average is 80 × 0.3 + 84 × 0.3 + 82 × 0.4 = 82. The final grade is the average of the three course averages, weighted by credits. All three courses are worth 3 credits here, so it is (82 + 78 + 91) ÷ 3 = 83.67.

## 4. Add a student

Type `1` ("Ajouter un étudiant"). The program asks for these in order:

1. The ID (a whole number above 0 that no one else has)
2. First name and last name
3. Email and phone number
4. How many courses (1 to 5)
5. For each course: the code, the title, the hours and the credits
6. For each of the three grades: the grade, then its weight as a decimal (for example `0.5`)

If you type something the program can't accept, it shows the problem in red and asks again where it can. If a course's weights don't add up to 1, the program tells you and asks for that whole course again, from its code onward.

## 5. Search, update and delete

- `3` searches by ID and shows the student's name, or tells you in yellow that no student was found.
- `10` updates a student. After you enter an ID, you choose what to change: first name, last name, email, phone number, or the grades of one course. The final grade is recalculated when you change grades.
- `5` deletes a student by ID.

## 6. Statistics and sorting

- `6` shows the lowest and highest final grade in the class.
- `7` shows the class average.
- `4` sorts the students by ID and prints the sorted list. Option `2` shows the new order afterwards too.

With no students loaded, these options tell you so in yellow instead of showing numbers.

## 7. Save and quit

Type `8` to save everyone to `etudiants.txt`. If the list is empty, the program does nothing and tells you, so it never wipes an existing file with an empty one. Type `11` to quit. The goodbye message stays on screen for 3 seconds.

Changes you don't save are lost when you quit.

## What the program checks

| Field | Rule |
| --- | --- |
| ID | A whole number above 0, not used by another student |
| First and last name | Can't be empty |
| Email | Needs something before an `@`, a `.` after it with at least one character in between, no spaces, and can't end with a `.` |
| Phone number | 10 to 15 digits; spaces, `-`, `(`, `)` and `+` are allowed, other characters are not |
| Number of courses | 1 to 5 |
| Course weights | The three weights must add up to 1 (a difference smaller than 0.0001 is accepted) |

## The save file

`etudiants.txt` is plain text, one student per line, with fields separated by `;`. A line starts with the student's ID, names, email, phone number and number of courses, followed by 10 fields for each course: code, title, hours, credits, then each grade with its weight. The program looks for the file in the folder you run it from, so if you see "Aucun fichier trouvé", you are probably in a different folder.

You can edit the file by hand, but a field can't contain a `;`. A line the program can't read is skipped and reported.

## If something goes wrong

| Problem | What to do |
| --- | --- |
| The program crashes when it waits for a key | You're probably in an IDE console. Open `Program.cs` and change `DEVELOPMENT` to `true`. The program will then wait for Enter and stop clearing the screen. |
| The banners show `?` or odd symbols | Use a terminal that supports UTF-8, such as Terminal on macOS or Windows Terminal. |
| "Aucun fichier trouvé" | Run the program from the folder that contains `etudiants.txt`. |
| A student you loaded doesn't appear | Their ID was probably already in the list, so the line was skipped. |

## If you change the code

- Rebuild and run with `dotnet run`. The `bin` and `obj` folders are ignored by Git, so they won't end up in a commit.
- Run `git pull` before you start editing, so you begin from the latest version.
- Add files by name (`git add Program.cs`) instead of `git add .`, so a stray file can't slip into a commit. Check `git status` before you commit.
- The README's Project structure section lists which file does what.
