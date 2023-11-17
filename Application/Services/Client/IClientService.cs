using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Client;

public interface IClientService
{
    public Task<BaseResponse> AjouterUnClient(ClientRequest clientRequest);
    public BaseResponse ListerLesClients();
}