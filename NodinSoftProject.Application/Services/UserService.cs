using Microsoft.AspNetCore.Identity;
using NodinSoftProject.Application.DTOs.Account;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Domain.Models.User;

namespace NodinSoftProject.Application.Services
{
    public class UserService : IUserService
    {
        #region constructor
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion


        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var user = new ApplicationUser()
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.Email,
            };
            var IdentityResult = await _userManager.CreateAsync(user, registerUserDTO.Password);
            return IdentityResult;

        }

        public async Task<IdentityUser> IsUserNameInUseAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }


        public async Task<SignInResult> LoginUserAsync(LoginUserDTO loginUserDTO)
        {

            var result = await _signInManager.PasswordSignInAsync(loginUserDTO.UserName, loginUserDTO.Password, loginUserDTO.RememberMe, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginUserDTO.UserName);
                await _signInManager.SignInAsync(user, loginUserDTO.RememberMe);
            }
            return result;
        }



        public async Task LogOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }





        public async Task<ApplicationUser> GetUserWithUserIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }



        public async Task<string> GetUserNameWithUserIdAsync(string userId)
        {
            var user = await GetUserWithUserIdAsync(userId);
            return user.UserName;
        }




        public async Task<IdentityResult> ChangePasswordAsync(ChangepasswordDTO changepasswordDTO)
        {
            var user = await GetUserWithUserIdAsync(changepasswordDTO.UserId);
            return await _userManager.ChangePasswordAsync(user, changepasswordDTO.CurrentPassword, changepasswordDTO.NewPassword);
        }



        public async Task<string> GetEmailConfirmationTokenAsync(string email)
        {
            var user = await GetUserWithEmailAsync(email);
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return emailConfirmationToken;
        }



        public async Task<ApplicationUser> GetUserWithEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }



        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {

            return await _userManager.UpdateAsync(user);
        }



        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            return await _userManager.ConfirmEmailAsync(user, code);

        }



        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public void Dispose()
        {
            _userManager.Dispose();
            
        }
    }
}
