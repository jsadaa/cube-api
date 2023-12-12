using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Mappers.CommandeFournisseur;

public class CommandeFournisseurMapper : ICommandeFournisseurMapper
{
    public Entities.CommandeFournisseur Mapper(
        Persistence.Models.CommandeFournisseurModel commandeFournisseurModel, 
        Entities.Fournisseur fournisseur, 
        Entities.Employe employe, 
        List<Entities.LigneCommandeFournisseur> ligneCommandeFournisseurs,
        StatutCommande statutCommande
    )
    {
        return new Entities.CommandeFournisseur(
            id: commandeFournisseurModel.Id,
            dateCommande: commandeFournisseurModel.DateCommande,
            dateReception: commandeFournisseurModel.DateReception,
            fournisseur: fournisseur,
            ligneCommandeFournisseurs: ligneCommandeFournisseurs,
            statut: statutCommande,
            employe: employe
        );
    }
}