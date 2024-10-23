using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINSHARK2.Models
{
    [Table("AppUser")]

    public class AppUser : IdentityUser
    {
        public List<Portfolio> portfolios { get; set; } = new List<Portfolio>();
    }
}
