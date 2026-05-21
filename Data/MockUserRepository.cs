using Currency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Data
{
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "Олексій Іваненко", Email = "oleksiy@gmail.com" },
        new User { Id = 2, Name = "Марія Коваль", Email = "maria@gmail.com" },
        new User { Id = 3, Name = "Дмитро Шевченко", Email = "dmytro@gmail.com" },
        new User { Id = 4, Name = "Анна Петренко", Email = "anna@gmail.com" }
    };

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return Task.FromResult<IEnumerable<User>>(_users);
        }
    }
}
