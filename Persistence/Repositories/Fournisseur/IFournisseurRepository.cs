using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.Fournisseur;

public interface IFournisseurRepository
{
    public int Ajouter(AjouterFournisseurRequest fournisseurRequest);
    public List<FournisseurDTO> Lister();
    public FournisseurDTO? Trouver(int id);
    public FournisseurDTO? Trouver(string nom);
    public int? Modifier(int id, AjouterFournisseurRequest fournisseurRequest);
    public void Supprimer(int id);
}