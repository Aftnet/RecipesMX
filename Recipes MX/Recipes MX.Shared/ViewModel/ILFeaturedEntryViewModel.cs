using GalaSoft.MvvmLight;
using Recipes_MX.Logic.Common;
using Recipes_MX.Logic.Model;
using Recipes_MX.Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Recipes_MX.ViewModel
{
    class ILFeaturedEntryViewModel : FeaturedEntryViewModel
    {
        ILObservableCollection<Recipe> ILSearchResults;

        public ILFeaturedEntryViewModel(IDataService dataService, INavigationService navigationService, IPageLocator pageLocator)
            : base(dataService, navigationService, pageLocator)
        {
            if (ViewModelBase.IsInDesignModeStatic == true) return;

            ILSearchResults = new ILObservableCollection<Recipe>();
            ILSearchResults.LoadDataAsyncHandler += ILSearchResults_LoadDataAsyncHandler;
            SearchResults = ILSearchResults;
        }

        async Task<ObservableCollection<Recipe>> ILSearchResults_LoadDataAsyncHandler(uint count)
        {
            return await _dataService.SearchRecipesAsync(CurrentFeaturedEntry.SearchQuery, count);
        }
    }
}
