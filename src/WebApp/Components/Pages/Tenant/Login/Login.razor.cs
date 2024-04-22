using hms.Tenant.API.Model;
using System.ComponentModel.DataAnnotations;
using WebApp.Services;


namespace WebApp.Components.Pages.Tenant.Login
{
    //interface UserLoginData 
    //{
    //    string Username { get; set; }
    //    string Password { get; set; }
    //}
    public partial class Login
    {
        string Username { get; set; }
        string Password { get; set; }

        private string requestUri;

        private ApiHelper helper;



        async private void HandleLogin()
        {
            Console.WriteLine(Username, Password);
            Console.WriteLine(" clicked");
            //try
            //{
            //    UserLoginData data =
            //    {
            //         Username= Username,
            //         Password= Password
            //    };
            //    requestUri = $"/api/tenants/";
            //    var response = await helper.PostRequest<UserLoginData>(requestUri,data);

            //}
            //catch (HttpRequestException ex)
            //{
            //    // Handle exceptions (e.g., network errors)
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

        }

    }
}
