using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    public static class InputHelper
    {
        public static Transaction GetTransactionFromUser()
        {
            decimal amount = GetAmountFromUser();
            string description = GetDescriptionFromUser();
            CategoryType type = GetCategoryTypeFromUser();

            using (ApplicationContext db = Program.DbContext())
            {
                // Достаем категорию перред добавлением
                var category = db.Categories.FirstOrDefault(e => e.CategoryType == type);  

                return new Transaction
                {
                    Amount = amount,
                    Description = description,
                    Date = DateTime.Now,
                    CategoryId = category.Id
                };
            }

        }

        private static decimal GetAmountFromUser()
        {
            decimal amount;
            Console.Write(TextColor.RequestText("Enter the $ transaction amount: "));
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine(TextColor.ErrorText("Invalid input. Try again."));
            }
            return amount;
        }

        private static string GetDescriptionFromUser()
        {
            Console.Write(TextColor.RequestText("Enter description: "));
            return Console.ReadLine();
        }

        private static CategoryType GetCategoryTypeFromUser()
        {
            CategoryType type;
            Console.Write(TextColor.RequestText("Enter type (Income/Expense): "));
            string typeInput = Console.ReadLine();

            while (!Enum.TryParse(typeInput, true, out type) || !Enum.IsDefined(typeof(CategoryType), type))
            {
                Console.WriteLine(TextColor.ErrorText("Invalid type. Try again."));
                typeInput = Console.ReadLine();
            }

            return type;
        }




    }
}
