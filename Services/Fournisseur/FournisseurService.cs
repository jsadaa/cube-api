using System.Net;
using ApiCube.Domain.Factories;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Repositories.Fournisseur;

namespace ApiCube.Services.Fournisseur;

public class FournisseurService : IFournisseurService
{
    
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly FournisseurFactory _fournisseurFactory;
    
    public FournisseurService(IFournisseurRepository fournisseurRepository, FournisseurFactory fournisseurFactory)
    {
        _fournisseurRepository = fournisseurRepository;
        _fournisseurFactory = fournisseurFactory;
    }
    
    public BaseResponse AjouterUnFournisseur(AjouterFournisseurRequest fournisseurRequest)
    {
        try
        {
            Domain.Entities.Fournisseur nouveauFournisseur = _fournisseurFactory.CreerFournisseur(fournisseurRequest);
            _fournisseurRepository.Ajouter(nouveauFournisseur.ToRequestDTO());

            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Fournisseur ajouté avec succès" }
            );
            
            return response;
        }
        catch (Exception e)
        {
            BaseResponse response = new BaseResponse(
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
            List<FournisseurDTO> fournisseurs = _fournisseurRepository.Lister();
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { fournisseurs }
            );
            
            return response;
        }
        catch (Exception e)
        {
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
                );
            
            return response;
        }
    }
}