namespace ApiCube.Application.DTOs.Responses;

public class FournisseurResponseDTO
{
    public int Id { get; set; }

    public required string Nom { get; set; }

    public required AdresseDTO Adresse { get; set; }

    public required string Telephone { get; set; }

    public required string Email { get; set; }
}