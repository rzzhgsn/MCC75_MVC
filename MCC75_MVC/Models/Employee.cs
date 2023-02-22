using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCC75_MVC.Models;

[Table("tb_m_employess")]

public class Employee
{ 
    [Key, Column("nik", TypeName = "nchar(5)")]
    public string NIK { get; set; }
    [Required, Column("first_name"), MaxLength(50)]
    public string FirstName { get; set; }
    [Column("last_name"), MaxLength(50)]
    public string? LastName { get; set; }
    [Required, Column("birthdate")]
    public DateTime Birthdate { get; set; }
    [Required, Column("gender")]
    public GenderEnum Gender { get; set; }
    [Required, Column("hiring_date")]
    public DateTime HiringDate { get; set; } = DateTime.Now;
    [Required, Column("email"), MaxLength(50)]
    public string Email { get; set; }
    [Column("phone_number"), MaxLength(20)]
    public string? PhoneNumber { get; set; }


    // Cardinality
    public ICollection<Profilling>? Profillings { get; set; }
    public Account? Account { get; set; }

    public enum GenderEnum
    {
        Male,
        Female
    }
}
