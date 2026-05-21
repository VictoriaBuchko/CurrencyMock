using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendCurrencyAlertAsync(string userEmail, string userName, string currencyCode, decimal targetRate, decimal currentRate)
        {
            // Логуємо відправлені листи для тестування
            _logger.LogInformation(
                "EMAIL відправлено на {Email} | Користувач: {Name} | Валюта: {Code} | Цільовий курс: {Target} | Поточний курс: {Current}",
                userEmail, userName, currencyCode, targetRate, currentRate);

            return Task.CompletedTask;
        }
    }
}
