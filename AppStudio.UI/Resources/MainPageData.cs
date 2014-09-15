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
                                    "Top",
                                    "More"
                                };

        string[] queryName = {
                                 "PLCqWLShJxG30fp-T7OGk3jlhDdwWkCb5-",
                                 "PL-tIaps7Zoh7-9jSr9oA6uP-laav0so6f",
                                 "PLvbn9ju0oTpHGRXlirYJOeMUT1DfU0_nk",
                                 "PLE682492CD8DCD186",
                                 "PLjgSL-s1Odxo5Tx5GWcVkS2SXUXFqYXzB"
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
