namespace ApiCube.Application.DTOs.Responses;

public class AdresseDTO
{
    public required string Rue { get; set; } = string.Empty;

    public required string CodePostal { get; set; } = string.Empty;

    public required string Ville { get; set; } = string.Empty;

    public required string Pays { get; set; } = string.Empty;
}