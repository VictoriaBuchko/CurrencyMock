using Currency;
using Currency.Data;
using Currency.Services;

var builder = Host.CreateApplicationBuilder(args);

// Реєструємо репозиторії
builder.Services.AddScoped<IUserRepository, MockUserRepository>();
builder.Services.AddScoped<ICurrencyRateRepository, MockCurrencyRateRepository>();
builder.Services.AddScoped<IUserAlertRepository, MockUserAlertRepository>();

// Реєструємо сервіси
builder.Services.AddScoped<IEmailService, EmailService>();

// Реєструємо фоновий сервіс
builder.Services.AddHostedService<CurrencyMonitoringService>();

var host = builder.Build();

host.Run();