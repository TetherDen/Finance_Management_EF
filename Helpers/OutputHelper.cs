using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    public static class OutputHelper
    {
        public static void PrintTransactions(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"ID: {transaction.Id}\n" +
                    $" Amount: {transaction.Amount}\n" +
                    $" Description: {transaction.Description}\n" +
                    $" Date: {transaction.Date:yyyy-MM-dd}\n" +
                    $" Category: {transaction.Category?.CategoryType}\n");
            }
        }

        public static void PrintFinanceReport(FinanceReport report)
        {
            Console.WriteLine("Finance Report:");
            Console.WriteLine($"Total Income: {report.TotalIncome}");
            Console.WriteLine($"Total Expense: {report.TotalExpense}");
            Console.WriteLine($"Total Balance: {report.TotalBalance}");
            Console.WriteLine($"Number of Income Transactions: {report.IncomeTransactionsCount}");
            Console.WriteLine($"Number of Expense Transactions: {report.ExpenseTransactionsCount}");
            Console.WriteLine($"Average Income Transaction: {report.AvgIncomeTransaction}");
            Console.WriteLine($"Average Expense Transaction: {report.AvgExpenseTransaction}");
        }



    }
}
