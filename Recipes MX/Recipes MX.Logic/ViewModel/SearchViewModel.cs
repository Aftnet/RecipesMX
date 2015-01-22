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
    public class SearchViewModel : AppViewModelBase
    {
        private bool CurrentlySearching { get; set; }
        private bool QueryChangedWhileSearching { get; set; }

        /// <summary>
        /// The <see cref="SearchQuery" /> property's name.
        /// </summary>
        public const string SearchQueryPropertyName = "SearchQuery";

        private string _searchQuery = string.Empty;

        /// <summary>
        /// Sets and gets the SearchQuery property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                Set(() => SearchQuery, ref _searchQuery, value, true);
            }
        }

        /// <summary>
        /// The <see cref="SearchResults" /> property's name.
        /// </summary>
        public const string RecipesListPropertyName = "SearchResults";

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

        public SearchViewModel(IDataService dataService, INavigationService navigationService, IPageLocator pageLocator)
            : base(dataService, navigationService, pageLocator)
        {
            CurrentlySearching = false;
            QueryChangedWhileSearching = false;
            SearchResults = new ObservableCollection<Recipe>();

            MessengerInstance.Register<NotificationMessage<string>>(this, d => LoadSearchPageHandler(d));

            if (ViewModelBase.IsInDesignModeStatic == true)
            {
                SearchQuery = "Search query";
                SearchResults = _dataService.SearchRecipesAsync(SearchQuery, 0).Result;
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

        private RelayCommand<string> _searchQueryCommand;

        // <summary>
        /// Gets the SearchQueryCommand.
        /// </summary>
        public RelayCommand<string> SearchQueryCommand
        {
            get
            {
                return _searchQueryCommand
                    ?? (_searchQueryCommand = new RelayCommand<string>(SearchQueryCommandHandler));
            }
        }

        protected async void SearchQueryCommandHandler(string Param)
        {
            ProgressBarIsVisible = true;
            SearchQuery = Param;
            if (CurrentlySearching == true)
            {
                QueryChangedWhileSearching = true;
                return;
            }

            CurrentlySearching = true;
            QueryChangedWhileSearching = false;
            var TempSearchResults = await _dataService.SearchRecipesAsync(SearchQuery, 0);
            CurrentlySearching = false;

            if(QueryChangedWhileSearching == true)
            {
                SearchQueryCommandHandler(SearchQuery);
            }
            else
            {
                SearchResults.Clear();
                ProgressBarIsVisible = false;
                if (TempSearchResults == null)
                {
                    return;
                }
                foreach(var i in TempSearchResults)
                {
                    SearchResults.Add(i);
                }
            }
        }

        public void LoadSearchPageHandler(NotificationMessage<string> Param)
        {
            if (Param.Notification != Messaging.MessageTokens.LoadSearchPage.ToString())
            {
                return;
            }

            SearchResults.Clear();
            SearchQueryCommandHandler(Param.Content);
        }
    }
}
