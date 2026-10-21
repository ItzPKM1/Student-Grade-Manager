namespace DSA_Projet_VérificateurDeNotesÉtudiants;

class Program
{
    public static void Main(string[] args)
    {
        GestionEtudiants gestion = new GestionEtudiants();
        bool quitter = false;

        while (!quitter)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Ajouter un étudiant");
            Console.WriteLine("2. Afficher les étudiants");
            Console.WriteLine("3. Rechercher un étudiant par ID");
            Console.WriteLine("4. Trier les étudiants par ID");
            Console.WriteLine("5. Supprimer un étudiant");
            Console.WriteLine("6. Trouver et afficher la note minimale et maximale");
            Console.WriteLine("7. Afficher la moyenne de la classe");
            Console.WriteLine("8. Sauvegarder les étudiants dans un fichier");
            Console.WriteLine("9. Lire les étudiants du fichier");
            Console.WriteLine("10. Modifier un étudiant");
            Console.WriteLine("11. Quitter");
            Console.Write("Choisissez une option: ");

            string choix = Lire();

            switch (choix)
            {
                case "1":
                    //Code pour ajouter un étudiant
                    // Main faire le création de l'étudiant à partir de l'input des entrées 
                    //et envoi à la classe gestion.
                    AjouterEtudiant(gestion);
                    break;

                case "2":
                    gestion.afficherEtudiants();
                    break;

                case "3":
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
                            Console.WriteLine("Étudiant non trouvé.");
                        }
                    }
                    //catch (quelque chose qui interceptera si le user entrer quelque chose invalide pour l'ID)
                    catch
                    {
                        Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
                    }

                    break;

                case "4":
                    //Code pour trier les étudiants par ID
                    gestion.QuickSortByID();
                    if (gestion.getHead() != null)
                    {
                        Console.WriteLine("Étudiants triés par ID.");
                    }
                    break;

                case "5":
                    //Code pour supprimer un étudiant
                    try // intercepter les erreurs si le user entrer quelque chose invalide pour l'ID
                    {
                        Console.Write("Entrez l'ID de l'étudiant à supprimer: ");
                        int idSupprimer = int.Parse(Lire());
                        gestion.supprimerEtudiant(idSupprimer);
                    }

                    catch
                    {
                        Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
                    }
                    break;

                case "6":
                    //Code pour trouver et afficher la note minimale et maximale
                    try
                    {
                        double[] resultats = gestion.FindMinMax();
                        Console.WriteLine($"Note minimale: {Math.Round(resultats[0], 2)}, Note maximale: {Math.Round(resultats[1], 2)}");
                    }

                    catch
                    {
                        Console.WriteLine("Il n'y a pas d'étudiants dans la classe pour trouver les notes minimale et maximale.");
                    }
                    break;

                case "7":
                    // code pour afficher les statistiques
                    try
                    {
                        Console.WriteLine("Moyenne de la classe: " + Math.Round(gestion.CalculMoyenneClasse(), 2));
                    }
                    catch
                    {
                        Console.WriteLine("Il n'y a pas d'étudiants dans la classe pour calculer la moyenne.");
                    }

                    break;

                case "10":
                    ModifierEtudiant(gestion);
                    break;

                case "11":
                    quitter = true;
                    Console.WriteLine("Quitter le programme. Au revoir!");
                    break;
                case "8":
                    FichierEtudiants.SaveEtudiantsToFile(gestion);
                    break;

                case "9":
                    FichierEtudiants.ReadEtudiantsFromFile(gestion);
                    break;

                default:
                    Console.WriteLine("Option invalide, veuillez réessayer.");
                    break;
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
                    Console.WriteLine("L'ID doit être un nombre positif. Veuillez réessayer.");
                }
                else if (gestion.idExiste(id)) // fonction pour vérifier si l'ID existe déjà dans le tableau de hachage.
                {
                    Console.WriteLine("Un étudiant avec cet ID existe déjà. Veuillez entrer un ID différent.");
                }
                else
                {
                    e.setId(id);
                    idValide = true;
                }
            }
            catch
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
            }
        }

        Console.Write("Entrez le prénom de l'étudiant: ");
        string prenom = Lire();
        if (string.IsNullOrWhiteSpace(prenom))
        {
            Console.WriteLine("Le prénom ne peut pas être vide. Veuillez réessayer.");
            return;
        }
        e.setPrenom(prenom);

        Console.Write("Entrez le nom de l'étudiant: ");
        string nom = Lire();
        if (string.IsNullOrWhiteSpace(nom))
        {
            Console.WriteLine("Le nom ne peut pas être vide. Veuillez réessayer.");
            return;
        }
        e.setNom(nom);

        //nouveau email
        Console.WriteLine("Entrez le email de l'étudiant: ");
        string email = Lire();
        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Le email ne peut pas être vide. Veuillez réessayer.");
            return;
        }
        while (!EmailValide(email))
        {
            Console.WriteLine("Email invalide. Veuillez entrer un email valide (ex: nom@exemple.com).");
            email = Lire();
        }

        e.setEmail(email);

        //ajout de num de téléphone
        Console.WriteLine("Entrez le numéro de téléphone de l'étudiant");
        string numeroDeTéléphone = Lire();
        if (string.IsNullOrWhiteSpace(numeroDeTéléphone))
        {
            Console.WriteLine("Le numéro de téléphone ne peut pas être vide. Veuillez réessayer.");
            return;
        }
        while (!TelephoneValide(numeroDeTéléphone))
        {
            Console.WriteLine("Numéro de téléphone invalide. Il doit contenir entre 10 et 15 chiffres (ex: 514-555-1234).");
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
                    Console.WriteLine("Le nombre de cours doit être entre 1 et 5.");
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
                                            Console.WriteLine("Entrée invalide. Veuillez entrer un nombre valide pour la note.");
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
                                            Console.WriteLine("Entrée invalide. Veuillez entrer un nombre décimal pour la pondération.");
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
                                    Console.WriteLine("La somme des pondérations pour le cours doit être égale à 1. Réessayer.");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
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
                        Console.WriteLine("L'étudiant doit avoir au moins un cours valide.");
                    }

                    nombreCoursValide = true;
                }
            }
            catch
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
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
            Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
            return;
        }

        Etudiant e = gestion.RechercheLineaireParID(id);

        if (e == null)
        {
            Console.WriteLine("Étudiant non trouvé.");
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
                    Console.WriteLine("Le prénom ne peut pas être vide.");
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
                    Console.WriteLine("Le nom ne peut pas être vide.");
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
                    Console.WriteLine("Email invalide. Veuillez entrer un email valide (ex: nom@exemple.com).");
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
                    Console.WriteLine("Numéro de téléphone invalide. Il doit contenir entre 10 et 15 chiffres (ex: 514-555-1234).");
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
                    Console.WriteLine("Numéro de cours invalide.");
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
                                Console.WriteLine("Entrée invalide. Veuillez entrer un nombre valide pour la note.");
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
                                Console.WriteLine("Entrée invalide. Veuillez entrer un nombre décimal pour la pondération.");
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
                        Console.WriteLine("La somme des pondérations pour le cours doit être égale à 1. Réessayer.");
                    }
                }
                break;

            default:
                Console.WriteLine("Option invalide, veuillez réessayer.");
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