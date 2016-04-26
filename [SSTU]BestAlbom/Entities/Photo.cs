using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Photo
    {
        public Guid IDPhoto { get; set; }
        public Guid IDUser { get; set; }
        public string Name { get; set; }
        public string MymeType { get; set; }
        public byte[] File { get; set; }
        public int Rating { get; set; }         
        public Photo() { Rating = 0; }
        //И еще какието данные. Сколь угодно много
    }
}
