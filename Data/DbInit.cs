using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    public class DbInit
    {
        public void DbEnsureCreated()
        {
            using (ApplicationContext db = Program.DbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
        public void Init()
        {
            using (ApplicationContext db = Program.DbContext())
            {
                // Инициализация пользователей
                if (!db.Users.Any())
                {
                    db.Users.AddRange(new User[]
                    {
                new User { Email = "jane.doe@example.com" },
                new User { Email = "john.smith@example.com" },
                new User { Email = "alice.johnson@example.com" }
                    });
                    db.SaveChanges();
                }

                // Инициализация настроек пользователей
                if (!db.UserSettings.Any())
                {
                    var users = db.Users.ToList(); // Получаем всех пользователей после их добавления

                    db.UserSettings.AddRange(new UserSettings[]
                    {
                new UserSettings
                {
                    UserName = "Jane Doe",
                    Age = 28,
                    UserId = users.FirstOrDefault(u => u.Email == "jane.doe@example.com").Id
                },
                new UserSettings
                {
                    UserName = "John Smith",
                    Age = 34,
                    UserId = users.FirstOrDefault(u => u.Email == "john.smith@example.com").Id
                },
                new UserSettings
                {
                    UserName = "Alice Johnson",
                    Age = 22,
                    UserId = users.FirstOrDefault(u => u.Email == "alice.johnson@example.com").Id
                }
                    });
                    db.SaveChanges();
                }

                // Инициализация категорий
                if (!db.Categories.Any())
                {
                    db.Categories.AddRange(new Category[]
                    {
                new Category { CategoryType = CategoryType.Income },
                new Category { CategoryType = CategoryType.Expense }
                    });
                    db.SaveChanges();
                }

                // Инициализация транзакций
                if (!db.Transactions.Any())
                {
                    var categories = db.Categories.ToList();
                    var users = db.Users.ToList();

                    db.Transactions.AddRange(new Transaction[]
                    {
                new Transaction
                {
                    Amount = 150.00m,
                    Description = "Freelance work payment",
                    Date = DateTime.Now.AddDays(-10),
                    Category = categories.FirstOrDefault(c => c.CategoryType == CategoryType.Income),
                    UserId = users.FirstOrDefault(u => u.Email == "jane.doe@example.com").Id
                },
                new Transaction
                {
                    Amount = 50.00m,
                    Description = "Groceries",
                    Date = DateTime.Now.AddDays(-5),
                    Category = categories.FirstOrDefault(c => c.CategoryType == CategoryType.Expense),
                    UserId = users.FirstOrDefault(u => u.Email == "john.smith@example.com").Id
                },
                new Transaction
                {
                    Amount = 75.00m,
                    Description = "Books purchase",
                    Date = DateTime.Now.AddDays(-2),
                    Category = categories.FirstOrDefault(c => c.CategoryType == CategoryType.Expense),
                    UserId = users.FirstOrDefault(u => u.Email == "alice.johnson@example.com").Id
                }
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
