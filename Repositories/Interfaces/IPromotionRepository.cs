using ApiCube.Domain.Entities;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Interfaces;

public interface IPromotionRepository
{
    public void Ajouter(AjouterPromotionRequest promotion);
    public PromotionDTO? Trouver(int id);
    public List<PromotionDTO> Lister();
    public void Modifier(int id, AjouterPromotionRequest promotion);
    public void Supprimer(int id);
}