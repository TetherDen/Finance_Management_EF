using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace Finance_Management
{
    public class FinanceService
    {

        //1. Добавление транзакций: Пользователь может добавлять новые транзакции, указывая тип(доход или расход), сумму и описание.
        //  СОЗДАНИЕ ТРАНЗАКЦИИ В InputHelper !
        public void AddTransaction(int userId, Transaction transaction)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    transaction.UserId = userId;
                    transaction.User = user;
                    db.Transactions.Add(transaction);
                    db.SaveChanges();

                    Console.WriteLine(TextColor.SuccessfulText("Transaction added successfully."));
                }
            }
        }

        //2. Просмотр списка транзакций: Пользователь может просматривать список всех транзакций с указанием их типа, суммы и даты.
        public List<Transaction> GetUserTransactionList(int userId)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                return db.Transactions.Where(e => e.UserId == userId).Include(e => e.Category).ToList();

            }
        }

        //5) Прямое использование команд Sql и обращение к Хранимым процедурам.
        public List<Transaction> GetTransactionsBySqlRaw(int userId)
        {
            using(ApplicationContext db = Program.DbContext())
            {
                SqlParameter param = new SqlParameter("@userId", userId);
                return db.Transactions.FromSqlRaw("SELECT * FROM [Transactions] WHERE [UserId] = @userId", param).Include(e=>e.Category).ToList();
            }
        }



        public decimal GetIncomeByDate(int userId, DateTime start, DateTime end)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                return db.Transactions
                    .Include(e=>e.Category)
                    .Where(e=> e.UserId == userId && e.Date >= start && e.Date <= end && e.Category.CategoryType == CategoryType.Income)
                    .Sum(e=>e.Amount);

            }
        }

        //3. Расчет общего дохода и расхода: Пользователь может просматривать общую сумму доходов и расходов за определенный период времени.
        public decimal GetExpenseByDate(int userId, DateTime start, DateTime end)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                return db.Transactions
                    .Include (e=>e.Category)
                    .Where(e=>e.UserId==userId && e.Date >= start && e.Date <= end && e.Category.CategoryType == CategoryType.Expense)
                    .Sum(e=>e.Amount);
            }
        }

        //4. Фильтрация транзакций: Пользователь может фильтровать транзакции по типу(доход или расход) и периоду времени.
        public List<Transaction> GetTransactionsByCategoryAndDate(int userId, CategoryType type, DateTime start, DateTime end)
        {
            using (ApplicationContext db = Program.DbContext())
            {
                return  db.Transactions.Include(e=>e.Category)
                    .Where(e=> e.UserId == userId && e.Date >= start && e.Date <= end && e.Category.CategoryType == type).ToList();
            }
        }

        //5. Отчет о состоянии финансов: Пользователь может получать отчет о текущем состоянии своих финансов,
        // включая общий доход, расход, баланс и статистику по категориям транзакций.
        public FinanceReport? GetFinanceReport (int userId )
        {
            using (ApplicationContext db = Program.DbContext())
            {
                return db.Users.Where(u => u.Id == userId).Select(u => new FinanceReport
                {
                    TotalIncome = u.Transactions.Where(t => t.Category.CategoryType == CategoryType.Income).Sum(t => (decimal?)t.Amount) ?? 0,
                    TotalExpense = u.Transactions.Where(t => t.Category.CategoryType == CategoryType.Expense).Sum(t => (decimal?)t.Amount) ?? 0,
                    TotalBalance = (u.Transactions.Where(t => t.Category.CategoryType == CategoryType.Income).Sum(t => (decimal?)t.Amount) ?? 0)- (u.Transactions.Where(t => t.Category.CategoryType == CategoryType.Expense).Sum(t => (decimal?)t.Amount) ?? 0),
                    IncomeTransactionsCount = u.Transactions.Count(t => t.Category.CategoryType == CategoryType.Income),
                    ExpenseTransactionsCount = u.Transactions.Count(t => t.Category.CategoryType == CategoryType.Expense),
                    AvgIncomeTransaction = u.Transactions.Where(t => t.Category.CategoryType == CategoryType.Income).Average(t => (decimal?)t.Amount) ?? 0,
                    AvgExpenseTransaction = u.Transactions.Where(t => t.Category.CategoryType == CategoryType.Expense).Average(t => (decimal?)t.Amount) ?? 0
                }).FirstOrDefault();
            }
        }

    }
}
