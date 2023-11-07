using ApiCube.Application.DTOs.Requests;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Produit;

public interface IProduitMapper
{
    public Entities.Produit Mapper(ProduitRequestDTO produitRequestDTO, Entities.FamilleProduit familleProduit, Entities.Fournisseur fournisseur);
    public Entities.Produit Mapper(ProduitModel produitModel, Entities.FamilleProduit familleProduit, Entities.Fournisseur fournisseur);
}