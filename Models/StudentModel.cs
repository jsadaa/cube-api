using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

[Table("Student")]
public class StudentModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    
    [Column("first_name")]
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    
    [Column("address")]
    [Required]
    [StringLength(100)]
    public string Address { get; set; }
    
    [Column("email")]
    [Required]
    [StringLength(50)]
    public string Email { get; set; }
    
    [Column("contact_number")]
    [Required]
    [StringLength(50)]
    public string ContactNumber { get; set; }
}