using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Entities;

public class CommandeFournisseur
{
    public CommandeFournisseur(DateTime dateCommande, DateTime? dateReception, Fournisseur fournisseur,
        StatutCommande statut, Employe employe)
    {
        DateCommande = dateCommande;
        DateReception = dateReception;
        Fournisseur = fournisseur;
        Statut = statut;
        Employe = employe;
    }

    public CommandeFournisseur(int id, DateTime dateCommande, DateTime? dateReception, Fournisseur fournisseur,
        StatutCommande statut, Employe employe)
    {
        Id = id;
        DateCommande = dateCommande;
        DateReception = dateReception;
        Fournisseur = fournisseur;
        Statut = statut;
        Employe = employe;
    }

    public CommandeFournisseur(int id, DateTime dateCommande, DateTime? dateReception, Fournisseur fournisseur,
        ICollection<LigneCommandeFournisseur> ligneCommandeFournisseurs, StatutCommande statut, Employe employe)
    {
        Id = id;
        DateCommande = dateCommande;
        DateReception = dateReception;
        Fournisseur = fournisseur;
        LigneCommandeFournisseurs = ligneCommandeFournisseurs;
        Statut = statut;
        Employe = employe;
    }

    public int Id { get; set; }
    public DateTime DateCommande { get; set; }
    public DateTime? DateReception { get; set; }
    public Fournisseur Fournisseur { get; set; }

    public ICollection<LigneCommandeFournisseur> LigneCommandeFournisseurs { get; set; } =
        new List<LigneCommandeFournisseur>();

    public StatutCommande Statut { get; set; }
    public Employe Employe { get; set; }

    public void MettreAJour(DateTime dateCommande, DateTime? dateReception, Fournisseur fournisseur,
        StatutCommande statut, Employe employe)
    {
        DateCommande = dateCommande;
        DateReception = dateReception;
        Fournisseur = fournisseur;
        Statut = statut;
        Employe = employe;
    }

    public void MettreAJour(DateTime dateCommande, Fournisseur fournisseur, StatutCommande statut, Employe employe)
    {
        DateCommande = dateCommande;
        Fournisseur = fournisseur;
        Statut = statut;
        Employe = employe;
    }

    public void AjouterLigneCommandeFournisseur(LigneCommandeFournisseur ligneCommandeFournisseur)
    {
        LigneCommandeFournisseurs.Add(ligneCommandeFournisseur);
    }

    public void SupprimerLigneCommandeFournisseur(LigneCommandeFournisseur ligneCommandeFournisseur)
    {
        LigneCommandeFournisseurs.Remove(ligneCommandeFournisseur);
    }

    public void ViderLignesCommande()
    {
        LigneCommandeFournisseurs.Clear();
    }

    public void VerifierValiditeStatut()
    {
        if (Statut is StatutCommande.Annulee or StatutCommande.Livree) throw new StatutCommandeInvalide();
    }

    public void VerifierValiditeDateCommande()
    {
        if (DateCommande > DateTime.Now) throw new DateCommandeInvalide();
    }

    public void VerifierValidite()
    {
        VerifierValiditeStatut();
        VerifierValiditeDateCommande();
    }

    public void Annuler()
    {
        Statut = StatutCommande.Annulee;
    }

    public void Livrer()
    {
        Statut = StatutCommande.Livree;
    }
}