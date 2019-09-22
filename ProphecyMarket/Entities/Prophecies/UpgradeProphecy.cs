using Basic_Prophecy_Market.Entities.Uniques;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market.Entities.Prophecies
{
    class UpgradeProphecy : Prophecy
    {
        public BaseUnique baseItem { get; set; }
        public ResultUnique resultItem { get; set; }
    }
}
