using ApiBranch.Models;
using ApiBranch.Services.Contract;
using ApiBranch.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApiBranch.Mappers;
using ApiBranch.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//se inyecta la cadena de conexión
builder.Services.AddDbContext<TestDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionTest"));
});

//Se injecta la logica de negocio
builder.Services.AddScoped<ICurrencyService,CurrencyService>();
builder.Services.AddScoped<IBranchService, BranchService>();

//Se injecta el AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//Configuración de los cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", app => {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Peticiones Api Rest Currency
//Trae la lista de las monedas
app.MapGet("/currency/list", async (
    ICurrencyService _currencyServices,
    IMapper _mapper
    ) =>
{
    //Obtiene la Lista
    List<CurrencyTest> lstCurrency = await _currencyServices.GetList();
    //Mapea la lista para regresar el dato esperado
    List<CurrencyMapper> lstCurrencyMapper = _mapper.Map<List<CurrencyMapper>>(lstCurrency);

    if (lstCurrencyMapper.Count > 0)
        return Results.Ok(lstCurrencyMapper);
    else
        return Results.NotFound();

}


) ;
#endregion
#region Peticiones Api Rest Branch
//Trae el listado de las sucursales
app.MapGet("/branches/list", async (
    IBranchService _branchService,
    IMapper _mapper
    ) =>
{
    //Obtiene la Lista
    List<BranchTest> lstBranches = await _branchService.GetList();
    //Mapea la lista para regresar el dato esperado
    List<BranchMapper> lstBranchesMapper = _mapper.Map<List<BranchMapper>>(lstBranches);

    if (lstBranchesMapper.Count > 0)
        return Results.Ok(lstBranchesMapper);
    else
        return Results.NotFound();
});


//Agrega una nueva sucursal
app.MapPost("/branches/add", async (
    BranchMapper model,
    IBranchService _branchService,
    IMapper _mapper
    ) =>
{
    //Mapea el objeto recibido para que sea posible guardarlo en la BD
    BranchTest branch = _mapper.Map<BranchTest>(model);
    BranchTest branchCreate = await _branchService.Add(branch);
    //Mapea la lista para regresar el dato esperado
    BranchMapper branchResult = _mapper.Map<BranchMapper>(branchCreate);

    if (branchResult.IdBranch != 0)
        return Results.Ok(branchResult);
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});


//Actualiza una sucursal
app.MapPut("/branches/update/{idBranch}", async (
    int idBranch,
    BranchMapper model,
    IBranchService _branchService,
    IMapper _mapper
    ) =>
{
    //Busca la sucursal por ID
    BranchTest _encontrado = await _branchService.Get(idBranch);

    if (_encontrado is null) return Results.NotFound();

    BranchTest branch = _mapper.Map<BranchTest>(model);

    //Mapea el objeto recibido para que sea posible guardarlo en la BD
    _encontrado.BranchAddress = branch.BranchAddress;
    _encontrado.BranchCode = branch.BranchCode;
    _encontrado.BranchDateCreation = branch.BranchDateCreation;
    _encontrado.BranchDescription = branch.BranchDescription;
    _encontrado.BranchId = branch.BranchId;

    var result = await _branchService.Update(_encontrado);

    if (result)
        //Mapea la lista para regresar el dato esperado
        return Results.Ok(_mapper.Map<BranchMapper>(_encontrado));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);

});

//Elimina una sucursal
app.MapDelete("/branches/delete/{idBranch}", async (
    int idBranch,
    IBranchService _branchService
    ) =>
{
    //Busca la sucursal por ID
    BranchTest _encontrado = await _branchService.Get(idBranch);

    if (_encontrado is null) return Results.NotFound();

    var result = await _branchService.Delete(_encontrado);

    if (result)
        return Results.Ok();
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);

});

#endregion

//Se agrega la configuración de los cors
app.UseCors("corsPolicy");

app.Run();

