using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DataHelper;
using System.Text.Json.Serialization;
using StudentManagementSystem.API.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.json");


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        var serializerOptions = options.JsonSerializerOptions;
        serializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
       
    });


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddDbContext<IStudentManagementDbContext, StudentManagementDbContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your_issuer",
        ValidAudience = "your_audience",
        IssuerSigningKey = GenerateSymmetricSecurityKey()
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

SymmetricSecurityKey GenerateSymmetricSecurityKey()
{
    using var provider = new RNGCryptoServiceProvider();
    var key = new byte[32]; 
    provider.GetBytes(key);

    return new SymmetricSecurityKey(key);
}