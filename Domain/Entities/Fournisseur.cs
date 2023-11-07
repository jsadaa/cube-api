using ApiCube.Domain.ValuesObjects;

namespace ApiCube.Domain.Entities;

public class Fournisseur
{
    public int Id { get; set; } = 0;

    public string Nom { get; set; }

    public Adresse Adresse { get; set; }

    public Telephone Telephone { get; set; }

    public string Email { get; set; }

    public ICollection<Produit>? Produits { get; set; }

    public Fournisseur(string nom, string adresse, string codePostal, string ville, string pays, string telephone,
        string email)
    {
        Nom = nom;
        Adresse = new Adresse(adresse, codePostal, ville, pays);
        Telephone = new Telephone(telephone);
        Email = email;
    }

    public Fournisseur(int id, string nom, string adresse, string codePostal, string ville, string pays,
        string telephone, string email)
    {
        Id = id;
        Nom = nom;
        Adresse = new Adresse(adresse, codePostal, ville, pays);
        Telephone = new Telephone(telephone);
        Email = email;
    }

    public void MettreAJour(string nom, string adresse, string codePostal, string ville, string pays, string telephone,
        string email)
    {
        Nom = nom;
        Adresse = new Adresse(adresse, codePostal, ville, pays);
        Telephone = new Telephone(telephone);
        Email = email;
    }
}