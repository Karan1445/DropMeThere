using dropmethereapi.Repos.admin_dashboard;
using dropmethereapi.Repos.HelperSideViewRides;
using dropmethereapi.Repos.History;
using dropmethereapi.Repos.SeekerRequestHandler;
using dropmethereapi.Repos.UserFunctions;
using dropmethereapi.Repos.UserLogin;
using dropmethereapi.Repos.VehicalRegistration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.ListenAnyIP(5036);
    //serverOptions.Listen(IPAddress.Parse("192.168.43.21"),5036);
    serverOptions.Listen(IPAddress.Any, 5036);
    serverOptions.Listen(IPAddress.Loopback, 5036);
});
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("*", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
             
    });
});
builder.Services.AddControllers().AddFluentValidation(r => r.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<LoginRegistrations>();
builder.Services.AddScoped<VehicalRegistration>();
builder.Services.AddScoped<AdminUserRepo>();
builder.Services.AddScoped<SeekerRequestHandler>();
builder.Services.AddScoped<Users>();
builder.Services.AddScoped<HistoryAll>();
builder.Services.AddScoped<HelperSideViewRidesRepo>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer =true,
        ValidateAudience =true,
        ValidateLifetime =true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
var app = builder.Build();
app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("*");
app.UseAuthorization();


app.MapControllers();

app.Run();
