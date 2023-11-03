using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Promotion;

public interface IPromotionRepository
{
    public int Ajouter(AjouterPromotionRequest promotion);
    public PromotionDTO? Trouver(int id);
    public List<PromotionDTO> Lister();
    public int? Modifier(int id, AjouterPromotionRequest promotion);
    public void Supprimer(int id);
}