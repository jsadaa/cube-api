using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.CommandeFournisseur;

public class CommandeFournisseurMapper : ICommandeFournisseurMapper
{
    public Entities.CommandeFournisseur Mapper(
        CommandeFournisseurModel commandeFournisseurModel,
        Entities.Fournisseur fournisseur,
        Entities.Employe employe,
        List<Entities.LigneCommandeFournisseur> ligneCommandeFournisseurs,
        StatutCommande statutCommande
    )
    {
        return new Entities.CommandeFournisseur(
            commandeFournisseurModel.Id,
            commandeFournisseurModel.DateCommande,
            commandeFournisseurModel.DateReception,
            fournisseur,
            ligneCommandeFournisseurs,
            statutCommande,
            employe
        );
    }
}