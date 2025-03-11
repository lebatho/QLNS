using Newtonsoft.Json.Serialization;
using QLNS.Provider;
using QLNS.Models;
using QLNS.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using QLNS.Provider.Signalr;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddApiVersioning();

builder.Services.AddEFProcConfiguration(builder.Configuration);
//builder.Services.AddHostedService<WorkerBackground>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Token and Cookies
builder.Services.Configure<TokenSettingModel>(builder.Configuration.GetSection("TokenSettings"));
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenSettings:Secret").Value!);
var validationParams = new TokenValidationParameters()
{
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ClockSkew = TimeSpan.Zero,
    ValidateAudience = false,
    ValidateIssuer = false,
    ValidateIssuerSigningKey = true,
    RequireExpirationTime = true,
    ValidateLifetime = true,
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddCookie().AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = validationParams;
    cfg.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hubs/chat")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
#endregion

#region Add CORs
var generalSetting = builder.Configuration.GetSection("GeneralSetting").Get<GeneralSettingModel>();
//if (generalSetting != null)
//{
//    builder.Services.AddTelegramAction(option =>
//    {
//        option.BotToken = generalSetting.BotToken;
//        option.ChatId = generalSetting.ChatId;
//        option.BotStatus = true;
//        option.BotCommands = new List<string>();
//    });
//}
builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
})
);
#endregion

#region Serilog
//builder.Host.UseSerilog((ctx, lc) => lc
//     .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
//     .WriteTo.File("Logs/logs-.txt", Serilog.Events.LogEventLevel.Error,
//     "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3}] {Username} {Message:lj}{NewLine}{Exception}",
//     encoding: System.Text.Encoding.UTF8, rollingInterval: RollingInterval.Day)
//     .ReadFrom.Configuration(ctx.Configuration));
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CheckAcessMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseOptions();
app.MapControllers();

//app.MapHub<ClientHub>("/clienthub");
app.Run();