using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Domain.Entities;

public class Promotion
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public double Pourcentage { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public Produit Produit { get; set; }
    
    public Promotion(int id, string nom, string description, double pourcentage, DateTime dateDebut, DateTime dateFin, Produit produit)
    {
        Id = id;
        Nom = nom;
        Description = description;
        Pourcentage = pourcentage;
        DateDebut = dateDebut;
        DateFin = dateFin;
        Produit = produit;
    }
    
    public bool EstValide()
    {
        return DateDebut < DateTime.Now && DateFin > DateTime.Now;
    }
    
    public PromotionDTO ToResponsesDTO()
    {
        return new PromotionDTO
        {
            Id = Id,
            Nom = Nom,
            Description = Description,
            Pourcentage = Pourcentage,
            DateDebut = DateDebut,
            DateFin = DateFin,
        };
    }
    
    public AjouterPromotionRequest ToRequestDTO()
    {
        return new AjouterPromotionRequest
        {
            Nom = Nom,
            Description = Description,
            Pourcentage = Pourcentage,
            DateDebut = DateDebut,
            DateFin = DateFin,
        };
    }
}