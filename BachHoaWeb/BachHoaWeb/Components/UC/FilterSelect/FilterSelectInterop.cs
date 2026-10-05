using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FilterSelect
{
    public class FilterSelectInterop : IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _module;
        private readonly DotNetObjectReference<object> _dotNetRef;
        private ElementReference _wrapperRef;

        public FilterSelectInterop(IJSRuntime jsRuntime, object dotNetRef, ElementReference wrapperRef)
        {
            _jsRuntime = jsRuntime;
            _dotNetRef = DotNetObjectReference.Create(dotNetRef);
            _wrapperRef = wrapperRef;
        }

        public async Task InitializeAsync()
        {
            _module = await _jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./js/filteredSelectHelper.js");

            if (_module != null)
            {
                await _module.InvokeVoidAsync("attachOutsideClick", _wrapperRef, _dotNetRef);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_module != null)
            {
                await _module.InvokeVoidAsync("detachOutsideClick", _wrapperRef);
                await _module.DisposeAsync();
            }

            _dotNetRef.Dispose();
        }
    }
}
