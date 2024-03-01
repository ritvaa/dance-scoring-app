using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Contracts;
using API.Entities;
using AutoMapper;

namespace API {
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserReadModel>();
            CreateMap<UserWriteModel, User>();

        }
    }
}
