using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market
{
    class Prophecy : IItem
    {
        public string type { get { return "Prophecy"; } }
        public string name { get; set; }
    }
}
