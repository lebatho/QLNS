using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QLNS.Context;
using QLNS.Models;

namespace QLNS.Services
{
    public interface IPositionServices
    {
        Task<List<Position>> GetAll();
        Task<(bool, Position, string)> GetDetail(Guid id, string token);
        Task<(bool, string)> CreatePosition(Position obj, string token);
        Task<(bool, string)> UpdatePosition(Position obj, string token);
        Task<(bool, string)> DeletePosition(Guid id, string token);
    }
    public class PositionServices : IPositionServices
    {
        private readonly DataContext _Context;
        public PositionServices(DataContext Context)
        {
            _Context = Context;
        }
        public async Task<List<Position>> GetAll()
        {
            return _Context.Positions.ToList();
        }
        public async Task<(bool, string)> CreatePosition(Position obj, string token)
        {
            try
            {
                var item = new Position
                {
                    Id = Guid.NewGuid(),
                    Code = obj.Code,
                    Creator = obj.Creator,
                    DateCreated = DateTime.Now,
                    Description = obj.Description,
                    Name = obj.Name,
                };
                _Context.Positions.Add(item);
                await _Context.SaveChangesAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> UpdatePosition(Position obj, string token)
        {
            try
            {
                var item = await _Context.Positions.FindAsync(obj.Id);
                if (item == null)
                {
                    return (false, "Position not found");
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
        public async Task<(bool, string)> DeletePosition(Guid id, string token)
        {
            try
            {
                var item = await _Context.Positions.FindAsync(id);
                if (item == null)
                {
                    return (false, "Position not found");
                }
                _Context.Positions.Remove(item);
                await _Context.SaveChangesAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, Position, string)> GetDetail(Guid id, string token)
        {
            try
            {
                var position = await _Context.Positions.FindAsync(id);
                if (position == null)
                {
                    return (false, null, "Không tìm thấy chức vụ");
                }

                return (true, position, "Tìm thấy chức vụ thành công");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }
}
