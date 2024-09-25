using Core.Data.Dto.Account;
using Core.Exceptions;
using Core.IServices;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UserResponseDto> RegisterUserAsync(RegisterUserDto userDto)
        {
            var user = userDto.ToIdentityUser();

            await AddUserAsync(user, userDto.Password);

            var userResponse = new UserResponseDto()
            {
                Email = userDto.Email,
                Username = userDto.Username,
                Token = _tokenService.CreateToken(user)
            };

            return userResponse;
        }

        public async Task<UserResponseDto> LoginUserAsync(LoginUserDto userDto)
        {
            IdentityUser? user = await _userManager.FindByNameAsync(userDto.Username);

            if (user == null)
            {
                throw new InvalidLoginException("Username or password invalid.");
            }

            var signedIn = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

            if (!signedIn.Succeeded)
            {
                throw new InvalidLoginException("Username or password invalid.");
            }

            var userResponse = new UserResponseDto()
            {
                Email = user.Email!,
                Username = userDto.Username,
                Token = _tokenService.CreateToken(user)
            };

            return userResponse;
        }

        private async Task AddUserAsync(IdentityUser user, string password)
        {
            var userCreated = await _userManager.CreateAsync(user, password);

            if (!userCreated.Succeeded)
            {
                string err = string.Join(" | ", userCreated.Errors.Select(err => err.Description));

                throw new CreationFailedException(err);
            }

            var roleAssigned = await _userManager.AddToRoleAsync(user, "user");

            if (!roleAssigned.Succeeded)
            {
                string err = string.Join("\n", roleAssigned.Errors);

                throw new RoleAssignFailedException(err);
            }
        }
    }
}
