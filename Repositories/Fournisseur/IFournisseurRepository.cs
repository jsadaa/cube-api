using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Fournisseur;

public interface IFournisseurRepository
{
    public int Ajouter(AjouterFournisseurRequest fournisseurRequest);
    public List<FournisseurDTO> Lister();
    public FournisseurDTO? Trouver(int id);
    public int? Modifier(int id, AjouterFournisseurRequest fournisseurRequest);
    public void Supprimer(int id);
}