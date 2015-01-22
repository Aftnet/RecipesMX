using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace Recipes_MX.Logic.Model
{
    public class FeaturedEntry : ObservableObject
    {
        /// <summary>
        /// The <see cref="Title" /> property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        private string _title = string.Empty;

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Set(() => Title, ref _title, value);
            }
        }

        /// <summary>
        /// The <see cref="ImageUri" /> property's name.
        /// </summary>
        public const string ImageUriPropertyName = "ImageUri";

        private string _imageUri = string.Empty;

        /// <summary>
        /// Sets and gets the ImageUri property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ImageUri
        {
            get
            {
                return _imageUri;
            }
            set
            {
                Set(() => ImageUri, ref _imageUri, value);
            }
        }

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
                Set(() => SearchQuery, ref _searchQuery, value);
            }
        }
    }
}
