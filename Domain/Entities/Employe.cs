using ApiCube.Domain.Enums.Administration;

namespace ApiCube.Domain.Entities;

public class Employe
{
    public int Id { get; set; } = 0;

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Email { get; set; }

    public DateTime DateEmbauche { get; set; }

    public DateTime DateDepart { get; set; }

    public string Statut { get; set; }

    public string Login { get; set; }

    public string MotDePasse { get; set; }

    public Role Role { get; set; }

    public Employe(string nom, string prenom, string email, DateTime dateEmbauche, DateTime dateDepart, string statut,
        string login, string motDePasse, Role role)
    {
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        DateDepart = dateDepart;
        Statut = statut;
        Login = login;
        MotDePasse = motDePasse;
        Role = role;
    }

    public Employe(int id, string nom, string prenom, string email, DateTime dateEmbauche, DateTime dateDepart,
        string statut, double salaire, string login, string motDePasse, Role role)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        DateDepart = dateDepart;
        Statut = statut;
        Login = login;
        MotDePasse = motDePasse;
        Role = role;
    }

    public void MettreAJourMotDePasse(string motDePasse)
    {
        MotDePasse = motDePasse;
    }

    public void MettreAJourRole(Role role)
    {
        Role = role;
    }

    public bool EstAdmin()
    {
        return Role == Role.Admin;
    }

    public bool EstEmploye()
    {
        return Role == Role.Employe;
    }

    public bool EstGestionnaire()
    {
        return Role is Role.Admin or Role.Manager;
    }

    public bool EstManager()
    {
        return Role == Role.Manager;
    }
}