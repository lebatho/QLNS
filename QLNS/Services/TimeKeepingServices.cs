using QLNS.Context;
using QLNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLNS.Services
{
    public interface ITimeKeepingServices
    {
        Task<List<TimeKeeping>> GetAll();
        Task<(bool, string)> Update(Guid id, TimeKeeping timeKeeping);
        Task<(bool, string)> DeleteById(Guid id);
    }

    public class TimeKeepingServices : ITimeKeepingServices
    {
        private readonly DataContext _context;

        public TimeKeepingServices(DataContext context)
        {
            _context = context;
        }

        // 🔹 Lấy danh sách chấm công
        public async Task<List<TimeKeeping>> GetAll()
        {
            return _context.TimeKeepings.ToList();
        }

        // 🔹 Cập nhật bản ghi chấm công
        public async Task<(bool, string)> Update(Guid id, TimeKeeping timeKeeping)
        {
            var existingRecord = _context.TimeKeepings.FirstOrDefault(x => x.Id == id);
            if (existingRecord == null)
            {
                return (false, "Không tìm thấy bản ghi chấm công cần cập nhật!");
            }

            try
            {
                existingRecord.ChangedBy = timeKeeping.ChangedBy;
                existingRecord.Code = timeKeeping.Code;
                existingRecord.Creator = timeKeeping.Creator;
                existingRecord.DateChange = DateTime.UtcNow;
                existingRecord.Month = timeKeeping.Month;
                existingRecord.NumberDayOff = timeKeeping.NumberDayOff;
                existingRecord.NumberDayUnexcusedLeave = timeKeeping.NumberDayUnexcusedLeave;
                existingRecord.NumberOvertimeHours = timeKeeping.NumberOvertimeHours;
                existingRecord.NumberWorkDay = timeKeeping.NumberWorkDay;
                existingRecord.Year = timeKeeping.Year;
                existingRecord.EmployeeCode = timeKeeping.EmployeeCode;
                existingRecord.Status = timeKeeping.Status;

                await _context.SaveChangesAsync();
                return (true, "Cập nhật bản ghi chấm công thành công!");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật chấm công: {ex.Message}");
            }
        }

        // 🔹 Xóa bản ghi chấm công theo ID
        public async Task<(bool, string)> DeleteById(Guid id)
        {
            var existingRecord = _context.TimeKeepings.FirstOrDefault(x => x.Id == id);
            if (existingRecord == null)
            {
                return (false, "Không tìm thấy bản ghi chấm công cần xóa!");
            }

            try
            {
                _context.TimeKeepings.Remove(existingRecord);
                await _context.SaveChangesAsync();
                return (true, "Xóa bản ghi chấm công thành công!");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa chấm công: {ex.Message}");
            }
        }
    }
}
