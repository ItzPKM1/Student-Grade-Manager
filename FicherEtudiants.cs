namespace DSA_Projet_VérificateurDeNotesÉtudiants;

public class FicherEtudiants
{
    private static string fileName = "etudiants.txt";

    public static void SaveEtudiantsToFile(GestionEtudiants gestion)
    {
        using StreamWriter writer = new StreamWriter(fileName);

        NoeudEtudiant current = gestion.getHead();

        while (current != null)
        {
            writer.WriteLine(current.getData().ToString());
            current = current.getNext();
        }

        Console.WriteLine("Les étudiants ont été sauvegardés dans le fichier.");
    }

    public static void ReadEtudiantsFromFile()
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Aucun fichier trouvé.");
            return;
        }

        using StreamReader reader = new StreamReader(fileName);

        string line;

        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}