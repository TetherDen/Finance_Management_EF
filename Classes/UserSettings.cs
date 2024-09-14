using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    public class UserSettings
    {
        public int Id { set; get; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public int UserId { set; get; }
        public User User { get; set; }
    }
}
