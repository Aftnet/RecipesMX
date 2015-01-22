using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes_MX.Logic.Model;

namespace Recipes_MX.LogicUT
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void TestFeatured()
        {
            var DataSource = new DataService();
            var Featured = DataSource.GetFeaturedEntries();
            Assert.IsNotNull(Featured);
        }

        [TestMethod]
        public void TestSearch()
        {
            var DataSource = new DataService();
            var SearchResult = DataSource.SearchRecipesAsync("pasta", 0).Result;
            Assert.IsNotNull(SearchResult);
        }

        [TestMethod]
        public void TestRecipeDetails()
        {
            var DataSource = new DataService();
            var RecipeDetails = DataSource.GetFullRecipeDataAsync(167010).Result;
            Assert.IsNotNull(RecipeDetails);
        }
    }
}
