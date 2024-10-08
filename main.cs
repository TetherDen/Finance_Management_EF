﻿namespace Finance_Management
{
    internal class Program
    {
        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();

        static void Main(string[] args)
        {
            Initialize();

            FinanceService fs = new FinanceService();

            //// Создание и добавление транзакции в userId = 1
            var transaction = InputHelper.GetTransactionFromUser();   // Здесь Ввод данных транзакции
            fs.AddTransaction(1, transaction);      // Добавление транзакции в БД

            //// 2. Просмотр списка транзакций  userId = 1
            var userTransactionList = fs.GetUserTransactionList(1);      // Получаем Лист Транзакции Юзера
            foreach (var item in userTransactionList)
            {
                Console.WriteLine(item);
            }

            //3.Расчет общего дохода и расхода: Пользователь может просматривать общую сумму доходов и расходов за определенный период времени.
            decimal income = fs.GetIncomeByDate(1, new DateTime(2020, 1, 1), new DateTime(2025, 1, 1));
            decimal expense = fs.GetExpenseByDate(1, new DateTime(2020, 1, 1), new DateTime(2025, 1, 1));

            //4.Фильтрация транзакций: Пользователь может фильтровать транзакции по типу(доход или расход) и периоду времени.
            var userTransactions = fs.GetTransactionsByCategoryAndDate(1, CategoryType.Income, new DateTime(2020, 1, 1), new DateTime(2025, 1, 1));  // Указываем тип транзакции для фильрации

            //5. Отчет о состоянии финансов: Пользователь может получать отчет о текущем состоянии своих финансов,
            // включая общий доход, расход, баланс и статистику по категориям транзакций.

            var finReport = fs.GetFinanceReport(1);     // Возвращает Класс Finance Report по конкрретному юзеру
            Console.WriteLine(finReport);               // и распечатваем данные Finance Report'а

            //5) Прямое использование команд Sql и обращение к Хранимым процедурам.
            var result5 = fs.GetTransactionsBySqlRaw(1);        // Метод через SqlRaw + SqlParameter
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
