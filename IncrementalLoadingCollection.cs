using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace GameOfThronesApp
{
    class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        uint x = 0;
        public bool HasMoreItems => x < 2000;
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(async cancelToken =>
            {
                //await GOTFacade.AddCharactersToAppAsync(this, count);

                x += count;
                //return the actual number of items loaded (here it's just maxed)
                return new LoadMoreItemsResult { Count = count };
            });
        }

    }
}
