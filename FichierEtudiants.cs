namespace DSA_Projet_VérificateurDeNotesÉtudiants;

public class FichierEtudiants
{
    private static string fileName = "etudiants.txt";

    public static void SaveEtudiantsToFile(GestionEtudiants gestion)
    {
        if (gestion.getHead() == null)
        {
            Console.WriteLine("Il n'y a aucun étudiant à sauvegarder.");
            return;
        }

        using StreamWriter writer = new StreamWriter(fileName);

        NoeudEtudiant current = gestion.getHead();

        while (current != null)
        {
            Etudiant e = current.getData();

            string ligne = e.getId() + ";" + e.getPrenom() + ";" + e.getNom() + ";" + e.getEmail() + ";" + e.getNumeroDeTelephone() + ";" + e.getNombreCours();

            Cours[] cours = e.getCours();

            for (int i = 0; i < e.getNombreCours(); i++)
            {
                ligne += ";" + cours[i].getCodeDeCours() + ";" + cours[i].getTitre() + ";" + cours[i].getNombreHeures() + ";" + cours[i].getNombreCredits();

                double[] notes = cours[i].getNotes();
                double[] ponderations = cours[i].getPonderations();

                for (int j = 0; j < 3; j++)
                {
                    ligne += ";" + notes[j] + ";" + ponderations[j];
                }
            }

            writer.WriteLine(ligne);
            current = current.getNext();
        }

        Console.WriteLine("Les étudiants ont été sauvegardés dans le fichier.");
    }

    public static void ReadEtudiantsFromFile(GestionEtudiants gestion)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Aucun fichier trouvé.");
            return;
        }

        using StreamReader reader = new StreamReader(fileName);

        string line;
        int nombreCharges = 0;

        while ((line = reader.ReadLine()) != null)
        {
            try
            {
                string[] champs = line.Split(';');

                Etudiant e = new Etudiant();
                e.setId(int.Parse(champs[0]));
                e.setPrenom(champs[1]);
                e.setNom(champs[2]);
                e.setEmail(champs[3]);
                e.setNumeroDeTelephone(champs[4]);

                int nombreCours = int.Parse(champs[5]);

                if (e.getId() == 0 || champs.Length != 6 + nombreCours * 10)
                {
                    throw new ArgumentException("Ligne invalide.");
                }

                for (int i = 0; i < nombreCours; i++)
                {
                    int debut = 6 + i * 10;

                    Cours c = new Cours(champs[debut], champs[debut + 1], int.Parse(champs[debut + 2]), int.Parse(champs[debut + 3]));

                    for (int j = 0; j < 3; j++)
                    {
                        c.setNote(j, double.Parse(champs[debut + 4 + j * 2]));
                        c.setPonderation(j, double.Parse(champs[debut + 5 + j * 2]));
                    }

                    if (!c.verifierPonderations())
                    {
                        throw new ArgumentException("Pondérations invalides.");
                    }

                    c.CalculMoyenneDeCours();
                    e.ajouterCours(c);
                }

                e.CalculNoteFinal();

                if (gestion.idExiste(e.getId()))
                {
                    Console.WriteLine("L'étudiant avec l'ID " + e.getId() + " existe déjà, il a été ignoré.");
                }
                else
                {
                    gestion.ajouterEtudiant(e);
                    nombreCharges++;
                }
            }
            catch
            {
                Console.WriteLine("Ligne invalide, elle a été ignorée: " + line);
            }
        }

        Console.WriteLine("Nombre d'étudiants chargés: " + nombreCharges);
    }
}