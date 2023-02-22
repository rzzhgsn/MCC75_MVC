using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCC75_MVC.Models;

[Table("tb_tr_profillings")]

public class Profilling
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("employee_nik", TypeName = "nchar(5)")]
    [Display(Name = "Employee Name")]
    public string EmployeeNIK { get; set; }
    [Required, Column("employee_id")]
    [Display(Name = "Education Name")]
    public int EducationId { get; set; }

    // Relation // Cardinality
    [ForeignKey(nameof(EducationId))]
    public Education? Educations { get; set; }
    [ForeignKey(nameof(EmployeeNIK))]
    public Employee? Employee { get; set; }
}
