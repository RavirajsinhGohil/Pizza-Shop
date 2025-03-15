using Repository.Models;
using Repository.ViewModel;

namespace Repository.Interfaces;

public interface IUserRepository
{
    User CheckUserEmailAndPassword(string email, string password);

    User GetUserByEmail(string email);
    User GetUserById(int id);

    IQueryable<User> GetUsers();

    bool UpdateUser(User user);

    IQueryable<User> GetUsersQuery();
    Role GetRoleById(int id);
    void AddUser(User user);

    void DeleteUser(User user);

    Task<List<PermissionsViewModel>> GetPermissionsByRoleAsync(string roleName);

    Task<bool> UpdateRolePermissionsAsync(List<PermissionsViewModel> permissions);

}
