using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Framework.Dtos.Request;
using UserManagement.Framework.Dtos.Response;
using UserManagement.Framework.Entities;
using UserManagement.Framework.Helpers;
using UserManagement.Framework.Interfaces;

namespace UserManagement.Framework.Services
{
    public class UserService : IUserService
    {
        private IUserManagementUnitOfWork _uow;
        private readonly AppSettings _appSettings;

        public UserService(IUserManagementUnitOfWork uow, IOptions<AppSettings> appSettings)
        {
            _uow = uow;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return null;

            var user = await _uow.GetRepository<User>().FirstOrDefault(x => x.UserName == request.Username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var token = GenerateJwtToken(user);

            // authentication successful
            return new AuthenticateResponse(user, token);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _uow.GetRepository<User>().GetAll();
        }

        public async Task<User> GetById(int id)
        {
            return await _uow.GetRepository<User>().GetById(id);
        }

        public async Task<User> Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password is required");
            var userExists = _uow.GetRepository<User>().Any(x => x.UserName == user.UserName);

            if (userExists)
                throw new ArgumentNullException("Korisničko ime \"" + user.UserName + "\" već postoji!"); //mjenjati

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            try
            {
                _uow.GetRepository<User>().Insert(user);
                await _uow.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(User userParam, string password = null)
        {
            var user = await _uow.GetRepository<User>().GetById(userParam.Id);

            if (user == null)
                throw new ArgumentNullException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.UserName) && userParam.UserName != user.UserName)
            {
                var userExists = _uow.GetRepository<User>().Any(x => x.UserName == userParam.UserName);
                // throw error if the new username is already taken
                if (userExists)
                    throw new ArgumentNullException("Username " + userParam.UserName + " is already taken"); //mjenjati

                user.UserName = userParam.UserName;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }


            if (!string.IsNullOrWhiteSpace(userParam.Role))
                user.Role = userParam.Role;

            _uow.GetRepository<User>().Update(user);
            await _uow.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _uow.GetRepository<User>().GetById(id);
            if (user != null)
            {
                _uow.GetRepository<User>().Delete(user);
                await _uow.SaveChangesAsync();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}


