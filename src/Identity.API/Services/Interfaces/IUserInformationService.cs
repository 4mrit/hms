namespace thms.Identity.API.Services.Interfaces;
public interface IUserInformationService
{
  public Task<IList<System.Security.Claims.Claim>>
  GetClaimsUserUsingUserName(string userName);
}
