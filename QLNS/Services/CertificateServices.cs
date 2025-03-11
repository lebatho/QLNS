using Microsoft.EntityFrameworkCore;
using QLNS.Context;
using QLNS.Models;

namespace QLNS.Services
{
    public interface ICertificateServivces
    {
        Task<List<Certificate>> GetAll();
        Task<(bool, string)> CreateCertificate(Certificate obj, string token);
        Task<(bool, string)> UpdateCertificate(Certificate obj, string token);
        Task<(bool, string)> DeleteCertificate(Guid id, string token);

    }
    public class CertificateServices : ICertificateServivces
    {
        private readonly DataContext _context;
        public CertificateServices(DataContext Context)
        {
            _context = Context;
        }
        public async Task<List<Certificate>> GetAll()
        {
            return _context.Certificates.ToList();
        }

        public async Task<(bool, string)> CreateCertificate(Certificate obj, string token)
        {
            try
            {
                var item = new Certificate
                {
                    Id = Guid.NewGuid(),
                    ChangedBy = obj.ChangedBy,
                    Code = obj.Code,
                    Creator = obj.Creator,
                   
                    DateCreated = obj.DateCreated ?? DateTime.UtcNow,
                    GrantedBy = obj.GrantedBy,
                    IssuedDate = obj.IssuedDate,
                    Majors = obj.Majors,
                    Name = obj.Name
                };
                _context.Certificates.Add(item);
                await _context.SaveChangesAsync();
                return (true," ");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool, string)> UpdateCertificate(Certificate obj, string token)
        {
            try
            {
                var item = await _context.Certificates.FindAsync(obj.Id);
                if (item == null)
                {
                    return (false, "Certificate not found");
                }

                item.ChangedBy = obj.ChangedBy;
                item.Code = obj.Code;
                item.Creator = obj.Creator;
                item.DateChange = DateTime.UtcNow;
                item.GrantedBy = obj.GrantedBy;
                item.IssuedDate = obj.IssuedDate;
                item.Majors = obj.Majors;
                item.Name = obj.Name;

                await _context.SaveChangesAsync();
                return (true, "Certificate updated successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool, string)> DeleteCertificate(Guid id, string token)
        {
            try
            {
                var item = await _context.Certificates.FindAsync(id);
                if (item == null)
                {
                    return (false, "Certificate not found");
                }

                _context.Certificates.Remove(item);
                await _context.SaveChangesAsync();

                return (true, "Certificate deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
