using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace Recipes_MX.Logic.Model
{
    public class Recipe : ObservableObject
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
        /// The <see cref="ID" /> property's name.
        /// </summary>
        public const string IDPropertyName = "ID";

        private uint _id = 0;

        /// <summary>
        /// Sets and gets the ID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public uint ID
        {
            get
            {
                return _id;
            }
            set
            {
                Set(() => ID, ref _id, value);
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
        /// The <see cref="Category" /> property's name.
        /// </summary>
        public const string CategoryPropertyName = "Category";

        private string _category = string.Empty;

        /// <summary>
        /// Sets and gets the Category property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                Set(() => Category, ref _category, value);
            }
        }

        /// <summary>
        /// The <see cref="Cuisine" /> property's name.
        /// </summary>
        public const string CuisinePropertyName = "Cuisine";

        private string _cuisine = string.Empty;

        /// <summary>
        /// Sets and gets the Cuisine property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Cuisine
        {
            get
            {
                return _cuisine;
            }
            set
            {
                Set(() => Cuisine, ref _cuisine, value);
            }
        }

        /// <summary>
        /// The <see cref="Rating" /> property's name.
        /// </summary>
        public const string RatingPropertyName = "Rating";

        private string _rating = string.Empty;

        /// <summary>
        /// Sets and gets the Rating property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                Set(() => Rating, ref _rating, value);
            }
        }

        /// <summary>
        /// The <see cref="Content" /> property's name.
        /// </summary>
        public const string ContentPropertyName = "Content";

        private string _content = string.Empty;

        /// <summary>
        /// Sets and gets the Content property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                Set(() => Content, ref _content, value);
            }
        }
    }
}