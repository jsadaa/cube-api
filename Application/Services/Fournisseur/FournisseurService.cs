using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Mappers;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Persistence.Repositories.Fournisseur;
using AutoMapper;

namespace ApiCube.Application.Services.Fournisseur;

public class FournisseurService : IFournisseurService
{
    
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly IMapper _mapper;
    
    public FournisseurService(IFournisseurRepository fournisseurRepository, IFournisseurMapper fournisseurMapper, IMapper mapper)
    {
        _fournisseurRepository = fournisseurRepository;
        _fournisseurMapper = fournisseurMapper;
        _mapper = mapper;
    }
    
    public BaseResponse AjouterUnFournisseur(FournisseurRequestDTO fournisseurRequestDTO)
    {
        try
        {
            var nouveauFournisseur = _fournisseurMapper.Mapper(fournisseurRequestDTO);
                
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
                data: fournisseurs 
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