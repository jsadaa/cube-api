using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Persistence.Repositories.FamilleProduit;

public class FamilleProduitRepository : IFamilleProduitRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    
    public FamilleProduitRepository(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public int Ajouter(Domain.Entities.FamilleProduit nouvelleFamilleProduit)
    {
        var nouvelleFamilleProduitModel = _mapper.Map<FamilleProduitModel>(nouvelleFamilleProduit);
        
        _context.FamillesProduits.Add(nouvelleFamilleProduitModel);
        _context.SaveChanges();
        
        return nouvelleFamilleProduitModel.Id;
    }
    
    public List<Domain.Entities.FamilleProduit> Lister()
    {
        var famillesProduitModels = _context.FamillesProduits.ToList();
        var famillesProduits = _mapper.Map<List<Domain.Entities.FamilleProduit>>(famillesProduitModels);
        
        return famillesProduits;
    }
    
    public Domain.Entities.FamilleProduit Trouver(int id)
    {
        var familleProduitModel = _context.FamillesProduits.Find(id);
        if (familleProduitModel == null) throw new FamilleProduitIntrouvable();
        
        return _mapper.Map<Domain.Entities.FamilleProduit>(familleProduitModel);
    }
    
    public Domain.Entities.FamilleProduit Trouver(string nom)
    {
        var familleProduitModel = _context.FamillesProduits.FirstOrDefault(familleProduit => familleProduit.Nom == nom);
        if (familleProduitModel == null) throw new FamilleProduitIntrouvable();
        
        return _mapper.Map<Domain.Entities.FamilleProduit>(familleProduitModel);
    }
    
    public void Modifier(Domain.Entities.FamilleProduit familleProduitModifiee)
    {
        var familleProduitModel = _mapper.Map<FamilleProduitModel>(familleProduitModifiee);
        
        _context.FamillesProduits.Update(familleProduitModel);
        _context.SaveChanges();
    }
    
    
    public void Supprimer(Domain.Entities.FamilleProduit familleProduitASupprimer)
    {
        var familleProduitModel = _mapper.Map<FamilleProduitModel>(familleProduitASupprimer);
        
        _context.FamillesProduits.Remove(familleProduitModel);
        _context.SaveChanges();
    }
}