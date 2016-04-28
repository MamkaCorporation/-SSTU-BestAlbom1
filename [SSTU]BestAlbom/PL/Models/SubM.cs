using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class SubM
    {
        /// <summary>
        /// Подписывающийся
        /// </summary>
        public Guid IDUserFirst { get; set; }
        /// <summary>
        /// Юзер, на которого подписываются
        /// </summary>
        public Guid IDUserSecond { get; set; }

        public SubM() { }
    }
}