using IAM.Application.Contracts;
using IAM.Application.DTOs;
using IAM.Domain.Entities;
using IAM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly AuthenticationDbContext authenticationDb;
        public UserRepository(AuthenticationDbContext authenticationDb)
        {
            this.authenticationDb = authenticationDb;

        }
        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUser = await FindUserByEmail(loginDTO.Email!);
            if (getUser is null) return new LoginResponse(false, "User not found");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
            if (checkPassword)
                return new LoginResponse(true, "Login successfully", GenerateJWTToken(getUser));
            else
                return new LoginResponse(false, "Invalid credentials");
        }

        private async Task<ApplicationUser?> FindUserByEmail(string email) => await authenticationDb.Users.FirstOrDefaultAsync(u => u.Email == email);

        private string GenerateJWTToken(ApplicationUser getUser)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var getUser = await FindUserByEmail(registerUserDTO.Email!);
            if (getUser is not null) return new RegisterResponse(false, "User already exists");

            authenticationDb.Users.Add(new ApplicationUser()
            {
                Name = registerUserDTO.Name,
                Email = registerUserDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password)
            });

            await authenticationDb.SaveChangesAsync();
            return new RegisterResponse(true, "User registered successfully");
        }
    }
}
