using IAM.Application.DTOs;

namespace IAM.Application.Contracts
{
    public interface IUser
    {
        Task<RegisterResponse> RegisterUserAsync(RegisterUserDTO userDTO);
        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
    }
}
