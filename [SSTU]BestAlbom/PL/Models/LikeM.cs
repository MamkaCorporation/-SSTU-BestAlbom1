using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class LikeM
    {
        public Guid IDPhoto { get; set; }
        public Guid IDUser { get; set; }   //ID того кто поставил лайк
        //Возможно еще что-то
        public LikeM() { }
    }
}