using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Factories;
using ApiCube.Persistence.Repositories.Produit;
using AutoMapper;

namespace ApiCube.Application.Services.Produit;

public class ProduitService : IProduitService
{
    private readonly IProduitRepository _produitRepository;
    private readonly ProduitFactory _produitFactory;
    private readonly IMapper _mapper;
    
    public ProduitService(IProduitRepository produitRepository, ProduitFactory produitFactory, IMapper mapper)
    {
        _produitRepository = produitRepository;
        _produitFactory = produitFactory;
        _mapper = mapper;
    }

    public BaseResponse AjouterUnProduitAuCatalogue(ProduitRequestDTO produitRequestDTO)
    {
        try
        {
            var nouveauProduit = _produitFactory.Creer(produitRequestDTO);
            _produitRepository.Ajouter(nouveauProduit);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Produit ajouté au stock avec succès" }
            );
            
            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
    
    public BaseResponse ListerLesProduits()
    {
        try
        {
            var listeProduits = _produitRepository.Lister();
            var produits = _mapper.Map<List<ProduitResponseDTO>>(listeProduits);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { produits }
            );
            
            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
}