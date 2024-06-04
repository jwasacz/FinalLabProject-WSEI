using SoapCore;
using SoapService.ServiceContract;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();
builder.Services.AddSingleton<ICarWashService, CarWashService>();
var app = builder.Build();
app.UseSoapEndpoint<ICarWashService>("/CarWashService.asmx", new SoapEncoderOptions());
app.Run();

