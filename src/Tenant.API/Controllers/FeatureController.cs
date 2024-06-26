// TenantsController.cs
using Microsoft.AspNetCore.Mvc;
using thms.Tenant.API.Services;
using thms.Tenant.API.Model;
using thms.Tenant.API.Extensions;
namespace thms.Tenant.API.Controllers;

[Route("api/Tenants/[controller]")]
[ApiController]
public class FeatureController : ControllerBase
{
  public DatabaseFeatureService FeatureService { get; }
  public FeatureController(DatabaseFeatureService featureService)
  {
    this.FeatureService = featureService;
  }

  [HttpGet]
  public async Task<IEnumerable<Feature>> GetAllFeatures()
  {
    return await FeatureService.GetAllFeatures();
  }

  [HttpGet("{FeatureId}")]
  public async Task<ActionResult<Feature>> GetFeatureById(int FeatureId)
  {
    return await FeatureService.GetFeatureById(FeatureId);
  }

  [HttpPut("{FeatureId}")]
  public async Task<ActionResult<Feature>> UpdateTenant(int FeatureId,
                                                        Feature feature)
  {
    if (await FeatureService.UpdateFeatureUsingId(FeatureId, feature) == null)
    {
      return NotFound();
    }
    return NoContent();
  }

  [HttpPost]
  public async Task<ActionResult<Feature>> AddTenant(Feature feature)
  {
    Feature AddedFeature = await FeatureService.AddFeature(feature);
    return CreatedAtAction(
        actionName: nameof(GetAllFeatures),
        controllerName: ControllerContext.GetControllerName(),
        routeValues: new { FeatureId = AddedFeature.Id }, value: AddedFeature);
  }

  [HttpDelete("{FeatureId}")]
  public async Task<ActionResult> DeleteFeature(int FeatureId)
  {
    await FeatureService.DeleteFeatureUsingId(FeatureId);
    return NoContent();
  }
}
