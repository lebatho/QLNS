//using Hangfire;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QLNS.Context;
using QLNS.Models;

namespace QLNS.Services
{
    public interface IRefreshServices
    {
        void CronLoadData(Guid jobId);
    }

    public class RefreshServices : IRefreshServices
    {
        private readonly ILogger<RefreshServices> _logger;
        private readonly IDbContextFactory<DataContext> _context;
        public RefreshServices(ILogger<RefreshServices> logger, IDbContextFactory<DataContext> context)
        {
            _logger = logger;
            _context = context;
        }

        public void CronLoadData(Guid jobId)
        {
            try
            {
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public void InitLoadData()
        {
            try
            {
                using var _dbContext = _context.CreateDbContext();
               
                _dbContext.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
