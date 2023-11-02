using System.ComponentModel.DataAnnotations;

namespace ApiCube.DTOs.Requests;

public class CreateStudentRequest
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string ContactNumber { get; set; }
}