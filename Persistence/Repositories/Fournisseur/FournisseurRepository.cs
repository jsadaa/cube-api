using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Fournisseur;

public class FournisseurRepository : IFournisseurRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public FournisseurRepository(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.Fournisseur nouveauFournisseur)
    {
        var nouveauFournisseurModel = _mapper.Map<FournisseurModel>(nouveauFournisseur);
        _context.Fournisseurs.Add(nouveauFournisseurModel);
        _context.SaveChanges();
    }

    public List<Domain.Entities.Fournisseur> Lister()
    {
        var fournisseursModels = _context.Fournisseurs.AsNoTracking().ToList();
        return fournisseursModels.Select(fournisseurModel => _mapper.Map<Domain.Entities.Fournisseur>(fournisseurModel))
            .ToList();
    }

    public Domain.Entities.Fournisseur Trouver(int id)
    {
        var fournisseurModel = _context.Fournisseurs.AsNoTracking().FirstOrDefault(fournisseur => fournisseur.Id == id);
        if (fournisseurModel == null) throw new FournisseurIntrouvable();
        return _mapper.Map<Domain.Entities.Fournisseur>(fournisseurModel);
    }

    public Domain.Entities.Fournisseur Trouver(string nom)
    {
        var fournisseurModel =
            _context.Fournisseurs.AsNoTracking().FirstOrDefault(fournisseur => fournisseur.Nom == nom);
        if (fournisseurModel == null) throw new FournisseurIntrouvable();

        return _mapper.Map<Domain.Entities.Fournisseur>(fournisseurModel);
    }

    public void Modifier(Domain.Entities.Fournisseur fournisseurModifie)
    {
        var fournisseurModel = _mapper.Map<FournisseurModel>(fournisseurModifie);

        _context.Fournisseurs.Update(fournisseurModel);
        _context.SaveChanges();
    }


    public void Supprimer(Domain.Entities.Fournisseur fournisseurASupprimer)
    {
        var fournisseur = _mapper.Map<FournisseurModel>(fournisseurASupprimer);

        _context.Fournisseurs.Remove(fournisseur);
        _context.SaveChanges();
    }
}