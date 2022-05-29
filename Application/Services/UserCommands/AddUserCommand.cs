using AutoMapper;
using MediatR;
using ProjectAPI.Models;
using ProjectAPI.Repositories;
using ProjectAPI.Validators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAPI.Services.UserCommands
{
    public class AddUserCommand : IRequest<UserDTO>
    {
        public UserDTO User { get; set; }

        public AddUserCommand(UserDTO user)
        {
            User = user;
        }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDTO>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public AddUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }


        public async Task<UserDTO> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var result = await userRepository.Register(mapper.Map<User>(request.User));

            var validator = new RegisterAndLogInValidator();

            var validationResult = validator.Validate(result);

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            return mapper.Map<UserDTO>(result);
        }
    }
}
