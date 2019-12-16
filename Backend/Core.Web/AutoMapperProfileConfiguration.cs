using AutoMapper;
using Core.Domain;
using Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        : this("CoreProfile")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
            //CreateMap<AplicationModel,Aplication>();
            //CreateMap<Aplication, AplicationModel>();
        }
    }
}
