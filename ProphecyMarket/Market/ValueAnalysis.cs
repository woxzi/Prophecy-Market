using Basic_Prophecy_Market.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Prophecy_Market.Market
{
    interface ValueAnalysis
    {
        string name { get; }
        double totalCost { get; set; }
        double profit { get; set; }
        bool isUpdated { get; }
        string type { get; }
        void Update();
        Dictionary<IItem, double> costs { get; }
    }
}
