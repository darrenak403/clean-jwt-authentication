using IAM.Application.Contracts;
using IAM.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IAM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        public UserController(IUser user)
        {
            this.user = user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginDTO loginDTO)
        {
            var result = await user.LoginUserAsync(loginDTO);
            if (result.Flag)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> RegisterUser(RegisterUserDTO registerDTO)
        {
            var result = await user.RegisterUserAsync(registerDTO);
            if (result.Flag)
                return Ok(result);
            return BadRequest(result);
        }


    }
}
