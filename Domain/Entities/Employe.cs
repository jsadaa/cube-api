namespace ApiCube.Domain.Entities;

public class Employe
{
    public int Id { get; set; } = 0;

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Email { get; set; }

    public DateTime DateEmbauche { get; set; }

    public DateTime? DateDepart { get; set; } = null;

    public string Poste { get; set; }

    public Employe(string nom, string prenom, string email, DateTime dateEmbauche, string poste)
    {
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        Poste = poste;
    }

    public Employe(int id, string nom, string prenom, string email, DateTime dateEmbauche, DateTime? dateDepart,
        string poste)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        DateDepart = dateDepart;
        Poste = poste;
    }

    public void MettreAJour(string nom, string prenom, string email, DateTime dateEmbauche, string poste)
    {
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        Poste = poste;
    }
}