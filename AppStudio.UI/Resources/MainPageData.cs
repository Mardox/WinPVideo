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
                                    "Cool Stuff",
                                    "Projects",
                                    "Lectures",
                                    "Tutorials",
                                    "More"
                                };

        string[] queryName = {
                                 "PL1F8EF58643170AA6",
                                 "PLFE341BEC13576CB6",
                                 "PL550E6E2E14B96822",
                                 "PL9tn9rGywKUVvN_cOyh4TBH9L1yvjp16t",
                                 "PLGjRnIE6hf46IrASMqBYnWVnFL1TwjaRT"
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
