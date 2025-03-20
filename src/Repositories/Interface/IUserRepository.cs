using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Repositories.Interface;

public interface IUserRepository {
    public List<UserDto> GetAllUsers();
    public bool CreateUser(UserCreate user);
    public Task<UserDto> GetUserByUsername(string username);
    public Task<bool> DeleteUser(string userId);
}