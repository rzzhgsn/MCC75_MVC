using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCC75_MVC.Models;

[Table("tb_m_educations")]

public class Education
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("major"), MaxLength(100)]
    public string Major { get; set; }
    [Required, Column("degree", TypeName = "nchar(2)")]
    public string Degree { get; set; }
    [Required, Column("gpa")]
    public float GPA {get; set;}
    [Required, Column("university_id")]
    public int UniversityId { get; set; }

    //Relasi/Relation(yg ada ForeignKey & // Cardinality(yg public dibawahnya
    //One 
    [ForeignKey(nameof(UniversityId))] //--> sama dengan [ForeignKey("UniversityId)]
    public University? University { get; set; }
    // many
    public ICollection<Profilling>?Profillings { get; set; }
}
