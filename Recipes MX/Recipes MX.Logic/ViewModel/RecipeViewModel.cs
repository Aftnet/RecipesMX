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
    public class RecipeViewModel : AppViewModelBase
    {
        /// <summary>
        /// The <see cref="CurrentRecipe" /> property's name.
        /// </summary>
        public const string CurrentRecipePropertyName = "CurrentRecipe";

        private Recipe _currentRecipe = null;

        /// <summary>
        /// Sets and gets the CurrentRecipe property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public Recipe CurrentRecipe
        {
            get
            {
                return _currentRecipe;
            }
            set
            {
                Set(() => CurrentRecipe, ref _currentRecipe, value, true);
            }
        }

        public RecipeViewModel(IDataService dataService, INavigationService navigationService, IPageLocator pageLocator)
            : base(dataService, navigationService, pageLocator)
        {
            MessengerInstance.Register<NotificationMessage<Recipe>>(this, d => LoadRecipe(d));

            if (ViewModelBase.IsInDesignModeStatic == true)
            {
                CurrentRecipe = _dataService.GetFullRecipeDataAsync(0).Result;
            }
        }

        public async void LoadRecipe(NotificationMessage<Recipe> Param)
        {
            if (Param.Notification != Messaging.MessageTokens.LoadRecipePage.ToString())
            {
                return;
            }

            ProgressBarIsVisible = true;
            CurrentRecipe = null;
            CurrentRecipe = await _dataService.GetFullRecipeDataAsync(Param.Content.ID);
            ProgressBarIsVisible = false;
        }
    }
}
