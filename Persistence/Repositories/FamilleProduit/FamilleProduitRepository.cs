using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Persistence.Repositories.FamilleProduit;

public class FamilleProduitRepository : IFamilleProduitRepository
{
    private readonly ApiDbContext _context;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IMapper _mapper;

    public FamilleProduitRepository(ApiDbContext context, IFamilleProduitMapper familleProduitMapper, IMapper mapper)
    {
        _context = context;
        _familleProduitMapper = familleProduitMapper;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.FamilleProduit nouvelleFamilleProduit)
    {
        var nouvelleFamilleProduitModel = _mapper.Map<FamilleProduitModel>(nouvelleFamilleProduit);

        _context.FamillesProduits.Add(nouvelleFamilleProduitModel);
        _context.SaveChanges();
    }

    public List<Domain.Entities.FamilleProduit> Lister()
    {
        var famillesProduitModels = _context.FamillesProduits.ToList();
        var famillesProduits = new List<Domain.Entities.FamilleProduit>();

        foreach (var familleProduitModel in famillesProduitModels)
        {
            var familleProduit = _familleProduitMapper.Mapper(familleProduitModel);
            famillesProduits.Add(familleProduit);
        }

        return famillesProduits;
    }

    public Domain.Entities.FamilleProduit Trouver(int id)
    {
        var familleProduitModel = _context.FamillesProduits.Find(id);
        if (familleProduitModel == null) throw new FamilleProduitIntrouvable();

        return _familleProduitMapper.Mapper(familleProduitModel);
    }

    public Domain.Entities.FamilleProduit Trouver(string nom)
    {
        var familleProduitModel = _context.FamillesProduits.FirstOrDefault(familleProduit => familleProduit.Nom == nom);
        if (familleProduitModel == null) throw new FamilleProduitIntrouvable();

        return _familleProduitMapper.Mapper(familleProduitModel);
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