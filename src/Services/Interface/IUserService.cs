using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Services.Interface;

public interface IUserService {
    List<UserDto> GetAllUsers();
    bool CreateUser(UserCreate user);
    Task<bool> DeleteUser(UserCredentials userCredentials);
    Task<UserDto> Authenticate(UserCredentials userCredentials);
    Task<bool> ModifyUser(UserDto userModify);
}