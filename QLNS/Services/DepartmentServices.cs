using Microsoft.EntityFrameworkCore;
using QLNS.Context;
using QLNS.Models;

namespace QLNS.Services
{
    public interface IDepartmentServices
    {
        Task<List<Department>> GetAll();
        Task<(bool, Department, string)> GetDetail(Guid id, string token);
        Task<(bool, string)> CreateDepartment(Department obj, string token);
        Task<(bool, string)> UpdateDepartment(Department obj, string token);
        Task<(bool, string)> DeleteDepartment(Guid id, string token);

    }
    public class DepartmentServices : IDepartmentServices
    {
        private readonly DataContext _Context;
        public DepartmentServices(DataContext Context)
        {
            _Context = Context;
        }
        public async Task<List<Department>> GetAll()
        {
            return _Context.Departments.ToList();
        }
        public async Task<(bool, string)> CreateDepartment(Department obj, string token)
        {
            try
            {
                var item = new Department
                {
                    Id = Guid.NewGuid(),
                    ChangedBy = obj.ChangedBy,
                    Code = obj.Code,
                    Creator = obj.Creator,
                    DateCreated = obj.DateCreated ?? DateTime.UtcNow,
                    Description = obj.Description,
                    Name = obj.Name
                };
                _Context.Departments.Add(item);
                await _Context.SaveChangesAsync();
                return (true, "Tạo mới phòng ban thành công!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool, string)> UpdateDepartment(Department obj, string token)
        {
            try
            {
                var item = await _Context.Departments.FindAsync(obj.Id);
                if (item == null)
                {
                    return (false, "Không tìm thấy phòng ban");
                }

                item.ChangedBy = obj.ChangedBy;
                item.Code = obj.Code;
                item.Creator = obj.Creator;
                item.DateChange = DateTime.UtcNow;
                item.Name = obj.Name;

                await _Context.SaveChangesAsync();
                return (true, "Sửa thành công phòng ban!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> DeleteDepartment(Guid id, string token)
        {
            try
            {
                var item = await _Context.Departments.FindAsync(id);
                if (item == null)
                {
                    return (false, "Không tìm thấy phòng ban");
                }

                _Context.Departments.Remove(item);
                await _Context.SaveChangesAsync();

                return (true, "Xóa phòng ban thành công!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, Department?, string)> GetDetail(Guid id, string token)
        {
            try
            {
                var department = await _Context.Departments.FindAsync(id);
                if (department == null)
                {
                    return (false, null, "Không tìm thấy phòng ban");
                }

                return (true, department, "Tìm thấy phòng ban thành công");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }
}
