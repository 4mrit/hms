// public class UsersSeed(ILogger<UsersSeed> logger, UserManager<TenantContext> userManager) : IDbSeeder<ApplicationDbContext>
// {
//     public async Task SeedAsync(TenantContext context)
//     {
//         var hamrohospial = await userManager.FindByNameAsync("hamrohospital");

//         if (hamrohospial == null)
//         {
//             hamrohospial =  new HospitalTenant{
//                 Id = 1
//                 , Address = "sainamaina" 
//                 , Name = "hamrohospial"
//                 , Url = "hamrohospital.com"
//                 , PrimaryDatabase = new TenantDatabase(){
//                     Id = 1
//                     ,ConnectionString = "primarydb"
//                 }
//                 , SecondaryDatabase =new TenantDatabase(){
//                     Id = 2
//                     ,ConnectionString = "secondarydb"
//                 }
//                 , MediaRootPath = "/HOME/HAMROHOSPITAL"
//                 , Features = new Collection<Feature>(){
//                     new Feature(){
//                         Id = 1
//                         , Name = "admin"
//                     },
//                     new Feature(){
//                         Id = 2
//                         , Name = "member"
//                     }
//                 }
//                 , Scheme = new Scheme(){
//                     Id = 1,
//                     PrimaryColor = new Color{
//                         Id = 1,
//                         HexValue = "#000000"
//                     },
//                     SecondaryColor = new Color{
//                         Id = 2,
//                         HexValue = "#FFFFFF"
//                     }
//                 }
//             };
//             var result = userManager.CreateAsync(alice, "Pass123$").Result;

//             if (!result.Succeeded)
//             {
//                 throw new Exception(result.Errors.First().Description);
//             }

//             if (logger.IsEnabled(LogLevel.Debug))
//             {
//                 logger.LogDebug("alice created");
//             }
//         }
//         else
//         {
//             if (logger.IsEnabled(LogLevel.Debug))
//             {
//                 logger.LogDebug("alice already exists");
//             }
//         }
//     }
// }