using ApiCube.Domain.Enums.Administration;
using ApiCube.Domain.ValuesObjects;

namespace ApiCube.Domain.Entities;

public class Employe
{
    
    public int Id { get; set; } = 0;
    
    public string Nom { get; set; }
    
    public string Prenom { get; set; }
    
    public Adresse Adresse { get; set; }
    
    public Telephone Telephone { get; set; }
    
    public string Email { get; set; }
    
    public DateTime DateEmbauche { get; set; }
    
    public DateTime DateDepart { get; set; }
    
    public string Statut { get; set; }
    
    public double Salaire { get; set; }
    
    public string Login { get; set; } 
    
    public string MotDePasse { get; set; }
    
    public Role Role { get; set; }
    
    public Employe(string nom, string prenom, string adresse, string telephone, string email, DateTime dateEmbauche, DateTime dateDepart, string statut, double salaire, string login, string motDePasse, Role role)
    {
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        DateDepart = dateDepart;
        Statut = statut;
        Salaire = salaire;
        Login = login;
        MotDePasse = motDePasse;
        Role = role;
        Adresse = ValiderEtFormaterAdresse(adresse);
        Telephone = ValiderEtFormaterTelephone(telephone);
    }
    
    public Employe(int id, string nom, string prenom, string adresse, string telephone, string email, DateTime dateEmbauche, DateTime dateDepart, string statut, double salaire, string login, string motDePasse, Role role)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Email = email;
        DateEmbauche = dateEmbauche;
        DateDepart = dateDepart;
        Statut = statut;
        Salaire = salaire;
        Login = login;
        MotDePasse = motDePasse;
        Role = role;
        Adresse = ValiderEtFormaterAdresse(adresse);
        Telephone = ValiderEtFormaterTelephone(telephone);
    }

    private Adresse ValiderEtFormaterAdresse(string adresse)
    {
        string[] adresseSplit = adresse.Split(',');
        
        if (adresseSplit.Length != 5)
        {
            throw new Exception("L'adresse doit être composée de 5 champs séparés par une virgule");
        }
        
        return new Adresse(adresseSplit[0], adresseSplit[1], adresseSplit[2], adresseSplit[3], adresseSplit[4]);
    }

    private Telephone ValiderEtFormaterTelephone(string telephone)
    {
        if (telephone.Length != 10)
        {
            throw new Exception("Le numéro de téléphone doit être composé de 10 chiffres");
        }
        
        return new Telephone(telephone);
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
        return Role == Role.ResponsableStock || Role == Role.ResponsableAchat || Role == Role.ResponsableVente || Role == Role.ResponsableClient || Role == Role.ResponsableFournisseur;
    }
    
    public bool EstResponsableStock()
    {
        return Role == Role.ResponsableStock;
    }
    
    public bool EstResponsableAchat()
    {
        return Role == Role.ResponsableAchat;
    }
    
    public bool EstResponsableVente()
    {
        return Role == Role.ResponsableVente;
    }
}