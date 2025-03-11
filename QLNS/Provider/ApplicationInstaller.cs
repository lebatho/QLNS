using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using QLNS.Context;
using QLNS.Controllers;
using QLNS.Services;

namespace QLNS.Provider
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddEFProcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var mySQLConnectionString = configuration.GetConnectionString("DBConnection");
            services.AddDbContextFactory<DataContext>(options =>
            {
                options.UseLazyLoadingProxies().UseMySql(mySQLConnectionString, ServerVersion.AutoDetect(mySQLConnectionString));
                options.ConfigureWarnings(builder =>
                {
                    builder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
                });
            });

            services.AddScoped<IClaimsTransformation, AddClaimsTransformation>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITokenServices, TokenServices>();
            services.AddTransient<IPositionServices, PositionServices>();
            services.AddTransient<IDepartmentServices, DepartmentServices>();
            services.AddTransient<ICommendationandDisciplineSevices, CommendationandDisciplineServices>();
            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<IContractServices, ContractServices>();
            services.AddTransient<ICandidateProfileSevices, CandidateProfileServices>();
            services.AddTransient<IRecruitServices, RecruitServices>();
            services.AddTransient<ISalaryService, SalaryService>();
            services.AddTransient<ICertificateServivces, CertificateServices>();
            services.AddTransient<ILanguageServices, LanguageServices>();
            services.AddTransient<ITimeKeepingServices, TimeKeepingServices>();

            return services;
        }
    }
}
