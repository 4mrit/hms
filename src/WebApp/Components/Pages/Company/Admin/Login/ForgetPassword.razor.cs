using thms.Identity.API.DTOs;
using Microsoft.AspNetCore.Components;
using WebApp.Services;

namespace WebApp.Components.Pages.Company.Admin.Login
{
    public partial class ForgetPassword
    {
        string? Username { get; set; }
        
        private string? requestUri;

        [Inject]
        IHttpClientFactory ClientFactory { get; set; }



        private ApiHelper helper;

        protected override async Task OnInitializedAsync()
        {


            helper = new ApiHelper(ClientFactory, "MyHttpClient");
            base.OnInitialized();
        }
        async public void HandleSubmit() {

            ApplicationUserForgetPasswordDTO user = new();
            user.Email = Username;

            try
            {
                requestUri = $"http://localhost:5085/Account/forgotpassword";
                var response = await helper.PostRequest<ApplicationUserForgetPasswordDTO>(requestUri, user);
                Console.WriteLine(response);
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions (e.g., network errors)
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


    }
}
