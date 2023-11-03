using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.Promotion;

public interface IPromotionRepository
{
    public int Ajouter(AjouterPromotionRequest promotion);
    public PromotionDTO? Trouver(int id);
    public List<PromotionDTO> Lister();
    public int? Modifier(int id, AjouterPromotionRequest promotion);
    public void Supprimer(int id);
}