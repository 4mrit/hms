// TenantsController.cs
using Microsoft.AspNetCore.Mvc;
using hms.Tenant.API.Services;
using hms.Tenant.API.Model;
using hms.Tenant.API.Extensions;
namespace hms.Tenant.API.Controllers;

[Route("api/Tenants/[controller]")]
[ApiController]
public class FeatureController : ControllerBase {
  public DatabaseFeatureService FeatureService { get; }
  public FeatureController(DatabaseFeatureService featureService) {
    this.FeatureService = featureService;
  }

  [HttpGet]
  public async Task<IEnumerable<Feature>> GetAllFeatures() {
    return await FeatureService.GetAllFeatures();
  }

  [HttpGet("{FeatureId}")]
  public async Task<ActionResult<Feature>> GetFeatureById(int FeatureId) {
    return await FeatureService.GetFeatureById(FeatureId);
  }

  [HttpPost]
  public async Task<ActionResult<Feature>> AddTenant(Feature feature) {
    Feature AddedFeature = await FeatureService.AddFeature(feature);
    return CreatedAtAction(
        actionName: nameof(GetAllFeatures),
        controllerName: ControllerContext.GetControllerName(),
        routeValues: new { FeatureId = AddedFeature.Id }, value: AddedFeature);
  }
}
