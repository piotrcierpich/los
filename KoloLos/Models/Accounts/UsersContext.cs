using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace KoloLos.Models.Accounts
{
  public class UsersContext : DbContext
  {
    public UsersContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
  }

  [Table("UserProfile")]
  public class UserProfile
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string UserName { get; set; }
  }
}