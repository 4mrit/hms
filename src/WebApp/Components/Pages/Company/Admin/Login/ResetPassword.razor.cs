using hms.Identity.API.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using WebApp.Services;

namespace WebApp.Components.Pages.Company.Admin.Login
{
    public partial class ResetPassword
    {

        [Inject]
        public NavigationManager NavManager { get; set; }

        public string UserId { get; set; }
        public string Code { get; set; }

        string? Password { get; set; }
        string? ConfirmPassword { get; set; }

        private string? requestUri;

        [Inject]
        IHttpClientFactory ClientFactory { get; set; }



        private ApiHelper helper;


        protected override async Task OnInitializedAsync()
        {
            helper = new ApiHelper(ClientFactory, "MyHttpClient");
            base.OnInitialized();

            var uri = new Uri(NavManager.Uri);
            var query = QueryHelpers.ParseQuery(uri.Query);

            UserId = query.TryGetValue("userId", out var userId) ? userId.ToString() : string.Empty;
            Code = query.TryGetValue("code", out var code) ? code.ToString() : string.Empty;
            Console.WriteLine(userId);
            Console.WriteLine(code);
        }

        async public void HandleReset()
        {

            ApplicationUserResetPasswordDTO user = new();
            user.Password = Password;

            try
            {
                requestUri = $"http://localhost:5085/Account/resetpassword";
                var response = await helper.PostRequest<ApplicationUserResetPasswordDTO>(requestUri, user);
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
