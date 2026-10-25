namespace DSA_Projet_VérificateurDeNotesÉtudiants;

class Program
{
    // interrupteur vrai/faux
    // false = mode normal (on efface l'écran et on attend une touche), true = on n'efface pas et on attend Enter
    // (pratique si la console de l'IDE plante avec ReadKey)
    public static readonly bool DEVELOPMENT = false;

    public static void Main(string[] args)
    {
        GestionEtudiants gestion = new GestionEtudiants();
        bool quitter = false;

        PageAccueil(); // montre l'écran de départ une seule fois avant la boucle du menu

        while (!quitter)
        {
            if (!DEVELOPMENT) Console.Clear(); // ! veut dire "pas": si on est pas en mode DEVELOPMENT, on efface le terminal, donc le menu s'affiche sur un écran vide à chaque fois
            Console.ForegroundColor = ConsoleColor.Cyan; // le titre "Menu:" en cyan
            Console.WriteLine("Menu:");
            Console.ResetColor(); // remet la couleur normale pour la liste des options
            Console.WriteLine("");
            ItemMenu("1", "Ajouter un étudiant"); // écrit le numéro en cyan et le texte en couleur normale, voir ItemMenu plus bas
            Console.WriteLine("");
            ItemMenu("2", "Afficher les étudiants");
            Console.WriteLine("");
            ItemMenu("3", "Rechercher un étudiant par ID");
            Console.WriteLine("");
            ItemMenu("4", "Trier les étudiants par ID");
            Console.WriteLine("");
            ItemMenu("5", "Supprimer un étudiant");
            Console.WriteLine("");
            ItemMenu("6", "Trouver et afficher la note minimale et maximale");
            Console.WriteLine("");
            ItemMenu("7", "Afficher la moyenne de la classe");
            Console.WriteLine("");
            ItemMenu("8", "Sauvegarder les étudiants dans un fichier");
            Console.WriteLine("");
            ItemMenu("9", "Lire les étudiants du fichier");
            Console.WriteLine("");
            ItemMenu("10", "Modifier un étudiant");
            Console.WriteLine("");
            ItemMenu("11", "Quitter");
            Console.ForegroundColor = ConsoleColor.DarkGray; // la question en gris
            Console.WriteLine("");
            Console.Write("Choisissez une option: ");
            Console.ForegroundColor = ConsoleColor.Cyan; // ce que l'utilisateur tape s'affiche en cyan
            string choix = Lire();
            Console.ResetColor(); // remet la couleur normale après la réponse
            if (!DEVELOPMENT) Console.Clear(); // efface le menu pour que l'option choisie ait un écran propre

            switch (choix)
            {
                case "1":
                    AfficherBanniere(banniereAjouter);
                    //Code pour ajouter un étudiant
                    // Main faire le création de l'étudiant à partir de l'input des entrées 
                    //et envoi à la classe gestion.
                    AjouterEtudiant(gestion);
                    break;

                case "2":
                    gestion.afficherEtudiants();
                    break;

                case "3":
                    AfficherBanniere(banniereRecherche); // voir case 1
                    //Code pour rechercher un étudiant par ID
                    try
                    {
                        Console.Write("Entrez l'ID de l'étudiant à rechercher: ");
                        int id = int.Parse(Lire());
                        Etudiant eCase3 = gestion.RechercheLineaireParID(id);

                        if (eCase3 != null)
                        {
                            Console.WriteLine($"Étudiant trouvé: {eCase3.getPrenom()} {eCase3.getNom()}");
                        }
                        else
                        {
                            Program.Avertissement("Étudiant non trouvé."); // même message qu'avant mais en jaune; Program. parce que la méthode est dans la classe Program
                        }
                    }
                    //catch (quelque chose qui interceptera si le user entrer quelque chose invalide pour l'ID)
                    catch
                    {
                        Program.Erreur("ID invalide. Veuillez entrer un nombre entier."); // même message qu'avant mais en rouge; Program. parce que la méthode est dans la classe Program
                    }

                    break;

                case "4":
                    AfficherBanniere(banniereTri); // voir case 1
                    //Code pour trier les étudiants par ID
                    gestion.QuickSortByID();
                    if (gestion.getHead() != null)
                    {
                        Console.WriteLine("Étudiants triés par ID.");
                    }
                    break;

                case "5":
                    AfficherBanniere(banniereSupprimer); // voir case 1
                    //Code pour supprimer un étudiant
                    try // intercepter les erreurs si le user entrer quelque chose invalide pour l'ID
                    {
                        Console.Write("Entrez l'ID de l'étudiant à supprimer: ");
                        int idSupprimer = int.Parse(Lire());
                        gestion.supprimerEtudiant(idSupprimer);
                    }

                    catch
                    {
                        Program.Erreur("ID invalide. Veuillez entrer un nombre entier.");
                    }
                    break;

                case "6":
                    AfficherBanniere(banniereMinMax); // voir case 1
                    //Code pour trouver et afficher la note minimale et maximale
                    try
                    {
                        double[] resultats = gestion.FindMinMax();
                        Console.WriteLine($"Note minimale: {Math.Round(resultats[0], 2)}, Note maximale: {Math.Round(resultats[1], 2)}");
                    }

                    catch
                    {
                        Program.Avertissement("Il n'y a pas d'étudiants dans la classe pour trouver les notes minimale et maximale.");
                    }
                    break;

                case "7":
                    AfficherBanniere(banniereMoyenne); // voir case 1
                    // code pour afficher les statistiques
                    try
                    {
                        Console.WriteLine("Moyenne de la classe: " + Math.Round(gestion.CalculMoyenneClasse(), 2));
                    }
                    catch
                    {
                        Program.Avertissement("Il n'y a pas d'étudiants dans la classe pour calculer la moyenne.");
                    }

                    break;

                case "10":
                    AfficherBanniere(banniereModifier); // voir case 1
                    ModifierEtudiant(gestion);
                    break;

                case "11":
                    quitter = true;
                    Console.WriteLine("Quitter le programme. Au revoir!");
                    int millisecondes = 3000; // 3000 millisecondes = 3 secondes
                    Thread.Sleep(millisecondes); // gèle le programme pendant ce temps pour que le message d'au revoir reste à l'écran
                    if (!DEVELOPMENT) Console.Clear(); // et efface l'écran à la fin
                    break;
                case "8":
                    AfficherBanniere(banniereSauvegarde); // voir case 1
                    FichierEtudiants.SaveEtudiantsToFile(gestion);
                    break;

                case "9":
                    AfficherBanniere(banniereLecture); // voir case 1
                    FichierEtudiants.ReadEtudiantsFromFile(gestion);
                    break;

                default:
                    Program.Erreur("Option invalide, veuillez réessayer.");
                    break;
            }

            if (!quitter) // pas de pause si l'utilisateur quitte
            {
                Console.Write("\nContinuer... "); // \n = ligne vide avant; Write (pas WriteLine) pour que le curseur reste sur la même ligne
                if (!DEVELOPMENT) Console.ReadKey(false); else Lire(); // attend n'importe quelle touche (false = la touche s'affiche), ou Enter en mode DEVELOPMENT; sans cette pause le Clear du prochain tour efface le résultat avant qu'on le lise
            }
        }
    }

