using ApiBranch.Models;
using ApiBranch.Services.Contract;
using ApiBranch.Services.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionTest"));
});

builder.Services.AddScoped<ICurrencyService,CurrencyService>();
builder.Services.AddScoped<IBranchService, BranchService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Peticiones Api Rest
app.MapGet("/currency/list", async (
    ICurrencyService _currencyServices
    ) =>
{
    List<CurrencyTest> lstCurrency = await _currencyServices.GetList();
    if (lstCurrency.Count > 0)
        return Results.Ok(lstCurrency);
    else
        return Results.NotFound();

}


) ;
#endregion


app.Run();

