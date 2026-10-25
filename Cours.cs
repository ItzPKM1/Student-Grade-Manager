namespace DSA_Projet_VérificateurDeNotesÉtudiants;
public class Cours
{
    private string codeCours; //c'est un string parce que les codes de cours contiennent generalement un melange de letters et de chiffres. 
    private string titre;
    private int nombreHeures;
    private int nombreCredits;
    // tableaux contenant les 3 notes et les 3 pondérations
    private double[] notes;
    private double[] ponderations;

    private double moyenneDeCours;

    public Cours(string codeCours, string titre, int nombreHeures, int nombreCredits)
    {
        this.codeCours = codeCours;
        this.titre = titre;
        this.nombreHeures = nombreHeures;
        this.nombreCredits = nombreCredits;

        notes = new double[3];
        ponderations = new double[3];

        moyenneDeCours = 0;
    }

    public string getCodeDeCours()
    {
        return codeCours;
    }
    public string getTitre()
    {
        return titre;
    }
    public int getNombreHeures()
    {
        return nombreHeures;
    }
    public int getNombreCredits()
    {
        return nombreCredits;
    }
    public double[] getNotes()
    {
        return notes;
    }

    public double[] getPonderations()
    {
        return ponderations;
    }

    public double getMoyenneDeCours()
    {
        return moyenneDeCours;
    }
    public void setCodeCours(string codeCours)
    {
        if(!string.IsNullOrWhiteSpace(codeCours))
        {
            this.codeCours = codeCours;
        }
    }
    public void setTitre(string titre)
    {
        if(!string.IsNullOrWhiteSpace(titre))
        {
            this.titre = titre;
        }
    }
    public void setNombreHeures(int nombreHeures)
    {   
        if(nombreHeures > 0)
        {
            this.nombreHeures = nombreHeures;
        }
    }
    public void setNombreCredits(int nombreCredits)
    {
        if(nombreCredits > 0)
        {
            this.nombreCredits = nombreCredits;
        }
    }
    public void setNote(int i, double note)
    {
        if (i < 0 || i >= notes.Length) //Validation pour vérifier que l'index est valide.
        {
            throw new ArgumentException("Index de note invalide. Veuillez fournir un index entre 0 et 2.");
        }
        if (note < 0 || note > 100) //Validation pour vérifier que la note est entre au moins 0 et au plus 100.
        {
            throw new ArgumentException("Note invalide. Veuillez fournir une note entre 0 et 100.");
        }
        notes[i] = note;
    }
    public void setPonderation(int i, double ponderation)
    {
        if (i >= 0 && i < ponderations.Length && ponderation >= 0 && ponderation <= 1) //Validation pour verifier que l'index est valide et que la pondération est entre au moins 0 et au plus 1.
        {
            ponderations[i] = ponderation;
        }
    }

    public bool verifierPonderations() //Cette méthode vérifie que la somme des pondérations pour chaque sujet est égale à 1.
    {
        double total = 0;


        for (int i = 0; i < 3; i++)
        {
            total += ponderations[i];

        }
        return Math.Abs(total - 1) < 0.0001; //comme notre notes avec Azam pour algorithms.

    }
    public bool coursValide()
    {
        return !string.IsNullOrWhiteSpace(codeCours) &&
               !string.IsNullOrWhiteSpace(titre) &&
               nombreHeures > 0 &&
               nombreCredits > 0;
    }
    // calcul de la moyenne pondérée du cours
    public void CalculMoyenneDeCours()
    {
        moyenneDeCours = 0;

        for (int i = 0; i < 3; i++)
        {
            moyenneDeCours += notes[i] * ponderations[i];
        }
    }


    public override string ToString()
    {
        // si le titre est trop long on le coupe pour que les colonnes restent alignées (Substring(0, 15) garde les 15 premiers caractères)
        string titreCourt = titre;
        if (titre.Length > 16)
        {
            titreCourt = titre.Substring(0, 15) + "…";
        }

        // PadRight(n) ajoute des espaces à la fin du texte jusqu'à n caractères, comme ça les colonnes sont alignées d'un cours à l'autre
        return codeCours.PadRight(12) + // colonne Code
               titreCourt.PadRight(17) + // colonne Titre (17 = 16 caractères + 1 espace avant la colonne suivante)
               nombreHeures.ToString().PadRight(8) + // PadRight marche sur du texte, alors on transforme le nombre en texte avec ToString()
               nombreCredits.ToString().PadRight(9) + // colonne Crédits
               string.Join(", ", notes).PadRight(18) + // colonne Notes
               Math.Round(moyenneDeCours, 2); // colonne Moyenne, la dernière alors pas besoin de PadRight
    }
}