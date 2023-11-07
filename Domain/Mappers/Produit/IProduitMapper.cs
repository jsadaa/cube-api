using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Produit;

public interface IProduitMapper
{
    public Entities.Produit Mapper(ProduitModel produitModel, Entities.FamilleProduit familleProduit,
        Entities.Fournisseur fournisseur);
}