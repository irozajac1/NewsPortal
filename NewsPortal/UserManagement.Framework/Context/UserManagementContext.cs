using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Framework.Entities;

namespace UserManagement.Framework.Context
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext>options): base(options)
        {

        }

        DbSet<User> Users { get; set; }
    }
}
