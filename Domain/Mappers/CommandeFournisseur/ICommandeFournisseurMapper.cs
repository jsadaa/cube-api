using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.CommandeFournisseur;

public interface ICommandeFournisseurMapper
{
    public Entities.CommandeFournisseur Mapper(CommandeFournisseurModel commandeFournisseurModel, Entities.Fournisseur fournisseur, Entities.Employe employe, List<Entities.LigneCommandeFournisseur> ligneCommandeFournisseurs, StatutCommande statutCommande);
}