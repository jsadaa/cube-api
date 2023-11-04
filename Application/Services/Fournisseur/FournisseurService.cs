using System.Net;
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
            _fournisseurRepository.Ajouter(_mapper.Map<FournisseurRequestDTO>(nouveauFournisseur));
            
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
            var fournisseurs = _fournisseurRepository.Lister();
            var fournisseursResponse= _mapper.Map<List<FournisseurResponseDTO>>(fournisseurs);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { fournisseursResponse }
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