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

        for (int i = 0; i < nombreCours; i++)
        {
            cours[i].CalculMoyenneDeCours();
            somme += cours[i].getMoyenneDeCours();
        }

        noteFinal = somme / nombreCours;
    }

    public override string ToString()
    {
        string infoCours = "";

        for (int i = 0; i < nombreCours; i++)
        {
            infoCours += "\n   " + cours[i].ToString(); // fix
        }

        return "ID: " + id +
               " Prenom: " + prenom +
               " Nom: " + nom +
               " Email: " + email +
               " Numéro de téléphone: " + numeroDeTelephone +
               " Cours: " + infoCours +
               " Note finale: " + noteFinal;
    }
}