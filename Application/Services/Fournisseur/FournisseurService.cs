using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Factories;
using ApiCube.Persistence.Repositories.Fournisseur;
using AutoMapper;

namespace ApiCube.Application.Services.Fournisseur;

public class FournisseurService : IFournisseurService
{
    
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly FournisseurFactory _fournisseurFactory;
    private readonly IMapper _mapper;
    
    public FournisseurService(IFournisseurRepository fournisseurRepository, FournisseurFactory fournisseurFactory, IMapper mapper)
    {
        _fournisseurRepository = fournisseurRepository;
        _fournisseurFactory = fournisseurFactory;
        _mapper = mapper;
    }
    
    public BaseResponse AjouterUnFournisseur(FournisseurRequestDTO fournisseurRequestDTO)
    {
        try
        {
            var nouveauFournisseur = _fournisseurFactory.Creer(fournisseurRequestDTO);
            _fournisseurRepository.Ajouter(nouveauFournisseur);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Fournisseur ajouté avec succès" }
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
    
    public BaseResponse ListerLesFournisseurs()
    {
        try
        {
            var listeFournisseurs = _fournisseurRepository.Lister();
            var fournisseurs = _mapper.Map<List<FournisseurResponseDTO>>(listeFournisseurs);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { fournisseurs }
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