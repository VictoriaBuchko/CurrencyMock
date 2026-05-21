using Currency.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Services
{
    public class CurrencyMonitoringService : BackgroundService
    {
        private readonly ILogger<CurrencyMonitoringService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);

        public CurrencyMonitoringService(
            ILogger<CurrencyMonitoringService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Сервіс моніторингу валют запущено");

            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckCurrencyRatesAsync(stoppingToken);

                _logger.LogInformation("Наступна перевірка через 5 хвилин");
                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("Сервіс моніторингу валют зупинено");
        }

        private async Task CheckCurrencyRatesAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Перевірка курсів валют: {Time}", DateTime.Now);

            try
            {
                using var scope = _serviceProvider.CreateScope();

                var currencyRateRepository = scope.ServiceProvider.GetRequiredService<ICurrencyRateRepository>();
                var userAlertRepository = scope.ServiceProvider.GetRequiredService<IUserAlertRepository>();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var rates = await currencyRateRepository.GetAllRatesAsync();
                var activeAlerts = await userAlertRepository.GetActiveAlertsAsync();
                var users = await userRepository.GetAllUsersAsync();

                foreach (var alert in activeAlerts)
                {
                    if (stoppingToken.IsCancellationRequested) break;

                    var currentRate = rates.FirstOrDefault(r => r.CurrencyCode == alert.CurrencyCode);
                    if (currentRate == null) continue;

                    _logger.LogInformation(
                        "Валюта: {Code} | Поточний курс: {Rate} | Цільовий: {Target}",
                        alert.CurrencyCode, currentRate.Rate, alert.TargetRate);

                    // Перевіряємо чи досяг курс цільового значення
                    if (currentRate.Rate >= alert.TargetRate)
                    {
                        var user = users.FirstOrDefault(u => u.Id == alert.UserId);
                        if (user == null) continue;

                        await emailService.SendCurrencyAlertAsync(
                            user.Email,
                            user.Name,
                            alert.CurrencyCode,
                            alert.TargetRate,
                            currentRate.Rate);

                        // Деактивуємо налаштування після відправки
                        await userAlertRepository.DeactivateAlertAsync(alert.Id);

                        _logger.LogInformation(
                            "Сповіщення відправлено користувачу {Name}, налаштування деактивовано",
                            user.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка під час перевірки курсів валют");
            }
        }
    }
}