    static void AjouterEtudiant(GestionEtudiants gestion)
    {
        Etudiant e = new Etudiant();
        bool idValide = false;
        while (!idValide)
        {
            try
            {
                Console.Write("Entrez l'ID de l'étudiant: ");
                int id = int.Parse(Lire());

                if (id <= 0)
                {
                    Program.Erreur("L'ID doit être un nombre positif. Veuillez réessayer.");
                }
                else if (gestion.idExiste(id)) // fonction pour vérifier si l'ID existe déjà dans le tableau de hachage.
                {
                    Program.Erreur("Un étudiant avec cet ID existe déjà. Veuillez entrer un ID différent.");
                }
                else
                {
                    e.setId(id);
                    idValide = true;
                }
            }
            catch
            {
                Program.Erreur("Entrée invalide. Veuillez réessayer.");
            }
        }

        Console.Write("Entrez le prénom de l'étudiant: ");
        string prenom = Lire();
        if (string.IsNullOrWhiteSpace(prenom))
        {
            Program.Erreur("Le prénom ne peut pas être vide. Veuillez réessayer.");
            return;
        }
        e.setPrenom(prenom);

        Console.Write("Entrez le nom de l'étudiant: ");
        string nom = Lire();
        if (string.IsNullOrWhiteSpace(nom))
        {
            Program.Erreur("Le nom ne peut pas être vide. Veuillez réessayer.");
            return;
        }
        e.setNom(nom);

        //nouveau email
        Console.WriteLine("Entrez le email de l'étudiant: ");
        string email = Lire();
        if (string.IsNullOrWhiteSpace(email))
        {
            Program.Erreur("Le email ne peut pas être vide. Veuillez réessayer.");
            return;
        }
        while (!EmailValide(email))
        {
            Program.Erreur("Email invalide. Veuillez entrer un email valide (ex: nom@exemple.com).");
            email = Lire();
        }

        e.setEmail(email);

        //ajout de num de téléphone
        Console.WriteLine("Entrez le numéro de téléphone de l'étudiant");
        string numeroDeTéléphone = Lire();
        if (string.IsNullOrWhiteSpace(numeroDeTéléphone))
        {
            Program.Erreur("Le numéro de téléphone ne peut pas être vide. Veuillez réessayer.");
            return;
        }
        while (!TelephoneValide(numeroDeTéléphone))
        {
            Program.Erreur("Numéro de téléphone invalide. Il doit contenir entre 10 et 15 chiffres (ex: 514-555-1234).");
            numeroDeTéléphone = Lire();
        }

        e.setNumeroDeTelephone(numeroDeTéléphone);

        bool nombreCoursValide = false;

        while (!nombreCoursValide)
        {
            try
            {
                Console.Write("Entrez le nombre de cours de l'étudiant (1 à 5): ");
                int nombreCours = int.Parse(Lire());

                if (nombreCours < Etudiant.minCours || nombreCours > Etudiant.maxCours)
                {
                    Program.Erreur("Le nombre de cours doit être entre 1 et 5.");
                }
                else
                {
                    for (int c = 0; c < nombreCours; c++)
                    {
                        Console.WriteLine($"\n--- Cours {c + 1} ---");

                        bool coursAjoute = false;

                        while (!coursAjoute)
                        {
                            try
                            {
                                Console.Write("Entrez le code du cours: ");
                                string codeCours = Lire();

                                Console.Write("Entrez le titre du cours: ");
                                string titre = Lire();

                                Console.Write("Entrez le nombre d'heures du cours: ");
                                int nombreHeures = int.Parse(Lire());

                                Console.Write("Entrez le nombre de crédits du cours: ");
                                int nombreCredits = int.Parse(Lire());

                                Cours cours = new Cours(codeCours, titre, nombreHeures, nombreCredits);

                                Console.WriteLine("Notes de cours (3 notes): ");

                                for (int i = 0; i < 3; i++)
                                {
                                    bool noteValide = false;
                                    while (!noteValide)
                                    {
                                        try
                                        {
                                            Console.Write($"Entrez la note {i + 1} : ");
                                            cours.setNote(i, double.Parse(Lire()));
                                            noteValide = true;
                                        }
                                        catch
                                        {
                                            Program.Erreur("Entrée invalide. Veuillez entrer un nombre valide pour la note.");
                                        }
                                    }

                                    bool ponderationValide = false;
                                    while (!ponderationValide)
                                    {
                                        try
                                        {
                                            Console.Write($"Entrez la pondération de la note {i + 1} en décimal (ex: 0.5) : ");
                                            double ponderation = double.Parse(Lire());
                                            cours.setPonderation(i, ponderation);
                                            ponderationValide = true;
                                        }
                                        catch
                                        {
                                            Program.Erreur("Entrée invalide. Veuillez entrer un nombre décimal pour la pondération.");
                                        }
                                    }
                                }

                                if (cours.verifierPonderations())
                                {
                                    cours.CalculMoyenneDeCours();
                                    e.ajouterCours(cours);
                                    coursAjoute = true;
                                }
                                else
                                {
                                    Program.Erreur("La somme des pondérations pour le cours doit être égale à 1. Réessayer.");
                                }
                            }
                            catch
                            {
                                Program.Erreur("Entrée invalide. Veuillez réessayer.");
                            }
                        }
                    }

                    if (e.verifierNombreCours())
                    {
                        e.CalculNoteFinal();
                        gestion.ajouterEtudiant(e);
                    }
                    else
                    {
                        Program.Erreur("L'étudiant doit avoir au moins un cours valide.");
                    }

                    nombreCoursValide = true;
                }
            }
            catch
            {
                Program.Erreur("Entrée invalide. Veuillez réessayer.");
            }
        }
    }

