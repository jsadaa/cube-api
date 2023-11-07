using ApiCube.Application.DTOs.Requests;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.FamilleProduit;

public class FamilleProduitMapper : IFamilleProduitMapper
{
    public Entities.FamilleProduit Mapper(FamilleProduitRequestDTO familleProduitRequestDTO)
    {
        return new Entities.FamilleProduit(
            nom: familleProduitRequestDTO.Nom,
            description: familleProduitRequestDTO.Description
        );
    }
    
    public Entities.FamilleProduit Mapper(FamilleProduitModel familleProduitModel)
    {
        return new Entities.FamilleProduit(
            id: familleProduitModel.Id,
            nom: familleProduitModel.Nom,
            description: familleProduitModel.Description
        );
    }
}