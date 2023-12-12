namespace ApiCube.Persistence.Repositories.CommandeFournisseur;

public interface ICommandeFournisseurRepository
{
    public void Ajouter(Domain.Entities.CommandeFournisseur nouvelleCommandeFournisseur);
    public List<Domain.Entities.CommandeFournisseur> Lister();
    public Domain.Entities.CommandeFournisseur Trouver(int id);
    public void Modifier(Domain.Entities.CommandeFournisseur commandeFournisseur);
    public void Supprimer(Domain.Entities.CommandeFournisseur commandeFournisseur);
}