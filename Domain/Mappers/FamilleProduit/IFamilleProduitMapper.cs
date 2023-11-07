using ApiCube.Application.DTOs.Requests;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.FamilleProduit;

public interface IFamilleProduitMapper
{
    public Entities.FamilleProduit Mapper(FamilleProduitRequestDTO familleProduitRequestDTO);
    public Entities.FamilleProduit Mapper(FamilleProduitModel familleProduitModel);
}