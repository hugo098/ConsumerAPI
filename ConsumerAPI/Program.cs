using ConsumerAPI.Services.IServices;
using ConsumerAPI.Services.JSONPlaceholder;
using ConsumerAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using UtilityLibrary;
using ConsumerAPI.Data;
using Microsoft.EntityFrameworkCore;
using ConsumerAPI.Repositories.IRepository;
using ConsumerAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "Consumer API v1",
        Contact = new OpenApiContact
        {
            Name = "Hugo Rodriguez",
            Url = new Uri("https://github.com/hugo098")
        },
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "Consumer API v2",
        Contact = new OpenApiContact
        {
            Name = "Hugo Rodriguez",
            Url = new Uri("https://github.com/hugo098")
        },
    });
});
builder.Services.AddMvc().ConfigureApiBehaviorOptions(opt =>
{
    opt.InvalidModelStateResponseFactory = context =>
    {
        var problems = new CustomBadRequest(context);
        return new BadRequestObjectResult(problems);
    };
    opt.SuppressMapClientErrors = true;
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddHttpClient<IJSONPlaceholderService, JsonPlaceholderPostService>();
builder.Services.AddScoped<IJSONPlaceholderService, JsonPlaceholderPostService>();
builder.Services.AddHttpClient<JsonPlaceholderUserService>();
builder.Services.AddScoped<JsonPlaceholderUserService>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddDbContext<NorthwindContext>
    (options => options.UseSqlite("Name=NorthwindDB"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ConsumerAPIv1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "ConsumerAPIv2");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
