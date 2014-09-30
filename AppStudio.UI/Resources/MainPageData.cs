﻿using System;
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
                                    "Tutorials",
                                    "Draw Flowers",
                                    "How to Draw",
                                    "Drawing Basics",
                                };

        string[] queryName = {
                                 "draw rose tutorials",
                                 "draw flowers",
                                 "how to draw a rose",
                                 "PLtG4P3lq8RHFRfdirLJKk822fwOxR6Zn6",
                             };

        public string[] drawItemsList = {
                                            "Dragon 1",
                                            "Dragon 2",
                                            "Dragon 3",
                                            "Dragon 4",
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
