using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class EmployeRequest
{
    [Required] public required string Nom { get; set; }

    [Required] public required string Prenom { get; set; }

    [Required] public required string Email { get; set; }

    [Required] public required string Password { get; set; }

    [Required] public required DateTime DateEmbauche { get; set; }

    [Required] public required string Poste { get; set; }
}