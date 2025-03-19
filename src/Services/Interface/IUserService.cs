using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Services.Interface;

public interface IUserService {
    List<UserDto> GetAllUsers();
    bool CreateUser(UserCreate user);

    Task<UserDto> Authenticate(UserCredentials userCredentials);
}