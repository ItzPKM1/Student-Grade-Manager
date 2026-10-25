namespace DSA_Projet_VérificateurDeNotesÉtudiants;

public class Etudiant
{
    private int id;
    private string prenom;
    private string nom;
    private string email;
    private string numeroDeTelephone;

    private Cours[] cours;
    private int nombreCours;
    private double noteFinal;

    public const int minCours = 1;
    public const int maxCours = 5;

    public Etudiant()
    {
        id = 0;
        prenom = "";
        nom = "";
        email = "";
        numeroDeTelephone = "";

        cours = new Cours[maxCours];
        nombreCours = 0;
        noteFinal = 0;
    }

    public int getId()
    {
        return id;
    }

    public string getPrenom()
    {
        return prenom;
    }

    public string getNom()
    {
        return nom;
    }

    public string getEmail()
    {
        return email;
    }

    public string getNumeroDeTelephone()
    {
        return numeroDeTelephone;
    }

    public Cours[] getCours()
    {
        return cours;
    }

    public int getNombreCours()
    {
        return nombreCours;
    }

    public double getNoteFinal()
    {
        return noteFinal;
    }

    public void setId(int id)
    {
        if (id > 0)
        {
            this.id = id;
        }
    }

    public void setPrenom(string prenom)
    {
        this.prenom = prenom;
    }

    public void setNom(string nom)
    {
        this.nom = nom;
    }

    public void setEmail(string email)
    {
        this.email = email;
    }

    public void setNumeroDeTelephone(string numeroDeTelephone)
    {
        this.numeroDeTelephone = numeroDeTelephone;
    }

    public void ajouterCours(Cours c)
    {
        if (c == null || !c.coursValide())
        {
            throw new ArgumentException("Cours invalide.");
        }

        if (nombreCours >= maxCours)
        {
            throw new ArgumentException("Nombre maximum de cours atteint.");
        }

        cours[nombreCours] = c;
        nombreCours++;
    }

    public void remplacerCours(int index, Cours c)
    {
        if (index < 0 || index >= nombreCours)
        {
            throw new ArgumentException("Index de cours invalide.");
        }

        if (c == null || !c.coursValide())
        {
            throw new ArgumentException("Cours invalide.");
        }

        cours[index] = c;
    }

    public bool verifierNombreCours()
    {
        return nombreCours >= minCours && nombreCours <= maxCours;
    }

    public void CalculNoteFinal()
    {
        if (!verifierNombreCours())
        {
            throw new ArgumentException("L'étudiant doit avoir au moins un cours.");
        }

        double somme = 0;
        int totalCredits = 0;

        for (int i = 0; i < nombreCours; i++)
        {
            cours[i].CalculMoyenneDeCours();
            somme += cours[i].getMoyenneDeCours() * cours[i].getNombreCredits();
            totalCredits += cours[i].getNombreCredits();
        }

        noteFinal = somme / totalCredits;
    }

    public override string ToString()
    {
        // en-tête du tableau des cours, avec les mêmes largeurs que dans le ToString de Cours pour que les colonnes tombent au bon endroit
        string infoCours = "      " + "Code".PadRight(12) + "Titre".PadRight(17) + "Heures".PadRight(8) + "Crédits".PadRight(9) + "Notes".PadRight(18) + "Moyenne";

        for (int i = 0; i < nombreCours; i++)
        {
            infoCours += "\n      " + cours[i].ToString(); // fix
        }

        // \n = nouvelle ligne, ça met chaque info sur sa propre ligne au lieu d'une seule longue ligne
        return "ID " + id + " - " + prenom + " " + nom +
               "\n   Email:     " + email + // les espaces après Email: alignent l'email avec le téléphone en dessous
               "\n   Téléphone: " + numeroDeTelephone +
               "\n   Cours:" +
               "\n" + infoCours +
               "\n   Note finale: " + Math.Round(noteFinal, 2) +
               "\n" + new string('─', 70); // ligne de séparation entre les étudiants: new string('─', 70) répète le caractère ─ 70 fois
    }
}