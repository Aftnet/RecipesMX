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
    public class AppViewModelBase : ViewModelBase
    {
        protected IDataService _dataService;
        protected INavigationService _navigationService;
        protected IPageLocator _pageLocator;

        public AppViewModelBase(IDataService dataService, INavigationService navigationService, IPageLocator pageLocator)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            _pageLocator = pageLocator;
        }

        /// <summary>
        /// The <see cref="ProgressBarIsVisible" /> property's name.
        /// </summary>
        public const string ProgressBarIsVisiblePropertyName = "ProgressBarIsVisible";

        private bool _progressBarIsVisible = false;

        /// <summary>
        /// Sets and gets the ProgressBarIsVisible property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool ProgressBarIsVisible
        {
            get
            {
                return _progressBarIsVisible;
            }
            set
            {
                Set(() => ProgressBarIsVisible, ref _progressBarIsVisible, value, true);
            }
        }

        private RelayCommand<string> _launchSearchCommand;

        /// <summary>
        /// Gets the LaunchSearchCommand.
        /// </summary>
        public RelayCommand<string> LaunchSearchCommand
        {
            get
            {
                return _launchSearchCommand
                    ?? (_launchSearchCommand = new RelayCommand<string>(LaunchSearchCommandHandler));
            }
        }

        private void LaunchSearchCommandHandler(string Query)
        {
            Type TargetPage = _pageLocator.GetPageType("SearchPage");
            _navigationService.Navigate(TargetPage);
            var MessageParam = new NotificationMessage<string>(Query, Messaging.MessageTokens.LoadSearchPage.ToString());
            MessengerInstance.Send<NotificationMessage<string>>(MessageParam);
        }

        private RelayCommand _goBackCommand;

        /// <summary>
        /// Gets the GoBackCommand.
        /// </summary>
        public RelayCommand GoBackCommand
        {
            get
            {
                return _goBackCommand
                    ?? (_goBackCommand = new RelayCommand(GoBackCommandHandler));
            }
        }

        private void GoBackCommandHandler()
        {
            _navigationService.GoBack();
        }
    }
}
