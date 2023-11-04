using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
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
    
    public int Ajouter(FournisseurRequestDTO fournisseurRequestDTO)
    {
        var nouveauFournisseur = _fournisseurFactory.Creer(fournisseurRequestDTO);
        var nouveauFournisseurModel = _mapper.Map<FournisseurModel>(nouveauFournisseur);
        
        _context.Fournisseurs.Add(nouveauFournisseurModel);
        _context.SaveChanges();
        
        return nouveauFournisseur.Id;
    }
    
    public List<Domain.Entities.Fournisseur> Lister()
    {
        var fournisseursModels = _context.Fournisseurs.ToList();

        return fournisseursModels.Select(fournisseurModel => _fournisseurFactory.Mapper(fournisseurModel)).ToList();
    }
    
    public Domain.Entities.Fournisseur? Trouver(int id)
    {
        var fournisseurModel = _context.Fournisseurs.Find(id);
        if (fournisseurModel == null) throw new FournisseurIntrouvable();
        
        return _fournisseurFactory.Mapper(fournisseurModel);
    }
    
    public Domain.Entities.Fournisseur? Trouver(string nom)
    {
        var fournisseurModel = _context.Fournisseurs.FirstOrDefault(fournisseur => fournisseur.Nom == nom);
        if (fournisseurModel == null) throw new FournisseurIntrouvable();
        
        return _fournisseurFactory.Mapper(fournisseurModel);
    }
    
    public int? Modifier(int id, FournisseurRequestDTO fournisseurRequest)
    {
        var fournisseurModel = _context.Fournisseurs.Find(id);
        if (fournisseurModel == null) throw new FournisseurIntrouvable();
        
        var fournisseur = _fournisseurFactory.Mapper(fournisseurModel);
        fournisseur.MettreAJour(fournisseurRequest.Nom, fournisseurRequest.Adresse, fournisseurRequest.Telephone, fournisseurRequest.Email);
        
        _context.Fournisseurs.Update(_mapper.Map<FournisseurModel>(fournisseur));
        _context.SaveChanges();
        
        return fournisseur.Id;
    }
    
    public void Supprimer(int id)
    {
        var fournisseur = _context.Fournisseurs.Find(id);
        if (fournisseur == null) throw new FournisseurIntrouvable();
        
        _context.Fournisseurs.Remove(fournisseur);
        _context.SaveChanges();
    }
}