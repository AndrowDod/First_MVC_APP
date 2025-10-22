using AutoMapper;
using First_MVC_APP.DAL.Data.Models;
using First_MVC_APP.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace First_MVC_APP.PL.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();

            CreateMap<RoleViewModel , IdentityRole>()
                .ForMember(D => D.Name , O => O.MapFrom(S => S.RoleName))
                .ReverseMap();

        }
    }
}
