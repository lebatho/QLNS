using QLNS.Context;
using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLNS.Services
{
    public interface ISalaryService
    {
        Task<List<Salary>> GetAll();
        Task<(bool, string)> Update(Guid id, Salary salary);
        Task<(bool, string)> DeleteById(Guid id);
    }
    public class SalaryService : ISalaryService
    {
        private readonly DataContext _context;

        public SalaryService(DataContext context)
        {
            _context = context;
        }

        // 🔹 Lấy danh sách Salary
        public async Task<List<Salary>> GetAll()
        {
            return _context.Salarys.ToList();
        }

        // 🔹 Cập nhật Salary
        public async Task<(bool, string)> Update(Guid id, Salary salary)
        {
            var existingSalary = _context.Salarys.FirstOrDefault(x => x.Id == id);
            if (existingSalary == null)
            {
                return (false, "Không tìm thấy lương cần cập nhật!");
            }

            try
            {
                existingSalary.ChangedBy = salary.ChangedBy;
                existingSalary.Code = salary.Code;
                existingSalary.CoefficientSalary = salary.CoefficientSalary;
                existingSalary.Creator = salary.Creator;
                existingSalary.DateChange = DateTime.UtcNow;
                existingSalary.HourlyRate = salary.HourlyRate;
                existingSalary.SalaryLevel = salary.SalaryLevel;

                await _context.SaveChangesAsync();
                return (true, "Cập nhật lương thành công!");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật lương: {ex.Message}");
            }
        }

        // 🔹 Xóa Salary theo Id
        public async Task<(bool, string)> DeleteById(Guid id)
        {
            var existingSalary = _context.Salarys.FirstOrDefault(x => x.Id == id);
            if (existingSalary == null)
            {
                return (false, "Không tìm thấy lương cần xóa!");
            }

            try
            {
                _context.Salarys.Remove(existingSalary);
                await _context.SaveChangesAsync();
                return (true, "Xóa lương thành công!");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa lương: {ex.Message}");
            }
        }
    }
}
