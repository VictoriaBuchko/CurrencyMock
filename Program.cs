using Currency;
using Currency.Data;
using Currency.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IUserRepository, MockUserRepository>();
builder.Services.AddScoped<ICurrencyRateRepository, MockCurrencyRateRepository>();
builder.Services.AddScoped<IUserAlertRepository, MockUserAlertRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHostedService<CurrencyMonitoringService>();

var host = builder.Build();

host.Run();
