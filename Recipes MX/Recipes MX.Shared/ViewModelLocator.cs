using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Recipes_MX.Common;
using Recipes_MX.Logic.Common;
using Recipes_MX.Logic.Model;
using Recipes_MX.Logic.ViewModel;
using Recipes_MX.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes_MX
{
    public class ViewModelLocator
    {
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public FeaturedEntryViewModel Featured
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ILFeaturedEntryViewModel>();
            }
        }

        public RecipeViewModel Recipe
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RecipeViewModel>();
            }
        }

        public SearchViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ILSearchViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic == false)
            {
                SimpleIoc.Default.Register<IDataService, DataService>(true);
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataServiceDesign>(true);
            }

            SimpleIoc.Default.Register<IDialogService, DialogService>(true);
            SimpleIoc.Default.Register<INavigationService, NavigationService>(true);
            SimpleIoc.Default.Register<IPageLocator, PageLocator>(true);

            SimpleIoc.Default.Register<MainViewModel>(true);

            SimpleIoc.Default.Register<ILFeaturedEntryViewModel>(true);
            SimpleIoc.Default.Register<RecipeViewModel>(true);
            SimpleIoc.Default.Register<ILSearchViewModel>(true);
        }
    }
}
