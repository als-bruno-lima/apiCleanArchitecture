using System.Text.Json.Serialization;
using cleanArchitecture.Application.IRepository;
using cleanArchitecture.Application.IService;
using cleanArchitecture.Application.Service;
using cleanArchitecture.Infraestructure;
using cleanArchitecture.Infraestructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("cleanArchitectureApi")));

builder.Services.AddTransient<IApplicationContext, ApplicationContext>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("apiClean", new OpenApiInfo
    {
        Title = "API clean Prueba",
        Version = "2.0",
        Description = "API clean de prueba para el Desafio Backend",

    });
    var jwtSecurityScheme = new OpenApiSecurityScheme
{
    BearerFormat = "JWT",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = JwtBearerDefaults.AuthenticationScheme,
    Description = "Ingrese el token JWT en el campo de texto a continuaci�n.",
    Reference = new OpenApiReference
    {
        Type = ReferenceType.SecurityScheme,
        Id = JwtBearerDefaults.AuthenticationScheme
    }
};

c.AddSecurityDefinition("Bearer", jwtSecurityScheme);
c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {

        c.SwaggerEndpoint("/swagger/apiClean/swagger.json", "API Clean V2.0");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