    static void ModifierEtudiant(GestionEtudiants gestion)
    {
        int id;

        try
        {
            Console.Write("Entrez l'ID de l'étudiant à modifier: ");
            id = int.Parse(Lire());
        }
        catch
        {
            Program.Erreur("ID invalide. Veuillez entrer un nombre entier.");
            return;
        }

        Etudiant e = gestion.RechercheLineaireParID(id);

        if (e == null)
        {
            Program.Avertissement("Étudiant non trouvé.");
            return;
        }

        Console.WriteLine("Que voulez-vous modifier?");
        Console.WriteLine("1. Le prénom");
        Console.WriteLine("2. Le nom");
        Console.WriteLine("3. Le email");
        Console.WriteLine("4. Le numéro de téléphone");
        Console.WriteLine("5. Les notes d'un cours");
        Console.Write("Choisissez une option: ");

        string choix = Lire();

        switch (choix)
        {
            case "1":
                Console.Write("Entrez le nouveau prénom: ");
                string prenom = Lire();
                if (string.IsNullOrWhiteSpace(prenom))
                {
                    Program.Erreur("Le prénom ne peut pas être vide.");
                    break;
                }
                e.setPrenom(prenom);
                Console.WriteLine("Étudiant modifié avec succès.");
                break;

            case "2":
                Console.Write("Entrez le nouveau nom: ");
                string nom = Lire();
                if (string.IsNullOrWhiteSpace(nom))
                {
                    Program.Erreur("Le nom ne peut pas être vide.");
                    break;
                }
                e.setNom(nom);
                Console.WriteLine("Étudiant modifié avec succès.");
                break;

            case "3":
                Console.Write("Entrez le nouveau email: ");
                string email = Lire();
                while (!EmailValide(email))
                {
                    Program.Erreur("Email invalide. Veuillez entrer un email valide (ex: nom@exemple.com).");
                    email = Lire();
                }
                e.setEmail(email);
                Console.WriteLine("Étudiant modifié avec succès.");
                break;

            case "4":
                Console.Write("Entrez le nouveau numéro de téléphone: ");
                string numeroDeTéléphone = Lire();
                while (!TelephoneValide(numeroDeTéléphone))
                {
                    Program.Erreur("Numéro de téléphone invalide. Il doit contenir entre 10 et 15 chiffres (ex: 514-555-1234).");
                    numeroDeTéléphone = Lire();
                }
                e.setNumeroDeTelephone(numeroDeTéléphone);
                Console.WriteLine("Étudiant modifié avec succès.");
                break;

            case "5":
                Cours[] listeCours = e.getCours();

                for (int c = 0; c < e.getNombreCours(); c++)
                {
                    Console.WriteLine((c + 1) + ". " + listeCours[c].getCodeDeCours() + " - " + listeCours[c].getTitre());
                }

                Console.Write("Entrez le numéro du cours à modifier: ");
                int numero;

                try
                {
                    numero = int.Parse(Lire());
                }
                catch
                {
                    numero = 0;
                }

                if (numero < 1 || numero > e.getNombreCours())
                {
                    Program.Erreur("Numéro de cours invalide.");
                    break;
                }

                bool notesModifiees = false;

                while (!notesModifiees)
                {
                    Cours ancien = listeCours[numero - 1];
                    Cours nouveau = new Cours(ancien.getCodeDeCours(), ancien.getTitre(), ancien.getNombreHeures(), ancien.getNombreCredits());

                    Console.WriteLine("Notes de cours (3 notes): ");

                    for (int i = 0; i < 3; i++)
                    {
                        bool noteValide = false;
                        while (!noteValide)
                        {
                            try
                            {
                                Console.Write($"Entrez la nouvelle note {i + 1} : ");
                                nouveau.setNote(i, double.Parse(Lire()));
                                noteValide = true;
                            }
                            catch
                            {
                                Program.Erreur("Entrée invalide. Veuillez entrer un nombre valide pour la note.");
                            }
                        }

                        bool ponderationValide = false;
                        while (!ponderationValide)
                        {
                            try
                            {
                                Console.Write($"Entrez la nouvelle pondération de la note {i + 1} en décimal (ex: 0.5) : ");
                                double ponderation = double.Parse(Lire());
                                nouveau.setPonderation(i, ponderation);
                                ponderationValide = true;
                            }
                            catch
                            {
                                Program.Erreur("Entrée invalide. Veuillez entrer un nombre décimal pour la pondération.");
                            }
                        }
                    }

                    if (nouveau.verifierPonderations())
                    {
                        nouveau.CalculMoyenneDeCours();
                        e.remplacerCours(numero - 1, nouveau);
                        e.CalculNoteFinal();
                        notesModifiees = true;
                        Console.WriteLine("Étudiant modifié avec succès.");
                    }
                    else
                    {
                        Program.Erreur("La somme des pondérations pour le cours doit être égale à 1. Réessayer.");
                    }
                }
                break;

            default:
                Program.Erreur("Option invalide, veuillez réessayer.");
                break;
        }
    }

