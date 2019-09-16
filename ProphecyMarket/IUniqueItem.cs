using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market
{
    interface IUniqueItem : IItem
    {
        new string type { get; set; }
    }
}
