using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Recipes_MX.Logic.Common;
using Recipes_MX.Logic.Model;
using Recipes_MX.Logic.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Recipes_MX.Logic.ViewModel
{
    public class FeaturedEntryViewModel : AppViewModelBase
    {
        /// <summary>
        /// The <see cref="CurrentFeaturedEntry" /> property's name.
        /// </summary>
        public const string CurrentFeaturedEntryPropertyName = "CurrentFeaturedEntry";

        private FeaturedEntry _currentFeaturedEntry = null;

        /// <summary>
        /// Sets and gets the CurrentFeaturedEntry property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public FeaturedEntry CurrentFeaturedEntry
        {
            get
            {
                return _currentFeaturedEntry;
            }
            set
            {
                Set(() => CurrentFeaturedEntry, ref _currentFeaturedEntry, value, true);
            }
        }

        /// <summary>
        /// The <see cref="SearchResults" /> property's name.
        /// </summary>
        public const string SearchResultsPropertyName = "SearchResults";

        private ObservableCollection<Recipe> _searchResults = null;

        /// <summary>
        /// Sets and gets the SearchResults property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public ObservableCollection<Recipe> SearchResults
        {
            get
            {
                return _searchResults;
            }
            set
            {
                Set(() => SearchResults, ref _searchResults, value, true);
            }
        }

        private RelayCommand<Recipe> _recipeClickedCommand;

        /// <summary>
        /// Gets the RecipeClickedCommand.
        /// </summary>
        public RelayCommand<Recipe> RecipeClickedCommand
        {
            get
            {
                return _recipeClickedCommand
                    ?? (_recipeClickedCommand = new RelayCommand<Recipe>(RecipeClickedCommandHandler));
            }
        }

        private void RecipeClickedCommandHandler(Recipe Param)
        {
            Type TargetPage = _pageLocator.GetPageType("RecipePage");
            _navigationService.Navigate(TargetPage);
            var MessageParam = new NotificationMessage<Recipe>(Param, Messaging.MessageTokens.LoadRecipePage.ToString());
            MessengerInstance.Send<NotificationMessage<Recipe>>(MessageParam);
        }

        public FeaturedEntryViewModel(IDataService dataService, INavigationService navigationService, IPageLocator pageLocator)
            : base(dataService, navigationService, pageLocator)
        {
            MessengerInstance.Register<NotificationMessage<FeaturedEntry>>(this, d => LoadFeaturedRecipes(d));

            if (ViewModelBase.IsInDesignModeStatic == true)
            {
                CurrentFeaturedEntry = new FeaturedEntry() { Title = "Featured", SearchQuery = "searchquery", ImageUri = "Assets/RecipeImg.jpg" };
                SearchResults = _dataService.SearchRecipesAsync(CurrentFeaturedEntry.SearchQuery, 0).Result;
            }
        }

        public async void LoadFeaturedRecipes(NotificationMessage<FeaturedEntry> Param)
        {
            if (Param.Notification != Messaging.MessageTokens.LoadFeaturedPage.ToString())
            {
                return;
            }

            ProgressBarIsVisible = true;
            CurrentFeaturedEntry = Param.Content;
            SearchResults.Clear();
            var TempResults = await _dataService.SearchRecipesAsync(CurrentFeaturedEntry.SearchQuery, 0);
            foreach(var i in TempResults)
            {
                SearchResults.Add(i);
            }
            ProgressBarIsVisible = false;
        }
    }
}
