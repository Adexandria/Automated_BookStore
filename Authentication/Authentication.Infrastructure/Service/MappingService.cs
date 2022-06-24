using Authentication.Application.DTO;
using Authentication.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Infrastructure.Service
{
    public class MappingService
    {
        public TypeAdapterConfig UserConfig()
        {
            var map = TypeAdapterConfig<SignUpCreate, User>.NewConfig().Map(dest => dest.PasswordHash, src => src.Password);
                    return map.Config;
        }
        public TypeAdapterConfig AdminConfig()
        {
            var map = TypeAdapterConfig<AdminCreate, User>.NewConfig().Map(dest => dest.PasswordHash, src => src.Password);
            return map.Config;
        }
    }
}
