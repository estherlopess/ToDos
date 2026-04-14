 // Services/IUserService.cs
public interface IUserService
{
Task<UserVM> GetLoggedUser();
Task<SignInResult> Login(LoginVM login);
Task Logout();
Task<List<string>> Register(RegisterVM register);
}
// Services/IUserService.cs
public interface IUserService
{
Task<UserVM> GetLoggedUser();
Task<SignInResult> Login(LoginVM login);
Task Logout();
Task<List<string>> Register(RegisterVM register);
}
