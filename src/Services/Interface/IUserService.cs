using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Services.Interface;

public interface IUserService {
    
    public List<UserDto> GetAllUsers();
}