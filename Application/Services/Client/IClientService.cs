using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Client;

public interface IClientService
{
    public Task<BaseResponse> AjouterUnClient(ClientRequest clientRequest);
    public BaseResponse ListerLesClients();
    public BaseResponse TrouverUnClient(int id);
    public Task<BaseResponse> ModifierUnClient(int id, ClientUpdate clientUpdate);
    public Task<BaseResponse> SupprimerUnClient(int id);
}