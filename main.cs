namespace Finance_Management
{
    internal class Program
    {
        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();

        static void Main(string[] args)
        {
            Initialize();

            FinanceService fs = new FinanceService();

            //// Создание и добавление транзакции в userId = 1
            var transaction = InputHelper.GetTransactionFromUser();
            fs.AddTransaction(1, transaction);

            //// 2. Просмотр списка транзакций  userId = 1
            var userTransactionList = fs.GetUserTransactionList(1);
            foreach (var item in userTransactionList)
            {
                Console.WriteLine(item);
            }

            //3.Расчет общего дохода и расхода: Пользователь может просматривать общую сумму доходов и расходов за определенный период времени.
            decimal income = fs.GetIncomeByDate(1, new DateTime(2020, 1, 1), new DateTime(2025, 1, 1));
            decimal expense = fs.GetExpenseByDate(1, new DateTime(2020, 1, 1), new DateTime(2025, 1, 1));

            //4.Фильтрация транзакций: Пользователь может фильтровать транзакции по типу(доход или расход) и периоду времени.
            var userTransactions = fs.GetTransactionsByCategoryAndDate(1, CategoryType.Income, new DateTime(2020, 1, 1), new DateTime(2025, 1, 1));

            //5. Отчет о состоянии финансов: Пользователь может получать отчет о текущем состоянии своих финансов,
            // включая общий доход, расход, баланс и статистику по категориям транзакций.

            var finReport = fs.GetFinanceReport(1);
            Console.WriteLine(finReport);

            //5) Прямое использование команд Sql и обращение к Хранимым процедурам.
            var result5 = fs.GetTransactionsBySqlRaw(1);
            foreach (var item in result5)
            {
                Console.WriteLine(item);
            }


        }

        static void Initialize()
        {
            DbInit dbInit = new DbInit();
            dbInit.DbEnsureCreated();
            dbInit.Init();
        }
    }
}
