using Read_Planet.Models;

namespace Read_Planet.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(AppUser appUser, IEnumerable<string> roles);
    }
}
