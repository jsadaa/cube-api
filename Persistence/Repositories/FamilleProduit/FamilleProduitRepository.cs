using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Factories;
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
    
    public int Ajouter(FamilleProduitRequestDTO familleProduitRequestDTO)
    {
        var nouvelleFamilleProduit = _mapper.Map<Domain.Entities.FamilleProduit>(familleProduitRequestDTO);
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
    
    public Domain.Entities.FamilleProduit? Trouver(int id)
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
    
    public int? Modifier(int id, FamilleProduitRequestDTO familleProduitRequest)
    {
        var familleProduitModel = _context.FamillesProduits.Find(id);
        if (familleProduitModel == null) throw new FamilleProduitIntrouvable();
        
        var familleProduit = _mapper.Map<Domain.Entities.FamilleProduit>(familleProduitModel);
        familleProduit.MettreAJour(familleProduitRequest.Nom, familleProduitRequest.Description);
        
        _context.FamillesProduits.Update(_mapper.Map<FamilleProduitModel>(familleProduit));
        
        return familleProduitModel.Id;
    }
    
    public void Supprimer(int id)
    {
        FamilleProduitModel? familleProduitModel = _context.FamillesProduits.Find(id);
        if (familleProduitModel == null) throw new FamilleProduitIntrouvable();
        
        _context.FamillesProduits.Remove(familleProduitModel);
        _context.SaveChanges();
    }
}