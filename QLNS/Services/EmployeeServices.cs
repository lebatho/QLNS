using Microsoft.EntityFrameworkCore;
using QLNS.Context;
using QLNS.Models;

namespace QLNS.Services
{
    public interface IEmployeeServices
    {
        Task<List<Employee>> GetAll();
        Task<(bool, string)> CreateEmployee(Employee obj, string token);
        Task<(bool, string)> UpdateEmployee(Employee obj, string token);
        Task<(bool, string)> DeleteEmployee(Guid id, string token);

    }
    public class EmployeeServices : IEmployeeServices
    {
        public DataContext _Context;
        public EmployeeServices(DataContext context) 
        {  
            _Context = context;
        }
        public async Task<List<Employee>> GetAll()
        {
            return _Context.Employees.ToList();
        }

        public async Task<(bool, string)> CreateEmployee(Employee obj, string token)
        {
            try
            {
                var item = new Employee
                {
                    Id = Guid.NewGuid(),
                    Address = obj.Address, 
                    Code = obj.Code,
                    Creator = obj.Creator,
                    DateCreated = obj.DateCreated ?? DateTime.UtcNow,
                    DateOfBirth = obj.DateOfBirth,
                    Education = obj.Education,
                    Email = obj.Email,
                    FullName = obj.FullName,
                    Image = obj.Image,
                    IssuanceDateIdentityCard = obj.IssuanceDateIdentityCard,
                    IssuedDateMedicalInsurance = obj.IssuedDateMedicalInsurance,
                    IssuedDateSocialInsurance = obj.IssuedDateSocialInsurance,
                    Major = obj.Major,
                    Nation = obj.Nation,
                    NumberIdCard = obj.NumberIdCard,
                    NumberMedicalInsurance = obj.NumberMedicalInsurance,
                    NumberSocialInsurance = obj.NumberSocialInsurance,
                    Phone = obj.Phone,
                    PlaceOfGrantIdentityCard = obj.PlaceOfGrantIdentityCard,
                    PlaceOfIssueMedicalInsurance = obj.PlaceOfIssueMedicalInsurance,
                    PlaceOfIssueSocialInsurance = obj.PlaceOfIssueSocialInsurance,
                    QuitJobDate = obj.QuitJobDate,
                    RefusalReason = obj.RefusalReason,
                    Religion = obj.Religion,
                    Sex = obj.Sex,
                    Status = obj.Status,
                    CertificateId = obj.CertificateId,
                    ContractId = obj.ContractId,
                    DepartmentId = obj.DepartmentId,
                    AdditionalRequestContent = obj.AdditionalRequestContent,
                    ImageName = obj.ImageName,
                    CandidateProfileId = obj.CandidateProfileId,
                    Note = obj.Note,
                    TitleRecruit = obj.TitleRecruit
                };

                _Context.Employees.Add(item);
                await _Context.SaveChangesAsync();
                return (true, "Employee created successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Cập nhật Employee
        public async Task<(bool, string)> UpdateEmployee(Employee obj, string token)
        {
            try
            {
                var item = await _Context.Employees.FindAsync(obj.Id);
                if (item == null)
                {
                    return (false, "Employee not found");
                }

                item.Address = obj.Address;
                item.ChangedBy = obj.ChangedBy;
                item.Code = obj.Code;
                item.Creator = obj.Creator;
                item.DateChange = DateTime.UtcNow;
                item.DateOfBirth = obj.DateOfBirth;
                item.Education = obj.Education;
                item.Email = obj.Email;
                item.FullName = obj.FullName;
                item.Image = obj.Image;
                item.IssuanceDateIdentityCard = obj.IssuanceDateIdentityCard;
                item.IssuedDateMedicalInsurance = obj.IssuedDateMedicalInsurance;
                item.IssuedDateSocialInsurance = obj.IssuedDateSocialInsurance;
                item.Major = obj.Major;
                item.Nation = obj.Nation;
                item.NumberIdCard = obj.NumberIdCard;
                item.NumberMedicalInsurance = obj.NumberMedicalInsurance;
                item.NumberSocialInsurance = obj.NumberSocialInsurance;
                item.Phone = obj.Phone;
                item.PlaceOfGrantIdentityCard = obj.PlaceOfGrantIdentityCard;
                item.PlaceOfIssueMedicalInsurance = obj.PlaceOfIssueMedicalInsurance;
                item.PlaceOfIssueSocialInsurance = obj.PlaceOfIssueSocialInsurance;
                item.QuitJobDate = obj.QuitJobDate;
                item.RefusalReason = obj.RefusalReason;
                item.Religion = obj.Religion;
                item.Sex = obj.Sex;
                item.Status = obj.Status;
                item.CertificateId = obj.CertificateId;
                item.ContractId = obj.ContractId;
                item.DepartmentId = obj.DepartmentId;
                item.AdditionalRequestContent = obj.AdditionalRequestContent;
                item.ImageName = obj.ImageName;
                item.CandidateProfileId = obj.CandidateProfileId;
                item.Note = obj.Note;
                item.TitleRecruit = obj.TitleRecruit;

                await _Context.SaveChangesAsync();
                return (true, "Employee updated successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool, string)> DeleteEmployee(Guid id, string token)
        {
            try
            {
                var item = await _Context.Employees.FindAsync(id);
                if (item == null)
                {
                    return (false, "Employee not found");
                }

                _Context.Employees.Remove(item);
                await _Context.SaveChangesAsync();

                return (true, "Employee deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
