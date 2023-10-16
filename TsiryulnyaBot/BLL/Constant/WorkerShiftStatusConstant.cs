using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsiryulnyaBot.BLL.Constant
{
    public static class WorkerShiftStatusConstant
    {
        /// <summary>
        /// Открыто
        /// </summary>
        public static readonly Guid Open = new Guid("f26914c3-d5a0-4a6e-8ae3-684ee42d44a2");

        /// <summary>
        /// Закрыто
        /// </summary>
        public static readonly Guid Closed = new Guid("ad402cc0-7e07-4a0c-83b8-64c4194feb74");
    }
}
