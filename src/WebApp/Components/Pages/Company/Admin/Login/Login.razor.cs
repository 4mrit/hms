using System.ComponentModel.DataAnnotations;

namespace WebApp.Components.Pages.Company.Admin.Login
{
    public partial class Login
    {
        string Username { get; set; }
        string Password { get; set; }

        void HandleLogin()
        {
            Console.WriteLine(Username, Password);
            Console.WriteLine(" clicked");

        }

    }
}
