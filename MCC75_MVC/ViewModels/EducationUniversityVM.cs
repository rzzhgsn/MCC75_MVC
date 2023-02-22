using System.ComponentModel.DataAnnotations;

namespace MCC75_MVC.ViewModels;

public class EducationUniversityVM
{
    public int Id { get; set; } 
    public string Major { get; set; }
    [MaxLength(2), MinLength(2, ErrorMessage = "ex: S1/D3")]
    [Required(ErrorMessage = "ex: D3/S1")]
    public string Degree { get; set; }
    [Range (0, 4, ErrorMessage = "Inputan Harus Lebih dari {1} dan Kurang dari {2}")]
    public float GPA { get; set; }
    [Display (Name = "University Name")] //display utk mengubah nama
    public string UniversityName { get; set; }
}
