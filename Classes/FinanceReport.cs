using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    // Класс Для Получения Финансового Отчета Юзера

    public class FinanceReport
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalBalance { get; set; }
        public int IncomeTransactionsCount { get; set; }
        public int ExpenseTransactionsCount { get; set; }
        public decimal AvgIncomeTransaction { get; set; }
        public decimal AvgExpenseTransaction { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Total Income: {TotalIncome}");
            sb.AppendLine($"Total Expense: {TotalExpense}");
            sb.AppendLine($"Total Balance: {TotalBalance}");
            sb.AppendLine($"Income Transactions Count: {IncomeTransactionsCount}");
            sb.AppendLine($"Expense Transactions Count: {ExpenseTransactionsCount}");
            sb.AppendLine($"Average Income Transaction: {AvgIncomeTransaction}");
            sb.AppendLine($"Average Expense Transaction: {AvgExpenseTransaction}");

            return sb.ToString();
        }
    }

}
