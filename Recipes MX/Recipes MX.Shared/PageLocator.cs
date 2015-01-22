using Recipes_MX.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes_MX
{
    class PageLocator : IPageLocator
    {
        private Dictionary<string, Type> PageDict;

        public PageLocator()
        {
            PageDict = new Dictionary<string, Type>()
            { 
                { "FeaturedEntryPage", typeof(FeaturedEntryPage) },
                { "MainPage", typeof(MainPage) },
                { "RecipePage", typeof(RecipePage) },
                { "SearchPage", typeof(SearchPage) }
            };
        }

        public Type GetPageType(string PageName)
        {
            return PageDict[PageName];
        }
    }

    partial class FeaturedEntryPage { }
    partial class MainPage { }
    partial class RecipePage { }
    partial class SearchPage { }
}
