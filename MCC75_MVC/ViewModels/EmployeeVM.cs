using System.ComponentModel.DataAnnotations;

namespace MCC75_MVC.ViewModels;

public class EmployeeVM
{
    [MaxLength(5), MinLength(5, ErrorMessage = "Inputan Harus 5 angka, ex: 10115")]
    [Required(ErrorMessage = "Inputan Harus 5 angka, ex: 10115")]
    public string NIK { get; set; }
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }
    public DateTime Birthdate { get; set; }
    [Required(ErrorMessage = "Select Your Gender")]
    public GenderEnum Gender { get; set; }
    [Display(Name = "Hiring Date")]
    public DateTime HiringDate { get; set; } = DateTime.Now;
    [EmailAddress]
    public string Email { get; set; }
    [Display(Name = "Phone Number")]
    [Phone]
    public string? PhoneNumber { get; set; }


}

