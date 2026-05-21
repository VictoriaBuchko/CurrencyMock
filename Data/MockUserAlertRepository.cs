using Currency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Data
{
    public class MockUserAlertRepository : IUserAlertRepository
    {
        private readonly List<UserAlert> _alerts = new()
    {
        new UserAlert { Id = 1, UserId = 1, CurrencyCode = "USD", TargetRate = 41.00m, IsActive = true },
        new UserAlert { Id = 2, UserId = 2, CurrencyCode = "EUR", TargetRate = 44.00m, IsActive = true },
        new UserAlert { Id = 3, UserId = 3, CurrencyCode = "USD", TargetRate = 40.50m, IsActive = true },
        new UserAlert { Id = 4, UserId = 4, CurrencyCode = "GBP", TargetRate = 51.00m, IsActive = true }
    };

        public Task<IEnumerable<UserAlert>> GetActiveAlertsAsync()
        {
            return Task.FromResult<IEnumerable<UserAlert>>(_alerts.Where(a => a.IsActive));
        }

        public Task DeactivateAlertAsync(int alertId)
        {
            var alert = _alerts.FirstOrDefault(a => a.Id == alertId);
            if (alert != null)
            {
                alert.IsActive = false;
            }
            return Task.CompletedTask;
        }
    }
}
