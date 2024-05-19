using thms.Tenant.API.Model;
using Microsoft.CodeAnalysis.Differencing;
using System.Collections.ObjectModel;
using WebApp.Services;
using System.Collections.ObjectModel;
using thms.Tenant.API.Model;
using thms.Media.API.Model;
using Microsoft.AspNetCore.Components;

namespace WebApp.Components.Pages.Company.Admin
{
    public partial class CreateNewTenant
    {

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IHttpClientFactory ClientFactory { get; set; }

        private HospitalTenant tenants = new HospitalTenant();



        private ApiHelper helper;

        public class FeatureViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }



        private FeatureViewModel[] getfeatures;

        protected override async Task OnInitializedAsync()
        {
            try
            {

                helper = new ApiHelper(ClientFactory, "MyHttpClient");
                base.OnInitialized();


                // Api request to get list of all the features
                var requestUri = "api/Tenants/Feature";
                getfeatures = await helper.GetRequest<FeatureViewModel[]>(requestUri);

                // Api request to get Tenants Detail



            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

            }
        }

        private string name;
        private string address;
        private string url;
        private string mediaRootPath;
        private string primaryColorHexValue;
        private string secondaryColorHexValue;
        private string primaryDatabase;
        private string secondaryDatabase;

        private async Task DetailSubmit()
        {



            tenants.Name = name;
            tenants.Address = address;
            tenants.Url = url;
            tenants.MediaRootPath = mediaRootPath;


            tenants.Features = new List<Feature>();
            foreach (var featuredata in getfeatures)
            {
                if (featuredata.IsChecked)
                {
                    Feature newfeature = new Feature
                    {
                        // Id = featuredata.Id,
                        Name = featuredata.Name
                    };
                    tenants.Features.Add(newfeature);


                }
            }

            tenants.Scheme = new Scheme();
            tenants.Scheme.PrimaryColor = new Color();
            tenants.Scheme.SecondaryColor = new Color();


            tenants.Scheme.PrimaryColor.HexValue = primaryColorHexValue;
            tenants.Scheme.SecondaryColor.HexValue = secondaryColorHexValue;

            tenants.Databases = new Collection<TenantDatabase>()
        {
            new TenantDatabase(){IsPrimary = true, ConnectionString = primaryDatabase},
            new TenantDatabase(){IsPrimary = false, ConnectionString= secondaryDatabase}

        };



            try
            {
                var requestUri = "api/Tenants/";

                var response = await helper.PostRequest<HospitalTenant>(requestUri, tenants);
                Console.WriteLine("Form data submitted successfully!");
                NavigationManager.NavigateTo("admin/tenants");

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

            }
            

        }
    }
}
