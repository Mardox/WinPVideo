using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.Resources
{
    public class MainPageData
    {
        public MainPageData() { }
        string[] categoryType = {
                                    "Search",
                                    "Playlist"
                                };

        string[] categoryName = {
                                    "New",
                                    "Old",
                                    "Pop",
                                    "Trailers",
                                    "More"
                                };

        string[] queryName = {
                                 "PLIS2cJG94zfc6xv-OZdc3_WXlbJtdgGki",
                                 "PLPciVFPE-Ba-VhnNJ6MIqlIpiWFOribHZ",
                                 "PLBDDB31C2143AAAA1",
                                 "PLmqDyI-k0XEgFcm3ObOy_pNqEQP-1yCGS",
                                 "PL7EdbmN-oWpO8v0_Gb8KoE8b6EpuAy-Pf"
                             };

        public string[] returnCategoryName()
        {
            return categoryName;
        }
        public string[] returnFinalQuery()
        {
            string[] finalQuery = new string[queryName.Length];
            for (int i = 0; i < queryName.Length; i++)
            {
                if (queryName[i].StartsWith("PL"))
                {
                    finalQuery[i] = categoryType[1] + queryName[i];
                }
                else
                {
                    finalQuery[i] = categoryType[0] + queryName[i];
                }
            }
            return finalQuery;
        }
    }
}
