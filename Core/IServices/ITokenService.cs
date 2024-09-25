using Microsoft.AspNetCore.Identity;

namespace Core.IServices
{
    public interface ITokenService
    {
        /// <summary>
        /// Creates a JWT for user
        /// </summary>
        /// <param name="user">User info</param>
        /// <returns>JWT</returns>
        public string CreateToken(IdentityUser user);
    }
}
