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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #endregion


        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var user = new ApplicationUser()
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.Email,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                EmailConfirmed = true
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

        public async Task AddRoleWithEmailUserAsync(AddRoleWithEmailUserDTO addRoleWithEmailUserDTO)
        {
            IdentityResult roleResult;
            bool adminRoleExists = await _roleManager.RoleExistsAsync(addRoleWithEmailUserDTO.Role);
            if (!adminRoleExists)
            {
                roleResult = await _roleManager.CreateAsync(new IdentityRole(addRoleWithEmailUserDTO.Role));
            }

            // Select the user, and then add the admin role to the user
            var user = await GetUserWithEmailAsync(addRoleWithEmailUserDTO.EmailUser);
            if (!await _userManager.IsInRoleAsync(user, addRoleWithEmailUserDTO.Role))
            {
                var userResult = await _userManager.AddToRoleAsync(user, addRoleWithEmailUserDTO.Role);
            }
        }

        public async Task<bool> CheckUserWithEmailAsync(CheckUserWithEmailDTO checkUserWithEmailDTO)
        {
            var user = await GetUserWithEmailAsync(checkUserWithEmailDTO.Email);
            return await _userManager.CheckPasswordAsync(user, checkUserWithEmailDTO.Password);
        }

        public async Task<IList<string>> GetRolesWithEmailAsync(string email)
        {
            var user = await GetUserWithEmailAsync(email);
            var listRoles = await _userManager.GetRolesAsync(user);
            return listRoles;
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