    static bool EmailValide(string email)
    {
        int arobase = email.IndexOf('@');

        return arobase > 0 && email.LastIndexOf('.') > arobase + 1 && !email.EndsWith(".") && !email.Contains(" ");
    }

    static bool TelephoneValide(string numero)
    {
        int chiffres = 0;

        for (int i = 0; i < numero.Length; i++)
        {
            if (char.IsDigit(numero[i]))
            {
                chiffres++;
            }
            else if (numero[i] != '-' && numero[i] != ' ' && numero[i] != '(' && numero[i] != ')' && numero[i] != '+')
            {
                return false;
            }
        }

        return chiffres >= 10 && chiffres <= 15;
    }

    static void PageAccueil()
    {
        if (!DEVELOPMENT) Console.Clear(); 
        Console.ForegroundColor = ConsoleColor.Cyan; // cyan pour la grosse bannière
        Console.WriteLine(banniereAccueil); // affiche la grosse bannière "NOTES", maintenant en cyan
        Console.ResetColor(); // remet la couleur normale, sinon tout le programme reste cyan
        Console.WriteLine("\nProjet de structures de données et algorithmes\n"); // sous-titre avec une ligne vide avant et après
        Console.Write("Appuyez sur une touche pour commencer... "); // Write pour que le curseur reste sur la même ligne
        if (!DEVELOPMENT) Console.ReadKey(false); else Lire(); // attend une touche, comme la pause plus haut
    }

