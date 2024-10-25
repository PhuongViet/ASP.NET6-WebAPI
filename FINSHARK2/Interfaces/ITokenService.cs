using FINSHARK2.Models;

namespace FINSHARK2.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
