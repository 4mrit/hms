namespace hms.Identity.API.Services.Interfaces;
public interface IRegisterService<TRegister>
{
  public Task Register(TRegister user);
  public Task RegisterAdmin(TRegister user);
}
