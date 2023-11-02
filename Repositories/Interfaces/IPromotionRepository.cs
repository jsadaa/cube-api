using ApiCube.Domain.Entities;
using ApiCube.DTOs.Requests;

namespace ApiCube.Repositories.Interfaces;

public interface IPromotionRepository
{
    public void Ajouter(Promotion promotion);
    public AjouterPromotionRequest? Trouver(int id);
    public List<AjouterPromotionRequest> Lister();
    public void Modifier(int id, Promotion promotion);
    public void Supprimer(int id);
}