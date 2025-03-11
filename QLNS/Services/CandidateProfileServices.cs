using Microsoft.EntityFrameworkCore;
using QLNS.Context;
using QLNS.Models;
using System;
using System.Threading.Tasks;

namespace QLNS.Services
{
    public interface ICandidateProfileSevices
    {
        Task<List<CandidateProfile>> GetAll();
        Task<(bool, CandidateProfile, string)> GetDetail(Guid id, string token);
        Task<(bool, string)> CreateCandidate(CandidateProfile obj, string token);
        Task<(bool, string)> UpdateCandidate(CandidateProfile obj, string token);
        Task<(bool, string)> DeleteCandidate(Guid id, string token);
    }
    public class CandidateProfileServices : ICandidateProfileSevices
    {
        private readonly DataContext _context;

        public CandidateProfileServices(DataContext context)
        {
            _context = context;
        }
        public async Task<List<CandidateProfile>> GetAll()
        {
            return _context.Candidates.ToList();
        }

        // Thêm ứng viên mới
        public async Task<(bool, string)> CreateCandidate(CandidateProfile obj, string token)
        {
            try
            {
                var candidate = new CandidateProfile
                {
                    Id = Guid.NewGuid(),
                    Address = obj.Address,
                    Age = obj.Age,
                    ChangedBy = obj.ChangedBy,
                    Code = obj.Code,
                    Creator = obj.Creator,
                    DateCreated = obj.DateCreated ?? DateTime.UtcNow,
                    DateOfBirth = obj.DateOfBirth,
                    Education = obj.Education,
                    Email = obj.Email,
                    FullName = obj.FullName,
                    InterviewDate = obj.InterviewDate,
                    Interviewer = obj.Interviewer,
                    Major = obj.Major,
                    Phone = obj.Phone,
                    RefusalReason = obj.RefusalReason,
                    status = obj.status,
                    Image = obj.Image,
                    ImageName = obj.ImageName,
                    CareerGoals = obj.CareerGoals,
                    Hobby = obj.Hobby,
                    Skill = obj.Skill,
                    WorkingExperience = obj.WorkingExperience,
                    TitleRecruit = obj.TitleRecruit
                };

                _context.Candidates.Add(candidate);
                await _context.SaveChangesAsync();
                return (true, "Candidate created successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Cập nhật ứng viên
        public async Task<(bool, string)> UpdateCandidate(CandidateProfile obj, string token)
        {
            try
            {
                var candidate = await _context.Candidates.FindAsync(obj.Id);
                if (candidate == null)
                {
                    return (false, "Candidate not found");
                }

                candidate.Address = obj.Address;
                candidate.Age = obj.Age;
                candidate.ChangedBy = obj.ChangedBy;
                candidate.Code = obj.Code;
                candidate.Creator = obj.Creator;
                candidate.DateChange = DateTime.UtcNow;
                candidate.DateOfBirth = obj.DateOfBirth;
                candidate.Education = obj.Education;
                candidate.Email = obj.Email;
                candidate.FullName = obj.FullName;
                candidate.InterviewDate = obj.InterviewDate;
                candidate.Interviewer = obj.Interviewer;
                candidate.Major = obj.Major;
                candidate.Phone = obj.Phone;
                candidate.RefusalReason = obj.RefusalReason;
                candidate.status = obj.status;
                candidate.Image = obj.Image;
                candidate.ImageName = obj.ImageName;
                candidate.CareerGoals = obj.CareerGoals;
                candidate.Hobby = obj.Hobby;
                candidate.Skill = obj.Skill;
                candidate.WorkingExperience = obj.WorkingExperience;
                candidate.TitleRecruit = obj.TitleRecruit;

                await _context.SaveChangesAsync();
                return (true, "Candidate updated successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Xóa ứng viên
        public async Task<(bool, string)> DeleteCandidate(Guid id, string token)
        {
            try
            {
                var candidate = await _context.Candidates.FindAsync(id);
                if (candidate == null)
                {
                    return (false, "Candidate not found");
                }

                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();

                return (true, "Candidate deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, CandidateProfile, string)> GetDetail(Guid id, string token)
        {
            try
            {
                var candidateprofile = await _context.Candidates.FindAsync(id);
                if (candidateprofile == null)
                {
                    return (false, null, "CandidateProfile record not found");
                }

                return (true, candidateprofile, "CandidateProfile record found successfully");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }
}
