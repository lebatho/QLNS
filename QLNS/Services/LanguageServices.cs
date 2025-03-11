using QLNS.Context;
using QLNS.Models;

namespace QLNS.Services
{
    public interface ILanguageServices
    {
        Task<List<Language>> GetAll();
        Task<(bool, string)> CreateLanguage(Language obj, string token);
        Task<(bool, string)> UpdateLanguage(Language obj, string token);
        Task<(bool, string)> DeleteLanguage(Guid id, string token);
    }
    public class LanguageServices : ILanguageServices
    {
        private readonly DataContext _Context;

        public LanguageServices(DataContext Context)
        {
            _Context = Context;
        }

        // Lấy danh sách chứng chỉ
        public async Task<List<Language>> GetAll()
        {
            return _Context.Languages.ToList();
        }


        // Thêm chứng chỉ
        public async Task<(bool, string)> CreateLanguage(Language obj, string token)
        {
            try
            {
                var item = new Language
                {
                    Id = Guid.NewGuid(),
                    Code = obj.Code,
                    Creator = obj.Creator,
                    DateCreated = DateTime.Now,
                    Description = obj.Description,
                    Name = obj.Name,
                };
                _Context.Languages.Add(item);
                await _Context.SaveChangesAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Cập nhật chứng chỉ
        public async Task<(bool, string)> UpdateLanguage(Language obj, string token)
        {
            try
            {
                var item = await _Context.Languages.FindAsync(obj.Id);
                if (item == null)
                {
                    return (false, "Language not found");
                }

                item.ChangedBy = obj.ChangedBy;
                item.Code = obj.Code;
                item.DateChange = DateTime.Now;
                item.Description = obj.Description;
                item.Name = obj.Name;

                await _Context.SaveChangesAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Xóa chứng chỉ
        public async Task<(bool, string)> DeleteLanguage(Guid id, string token)
        {
            try
            {
                var item = await _Context.Languages.FindAsync(id);
                if (item == null)
                {
                    return (false, "Language not found");
                }
                _Context.Languages.Remove(item);
                await _Context.SaveChangesAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }

}
