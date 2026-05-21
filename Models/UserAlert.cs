using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Models
{
    public class UserAlert
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public decimal TargetRate { get; set; }
        public bool IsActive { get; set; }
    }
}
