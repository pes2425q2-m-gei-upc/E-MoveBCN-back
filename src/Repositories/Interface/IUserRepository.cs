using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Repositories.Interface;

public interface IUserRepository {
    public List<UserDto> GetAllUsers();
}