using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.Fournisseur;

public interface IFournisseurRepository
{
    public int Ajouter(FournisseurRequestDTO fournisseurRequestDTO);
    public List<Domain.Entities.Fournisseur> Lister();
    public Domain.Entities.Fournisseur? Trouver(int id);
    public Domain.Entities.Fournisseur? Trouver(string nom);
    public int? Modifier(int id, FournisseurRequestDTO fournisseurRequest);
    public void Supprimer(int id);
}