    static void AfficherBanniere(string banniere) // reçoit le texte de la bannière en paramètre, comme ça une seule méthode sert pour les 11 bannières
    {
        // dans le projet factoid de Shayan, il font ces 3 lignes directement dans chaque méthode, moi je les mets ici pour ne pas les répéter 11 fois
        Console.ForegroundColor = ConsoleColor.Cyan; // Console.ForegroundColor change la couleur de tout ce qui s'écrit après
        Console.WriteLine(banniere); // affiche la bannière (en jaune foncé)
        Console.ResetColor(); // sans ça tout le reste du programme reste jaune
        Console.WriteLine(); // ligne vide sous la bannière
    }

    static void ItemMenu(string numero, string texte) // reçoit le numéro et le texte séparément pour pouvoir colorer juste le numéro
    {
        Console.ForegroundColor = ConsoleColor.Cyan; // la couleur ne s'applique qu'au numéro
        Console.Write(numero + ". "); // Write (pas WriteLine) pour que le texte s'écrive sur la même ligne; les numéros restent alignés à gauche, comme sur le site
        Console.ResetColor(); // remet la couleur normale avant d'écrire le texte
        Console.WriteLine(texte); // le texte reste dans la couleur normale
    }

    public static void Erreur(string message) // public pour que les autres fichiers puissent l'utiliser, static pour l'appeler avec Program.Erreur(...) sans créer d'objet Program
    {
        // le projet factoid fait ces 3 lignes à chaque fois qu'il y a une erreur (rouge pour "question invalide"), moi j'ai ~40 messages alors c'est dans une méthode
        Console.ForegroundColor = ConsoleColor.Red; // met la couleur en rouge
        Console.WriteLine(message); // affiche le message en rouge
        Console.ResetColor(); // remet la couleur normale
    }

