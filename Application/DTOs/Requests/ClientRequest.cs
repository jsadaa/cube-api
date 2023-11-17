using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class ClientRequest
{
    [Required] public required string Nom { get; set; }

    [Required] public required string Prenom { get; set; }

    [Required] public required string Adresse { get; set; }

    [Required] public required string CodePostal { get; set; }

    [Required] public required string Ville { get; set; }

    [Required] public required string Pays { get; set; }

    [Required] public required string Telephone { get; set; }

    [Required] public required string Email { get; set; }

    [Required] public required string Password { get; set; }

    [Required] public DateTime DateNaissance { get; set; }
}