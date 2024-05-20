using System.Text;
using FacultyApp;
using FacultyApp.ApiKey;
using FacultyApp.Attributes;
using FacultyApp.Middleware;
using FacultyApp.Notifications;
using FacultyApp.Repository.Interfaces;
using FacultyApp.Repository.Implementations;
using FacultyApp.Services.Interfaces;
using FacultyApp.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var pgConnectionString = builder.Configuration.GetConnectionString("PostgresStudentsDB");
builder.Services.AddDbContext<StudentsDbContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseNpgsql(pgConnectionString)
);

builder.Services.AddAuthentication(options =>
    {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{   
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtAuth:Issuer"],
        ValidAudience = builder.Configuration["JwtAuth:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JwtAuth:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
    o.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["X-Access-Token"];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
builder.Services.AddScoped<IAccountsService, AccountsService>();

builder.Services.AddSignalR();

var allOrigins = "AllowAllOrigins";
var allowAngularFront = "AllowAngularFront";

builder.Services.AddCors(options => {
    options.AddPolicy(
            name: allOrigins,
            policy => {
                policy.AllowAnyOrigin(); 
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
        });
    options.AddPolicy(
            name: allowAngularFront,
            policy => {
                policy.WithOrigins("http://localhost:4200");
                policy.AllowAnyHeader();
                policy.AllowCredentials();
                policy.AllowAnyMethod();
        });
});

builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<ICoursesService, CoursesService>();

builder.Services.AddScoped<IExaminationsRepository, ExaminationsRepository>();
builder.Services.AddScoped<ITeacherExaminationsService, TeacherExaminationsService>();
builder.Services.AddScoped<IStudentExaminationsService, StudentExaminationsService>();

builder.Services.AddScoped<IApiKeyValidator, SingleApiKeyValidator>();

builder.Services.AddScoped<AdminApiKeyAuthorizationFilter>();
builder.Services.AddScoped<RequireCourseOwnershipFilter>();
builder.Services.AddScoped<StudentExaminationApplicationFilter>();

builder.Services.AddTransient<NotificationsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<UserClaimsMiddleware>();

app.UseCors(allowAngularFront);

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationsHub>("/notifs");

app.Run();