using Bogus;
using Microsoft.EntityFrameworkCore;
using hms.Tenant.API.Data;
using hms.Tenant.API.Model;
using hms.Media.API.Model;

namespace hms.Tenant.API.Services;

using System.Collections.ObjectModel;

public class DatabaseTenantService {
  public IWebHostEnvironment WebHostEnvironment { get; }
  private readonly TenantContext _context;

  public DatabaseTenantService(IWebHostEnvironment webHostEnvironment,
                               TenantContext context) {
    this.WebHostEnvironment = webHostEnvironment;
    this._context = context;
  }

  public async Task<IEnumerable<HospitalTenant>> GetAllTenants() {
    IEnumerable<HospitalTenant> tenants = null!;
    tenants = await _context.Tenants.Include(t => t.Scheme)
                  .Include(t => t.Scheme!.PrimaryColor)
                  .Include(t => t.Scheme!.SecondaryColor)
                  .Include(t => t.Features)
                  .Include(t => t.PrimaryDatabase)
                  .Include(t => t.SecondaryDatabase)
                  .AsNoTracking()
                  .ToListAsync();

    return tenants;
  }

  public async Task<HospitalTenant> GetTenantById(int TenantId) {
    HospitalTenant tenant = null!;
    tenant = await _context.Tenants.Include(t => t.Scheme)
                 .Include(t => t.Scheme!.PrimaryColor)
                 .Include(t => t.Scheme!.SecondaryColor)
                 .Include(t => t.Features)
                 .Include(t => t.PrimaryDatabase)
                 .Include(t => t.SecondaryDatabase)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(t => t.Id == TenantId);
    return tenant;
  }

  public async Task<IEnumerable<Feature>> GetAllFeatures() {
    IEnumerable<Feature> features;
    features = await _context.Features.AsNoTracking().ToListAsync();
    return features;
  }

  public async Task<HospitalTenant> AddTenant(HospitalTenant Tenant) {
    await _context.AddAsync(Tenant);
    await _context.SaveChangesAsync();
    return Tenant;
  }

  public async Task<HospitalTenant> UpdateTenant(HospitalTenant tenant) {
    _context.Update(tenant);
    await _context.SaveChangesAsync();
    return tenant;
  }

  public async Task<HospitalTenant> DeleteTenant(int TenantId) {
    HospitalTenant DbTenant;
    DbTenant = await GetTenantById(TenantId);
    _context.Remove(DbTenant);

    _context.ChangeTracker.DetectChanges();
    Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
    await _context.SaveChangesAsync();
    return DbTenant;
  }

  public void InsertDummyData() {

    Console.WriteLine("Adding DummyData");
    var faker = new Faker();

    HospitalTenant tenant = new HospitalTenant {

      Address = faker.Address.StreetAddress(),
      Name = faker.Name.FullName(),
      Url = faker.Internet.DomainName(),
      PrimaryDatabase =
          new TenantDatabase() { ConnectionString = faker.Internet.Url() },
      SecondaryDatabase =
          new TenantDatabase() { ConnectionString = faker.Internet.Url() },
      MediaRootPath = faker.System.FilePath(),
      Features =
          new Collection<Feature>() {
            new Feature() { Name = faker.Internet.UserName() },
            new Feature() { Name = faker.Internet.UserName() }
          },
      Scheme =
          new Scheme() {
            PrimaryColor =
                new Color { HexValue = faker.Random.Hexadecimal(length: 6) },
            SecondaryColor =
                new Color { HexValue = faker.Random.Hexadecimal(length: 6) }
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
