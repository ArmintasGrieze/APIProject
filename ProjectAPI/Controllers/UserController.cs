using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Models;
using ProjectAPI.Services.UserCommands;
using System.Threading.Tasks;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public static User user = new User();

        public UserController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(UserDTO request)
        {
            var result = await this.mediator.Send(new AddUserCommand(request));

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserDTO request)
        {
            var result = await this.mediator.Send(new LoginUserCommand(request));

            return Ok(mapper.Map<AuthResponse>(result));
        }
    }
}