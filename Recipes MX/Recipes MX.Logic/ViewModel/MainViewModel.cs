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
    public class MainViewModel : AppViewModelBase
    {
        /// <summary>
        /// The <see cref="FeaturedEntries" /> property's name.
        /// </summary>
        public const string FeaturedEntriesPropertyName = "FeaturedEntries";

        private ObservableCollection<FeaturedEntry> _featuredEntries = null;

        /// <summary>
        /// Sets and gets the FeaturedEntries property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public ObservableCollection<FeaturedEntry> FeaturedEntries
        {
            get
            {
                return _featuredEntries;
            }
            set
            {
                Set(() => FeaturedEntries, ref _featuredEntries, value, true);
            }
        }

        private RelayCommand<FeaturedEntry> _featuredEntryClickedCommand;

        /// <summary>
        /// Gets the FeaturedEntryClickedCommand.
        /// </summary>
        public RelayCommand<FeaturedEntry> FeaturedEntryClickedCommand
        {
            get
            {
                return _featuredEntryClickedCommand
                    ?? (_featuredEntryClickedCommand = new RelayCommand<FeaturedEntry>(FeaturedEntryClickedCommandHandler));
            }
        }

        private void FeaturedEntryClickedCommandHandler(FeaturedEntry Param)
        {
            Type TargetPage = _pageLocator.GetPageType("FeaturedEntryPage");
            _navigationService.Navigate(TargetPage);
            var MessageParam = new NotificationMessage<FeaturedEntry>(Param, Messaging.MessageTokens.LoadFeaturedPage.ToString());
            MessengerInstance.Send<NotificationMessage<FeaturedEntry>>(MessageParam);
        }

        public MainViewModel(IDataService dataService, INavigationService navigationService, IPageLocator pageLocator)
            : base(dataService, navigationService, pageLocator)
        {
            FeaturedEntries = _dataService.GetFeaturedEntries();
        }
    }
}
