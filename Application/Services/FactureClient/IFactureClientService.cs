using ApiCube.Application.DTOs;

namespace ApiCube.Application.Services.FactureClient;

public interface IFactureClientService
{
    public BaseResponse TrouverUneFacture(int id);
    public BaseResponse TrouverUneFactureParCommande(int id);
    public BaseResponse ListerLesFactures();
    public BaseResponse MarquerUneFactureCommePayee(int id);
    public BaseResponse ListerLesFacturesDUnClient(int id);
}