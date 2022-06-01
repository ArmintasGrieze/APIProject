using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectAPI.Models;
using ProjectAPI.Repositories;
using ProjectAPI.Validators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAPI.Services.UserCommands
{
    public class LoginUserCommand : IRequest<AuthResponse>
    {
        public UserDTO User { get; set; }

        public LoginUserCommand(UserDTO user)
        {
            User = user;
        }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public LoginUserCommandHandler(IMapper mapper, IUserRepository userRepository, IConfiguration configuration)
        {

            this.mapper = mapper;
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterAndLogInValidator();

            var validationResult = validator.Validate(mapper.Map<User>(request.User));

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }
            var result = await userRepository.GetUserByUsername(mapper.Map<User>(request.User));

            if (!VerifyPasswordHash(request.User.Password, result.PasswordHash, result.PasswordSalt))
            {
                throw new Exception("Incorrect username or password.");
            }

            result.Token = CreateToken(result);

            return mapper.Map<AuthResponse>(result);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:JWTToken").Value));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credential
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