    public static void Avertissement(string message)
    {
        // pareil que Erreur mais en jaune
        Console.ForegroundColor = ConsoleColor.Yellow; // met la couleur en jaune
        Console.WriteLine(message); // affiche le message en jaune
        Console.ResetColor(); // remet la couleur normale
    }

    // chaque bannière est du texte ASCII dans une variable, fait avec un générateur (figlet), comme dans le projet factoid
    // le @ devant les guillemets permet d'écrire des \ sans que C# les prenne pour des caractères spéciaux (sans ça le \ casserait le string)
    static string banniereAccueil = @"███╗   ██╗ ██████╗ ████████╗███████╗███████╗
████╗  ██║██╔═══██╗╚══██╔══╝██╔════╝██╔════╝
██╔██╗ ██║██║   ██║   ██║   █████╗  ███████╗
██║╚██╗██║██║   ██║   ██║   ██╔══╝  ╚════██║
██║ ╚████║╚██████╔╝   ██║   ███████╗███████║
╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚══════╝╚══════╝";

    static string banniereAjouter = @"    _     _             _
   / \   (_) ___  _   _| |_ ___ _ __
  / _ \  | |/ _ \| | | | __/ _ \ '__|
 / ___ \ | | (_) | |_| | ||  __/ |
/_/   \_\/ |\___/ \__,_|\__\___|_|
       |__/";

    static string banniereRecherche = @" ____           _                   _
|  _ \ ___  ___| |__   ___ _ __ ___| |__   ___
| |_) / _ \/ __| '_ \ / _ \ '__/ __| '_ \ / _ \
|  _ <  __/ (__| | | |  __/ | | (__| | | |  __/
|_| \_\___|\___|_| |_|\___|_|  \___|_| |_|\___|";

    static string banniereTri = @" _____     _
|_   _| __(_)
  | || '__| |
  | || |  | |
  |_||_|  |_|";

    static string banniereSupprimer = @" ____                         _
/ ___| _   _ _ __  _ __  _ __(_)_ __ ___   ___ _ __
\___ \| | | | '_ \| '_ \| '__| | '_ ` _ \ / _ \ '__|
 ___) | |_| | |_) | |_) | |  | | | | | | |  __/ |
|____/ \__,_| .__/| .__/|_|  |_|_| |_| |_|\___|_|
            |_|   |_|";

    static string banniereMinMax = @" __  __ _                 __  __
|  \/  (_)_ __           |  \/  | __ ___  __
| |\/| | | '_ \   _____  | |\/| |/ _` \ \/ /
| |  | | | | | | |_____| | |  | | (_| |>  <
|_|  |_|_|_| |_|         |_|  |_|\__,_/_/\_\";

    static string banniereMoyenne = @" __  __
|  \/  | ___  _   _  ___ _ __  _ __   ___
| |\/| |/ _ \| | | |/ _ \ '_ \| '_ \ / _ \
| |  | | (_) | |_| |  __/ | | | | | |  __/
|_|  |_|\___/ \__, |\___|_| |_|_| |_|\___|
              |___/";

    static string banniereSauvegarde = @" ____                                            _
/ ___|  __ _ _   ___   _____  __ _  __ _ _ __ __| | ___
\___ \ / _` | | | \ \ / / _ \/ _` |/ _` | '__/ _` |/ _ \
 ___) | (_| | |_| |\ V /  __/ (_| | (_| | | | (_| |  __/
|____/ \__,_|\__,_| \_/ \___|\__, |\__,_|_|  \__,_|\___|
                             |___/";

    static string banniereLecture = @" _              _
| |    ___  ___| |_ _   _ _ __ ___
| |   / _ \/ __| __| | | | '__/ _ \
| |__|  __/ (__| |_| |_| | | |  __/
|_____\___|\___|\__|\__,_|_|  \___|";

    static string banniereModifier = @" __  __           _ _  __ _
|  \/  | ___   __| (_)/ _(_) ___ _ __
| |\/| |/ _ \ / _` | | |_| |/ _ \ '__|
| |  | | (_) | (_| | |  _| |  __/ |
|_|  |_|\___/ \__,_|_|_| |_|\___|_|";

    static string Lire()
    {
        string? ligne = Console.ReadLine();

        if (ligne == null)
        {
            Environment.Exit(0);
        }

        return ligne;
    }
}