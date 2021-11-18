using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdMan_server.Helpers
{
    public static class Helper
    {

        public async static Task SuccessToastr(this IJSRuntime runtime, string message)
        {
            await runtime.InvokeVoidAsync("ShowToast", "success", message);
        }
    }
}
