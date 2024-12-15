using CryptoPredict.Api.Extensions;
using CryptoPredict.Api.Interfaces;
using CryptoPredict.Api.Schema.Types;
using CryptoPredict.Api.Services;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICryptoPriceService, CryptoPriceService>();
builder.Services.AddSingleton<IUserScoreDataService, UserScoreDataService>();
builder.Services.AddStorageService(configuration);
builder.Services.AddHttpClient<ICryptoPriceService,CryptoPriceService>(configuration);
builder.Services.AddSingleton<ISchema, CryptoPriceSchema>(services => new CryptoPriceSchema(services.GetRequiredService<IServiceProvider>()));
builder.Services.AddSingleton<UserScoreDataType>();
builder.Services.AddSingleton<ISchema, UserScoreSchema>(services => new UserScoreSchema(services.GetRequiredService<IServiceProvider>()));
builder.Services.AddSingleton<CryptoPriceQuery>();
builder.Services.AddSingleton<MarketRangeType>();
builder.Services.AddSingleton<PricePointType>();
builder.Services.AddSingleton<MarketCapPointType>();
builder.Services.AddSingleton<VolumePointType>();
builder.Services.AddGraphQL(b => b
                .AddHttpMiddleware<ISchema>()
                .AddSystemTextJson()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddSchema<CryptoPriceSchema>()
                .AddGraphTypes(typeof(CryptoPriceSchema).Assembly));


var app = builder.Build();

app.UseCors(
		corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
	);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
	app.UseCors(
		corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
	);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseGraphQL<ISchema>();
app.UseGraphQLPlayground();
app.Run();
