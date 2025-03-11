//using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QLNS.Models;
//using VoiceOtp.Models.Auth;
//using VoiceOtp.Models.Entities;
//using VoiceOtp.Models.Otp;
namespace QLNS.Context
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
              : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SSOToken>(e =>
            //{
            //    e.HasQueryFilter(x => x.IsActived);
            //    e.ToTable("crm_ssotoken").HasKey(k => new { k.Id, k.DomainId, k.UserId });
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            modelBuilder.Entity<Position>(e =>
            {
                e.ToTable("tbl_position").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("tbl_user").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Certificate>(e =>
            {
                e.ToTable("tbl_certificate").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Department>(e =>
            {
                e.ToTable("tbl_department").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Employee>(e =>
            {
                e.ToTable("tbl_employee").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CommendationAndDisciplines>(e =>
            {
                e.ToTable("tbl_commendation_and_discipline").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Contract>(e =>
            {
                e.ToTable("tbl_contract").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CandidateProfile>(e =>
            {
                e.ToTable("tbl_candidate_profile").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Recruit>(e =>
            {
                e.ToTable("tbl_recruit").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Salary>(e =>
            {
                e.ToTable("tbl_salary").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Language>(e =>
            {
                e.ToTable("tbl_language").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<TimeKeeping>(e =>
            {
                e.ToTable("tbl_time_keeping").HasKey(k => new { k.Id });
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            #region Test
            //modelBuilder.Entity<Users>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("crm_users").HasKey(k => k.Id);
            //    e.HasOne(x => x.Customer).WithOne(x => x.Users);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<Domain>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("crm_domain").HasKey(k => k.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<Permissions>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("crm_permissions").HasKey(k => k.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<MenuContent>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("crm_menu_content").HasKey(k => k.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<DomainSettings>(e =>
            //{
            //    e.ToTable("crm_domain_settings").HasKey(k => k.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<LogHistory>(e =>
            //{
            //    e.ToTable("crm_log_history").HasKey(k => k.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<ServerConfig>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("crm_server_config").HasKey(k => k.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<MenuPermissions>(e =>
            //{
            //    e.ToTable("crm_menu_permissions").HasKey(k => new { k.MenuId, k.PermissionId });
            //});
            //modelBuilder.Entity<UserPermissions>(e =>
            //{
            //    e.ToTable("crm_user_permissions").HasKey(k => new { k.UserId, k.PermissionId });
            //});

            //modelBuilder.Entity<Customers>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("crm_customers").HasKey(k => k.Id);
            //    e.HasOne(x => x.Users).WithOne(x => x.Customer);
            //    e.HasMany(x => x.ZnsConfigs).WithOne(x => x.Customer);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<OaInfo>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("crm_oa_info").HasKey(k => k.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<ZnsApplication>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("zns_app").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<ZnsURL>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("zns_url").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<OtpPackage>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("otp_package").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<OtpHotline>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("otp_hotline").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<OtpHotlineUser>(e =>
            //{
            //    e.ToTable("otp_hotlineUser").HasKey(x => new { x.OtpHotlineId, x.UsersId });
            //});
            //modelBuilder.Entity<OtpVoice>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("otp_voice").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<OtpTemplate>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("otp_template").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<OtpCustomerTemplate>(e =>
            //{
            //    e.ToTable("customer_otp_template").HasKey(k => new { k.CustomerId, k.TemplateId });
            //});

            //modelBuilder.Entity<OtpApiTTS>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("otp_api_tts").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<OtpApiTTSWebhook>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("otp_api_tts_webhook").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<CrmTelegramMessage>(e =>
            //{
            //    e.ToTable("crm_telegram_message").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<Money>(e =>
            //{
            //    e.ToTable("crm_money").HasKey(k => k.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});


            //modelBuilder.Entity<OtpPackageCustomers>(e =>
            //{
            //    e.ToTable("otp_otpPackageCustomers").HasKey(x => new { x.OtpPackageId, x.CustomersId });
            //});

            //modelBuilder.Entity<OtpVoucher>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("otp_voucher").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<OtpVoucherPackage>(e =>
            //{
            //    e.ToTable("otp_voucherPackage").HasKey(x => new { x.OtpVoucherId, x.OtpPackageId });
            //});
            //modelBuilder.Entity<OtpPackagePayCus>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("otp_packagePayCus").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            #endregion

            #region ZALO
            //modelBuilder.Entity<ZnsConfig>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("zns_config").HasKey(s => s.Id);
            //    e.HasOne(x => x.ZnsServices).WithOne(x => x.Config);
            //    e.HasOne(x => x.Customer).WithMany(x => x.ZnsConfigs);
            //    e.HasMany(x => x.Templates).WithOne(x => x.Config);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<ZnsTemplate>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("zns_template").HasKey(s => s.Id);
            //    e.HasOne(x => x.Config).WithMany(x => x.Templates);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<ZnsServices>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("zns_services").HasKey(s => s.Id);
            //    e.HasOne(x => x.Config).WithOne(x => x.ZnsServices);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<ZnsRequest>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("zns_request").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            //modelBuilder.Entity<ZnsReport>(e =>
            //{
            //    e.HasQueryFilter(x => !x.IsDeleted);
            //    e.ToTable("zns_report").HasKey(s => s.Id);
            //    e.Property(p => p.Id).ValueGeneratedOnAdd();
            //});
            #endregion
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        //public virtual DbSet<SSOToken> SSOTokens { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<CommendationAndDisciplines> CommendationAndDiscipliness { get; set; }
        public virtual DbSet<TimeKeeping> TimeKeepings { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<CandidateProfile> Candidates { get; set; }
        public virtual DbSet<Recruit> Recruits { get; set; }
        public virtual DbSet<Salary> Salarys { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        //public virtual DbSet<OaInfo> OaInfos { get; set; }
        //public virtual DbSet<ZnsConfig> ZnsConfigs { get; set; }
        //public virtual DbSet<ZnsTemplate> ZnsTemplates { get; set; }
        //public virtual DbSet<ZnsServices> ZnsServices { get; set; }
        //public virtual DbSet<ZnsRequest> ZnsRequests { get; set; }
        //public virtual DbSet<ZnsReport> ZnsReports { get; set; }
        //public virtual DbSet<ZnsURL> ZnsURLs { get; set; }
        //public virtual DbSet<ZnsApplication> ZnsApplication { get; set; }
        //public virtual DbSet<CrmTelegramMessage> CrmTeleMessage { get; set; }
        //public virtual DbSet<OtpPackage> OtpPackages { get; set; }
        //public virtual DbSet<OtpHotline> OtpHotline { get; set; }
        //public virtual DbSet<OtpVoice> OtpVoice { get; set; }
        //public virtual DbSet<OtpTemplate> OtpTemplate { get; set; }
        //public virtual DbSet<OtpApiTTS> OtpApiTTS { get; set; }
        //public virtual DbSet<OtpApiTTSWebhook> OtpApiTTSWebhook { get; set; }
        //public virtual DbSet<Money> Moneys { get; set; }
        //public virtual DbSet<OtpPackageCustomers> OtpPackageCustomers { get; set; }
        //public virtual DbSet<OtpVoucher> OtpVouchers { get; set; }
        //public virtual DbSet<OtpVoucherPackage> OtpVoucherPackages { get; set; }
        //public virtual DbSet<OtpPackagePayCus> OtpPackagePayCus { get; set; }
        //public virtual DbSet<OtpHotlineUser> OtpHotlineUsers { get; set; }
        //public virtual DbSet<OtpCustomerTemplate> OtpCustomerTemplates { get; set; }
    }
}
