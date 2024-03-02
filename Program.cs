using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using RESTful_API__ASP.NET_Core.DbContext;
using RESTful_API__ASP.NET_Core.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//support to XML and JSON always else return not acceptable for text/plain
builder.Services.AddControllers(
    options => { options.ReturnHttpNotAcceptable = true; })
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddDbContext<DBContext>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AppAspConnection"),
        b => b.MigrationsAssembly(typeof(DBContext).Assembly.FullName));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for downloading static file types
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

//registering the service
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
