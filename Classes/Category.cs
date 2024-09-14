using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    public enum CategoryType
    {
        Income,
        Expense
    }
    public class Category
    {
        public int Id { set; get; }
        public CategoryType CategoryType { get; set; }

    }
}
