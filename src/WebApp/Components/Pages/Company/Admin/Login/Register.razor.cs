using BlazorBootstrap;
using Bogus.DataSets;
using hms.Identity.API.DTOs;
using Microsoft.AspNetCore.Components;
using WebApp.Services;


namespace WebApp.Components.Pages.Company.Admin.Login
{


    public partial class Register
    {
        [Inject] IHttpClientFactory ClientFactory { get; set; }
        [Inject] protected ToastService ToastService { get; set; }

        [Inject] NavigationManager NavigationManager { get; set; }

        private ApiHelper helper;


        string Username { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }


        protected override async Task OnInitializedAsync()
        {


            helper = new ApiHelper(ClientFactory, "MyHttpClient");
            base.OnInitialized();
        }

        async void HandleRegister()
        {
            if (PhoneNumber.Length == 10 && PhoneNumber.StartsWith("98") && PhoneNumber.All(char.IsDigit))
            {

                if (Password == ConfirmPassword)
                {
                    if (Password.Any(char.IsUpper) && Password.Any(char.IsDigit) && Password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    ApplicationUserRegisterDTO user = new ApplicationUserRegisterDTO();
                    user.Email = Email;
                    user.PhoneNumber = PhoneNumber;
                    user.Password = Password;
                    user.UserName = Username;

                    try
                    {
                        var requestUri = $"http://localhost:5085/Account/register";
                        var response = await helper.PostRequest<ApplicationUserRegisterDTO>(requestUri, user);
                        Console.WriteLine(response);

                        if (response.IsSuccessStatusCode)
                        {



                            ToastService.Notify(new(ToastType.Success, $"Registration Successful"));
                            await Task.Delay(3000);

                            NavigationManager.NavigateTo("/admin");
                        }
                        else
                        {
                            ToastService.Notify(new(ToastType.Danger, $"Registration Failed"));


                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        // Handle exceptions (e.g., network errors)
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    ToastService.Notify(new(ToastType.Danger, $"Password is Weak"));

                }

            }
            else
            {
                ToastService.Notify(new(ToastType.Danger, $"Password do not match"));

            }
            }
            else
            {
                ToastService.Notify(new(ToastType.Danger, $"Provide Correct PhoneNumber"));

            }
        }
    }
}
