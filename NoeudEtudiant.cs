namespace DSA_Projet_VérificateurDeNotesÉtudiants;

public class NoeudEtudiant
{
    // attributs
    public Etudiant data;
    private NoeudEtudiant next;
    private NoeudEtudiant previous;

    //constructeur 
    public NoeudEtudiant(Etudiant data)
    {
        this.data = data;
        next = null;
        previous = null;
    }
    
    //getters 
    public Etudiant getData()
    {
        return data;
    }

    public NoeudEtudiant getNext()
    {
        return next;
    }

    public NoeudEtudiant getPrevious()
    {
        return previous;
    }
    
    //Setters
    public void setData(Etudiant data)
    {
        this.data = data;
    }

    public void setNext(NoeudEtudiant next)
    {
        this.next = next;
    }

    public void setPrevious(NoeudEtudiant previous)
    {
        this.previous = previous;
    }
}