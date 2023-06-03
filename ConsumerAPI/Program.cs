using JSONPlaceholderConsumer.Services.IServices;
using JSONPlaceholderConsumer.Services.JSONPlaceholder;
using JSONPlaceholderConsumer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using UtilityLibrary;

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
        Title = "JSON Placeholder Consumer API v1",
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

builder.Services.AddHttpClient<IJSONPlaceholderService, JsonPlaceholderPostService>();
builder.Services.AddScoped<IJSONPlaceholderService, JsonPlaceholderPostService>();
builder.Services.AddHttpClient<JsonPlaceholderUserService>();
builder.Services.AddScoped<JsonPlaceholderUserService>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ConsumerAPIv1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
