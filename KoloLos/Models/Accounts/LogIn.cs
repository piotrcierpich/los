using System.ComponentModel.DataAnnotations;

namespace KoloLos.Models.Accounts
{
  public class LogIn
  {
    [Required]
    [Display(Name = "Login")]
    public string Login { get; set; }

    [Required]
    [Display(Name = "Hasło")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Pamiętaj mnie")]
    public bool RememberMe { get; set; }
  }
}