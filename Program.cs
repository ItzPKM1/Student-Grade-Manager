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
            Console.WriteLine("8. Quitter");
            Console.WriteLine("9. Sauvegarder les étudiants dans un fichier");
            Console.WriteLine("10. Lire les étudiants du fichier");
            Console.Write("Choisissez une option: ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    //Code pour ajouter un étudiant
                    // Main faire le création de l'étudiant à partir de l'input des entrées 
                    //et envoi à la classe gestion.
                    Etudiant e = new Etudiant();
                    bool idValide = false;
                    while (!idValide)
                    {
                        try
                        {
                            Console.Write("Entrez l'ID de l'étudiant: ");
                            int id = int.Parse(Console.ReadLine());

                            if (gestion.idExiste(id)) // fonction pour vérifier si l'ID existe déjà dans le tableau de hachage.
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
                    string prenom = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(prenom))
                    {
                        Console.WriteLine("Le prénom ne peut pas être vide. Veuillez réessayer.");
                        break;
                    }
                    e.setPrenom(prenom);

                    Console.Write("Entrez le nom de l'étudiant: ");
                    string nom = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nom))
                    {
                        Console.WriteLine("Le nom ne peut pas être vide. Veuillez réessayer.");
                        break;
                    }
                    e.setNom(nom);

                    //nouveau email
                    Console.WriteLine("Entrez le email de l'étudiant: ");
                    string email = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(email))
                    {
                        Console.WriteLine("Le email ne peut pas être vide. Veuillez réessayer.");
                        break;
                    }
                    e.setEmail(email);

                    //ajout de num de téléphone
                    Console.WriteLine("Entrez le numéro de téléphone de l'étudiant");
                    string numeroDeTéléphone = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(numeroDeTéléphone))
                    {
                        Console.WriteLine("Le numéro de téléphone ne peut pas être vide. Veuillez réessayer.");
                        break;
                    }
                    e.setNumeroDeTelephone(numeroDeTéléphone);

                    bool nombreCoursValide = false;

                    while (!nombreCoursValide)
                    {
                        try
                        {
                            Console.Write("Entrez le nombre de cours de l'étudiant (1 à 5): ");
                            int nombreCours = int.Parse(Console.ReadLine());

                            if (nombreCours < Etudiant.minCours || nombreCours > Etudiant.maxCours)
                            {
                                Console.WriteLine("Le nombre de cours doit être entre 1 et 5.");
                            }
                            else
                            {
                                for (int c = 0; c < nombreCours; c++)
                                {
                                    Console.WriteLine($"\n--- Cours {c + 1} ---");

                                    Console.Write("Entrez le code du cours: ");
                                    string codeCours = Console.ReadLine();

                                    Console.Write("Entrez le titre du cours: ");
                                    string titre = Console.ReadLine();

                                    Console.Write("Entrez le nombre d'heures du cours: ");
                                    int nombreHeures = int.Parse(Console.ReadLine());

                                    Console.Write("Entrez le nombre de crédits du cours: ");
                                    int nombreCredits = int.Parse(Console.ReadLine());

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
                                                cours.setNote(i, double.Parse(Console.ReadLine()));
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
                                                double ponderation = double.Parse(Console.ReadLine());
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
                                    }
                                    else
                                    {
                                        Console.WriteLine("La somme des pondérations pour le cours doit être égale à 1. Réessayer.");
                                        break;
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

                    break;

                case "2":
                    gestion.afficherEtudiants();
                    break;

                case "3":
                    //Code pour rechercher un étudiant par ID
                    try
                    {
                        Console.Write("Entrez l'ID de l'étudiant à rechercher: ");
                        int id = int.Parse(Console.ReadLine());
                        Etudiant eCase3 = gestion.getEtudiantById(id);

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
                    Console.WriteLine("Étudiants triés par ID."); // ici ça clash avec l'autre getEtudiantById
                    break;

                case "5":
                    //Code pour supprimer un étudiant
                    try // intercepter les erreurs si le user entrer quelque chose invalide pour l'ID
                    {
                        Console.Write("Entrez l'ID de l'étudiant à supprimer: ");
                        int idSupprimer = int.Parse(Console.ReadLine());
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
                        Console.WriteLine($"Note minimale: {resultats[0]}, Note maximale: {resultats[1]}");
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
                        Console.WriteLine("Moyenne de la classe: " + gestion.CalculMoyenneClasse());
                    }
                    catch
                    {
                        Console.WriteLine("Il n'y a pas d'étudiants dans la classe pour calculer la moyenne.");
                    }

                    break;

                case "8":
                    quitter = true;
                    Console.WriteLine("Quitter le programme. Au revoir!");
                    break;
                case "9":
                    FicherEtudiants.SaveEtudiantsToFile(gestion);
                    break;

                case "10":
                    FicherEtudiants.ReadEtudiantsFromFile();
                    break;

                default:
                    Console.WriteLine("Option invalide, veuillez réessayer.");
                    break;
            }
        }
    }
}