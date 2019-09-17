using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market
{
    interface IItem
    {
        string name { get; set; }
        string type { get; }

        //set up stats here if needed
    }
}
