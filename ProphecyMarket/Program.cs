using System;
using System.Collections.Generic;
using Basic_Prophecy_Market.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Basic_Prophecy_Market
{
    class Program
    {
        public static string currentLeague = "Blight";
        static void Main(string[] args)
        {
            List<ValueAnalysis> list = InitUpgrades();

            foreach(ValueAnalysis v in list)
            {
                v.Update();
                Console.WriteLine(v);
            }
        }

        //intitializes the list of prophecies, without market data
        static List<ValueAnalysis> InitUpgrades()
        {
            List<ValueAnalysis> list = new List<ValueAnalysis>();

            #region King's Path
            BaseUnique baseItem = new BaseUnique
            {
                name = "Kaom's Sign",
                type = "Coral Ring"
            };

            ResultUnique resultItem = new ResultUnique
            {
                name = "Kaom's Way",
                type = "Coral Ring"
            };

            UpgradeProphecy prophecy = new UpgradeProphecy
            {
                name = "The King's Path",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region A Dishonourable Death
            baseItem = new BaseUnique
            {
                name = "Hyrri's Bite",
                type = "Sharktooth Arrow Quiver"
            };

            resultItem = new ResultUnique
            {
                name = "Hyrri's Demise",
                type = "Sharktooth Arrow Quiver"
            };

            prophecy = new UpgradeProphecy
            {
                name = "A Dishonourable Death",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region A Forest of False Idols
            baseItem = new BaseUnique
            {
                name = "Araku Tiki",
                type = "Coral Amulet"
            };

            resultItem = new ResultUnique
            {
                name = "Ngamahu Tiki",
                type = "Coral Amulet"
            };

            prophecy = new UpgradeProphecy
            {
                name = "A Forest of False Idols",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region A Rift in Time
            baseItem = new BaseUnique
            {
                name = "Timeclasp",
                type = "Moonstone Ring"
            };

            resultItem = new ResultUnique
            {
                name = "Timetwist",
                type = "Moonstone Ring"
            };

            prophecy = new UpgradeProphecy
            {
                name = "A Rift in Time",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region A Vision of Ice and Fire
            baseItem = new BaseUnique
            {
                name = "Heatshiver",
                type = "Leather Hood"
            };

            resultItem = new ResultUnique
            {
                name = "Frostferno",
                type = "Leather Hood"
            };

            prophecy = new UpgradeProphecy
            {
                name = "A Vision of Ice and Fire",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Agony at Dusk
            baseItem = new BaseUnique
            {
                name = "Dusktoe",
                type = "Ironscale Boots"
            };

            resultItem = new ResultUnique
            {
                name = "Duskblight",
                type = "Ironscale Boots"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Agony at Dusk",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Ancient Doom
            baseItem = new BaseUnique
            {
                name = "Doomfletch",
                type = "Royal Bow"
            };

            resultItem = new ResultUnique
            {
                name = "Doomfletch's Prism",
                type = "Royal Bow"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Ancient Doom",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Battle Hardened
            baseItem = new BaseUnique
            {
                name = "Iron Heart",
                type = "Crusader Plate"
            };

            resultItem = new ResultUnique
            {
                name = "The Iron Fortress",
                type = "Crusader Plate"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Battle Hardened",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Black Devotion
            baseItem = new BaseUnique
            {
                name = "Geofri's Baptism",
                type = "Brass Maul"
            };

            resultItem = new ResultUnique
            {
                name = "Geofri's Devotion",
                type = "Brass Maul"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Black Devotion",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Blind Faith
            baseItem = new BaseUnique
            {
                name = "The Ignomon",
                type = "Gold Amulet"
            };

            resultItem = new ResultUnique
            {
                name = "The Effigon",
                type = "Gold Amulet"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Blind Faith",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Blinding Light
            baseItem = new BaseUnique
            {
                name = "Eclipse Solaris",
                type = "Crystal Wand"
            };

            resultItem = new ResultUnique
            {
                name = "Corona Solaris",
                type = "Crystal Wand"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Blinding Light",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Burning Dread
            baseItem = new BaseUnique
            {
                name = "Dreadarc",
                type = "Cleaver"
            };

            resultItem = new ResultUnique
            {
                name = "Dreadsurge",
                type = "Cleaver"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Burning Dread",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion
            
            #region Cold Blooded Fury
            baseItem = new BaseUnique
            {
                name = "Bloodboil",
                type = "Coral Ring"
            };

            resultItem = new ResultUnique
            {
                name = "Winterweave",
                type = "Coral Ring"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Cold Blooded Fury",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Cold Greed
            baseItem = new BaseUnique
            {
                name = "Cameria's Maul",
                type = "Gavel"
            };

            resultItem = new ResultUnique
            {
                name = "Cameria's Avarice",
                type = "Gavel"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Cold Greed",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            #region Crimson Hues
            baseItem = new BaseUnique
            {
                name = "Goredrill",
                type = "Skinning Knife"
            };

            resultItem = new ResultUnique
            {
                name = "Sanguine Gambol",
                type = "Skinning Knife"
            };

            prophecy = new UpgradeProphecy
            {
                name = "Crimson Hues",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion
            return list;
        }

    }
}
