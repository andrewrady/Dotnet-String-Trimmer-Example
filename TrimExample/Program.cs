using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TrimExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        var jsonSettings = options.SerializerSettings;

        jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        jsonSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = false
            }
        };
        jsonSettings.Formatting = Formatting.Indented;

        jsonSettings.Converters.Add(new StringTrimmingOnReadConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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