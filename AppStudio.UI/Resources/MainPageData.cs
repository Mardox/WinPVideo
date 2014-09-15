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
                                 "PLvyP6vkINv2c3Ys4cveQdZYlOIb8VJiTa",
                                 "PL34FC12DA2F3D00D8",
                                 "PL74F8B1C82A4614B9",
                                 "PL304B8FDD0E191EC2",
                                 "PLA4FCA936861CF01A"
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
