using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Fournisseur;

public interface IFournisseurMapper
{
    public Entities.Fournisseur Mapper(FournisseurModel fournisseurModel);
}