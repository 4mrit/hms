using Microsoft.AspNetCore.Identity;

namespace thms.Identity.API.Services.Interfaces;
public interface IRegisterService<TRegister> {
  public Task<IdentityResult> Register(TRegister user);
  public Task<IdentityResult> RegisterAdmin(TRegister user);
}
