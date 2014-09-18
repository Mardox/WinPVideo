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
                                    "Movie Song",
                                    "More"
                                };

        string[] queryName = {
                                 "PLXvVUvHFbzXa-Q4KHUKp2H2_F7gOLErur",
                                 "PLFB7CD5D03FFB98A7",
                                 "PLEA875158D4401ECF",
                                 "PL21O_SWI5zjBj1VgdtzgWQPUlilE_7Su-",
                                 "kannada full songs"
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
