using ApiCube.Domain.Factories;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Persistence.Repositories.Fournisseur;

public class FournisseurRepository : IFournisseurRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly FournisseurFactory _fournisseurFactory;
    
    public FournisseurRepository(ApiDbContext context, IMapper mapper, FournisseurFactory fournisseurFactory)
    {
        _context = context;
        _mapper = mapper;
        _fournisseurFactory = fournisseurFactory;
    }
    
    public int Ajouter(Domain.Entities.Fournisseur nouveauFournisseur)
    {
        var nouveauFournisseurModel = _mapper.Map<FournisseurModel>(nouveauFournisseur);
        
        _context.Fournisseurs.Add(nouveauFournisseurModel);
        _context.SaveChanges();
        
        return nouveauFournisseurModel.Id;
    }
    
    public List<Domain.Entities.Fournisseur> Lister()
    {
        var fournisseursModels = _context.Fournisseurs.ToList();

        return fournisseursModels.Select(fournisseurModel => _fournisseurFactory.Mapper(fournisseurModel)).ToList();
    }
    
    public Domain.Entities.Fournisseur Trouver(int id)
    {
        var fournisseurModel = _context.Fournisseurs.Find(id);
        if (fournisseurModel == null) throw new FournisseurIntrouvable();
        
        return _fournisseurFactory.Mapper(fournisseurModel);
    }
    
    public Domain.Entities.Fournisseur Trouver(string nom)
    {
        var fournisseurModel = _context.Fournisseurs.FirstOrDefault(fournisseur => fournisseur.Nom == nom);
        if (fournisseurModel == null) throw new FournisseurIntrouvable();
        
        return _fournisseurFactory.Mapper(fournisseurModel);
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