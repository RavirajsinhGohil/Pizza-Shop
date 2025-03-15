using Repository.Models;
using Repository.ViewModel;

namespace Service.Interfaces;

public interface IUserService
{
    bool ResetPassword(string email, string newPassword, string confirmPassword, out string message);

    UserPaginationViewModel GetUsers(string searchTerm, int page, int pageSize, string sortBy, string sortOrder);

    Task<bool> AddUser(AddUserViewModel model);

    EditUserViewModel GetUserForEdit(int id);

    bool EditUser(int Userid, EditUserViewModel model);

    bool DeleteUser(int id);

    string GetEmailFromToken(string token);

    UserViewModel? GetUserProfile(string email);

    bool UpdateUserProfile(string email, UserViewModel model);

    Task<List<PermissionsViewModel>> GetPermissionsByRoleAsync(string roleName);

    string? ChangePassword(string email, ProfileChangePasswordViewModel model);

    Task<bool> UpdateRolePermissionsAsync(List<PermissionsViewModel> permissions);
}
