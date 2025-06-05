using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask
{
    internal class User
    { //base user class
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email    { get; set; }
        public string id { get; set; }
        public User(string firstName, string lastName, string email, string id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.id = id;
        }
    }
}
