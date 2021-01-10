using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Framework.Dtos.Request;
using UserManagement.Framework.Dtos.Response;
using UserManagement.Framework.Entities;

namespace UserManagement.Framework.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(User user, string password);
        Task Update(User user, string password = null);
        Task Delete(int id);
    }
}
