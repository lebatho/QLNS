using Microsoft.EntityFrameworkCore;
using QLNS.Context;
using QLNS.Models;

namespace QLNS.Services
{
    public interface ICommendationandDisciplineSevices
    {
        Task<List<CommendationAndDisciplines>> GetAll();
        Task<(bool, string)> CreateCommendationAndDiscipline(CommendationAndDisciplines obj, string token);
        Task<(bool, string)> UpdateCommendationAndDiscipline(CommendationAndDisciplines obj, string token);
        Task<(bool, string)> DeleteCommendationAndDiscipline(Guid id, string token);
    }
    public class CommendationandDisciplineServices : ICommendationandDisciplineSevices
    {
        public DataContext _context;
        public CommendationandDisciplineServices(DataContext context)
        {
            _context = context;
        }
        public async Task<List<CommendationAndDisciplines>> GetAll()
        {
            return _context.CommendationAndDiscipliness.ToList();
        }
        public async Task<(bool, string)> CreateCommendationAndDiscipline(CommendationAndDisciplines obj, string token)
        {
            try
            {
                var item = new CommendationAndDisciplines
                {
                    Id = Guid.NewGuid(),
                    ChangedBy = obj.ChangedBy,
                    Creator = obj.Creator,
                    DateChange = obj.DateChange ?? DateTime.UtcNow,
                    DateCreated = obj.DateCreated ?? DateTime.UtcNow,
                    IssuedDate = obj.IssuedDate,
                    Reason = obj.Reason,
                    Status = obj.Status,
                    Day = obj.Day,
                    DecisionDay = obj.DecisionDay,
                    DecisionNumber = obj.DecisionNumber,
                    Month = obj.Month,
                    RewardDisciplineLevel = obj.RewardDisciplineLevel,
                    StaffName = obj.StaffName,
                    Year = obj.Year,
                    Type = obj.Type,
                    EmployeeCode = obj.EmployeeCode,
                    BasicSalary = obj.BasicSalary,
                    CoefficientSalary = obj.CoefficientSalary,
                    HourlyRate = obj.HourlyRate
                };

                _context.CommendationAndDiscipliness.Add(item);
                await _context.SaveChangesAsync();
                return (true, "Commendation or Discipline record created successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Cập nhật CommendationAndDiscipline
        public async Task<(bool, string)> UpdateCommendationAndDiscipline(CommendationAndDisciplines obj, string token)
        {
            try
            {
                var item = await _context.CommendationAndDiscipliness.FindAsync(obj.Id);
                if (item == null)
                {
                    return (false, "Commendation or Discipline record not found");
                }

                item.ChangedBy = obj.ChangedBy;
                item.DateChange = DateTime.UtcNow;
                item.IssuedDate = obj.IssuedDate;
                item.Reason = obj.Reason;
                item.Status = obj.Status;
                item.Day = obj.Day;
                item.DecisionDay = obj.DecisionDay;
                item.DecisionNumber = obj.DecisionNumber;
                item.Month = obj.Month;
                item.RewardDisciplineLevel = obj.RewardDisciplineLevel;
                item.StaffName = obj.StaffName;
                item.Year = obj.Year;
                item.Type = obj.Type;
                item.EmployeeCode = obj.EmployeeCode;
                item.BasicSalary = obj.BasicSalary;
                item.CoefficientSalary = obj.CoefficientSalary;
                item.HourlyRate = obj.HourlyRate;

                await _context.SaveChangesAsync();
                return (true, "Commendation or Discipline record updated successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Xóa CommendationAndDiscipline
        public async Task<(bool, string)> DeleteCommendationAndDiscipline(Guid id, string token)
        {
            try
            {
                var item = await _context.CommendationAndDiscipliness.FindAsync(id);
                if (item == null)
                {
                    return (false, "Commendation or Discipline record not found");
                }

                _context.CommendationAndDiscipliness.Remove(item);
                await _context.SaveChangesAsync();

                return (true, "Commendation or Discipline record deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
