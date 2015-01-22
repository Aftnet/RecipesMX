using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Recipes_MX.Logic.Model
{
    public class DataServiceDesign : IDataService
    {
        public ObservableCollection<FeaturedEntry> GetFeaturedEntries()
        {
            var Output = new ObservableCollection<FeaturedEntry>()
            {
                new FeaturedEntry() { Title = "Grilled dishes", SearchQuery = "grill", ImageUri="Assets/FeaturedGrill.jpg" },
                new FeaturedEntry() { Title = "Springtime flavors", SearchQuery = "spring", ImageUri="Assets/FeaturedSpring.jpg" },
                new FeaturedEntry() { Title = "Appetizers", SearchQuery = "appetizer", ImageUri="Assets/FeaturedAppetizer.jpg" },
                new FeaturedEntry() { Title = "Yummy desserts", SearchQuery = "dessert", ImageUri="Assets/FeaturedDesserts.jpg" },
                new FeaturedEntry() { Title = "All about bread", SearchQuery = "bread", ImageUri="Assets/FeaturedBread.jpg" },
                new FeaturedEntry() { Title = "Light salads", SearchQuery = "salad", ImageUri="Assets/FeaturedSalad.jpg" },
                new FeaturedEntry() { Title = "Side dishes", SearchQuery = "side", ImageUri="Assets/FeaturedSide.jpg" },
                new FeaturedEntry() { Title = "Invigorating soups", SearchQuery = "soup", ImageUri="Assets/FeaturedSoup.jpg" },
                new FeaturedEntry() { Title = "Drinks", SearchQuery = "drink", ImageUri="Assets/FeaturedDrink.jpg" }
            };
            return Output;
        }

        public async Task<ObservableCollection<Recipe>> SearchRecipesAsync(string Query, uint StartItemIx)
        {
            var Output = new ObservableCollection<Recipe>();
            for (uint i = 0; i < 50; i++)
            {
                var NewRecipe = new Recipe()
                {
                    Title = string.Format("Recipe {0}", i),
                    ImageUri = "Assets/RecipeImg.jpg",
                    ID = i,
                    Category = "Category",
                    Rating = "2.5/5",
                    Cuisine = "#vegan"
                };
                Output.Add(NewRecipe);
            }
            return await Task.FromResult(Output);
        }

        public async Task<Recipe> GetFullRecipeDataAsync(uint RecipeID)
        {
            var ContentBuilder = new StringBuilder();
            ContentBuilder.AppendLine("Place the first seven ingredients in bread machine in order suggested by your manufacturer. Select dough cycle.");
            ContentBuilder.AppendLine("Turn finished dough out onto a lightly floured board and knead 5 to 10 times. Separate into 2 or 3 pieces. Roll with hands into strips. Braid or twist strips together. Place onto a parchment lined baking sheet. Set aside to rise in a warm place until doubled in size.");
            ContentBuilder.AppendLine("Preheat the oven to 350 degrees F (175 degrees C). Whisk together 1 egg white and the water. Brush onto the top of the loaf.");
            ContentBuilder.AppendLine("Bake in the preheated oven until deep golden brown, about 20 minutes.");
            ContentBuilder.AppendLine("");
            var Unit = ContentBuilder.ToString();
            for(uint i=0; i<30; i++)
            {
                ContentBuilder.Append(Unit);
            }

            var Output = new Recipe()
            {
                Title = string.Format("Recipe {0}", RecipeID),
                ImageUri = "Assets/RecipeImg.jpg",
                ID = RecipeID,
                Category = "Category",
                Rating = "2.5/5",
                Cuisine = "#vegan",
                Content = ContentBuilder.ToString()
            };
            return await Task.FromResult(Output);
        }
    }
}
