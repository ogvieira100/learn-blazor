using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Services
{
    using Microsoft.JSInterop;


    using System.Threading.Tasks;

    public class ToastrService
    {
        private readonly IJSRuntime _jsRuntime;

        public ToastrService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ShowSuccess(string message, string title = "Sucesso")
        {
            await _jsRuntime.InvokeVoidAsync("toastrNotify", "success", message, title);
        }

        public async Task ShowError(string message, string title = "Erro")
        {
            await _jsRuntime.InvokeVoidAsync("toastrNotify", "error", message, title);
        }

        public async Task ShowInfo(string message, string title = "Info")
        {
            await _jsRuntime.InvokeVoidAsync("toastrNotify", "info", message, title);
        }

        public async Task ShowWarning(string message, string title = "Aviso")
        {
            await _jsRuntime.InvokeVoidAsync("toastrNotify", "warning", message, title);
        }
    }

}
