using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Services
{
    public interface IEmailService
    {
        Task SendCurrencyAlertAsync(string userEmail, string userName, string currencyCode, decimal targetRate, decimal currentRate);
    }
}
