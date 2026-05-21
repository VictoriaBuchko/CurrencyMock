using Currency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Data
{
    public class MockCurrencyRateRepository : ICurrencyRateRepository
    {
        private readonly Random _random = new();

        public Task<IEnumerable<CurrencyRate>> GetAllRatesAsync()
        {
            // Використовуємо Random для генерації "флуктуючих" курсів
            var rates = new List<CurrencyRate>
        {
            new CurrencyRate
            {
                Id = 1,
                CurrencyCode = "USD",
                Rate = Math.Round(40.00m + (decimal)(_random.NextDouble() * 2.0), 2),
                LastUpdated = DateTime.Now
            },
            new CurrencyRate
            {
                Id = 2,
                CurrencyCode = "EUR",
                Rate = Math.Round(43.00m + (decimal)(_random.NextDouble() * 2.0), 2),
                LastUpdated = DateTime.Now
            },
            new CurrencyRate
            {
                Id = 3,
                CurrencyCode = "GBP",
                Rate = Math.Round(50.00m + (decimal)(_random.NextDouble() * 2.0), 2),
                LastUpdated = DateTime.Now
            }
        };

            return Task.FromResult<IEnumerable<CurrencyRate>>(rates);
        }
    }
}
