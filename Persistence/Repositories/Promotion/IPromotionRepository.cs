namespace ApiCube.Persistence.Repositories.Promotion;

public interface IPromotionRepository
{
    public void Ajouter(Domain.Entities.Promotion promotion);
    public Domain.Entities.Promotion Trouver(int id);
    public List<Domain.Entities.Promotion> Lister();
    public void Modifier(Domain.Entities.Promotion promotion);
    public void Supprimer(Domain.Entities.Promotion promotion);
}