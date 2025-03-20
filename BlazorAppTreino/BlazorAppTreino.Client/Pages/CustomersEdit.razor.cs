using BlazorAppTreino.Domain.Models;
using BlazorAppTreino.Domain.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BlazorAppTreino.Client.Pages
{
    public partial class CustomersEdit
    {
        [Parameter] public string CustomerId { get; set; }

        readonly IHttpClientFactory _httpClientFactory;
        readonly HttpClient _httpClient;
        Customers Customer;
        readonly IJSRuntime _jSRuntime;
        IJSObjectReference _jObjectReference;
        private string cpf = "21371885893";
        private string cpf2 = "21371885893";

        private void AddAddress()
        {
            Customer.Addresses.Add(new Address());
        }

        async Task LoadScript() {

            _jObjectReference = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/customer.js");

        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
                 await  LoadScript();
        }

        private void RemoveAddress(Address address)
        {
            Customer.Addresses.Remove(address);
        }

        async Task SaveCustomer()
        {
            await _jSRuntime.InvokeVoidAsync("messageToast", "'ola'");
            //toastr["success"]("My name is Inigo Montoya. You killed my father. Prepare to die!")
            //_jObjectReference = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/CustomersEdit.razor.js");
            // await _jObjectReference.InvokeVoidAsync("showalert");
            // Simular salvamento no banco de dados
            Console.WriteLine("Cliente salvo: " + Customer.CPF);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }



        protected override async Task OnParametersSetAsync()
        {
            if (Int64.TryParse(CustomerId, out var Id))
            {
                var customerResponse =  await _httpClient.GetFromJsonAsync<ResponseApi<Customers>>($"api/customers/{Id}");
                if (customerResponse != null)
                {
                    Customer = customerResponse.Obj;
                }
            }
        }
        public CustomersEdit(IHttpClientFactory httpClientFactory, IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime; 
            Customer = new Customers();
            _httpClient = httpClientFactory.CreateClient("ApiTreino");
        }
    }
}
