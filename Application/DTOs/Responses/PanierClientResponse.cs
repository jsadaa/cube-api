using Microsoft.Build.Framework;

namespace ApiCube.Application.DTOs.Responses;

public class PanierClientResponse
{
    [Required] public int Id { get; set; }
    [Required] public ClientResponse Client { get; set; }
    [Required] public double Total { get; set; }
    [Required] public ICollection<LignePanierClientResponse> LignePanierClients { get; set; }
}