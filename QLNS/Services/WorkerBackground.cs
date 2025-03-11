namespace QLNS.Services
{
    public class WorkerBackground : BackgroundService
    {
        private readonly ILogger<WorkerBackground> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public WorkerBackground(ILogger<WorkerBackground> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var refService = scope.ServiceProvider.GetService<IRefreshServices>();
            refService!.CronLoadData(Guid.NewGuid());
            await Task.CompletedTask;
        }
    }
}
