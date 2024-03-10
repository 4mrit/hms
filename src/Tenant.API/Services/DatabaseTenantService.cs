using hms.Tenant.API.Data;
using hms.Tenant.API.Model;
namespace hms.Tenant.API.Services;
using hms.Media.API.Model;

using System.Collections.ObjectModel;

public class DatabaseTenantService{
    public IWebHostEnvironment WebHostEnvironment { get;}
    private readonly TenantContext _context;

    public DatabaseTenantService(IWebHostEnvironment webHostEnvironment, TenantContext context ){
        this.WebHostEnvironment = webHostEnvironment;
        this._context = context;
    }

    public IEnumerable<HospitalTenant> GetAllTenants(){
        IEnumerable<HospitalTenant> tenants = null;
        tenants = _context.Tenants ;
        return tenants;
    }
    public void insertDummyData(){
        Console.WriteLine("Adding DummyData");
        // HospitalTenant tenant = new ();
        HospitalTenant tenant = new HospitalTenant{
            Id = 1
            , Address = "sainamaina" 
            , Name = "hamrohospial"
            , Url = "hamrohospital.com"
            , PrimaryDatabase = new TenantDatabase(){
                Id = 1
                ,ConnectionString = "primarydb"
            }
            , SecondaryDatabase =new TenantDatabase(){
                Id = 2
                ,ConnectionString = "secondarydb"
            }
            , MediaRootPath = "/HOME/HAMROHOSPITAL"
            , Features = new Collection<Feature>(){
                new Feature(){
                    Id = 1
                    , Name = "admin"
                },
                new Feature(){
                    Id = 2
                    , Name = "member"
                }
            }
            , Scheme = new Scheme(){
                Id = 1,
                PrimaryColor = new Color{
                    Id = 1,
                    HexValue = "#000000"
                },
                SecondaryColor = new Color{
                    Id = 2,
                    HexValue = "#FFFFFF"
                }
            }
        };
        _context.Add(tenant);
        _context.SaveChanges();
        Console.WriteLine("Added DummyData");
    }
}

// ----- fluent API ------------------//
// var devices = _context.Devices
//                     .Where(d=>d.DeviceName == "node")
//                     .OrderBy(d=> d.Id)
//                     .GroupBy(d=> d.DeviceName);
//--------------------------------------//

//------------ LINQ ---------------------//
// var devices = from device in _context.Devices
//                 where device.DeviceName == "node"
//                 orderby device.Id
//                 group device by device.DeviceName into newGroup
//                 select newGroup;
//----------------------------------------//
