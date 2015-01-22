using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Recipes_MX.Logic.Model
{
    public interface IDataService
    {
        ObservableCollection<FeaturedEntry> GetFeaturedEntries();
        Task<ObservableCollection<Recipe>> SearchRecipesAsync(string Query, uint StartItemIx);
        Task<Recipe> GetFullRecipeDataAsync(uint RecipeID);
    }
}
