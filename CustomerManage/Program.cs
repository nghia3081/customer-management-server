using BusinessObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Repository.Configuration;
using Repository.Entities;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Repository.Entities.Contract>("Contract");
    builder.EntitySet<Repository.Entities.ContractDetail>("ContractDetail");
    builder.EntitySet<Repository.Entities.Customer>("Customer");
    builder.EntitySet<Repository.Entities.Menu>("Menu");
    builder.EntitySet<Repository.Entities.Package>("PackageCategory");
    builder.EntitySet<Repository.Entities.StatusCategory>("StatusCategory");
    builder.EntitySet<Repository.Entities.TaxCategory>("TaxCategory");
    builder.EntitySet<Repository.Entities.User>("User");
    builder.EnableLowerCamelCase();
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);
var corsPolicy = "CorsPolicy";
// Add services to the container.
builder.Services.AddControllers().AddOData(option =>
{
    option.Select().Filter().Count().OrderBy().Expand()
    .SetMaxTop(100).AddRouteComponents("odata", GetEdmModel());
    option.EnableContinueOnErrorHeader = true;
}); ;
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, build => build.AllowAnyMethod()
        .AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(hostName => true).Build());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter login token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.OrderActionsBy(apiDescription => apiDescription.HttpMethod + apiDescription.RelativePath);

});
builder.Services.AddDbContext<CustomerManageContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddOptions();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddRepoDI();
builder.Services.AddJwtAuthentication();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(corsPolicy);

app.UseHttpsRedirection();
app.UseExceptionHandler("/error");
app.UseAuthorization();

app.MapControllers();
app.Run();
