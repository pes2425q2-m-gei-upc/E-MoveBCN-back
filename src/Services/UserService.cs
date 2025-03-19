using Services.Interface;
using Repositories.Interface;
using System.Collections.Generic;
using Dto;
using System.Threading.Tasks;
namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService (IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public List<UserDto> GetAllUsers() {
        return  _userRepository.GetAllUsers();
    }
    public bool CreateUser(UserCreate user) {
        return _userRepository.CreateUser(user);
    }
}