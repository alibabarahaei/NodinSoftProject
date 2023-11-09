using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NodinSoftProject.Application.DTOs.Account;
using NodinSoftProject.Domain.Models.User;

namespace NodinSoftProject.Application.InterfaceService
{
    public interface IUserService : IDisposable
    {

        Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO);

        Task<IdentityUser> IsUserNameInUseAsync(string userName);

        Task<SignInResult> LoginUserAsync(LoginUserDTO loginUserDTO);

        Task<ApplicationUser> GetUserWithUserIdAsync(string userId);

        Task<string> GetUserNameWithUserIdAsync(string userId);

        Task<IdentityResult> ChangePasswordAsync(ChangepasswordDTO changepasswordDTO);

        Task<string> GetEmailConfirmationTokenAsync(string email);

        Task<ApplicationUser> GetUserWithEmailAsync(string email);

        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);

        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code);

        Task AddRoleWithEmailUserAsync(AddRoleWithEmailUserDTO addRoleWithEmailUserDTO);

        Task<bool> CheckUserWithEmailAsync(CheckUserWithEmailDTO checkUserWithEmailDTO);

        Task<IList<string>> GetRolesWithEmailAsync(string email);

        Task SignOutAsync();
    }
}
