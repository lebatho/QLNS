using Microsoft.EntityFrameworkCore;
using QLNS.Context;
using QLNS.Models;
using System;
using System.Threading.Tasks;

namespace QLNS.Services
{
    public interface IRecruitServices
    {
        Task<List<Recruit>> GetAll();
        Task<(bool, Recruit, string)> GetDetail(Guid id, string token);
        Task<(bool, string)> CreateRecruit(Recruit obj, string token);
        Task<(bool, string)> UpdateRecruit(Recruit obj, string token);
        Task<(bool, string)> DeleteRecruit(Guid id, string token);
    }

    public class RecruitServices : IRecruitServices
    {
        private readonly DataContext _context;

        public RecruitServices(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Recruit>> GetAll()
        {
            return _context.Recruits.ToList();
        }
        // Thêm tuyển dụng mới
        public async Task<(bool, string)> CreateRecruit(Recruit obj, string token)
        {
            try
            {
                var recruit = new Recruit
                {
                    Id = Guid.NewGuid(),
                    BenefitsReceived = obj.BenefitsReceived,
                    Code = obj.Code,
                    Creator = obj.Creator,
                    DateCreated = obj.DateCreated ?? DateTime.UtcNow,
                    Description = obj.Description,
                    Feedback = obj.Feedback,
                    Quantity = obj.Quantity,
                    RecruitmentChannel = obj.RecruitmentChannel,
                    RequireRecruit = obj.RequireRecruit,
                    Status = obj.Status,
                    TitleRecruit = obj.TitleRecruit
                };

                _context.Recruits.Add(recruit);
                await _context.SaveChangesAsync();
                return (true, "Recruitment record created successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Cập nhật thông tin tuyển dụng
        public async Task<(bool, string)> UpdateRecruit(Recruit obj, string token)
        {
            try
            {
                var recruit = await _context.Recruits.FindAsync(obj.Id);
                if (recruit == null)
                {
                    return (false, "Recruitment record not found");
                }

                recruit.BenefitsReceived = obj.BenefitsReceived;
                recruit.ChangedBy = obj.ChangedBy;
                recruit.Code = obj.Code;
                recruit.Creator = obj.Creator;
                recruit.DateChange = DateTime.UtcNow;
                recruit.Description = obj.Description;
                recruit.Feedback = obj.Feedback;
                recruit.Quantity = obj.Quantity;
                recruit.RecruitmentChannel = obj.RecruitmentChannel;
                recruit.RequireRecruit = obj.RequireRecruit;
                recruit.Status = obj.Status;
                recruit.TitleRecruit = obj.TitleRecruit;

                await _context.SaveChangesAsync();
                return (true, "Recruitment record updated successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Xóa thông tin tuyển dụng
        public async Task<(bool, string)> DeleteRecruit(Guid id, string token)
        {
            try
            {
                var recruit = await _context.Recruits.FindAsync(id);
                if (recruit == null)
                {
                    return (false, "Recruitment record not found");
                }

                _context.Recruits.Remove(recruit);
                await _context.SaveChangesAsync();

                return (true, "Recruitment record deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, Recruit, string)> GetDetail(Guid id, string token)
        {
            try
            {
                var recruit = await _context.Recruits.FindAsync(id);
                if (recruit == null)
                {
                    return (false, null, "Recruitment record not found");
                }

                return (true, recruit, "Recruitment record found successfully");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }
}
