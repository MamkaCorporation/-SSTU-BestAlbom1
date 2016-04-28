using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class UserM
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordSecond { get; set; }
        public byte[] Avatar { get; set; }
        public string MymeType { get; set; }
        //И еще какието данные. Сколь угодно много

        public UserM() { }
    }
}