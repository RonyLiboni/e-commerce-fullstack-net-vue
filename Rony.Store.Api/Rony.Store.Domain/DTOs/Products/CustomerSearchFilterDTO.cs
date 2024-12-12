using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Domain.DTOs.Products;
public class CustomerSearchFilterDTO
{
    public List<string> Departments { get; set; } = [];
    public List<string> SubDepartments { get; set; } = [];
    public List<string> Categories { get; set; } = [];

    public CustomerSearchFilterDTO(List<Department> filters)
    {
        foreach (var department in filters ?? [])
        {
            Departments.Add(department.Name);

            foreach (var subDepartment in department.SubDepartments ?? [])
            {
                SubDepartments.Add(subDepartment.Name);

                foreach (var category in subDepartment.Categories ?? [])
                {
                    Categories.Add(category.Name);
                }
            }
        }
    }
}
