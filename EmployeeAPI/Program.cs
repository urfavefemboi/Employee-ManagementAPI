using EmployeeAPI.Converter;
using EmployeeAPI.Data;
using EmployeeAPI.Interface;
using EmployeeAPI.Interface.Common;
using EmployeeAPI.Interface.ModelInterface;
using EmployeeAPI.Reposistory;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static EmployeeAPI.Services.AuthServies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new TickConverter());
   
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee-Manangement API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
}); 
builder.Services.AddDbContext<EmployeeDBcontext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeconnectionString"),
sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null))
);

builder.Services.AddTransient<IAuthServices, AuthServices>();
builder.Services.AddTransient<IFileUpload, UploadFile>();
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();


builder.Services.AddScoped<INhanVien, NhanVienRepo>();
builder.Services.AddScoped<IChucVu, ChucVuRepo>();

builder.Services.AddScoped<ILuong, LuongRepo>();
builder.Services.AddScoped<IPhongBan, PhongBanRepo>();
builder.Services.AddScoped<IReportService, ReportServiceRepo>();
builder.Services.AddScoped<IHopDong, HopDongRepo>();
builder.Services.AddScoped<INghiPhep, NghiPhepRepo>();
builder.Services.AddScoped<IChamCong, ChamCongRepo>();
builder.Services.AddScoped<ITHanhToan, ThanhToanRepo>();
builder.Services.AddScoped<IlunchShift, LunchShiftRepo>();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RedishDemo_";
    
});

builder.Services.AddIdentity<ExtendIdentity, IdentityRole>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<EmployeeDBcontext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
    };
});




var cofigsetting = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
