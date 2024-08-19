using System.Text;
using DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserAuth.Models;
using UserAuth.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

var jwt =
	builder.Configuration.GetSection("Jwt").Get<Jwt>()
	?? throw new
	InvalidOperationException("No field for Jwt in appsettings");

builder
	.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
		var signingCreds = new SigningCredentials(
			signingKey,
			SecurityAlgorithms.HmacSha256Signature
		);

		options.RequireHttpsMetadata = false;

		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateAudience = true,
			ValidAudience = jwt.Audience,
			ValidateIssuer = true,
			ValidIssuer = jwt.Issuer,
			IssuerSigningKey = signingKey,
			ValidateLifetime = true,
			ClockSkew = TimeSpan.Zero,
		};
		options.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = context =>
			{
				var logger = context.HttpContext.RequestServices.GetRequiredService<
					ILogger<Program>
				>();
				logger.LogError("Authentication failed", context.Exception);
				Console.WriteLine(logger.ToString());
				return Task.CompletedTask;
			},
		};
	});

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding dbcontext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
	throw new InvalidOperationException("No sql server connection string at appsetting.json");

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

//configuring loggin
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetRequiredService<UserContext>();
dataContext.Database.Migrate();

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
