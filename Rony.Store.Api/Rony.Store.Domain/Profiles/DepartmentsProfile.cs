using AutoMapper;
using Rony.Store.Domain.DTOs.Departments;
using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Domain.Profiles;
public class DepartmentsProfile : Profile
{
    public DepartmentsProfile()
    {
        AddDepartmentMappings();
        AddSubDepartmentMappings();
        AddCategoryMappings();
    }

    private void AddDepartmentMappings()
    {
        CreateMap<DepartmentFormDTO, Department>();
        CreateMap<Department, DepartmentDTO>()
            .ForMember(departmentDTO => departmentDTO.SubDepartments,
            options => options.MapFrom(department => department.SubDepartments));
    }

    private void AddSubDepartmentMappings()
    {
        CreateMap<SubDepartmentFormDTO, SubDepartment>();
        CreateMap<SubDepartment, SubDepartmentDTO>()
            .ForMember(subDepartmentDTO => subDepartmentDTO.Categories,
                options => options.MapFrom(subDepartment => subDepartment.Categories));
    }
    private void AddCategoryMappings()
    {
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
        CreateMap<CategoryFormDTO, Category>();
    }
}