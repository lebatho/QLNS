using Microsoft.EntityFrameworkCore;
using QLNS.Context;
using QLNS.Models;
using System;
using System.Threading.Tasks;

namespace QLNS.Services
{
    public interface IContractServices
    {
        Task<List<Contract>> GetAll();
        Task<(bool, string)> CreateContract(Contract obj, string token);
        Task<(bool, string)> UpdateContract(Contract obj, string token);
        Task<(bool, string)> DeleteContract(Guid id, string token);
    }

    public class ContractServices : IContractServices
    {
        private readonly DataContext _context;

        public ContractServices(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Contract>> GetAll()
        {
            return _context.Contracts.ToList();
        }

        // Thêm hợp đồng mới
        public async Task<(bool, string)> CreateContract(Contract obj, string token)
        {
            try
            {
                var contract = new Contract
                {
                    Id = Guid.NewGuid(),
                    ChangedBy = obj.ChangedBy,
                    Code = obj.Code,
                    ContractEffect = obj.ContractEffect,
                    Creator = obj.Creator,
                    DateChange = obj.DateChange ?? DateTime.UtcNow,
                    DateCreated = obj.DateCreated ?? DateTime.UtcNow,
                    SigningDate = obj.SigningDate,
                    Status = obj.Status,
                    BasicSalary = obj.BasicSalary,
                    NameLeader = obj.NameLeader,
                    PositionLeader = obj.PositionLeader,
                    CoefficientSalary = obj.CoefficientSalary,
                    HourlyRate = obj.HourlyRate
                };

                _context.Contracts.Add(contract);
                await _context.SaveChangesAsync();
                return (true, "Contract created successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Cập nhật hợp đồng
        public async Task<(bool, string)> UpdateContract(Contract obj, string token)
        {
            try
            {
                var contract = await _context.Contracts.FindAsync(obj.Id);
                if (contract == null)
                {
                    return (false, "Contract not found");
                }

                contract.ChangedBy = obj.ChangedBy;
                contract.Code = obj.Code;
                contract.ContractEffect = obj.ContractEffect;
                contract.Creator = obj.Creator;
                contract.DateChange = DateTime.UtcNow;
                contract.SigningDate = obj.SigningDate;
                contract.Status = obj.Status;
                contract.BasicSalary = obj.BasicSalary;
                contract.NameLeader = obj.NameLeader;
                contract.PositionLeader = obj.PositionLeader;
                contract.CoefficientSalary = obj.CoefficientSalary;
                contract.HourlyRate = obj.HourlyRate;

                await _context.SaveChangesAsync();
                return (true, "Contract updated successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Xóa hợp đồng
        public async Task<(bool, string)> DeleteContract(Guid id, string token)
        {
            try
            {
                var contract = await _context.Contracts.FindAsync(id);
                if (contract == null)
                {
                    return (false, "Contract not found");
                }

                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();

                return (true, "Contract deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
