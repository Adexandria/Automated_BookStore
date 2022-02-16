using Authentication.Application.DTO;
using Authentication.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Application.Profiles
{
    public class AuthProfile :Profile
    {
        public AuthProfile()
        {
            CreateMap<SignUpCreate, SignUp>()
                .ForMember(s => s.Email, opt => opt.MapFrom(s => s.StudentEmail))
                .ForMember(s => s.First_Name, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(s => s.Last_Name, opt => opt.MapFrom(s => s.LastName))
                .ForMember(s => s.Matriculation_Number, opt => opt.MapFrom(s => s.MatriculationNumber))
                .ForMember(s => s.Middle_Name, opt => opt.MapFrom(s => s.MiddleName))
                .ForMember(s => s.PasswordHash, opt => opt.MapFrom(s => s.Password));

            CreateMap<Login, SignUp>().ForMember(s => s.PasswordHash, opt => opt.MapFrom(s => s.Password));
        }
    }
}
