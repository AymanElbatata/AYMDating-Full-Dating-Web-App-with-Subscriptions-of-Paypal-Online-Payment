using Microsoft.JSInterop;

namespace AYMDating.Blazor.Data.Helpers
{
    public class JsInteropService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ShowAlert(string message)
        {
            await _jsRuntime.InvokeVoidAsync("showAlert", message);
        }
    }
}
