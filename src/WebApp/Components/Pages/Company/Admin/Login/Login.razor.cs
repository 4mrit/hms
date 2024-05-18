using hms.Tenant.API.Model;
using System.ComponentModel.DataAnnotations;
using WebApp.Services;
using hms.Identity.API.DTOs;
using Microsoft.AspNetCore.Components;
using BlazorBootstrap;


namespace WebApp.Components.Pages.Company.Admin.Login
{



    public partial class Login
    {

        

        string ? Username { get; set; }
        string? Password { get; set; }

        private string? requestUri;

        [Inject]
        IHttpClientFactory ClientFactory { get; set; }

        [Inject] protected ToastService ToastService { get; set; }
        // Services

        private ApiHelper helper;
        //private NotificationHelper NotificationHelper;

        protected override async Task OnInitializedAsync()
        {


            helper = new ApiHelper(ClientFactory, "MyHttpClient");
            base.OnInitialized();
        }

        async private void HandleLogin()
        {
            //data.Username = Username;
            Console.WriteLine(Username);
            Console.WriteLine(Password);


            ApplicationUserLoginDTO user = new ApplicationUserLoginDTO();
            user.EmailOrUserName = Username;
            user.Password = Password;

            try
            {
                requestUri = $"http://localhost:5085/Account/login";
                var response = await helper.PostRequest<ApplicationUserLoginDTO>(requestUri, user);
                Console.WriteLine(response);

                if (response.IsSuccessStatusCode)
                {
                    

                        ToastService.Notify(new(ToastType.Success, $"Login Successful"));
                    

                }
                else
                {
                    ToastService.Notify(new(ToastType.Danger, $"Login Failed"));


                }




            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions (e.g., network errors)
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

    }
}
