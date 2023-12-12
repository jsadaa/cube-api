namespace ApiCube.Persistence.Repositories.LigneCommandeFournisseur;

public interface ILigneCommandeFournisseurRepository
{
    public void Ajouter(Domain.Entities.LigneCommandeFournisseur ligneCommandeFournisseur);
    public Domain.Entities.LigneCommandeFournisseur Trouver(int id);
    public List<Domain.Entities.LigneCommandeFournisseur> Lister();
    public void Modifier(Domain.Entities.LigneCommandeFournisseur ligneCommandeFournisseur);
    public void Supprimer(Domain.Entities.LigneCommandeFournisseur ligneCommandeFournisseur);
}