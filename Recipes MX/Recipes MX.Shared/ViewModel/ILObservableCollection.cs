using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Recipes_MX.ViewModel
{
    class ILObservableCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        public bool HasMoreItems { get; protected set; }

        public delegate Task<ObservableCollection<T>> LoadDataAsyncDelegate(uint count);
        public event LoadDataAsyncDelegate LoadDataAsyncHandler;

        public ILObservableCollection()
            : base()
        {
            HasMoreItems = true;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(async () =>
            {
                var Result = new LoadMoreItemsResult() { Count = 0 };
                HasMoreItems = false;

                ObservableCollection<T> NewItems = await LoadDataAsyncHandler((uint)this.Count);
                if (NewItems == null || NewItems.Count == 0)
                {
                    return Result;
                }

                await DispatcherHelper.RunAsync(() => {
                    foreach (var i in NewItems)
                    {
                        Add(i);
                    }
                });
                

                HasMoreItems = true;
                Result.Count = (uint)NewItems.Count;
                return Result;
            }).AsAsyncOperation();
        }
    }
}
