﻿using Microsoft.JSInterop;
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

        public async static Task ErrorToastr(this IJSRuntime runtime, string message)
        {
            await runtime.InvokeVoidAsync("ShowToast", "error", message);
        }

        public async static Task ShowDeleteConfirmation(this IJSRuntime runtime)
        {
            await runtime.InvokeVoidAsync("showDeleteConfirm");
        }

        public async static Task HideDeleteConfirmation(this IJSRuntime runtime)
        {
            await runtime.InvokeVoidAsync("hideDeleteConfirm");
        }
    }
}
