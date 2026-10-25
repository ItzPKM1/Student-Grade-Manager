namespace DSA_Projet_VérificateurDeNotesÉtudiants;

public class ListeDoubleEtudiants
{
    // pointeur vers le premier noeud
    private NoeudEtudiant head;
    //pointeur vers le dernier noeud
    private NoeudEtudiant tail;
    private int count;
    private NoeudEtudiant current;


    public ListeDoubleEtudiants()
    {
        head = null;
        tail = null;
        count = 0;
        current = null;
    }

    // retourne le premier noeud
    public NoeudEtudiant getHead()
    {
        return head;
    }

    // retourne le nombre d'étudiants
    public int getCount()
    {
        return count;
    }
    // retourne le noeud courant
    public NoeudEtudiant getCurrent()
    {
        return current;
    }
    // ajouter un étudiant à la fin de la liste
    public void AddLast(Etudiant e)
    {
        // créer un nouveau noeud 
        NoeudEtudiant nouveau = new NoeudEtudiant(e);

        // si la liste est vide
        if (head == null)
        {
            // le premier et dernier pointent vers le même noeud
            head = nouveau;
            tail = nouveau;
        }
        else
        {
            tail.setNext(nouveau);
            nouveau.setPrevious(tail);
            tail = nouveau;
        }

        count++;  //augmenter le nombre d'étudiants
    }

    //afficher tous les étudiants
    public void AfficherEtudiants()
    {
        current = head;

        // vérifier si la liste est vide ou pas
        if (current == null)
        {
            Program.Avertissement("Il n'y a pas d'étudiants dans la liste"); // même message qu'avant mais en jaune; Program. parce que la méthode est dans la classe Program
            return;
        }

        Console.Write("Nombre d'étudiants: "); // Write pour que le nombre s'écrive sur la même ligne
        Console.ForegroundColor = ConsoleColor.Cyan; // juste le nombre en cyan
        Console.WriteLine(count);
        Console.ResetColor(); // remet la couleur normale

    
        while (current != null)
        {
            Console.WriteLine(current.getData());
            current = current.getNext();
        }
    }

    // rechercher un étudiant par son ID
    public NoeudEtudiant RechercheParId(int id)
    {
        current = head;

        while (current != null)
        {
            if (current.getData().getId() == id) // verifier si l'etudiant a le meme ID que celui qu'on cherche.
            {
                return current;
            }

            current = current.getNext();
        }

        return null;
    }

    // supprimer un étudiant par son id
    public bool RemoveById(int id)
    {
        // chercher le noeud qu'on doit supprimer 
        NoeudEtudiant noeudASupprimer = RechercheParId(id);

        if (noeudASupprimer == null)  // voir si l'étudiant existe
        {
            return false;
        }

        if (head == tail)   //noeud ds la liste
        {
            head = null;
            tail = null;
        }
        else if (noeudASupprimer == head) // supprimer 1er noeud
        {
            head = head.getNext();
            head.setPrevious(null);
        }
        else if (noeudASupprimer == tail)    //supprimer dernier noeud
        {
            tail = tail.getPrevious();
            tail.setNext(null);
        }
        else      // récupérer les noeuds
        {
            NoeudEtudiant precedent = noeudASupprimer.getPrevious();
            NoeudEtudiant suivant = noeudASupprimer.getNext();

            precedent.setNext(suivant);
            suivant.setPrevious(precedent);
        }

        count--;
        return true;
    }
}