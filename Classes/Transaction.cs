using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { set; get; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Transaction ID: {Id}");
            sb.AppendLine($"Amount: {Amount}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Date: {Date:yyyy-MM-dd}");
            sb.AppendLine($"Category: {Category.CategoryType}");
            return sb.ToString();
        }
    }
}
