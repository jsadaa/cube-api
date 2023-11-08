namespace ApiCube.Application.DTOs.Responses;

public class FournisseurResponse
{
    public int Id { get; set; }

    public required string Nom { get; set; }

    public required AdresseResponse Adresse { get; set; }

    public required string Telephone { get; set; }

    public required string Email { get; set; }
}