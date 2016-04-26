using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sub
    {
        /// <summary>
        /// Подписывающийся
        /// </summary>
        public Guid IDUserFirst { get; set; }
        /// <summary>
        /// Юзер, на которого подписываются
        /// </summary>
        public Guid IDUserSecond { get; set; }

        public Sub() { }
    }
}
