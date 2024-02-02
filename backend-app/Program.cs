//using
using backend_app.IRepository;
using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using backend_app.Services;
using backend_app.Services.dashboard;
using backend_app.Services.home;
using backend_app.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddRateLimiter(RateLimiterOptions =>
{
    RateLimiterOptions.AddFixedWindowLimiter("fixed", op =>
    {
        op.PermitLimit = 1;
        op.Window = TimeSpan.FromDays(1);
        op.QueueLimit = 0;
    });
    RateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(op =>
    {
        op.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
    });

builder.Services.AddDbContext<DatabaseContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("myConnection")));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
//dashboard
builder.Services.AddScoped<IFeedbackServices, FeedbackService>();
builder.Services.AddScoped<IProfileStudent, ProfileStudentServices>();
builder.Services.AddScoped<IProfileStaff, ProfileStaffService>();
builder.Services.AddScoped<ICategory, CategoryServices>();
builder.Services.AddScoped<IArticle, ArticleServices>();
builder.Services.AddScoped<IContactUs, ContactUsServices>();
builder.Services.AddScoped<ICourses, CoursesService>();
builder.Services.AddScoped<IFaculty, FacultyService>();
builder.Services.AddScoped<IDepartment, DepartmentServices>();
builder.Services.AddScoped<IFacilitie, FacilitieService>();
builder.Services.AddScoped<IStaff, StaffServices>();
builder.Services.AddScoped<IAdmission, AdmissionServices>();
builder.Services.AddScoped<IStudent, StudentServices>();
builder.Services.AddScoped<ISessions, SessionServices>();
builder.Services.AddScoped<IStaffLogin, StaffLoginServices>();
builder.Services.AddScoped<IStudentLogin, StudentLoginServices>();
builder.Services.AddScoped<ISemester, SemesterServices>();

//home
builder.Services.AddScoped<IAdmissionHome, AdmissionServicesHome>();
builder.Services.AddScoped<IHome, HomeServices>();
builder.Services.AddScoped<ICoursesHome, CoursesHomeServices>();
builder.Services.AddScoped<IHomeArtical, HomeArticalService>();
builder.Services.AddScoped<IHomeFacilities, HomeFacilitieService>();
builder.Services.AddScoped<IHomeFeedbackServices, HomeFeedbackService>();

var allowOrigin = builder.Configuration.GetSection("AllowOrigin").Get<string[]>();

builder.Services.AddCors(op =>
{
    op.AddPolicy("MyAppCors", policy =>
    {
        policy.WithOrigins(allowOrigin).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), "Image")),
    RequestPath = "/Image"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseCors("MyAppCors");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
