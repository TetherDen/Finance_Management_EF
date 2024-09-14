using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public UserSettings Settings { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
