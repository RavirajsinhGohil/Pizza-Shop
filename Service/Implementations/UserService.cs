using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interfaces;
using Repository.Models;
using Repository.ViewModel;
using Service.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ApplicationDbContext _dbo;
    // private readonly AuthService _authservice;

    public UserService(IUserRepository userRepository, ApplicationDbContext dbo)
    {
        _userRepository = userRepository;
        _dbo = dbo;
        // _authservice = authservice;
    }
    public bool ResetPassword(string email, string newPassword, string confirmPassword, out string message)
    {
        if (newPassword != confirmPassword)
        {
            message = "The new Password and Confirmation password do not match";
            return false;
        }
        var user = _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            message = "User not found";
            return false;
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        _userRepository.UpdateUser(user);
        message = "Password has been successfully updated";
        return true;
    }

    public UserPaginationViewModel GetUsers(string searchTerm, int page, int pageSize, string sortBy, string sortOrder)
    {
        var query = _userRepository.GetUsersQuery();

        // Apply search filter
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.Firstname.Contains(searchTerm) || u.Lastname.Contains(searchTerm) && u.Isdeleted == false);
        }

        // Sorting logic
        query = sortBy switch
        {
            "Name" => sortOrder == "asc"
                ? query.OrderBy(u => u.Firstname).ThenBy(u => u.Lastname)
                : query.OrderByDescending(u => u.Firstname).ThenByDescending(u => u.Lastname),

            "Role" => sortOrder == "asc"
                ? query.OrderBy(u => u.Role.Rolename)
                : query.OrderByDescending(u => u.Role.Rolename),

            _ => query.OrderBy(u => u.Userid) // Default sorting by ID
        };

        int totalItems = query.Count();
        var users = query.Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();

        return new UserPaginationViewModel
        {
            Users = users,
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            SortBy = sortBy,
            SortOrder = sortOrder
        };
    }

    public async Task<bool> AddUser(AddUserViewModel model)
    {
        var role = _userRepository.GetRoleById(model.RoleId);
        if (role == null)
        {
            return false;
        }

        Country countryname = _dbo.Countries.FirstOrDefault(c => c.Countryid.ToString() == model.Country);
        State statename = _dbo.States.FirstOrDefault(c => c.Stateid.ToString() == model.State);
        City cityname = _dbo.Cities.FirstOrDefault(c => c.Cityid.ToString() == model.City);

        model.Country = countryname?.Countryname;
        model.State = statename?.Statename;
        model.City = cityname?.Cityname;

        Role Rolename = _dbo.Roles.FirstOrDefault(r => r.Roleid == model.RoleId);

        model.Rolename = Rolename.ToString();
        Task<int> index = _dbo.Users.CountAsync();
        var totalUsers = await _userRepository.GetUsers().CountAsync();

        var user = new User
        {
            Userid = totalUsers + 1,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Email = model.Email,
            Username = model.Username,
            Phone = model.Phone,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Roleid = model.RoleId,
            Country = model.Country,
            States = model.State,
            City = model.City,
            Address = model.Address,
            Zipcode = model.Zipcode,
            Createdby = model.RoleId,
            Status = model.Status
        };

        if (model.ProfileImage != null)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ProfileImage.CopyToAsync(fileStream);
            }

            user.Profileimagepath = "/images/users/" + uniqueFileName;
        }


        _userRepository.AddUser(user);
        return true;
    }

    public EditUserViewModel GetUserForEdit(int Userid)
    {
        User user = _userRepository.GetUserById(Userid);

        Role rolename = _dbo.Roles.FirstOrDefault(r => r.Roleid == user.Roleid);


        if (user == null) return null;
        var status1 = user.Status;
        if (status1 == "1")
        {
            status1 = "Active";
        }
        else
        {
            status1 = "Inactive";
        }

        return new EditUserViewModel
        {
            UserId = user.Userid,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Email = user.Email,
            Username = user.Username,
            Phone = user.Phone,
            RoleId = user.Roleid,
            Rolename = rolename.Rolename,
            Country = user.Country,
            State = user.States,
            City = user.City,
            Address = user.Address,
            Zipcode = user.Zipcode,
            Status = status1
        };
    }

    public bool EditUser(int Userid, EditUserViewModel model)
    {
        User user = _userRepository.GetUserById(Userid);
        if (user == null) return false;

        if (model.Country == "1" || model.Country == "2" || model.Country == "3" || model.Country == "4" || model.Country == "5")
        {
            Country countryname = _dbo.Countries.FirstOrDefault(c => c.Countryid.ToString() == model.Country);
            State statename = _dbo.States.FirstOrDefault(c => c.Stateid.ToString() == model.State);
            City cityname = _dbo.Cities.FirstOrDefault(c => c.Cityid.ToString() == model.City);

            model.Country = countryname?.Countryname;
            model.State = statename?.Statename;
            model.City = cityname?.Cityname;
        }


        Role role = _dbo.Roles.FirstOrDefault(r => r.Rolename == model.Rolename);
        model.RoleId = role.Roleid;

        var status = model.Status;
        if (status == "1")
        {
            status = "Active";
        }
        else
        {
            status = "Inactive";
        }

        user.Firstname = model.Firstname;
        user.Lastname = model.Lastname;
        user.Email = model.Email;
        user.Username = model.Username;
        user.Phone = model.Phone;
        user.Status = model.Status;
        user.Roleid = model.RoleId;
        user.Status = status;
        user.Country = model.Country;
        user.States = model.State;
        user.City = model.City;
        user.Address = model.Address;
        user.Zipcode = model.Zipcode;
        user.Createdat = DateTime.Now;


        _userRepository.UpdateUser(user);
        return true;
    }

    public bool DeleteUser(int id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return false;
        }

        _userRepository.DeleteUser(user);
        return true;
    }

    public string GetEmailFromToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return string.Empty;

        var handler = new JwtSecurityTokenHandler();
        var AuthToken = handler.ReadJwtToken(token);
        return AuthToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public UserViewModel? GetUserProfile(string email)
    {
        if (string.IsNullOrEmpty(email))
            return null;

        var user = _userRepository.GetUserByEmail(email);
        if (user == null)
            return null;

        UserViewModel model = new UserViewModel();
        model.Firstname = user.Firstname;
        model.Lastname = user.Lastname;
        model.Username = user.Username;
        model.Email = user.Email;
        // model.Rolename = user.Role.Rolename;
        // var rolename = _dbo.Roles.FirstOrDefault(r => r.Roleid ==)
        model.Country = user.Country;
        model.State = user.States;
        model.City = user.City;
        model.Phone = user.Phone;
        model.Address = user.Address;
        model.Zipcode = user.Zipcode;
        return model;
    }

    public bool UpdateUserProfile(string email, UserViewModel model)
    {
        var user = _userRepository.GetUserByEmail(email);
        var countryname = _dbo.Countries.FirstOrDefault(c => c.Countryid.ToString() == model.Country);
        var statename = _dbo.States.FirstOrDefault(c => c.Stateid.ToString() == model.State);
        var cityname = _dbo.Cities.FirstOrDefault(c => c.Cityid.ToString() == model.City);

        model.Country = countryname?.Countryname;
        model.State = statename?.Statename;
        model.City = cityname?.Cityname;
        if (user == null) return false;

        user.Firstname = model.Firstname;
        user.Lastname = model.Lastname;
        user.Username = model.Username;
        user.Phone = model.Phone;
        user.Country = model.Country;
        user.States = model.State;
        user.City = model.City;
        user.Address = model.Address;
        user.Zipcode = model.Zipcode;

        return _userRepository.UpdateUser(user);
    }

    public async Task<List<PermissionsViewModel>> GetPermissionsByRoleAsync(string roleName)
    {
        return await _userRepository.GetPermissionsByRoleAsync(roleName);
    }


    public string ChangePassword(string email, ProfileChangePasswordViewModel model)
    {
        var user = _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            return "UserNotFound";
        }

        if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, user.Password))
        {
            return "IncorrectPassword";
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
        _userRepository.UpdateUser(user);

        return "Success";
    }

    public async Task<bool> UpdateRolePermissionsAsync(List<PermissionsViewModel> permissions)
    {
        return await _userRepository.UpdateRolePermissionsAsync(permissions);
    }



}
