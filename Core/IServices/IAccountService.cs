using Core.Data.Dto.Account;

namespace Core.IServices
{
    public interface IAccountService
    {
        /// <summary>
        /// Register a user with specified information
        /// </summary>
        /// <param name="userDto">User information</param>
        /// <returns>User info with JWT</returns>
        public Task<UserResponseDto> RegisterUserAsync(RegisterUserDto userDto);

        /// <summary>
        /// Login user with specified information
        /// </summary>
        /// <param name="userDto">User information</param>
        /// <returns>User info with JWT</returns>
        public Task<UserResponseDto> LoginUserAsync(LoginUserDto userDto);
    }
}
