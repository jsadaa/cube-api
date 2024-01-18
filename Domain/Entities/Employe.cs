namespace ApiCube.Domain.Entities;

public class Employe
{
    public Employe(string nom, string prenom, string email, DateTime dateEmbauche, string poste,
        string applicationUserId)
    {
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        Poste = poste;
        ApplicationUserId = applicationUserId;
    }

    public Employe(int id, string nom, string prenom, string email, DateTime dateEmbauche, DateTime? dateDepart,
        string poste, string applicationUserId)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        DateDepart = dateDepart;
        Poste = poste;
        ApplicationUserId = applicationUserId;
    }

    public int Id { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Email { get; set; }

    public DateTime DateEmbauche { get; set; }

    public DateTime? DateDepart { get; set; }

    public string Poste { get; set; }

    public string ApplicationUserId { get; set; }

    public void MettreAJour(string nom, string prenom, string email, DateTime dateEmbauche, string poste,
        string applicationUserId)
    {
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        Poste = poste;
        ApplicationUserId = applicationUserId;
    }
}