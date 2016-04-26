using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Like
    {
        public Guid IDPhoto { get; set; }
        public Guid IDUser { get; set; }   //ID того кто поставил лайк
        //Возможно еще что-то
        public Like() { }
    }
}
