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
                                    "Basics",
                                    "Customize",
                                    "How to",
                                    "WP & Bootstrap",
                                    "More"
                                };

        string[] queryName = {
                                 "PLC5E59DD6D84D34DC",
                                 "PLLnpHn493BHGACfv4rC29kJamYMtw34D9",
                                 "PLCAC66DEEFFEDD36E",
                                 "PLwn9oKDowiP9Yz_80QdJ5hjwU--V9hSD7",
                                 "wordpress development" //Playlist ID
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
