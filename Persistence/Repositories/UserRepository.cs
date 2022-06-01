using APIdemo.Models;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProjectAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<User> GetUserByUsername(User user)
        {
            var result = await this.context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);

            if (result == null)
            {
                throw new ObjectNotFoundException($"User by username {user.Username} is not found.");
            }

            return result;
        }

        public async Task<User> Register(User user)
        {
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = user.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt; 

            this.context.Users.Add(user);

            await this.context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
