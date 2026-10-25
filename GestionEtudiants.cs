namespace DSA_Projet_VérificateurDeNotesÉtudiants;

public class GestionEtudiants
{
    private ListeDoubleEtudiants liste;
    private Dictionary<int, Etudiant> tableauHash;

    public GestionEtudiants()
    {
        liste = new ListeDoubleEtudiants();
        tableauHash = new Dictionary<int, Etudiant>();
    }

    public void ajouterEtudiant(Etudiant e)
    {
        if (tableauHash.ContainsKey(e.getId()))
        {
            Program.Erreur("Un étudiant avec cet ID existe déjà."); // même message qu'avant mais en rouge; Program. parce que la méthode est dans la classe Program
            return;
        }
        liste.AddLast(e);
        tableauHash[e.getId()] = e;
        Console.WriteLine("Étudiant ajouté avec succès.");
    }

    public bool idExiste(int id) // validation de l'ID pour éviter les doublons
    {
        return tableauHash.ContainsKey(id);
    }

    public void afficherEtudiants()
    {
        liste.AfficherEtudiants();
    }

    public Etudiant getEtudiantById(int id)
    {
        if (tableauHash.ContainsKey(id))
        {
            return tableauHash[id];
        }
        else
        {
            return null;
        }
    }
    public Etudiant RechercheLineaireParID(int id)
    {
        NoeudEtudiant noeud = liste.RechercheParId(id);

        if (noeud == null)
        {
            return null;
        }

        return noeud.getData();
    }

    public NoeudEtudiant getHead() //retorune le premier noeud de la liste chaînée des etudiants pour permettre l'accès aux données dans d'atures classes
    {
        return liste.getHead();
    }

    public void supprimerEtudiant(int id)
    {
        if (liste.RemoveById(id))
        {
            tableauHash.Remove(id);
            Console.WriteLine("Étudiant supprimé avec succès.");
        }
        else
        {
            Program.Avertissement("Étudiant avec l'ID spécifié non trouvé."); // même message qu'avant mais en jaune; Program. parce que la méthode est dans la classe Program
        }
    }

    public double CalculMoyenneClasse()
    {
        double somme = 0;
        int count = liste.getCount();

        if (count == 0)
        {
            throw new ArgumentException("Il n'y a pas d'étudiants.");
        }

        NoeudEtudiant temp = liste.getHead();

        while (temp != null)
        {
            somme += temp.getData().getNoteFinal();
            temp = temp.getNext();
        }

        return somme / count;
    }

    public double[] FindMinMax()
    {
        if (liste.getCount() == 0)
        {
            throw new ArgumentException("Il n'y a pas d'étudiants.");
        }

        double min = double.MaxValue;
        double max = double.MinValue;

        NoeudEtudiant temp = liste.getHead();

        while (temp != null)
        {
            double note = temp.getData().getNoteFinal();

            if (note < min)
            {
                min = note;
            }

            if (note > max)
            {
                max = note;
            }

            temp = temp.getNext();
        }

        return new double[] { min, max };
    }
    public void QuickSortByID()
    {
        
        int count = liste.getCount();
        if (count == 0)
        {
            Program.Avertissement("Il n'y a pas d'étudiants à trier.");
            return;
        }

        Etudiant[] etudiants = new Etudiant[count];
        NoeudEtudiant temp = liste.getHead();
        int i = 0;
        while (temp != null)
        {
            etudiants[i] = temp.getData(); //
            temp = temp.getNext();
            i++;
        }

        QuickSort(etudiants, 0, count - 1); //
        liste = new ListeDoubleEtudiants();
        for (int j = 0; j < count; j++)
        {
            liste.AddLast(etudiants[j]);
            Console.WriteLine(etudiants[j]);
        }
    }
    private void Swap(Etudiant[] etudiants, int num1, int num2)
    {
        Etudiant temp = etudiants[num1];
        etudiants[num1] = etudiants[num2];
        etudiants[num2] = temp;
    }

    private int Partition(Etudiant[] etudiants, int left, int right, int pivot)
    {
        int leftPointer = left - 1;
        int rightPointer = right;

        while (true)
        {
            while (etudiants[++leftPointer].getId() < pivot) { }
            while (rightPointer > 0 && etudiants[--rightPointer].getId() > pivot) { }
            if (leftPointer >= rightPointer)
            {
                break;
            }
            else
            {
                Swap(etudiants, leftPointer, rightPointer);
                
            }
        }
        Swap(etudiants, leftPointer, right);
        return leftPointer;
    }

    private void QuickSort(Etudiant[] etudiants, int left, int right)
    {
        if (right - left <= 0)
        {
            return;
        }
        else
        {
            int pivot = etudiants[right].getId();
            int partitionIndex = Partition(etudiants, left, right, pivot);
            QuickSort(etudiants, left, partitionIndex - 1);
            QuickSort(etudiants, partitionIndex + 1, right);
        }
    }

}