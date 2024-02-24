using hms.Tenant.API.Data;
using hms.Tenant.API.Model;
namespace hms.Tenant.API.Services;

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