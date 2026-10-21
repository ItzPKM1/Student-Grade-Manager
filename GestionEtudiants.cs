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
            Console.WriteLine("Un étudiant avec cet ID existe déjà.");
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
            Console.WriteLine("Étudiant avec l'ID spécifié non trouvé.");
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

    public double[] FindMinMax() //Mime we might not need this because we have the FindMinMax class but I think it's better to have it here because we can use the linked list instead of converting it to an array and then finding min and max.
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
            Console.WriteLine("Il n'y a pas d'étudiants à trier.");
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
        for (int j = 0; j < count; j++)
        {
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