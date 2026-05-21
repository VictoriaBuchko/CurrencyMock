using Currency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Data
{
    public interface ICurrencyRateRepository
    {
        Task<IEnumerable<CurrencyRate>> GetAllRatesAsync();
    }
}
