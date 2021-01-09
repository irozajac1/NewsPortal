using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Framework.Dtos.Request;
using UserManagement.Framework.Entities;

namespace NewsPortal.UserManagement.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<RegisterRequest, User>();
            CreateMap<UpdateUserRequest, User>();

        }
    }
}
