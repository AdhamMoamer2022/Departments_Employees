using AutoMapper;
using BackendEmployeeAPI.DTOs;
using BackendEmployeeAPI.Models;
using System.Globalization;

namespace BackendEmployeeAPI.Utilites
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            #region Department

            CreateMap<Department, DepartmentDTO>().ReverseMap();

            #endregion



            #region Employee

            CreateMap<Employee, EmployeeDTO>().
                ForMember(destinationMember => destinationMember.DepartmentName,
                opt => opt.MapFrom(origin => origin.IdDepartmentNavigation.Name))

                .ForMember(destinationMember => destinationMember.Salary,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Salary, CultureInfo.InvariantCulture)))

                .ForMember(destinationMember => destinationMember.HireDate,
                opt => opt.MapFrom(origin => origin.HireDate.Value.ToString("dd/MM/yyyy")));



            CreateMap<EmployeeDTO, Employee>()
                .ForMember(destinationMember => destinationMember.IdDepartmentNavigation, opt => opt.Ignore())

                .ForMember(destinationMember => destinationMember.Salary, opt => opt.MapFrom(
                    origin => decimal.Parse(origin.Salary, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture)))

                .ForMember(destinationMember => destinationMember.HireDate, opt => opt.MapFrom(
                     origin => DateTime.ParseExact(origin.HireDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            #endregion

        }
    }
}
