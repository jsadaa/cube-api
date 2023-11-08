namespace ApiCube.Domain.Entities;

public class Promotion
{
    public int Id { get; set; } = 0;
    public string Nom { get; set; }
    public string Description { get; set; }
    public double Pourcentage { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }

    public Promotion(string nom, string description, double pourcentage, DateTime dateDebut, DateTime dateFin)
    {
        Nom = nom;
        Description = description;
        Pourcentage = pourcentage;
        DateDebut = dateDebut;
        DateFin = dateFin;
    }

    public Promotion(int id, string nom, string description, double pourcentage, DateTime dateDebut, DateTime dateFin)
    {
        Id = id;
        Nom = nom;
        Description = description;
        Pourcentage = pourcentage;
        DateDebut = dateDebut;
        DateFin = dateFin;
    }

    public bool EstValide()
    {
        return DateDebut < DateTime.Now && DateFin > DateTime.Now;
    }

    public void MettreAJour(string nom, string description, double pourcentage, DateTime dateDebut, DateTime dateFin)
    {
        Nom = nom;
        Description = description;
        Pourcentage = pourcentage;
        DateDebut = dateDebut;
        DateFin = dateFin;
    }
}