using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.ValuesObjects;

namespace ApiCube.Domain.Entities;

public class Fournisseur
{
    public int Id { get; set; }
    
    public string Nom { get; set; }
    
    public Adresse Adresse { get; set; }
    
    public Telephone Telephone { get; set; }
    
    public string Email { get; set; }
    
    public ICollection<Produit> Produits { get; set; }
    
    public Fournisseur(int id, string nom, string adresse, string telephone, string email)
    {
        Id = id;
        Nom = nom;
        Adresse = ValiderEtFormaterAdresse(adresse);
        Telephone = ValiderEtFormaterTelephone(telephone);
        Email = email;
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
    
    public void MettreAJour(string nom, string adresse, string telephone, string email)
    {
        Nom = nom;
        Adresse = ValiderEtFormaterAdresse(adresse);
        Telephone = ValiderEtFormaterTelephone(telephone);
        Email = email;
    }
    
    public FournisseurDTO ToResponseDTO()
    {
        return new FournisseurDTO
        {
            Id = Id,
            Nom = Nom,
            Adresse = Adresse.ToString(),
            Telephone = Telephone.ToString(),
            Email = Email,
        };
    }
    
    public AjouterFournisseurRequest ToRequestDTO()
    {
        return new AjouterFournisseurRequest
        {
            Nom = Nom,
            Adresse = Adresse.ToString(),
            Telephone = Telephone.ToString(),
            Email = Email,
        };
    }
}