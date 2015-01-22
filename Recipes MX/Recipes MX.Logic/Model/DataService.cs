using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Recipes_MX.Logic.Model
{
    public class DataService : IDataService
    {
        protected const string BigOvenAPIKey = "dvx38f7eCz6EP82X4fKjF62geRIPf7N6";
        protected readonly Uri BigOvenAPIBaseUri = new Uri("http://api.bigoven.com/");
        protected const uint ResultsPerPage = 50;

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
            if(string.IsNullOrEmpty(Query) || string.IsNullOrWhiteSpace(Query))
            {
                return new ObservableCollection<Recipe>(); 
            }

            string PageContent = string.Empty;
            using(var Client = new HttpClient())
            {
                uint PageNo = (StartItemIx) / ResultsPerPage;
                var RequestUri = new Uri(BigOvenAPIBaseUri, string.Format("recipes?title_kw={0}&pg={1}&rpp={2}&api_key={3}", Query, PageNo, ResultsPerPage, BigOvenAPIKey));
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                HttpResponseMessage Response = await Client.GetAsync(RequestUri);
                if(Response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                PageContent = await Response.Content.ReadAsStringAsync();
            }

            Model.BigOvenAPI.RecipeSearchResult SearchResults = null;
            using(var PageContentReader = new StringReader(PageContent))
            {
                var Serializer = new System.Xml.Serialization.XmlSerializer(typeof(Model.BigOvenAPI.RecipeSearchResult));
                try
                {
                    SearchResults = Serializer.Deserialize(PageContentReader) as Model.BigOvenAPI.RecipeSearchResult;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            var Output = new ObservableCollection<Recipe>();
            foreach(var i in SearchResults.Results)
            {
                var NewRecipe = new Recipe()
                {
                    Title = i.Title,
                    ImageUri = i.ImageURL120,
                    ID = i.RecipeID,
                    Category = i.Category,
                    Cuisine = i.Cuisine,
                    Rating = string.Format("{0}/5", i.StarRating.ToString("F1"))
                };
                Output.Add(NewRecipe);
            }
            return Output; 
        }

        public async Task<Recipe> GetFullRecipeDataAsync(uint RecipeID)
        {
            if (RecipeID == 0)
            {
                return new Recipe();
            }

            string PageContent = string.Empty;
            using (var Client = new HttpClient())
            {
                var RequestUri = new Uri(BigOvenAPIBaseUri, string.Format("recipe/{0}?api_key={1}", RecipeID, BigOvenAPIKey));
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                HttpResponseMessage Response = await Client.GetAsync(RequestUri);
                if (Response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                PageContent = await Response.Content.ReadAsStringAsync();
            }

            Model.BigOvenAPI.Recipe RecipeDetails = null;
            using (var PageContentReader = new StringReader(PageContent))
            {
                var Serializer = new System.Xml.Serialization.XmlSerializer(typeof(Model.BigOvenAPI.Recipe));
                try
                {
                    RecipeDetails = Serializer.Deserialize(PageContentReader) as Model.BigOvenAPI.Recipe;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            var Output = new Recipe()
            {
                Title = RecipeDetails.Title,
                ImageUri = RecipeDetails.ImageURL,
                ID = RecipeDetails.RecipeID,
                Category = RecipeDetails.Category,
                Cuisine = RecipeDetails.Cuisine,
                Rating = string.Format("{0}/5", RecipeDetails.StarRating.ToString("N1")),
                Content = RecipeDetails.Instructions
            };
            return Output;
        }
    }
}
