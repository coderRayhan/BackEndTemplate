namespace Application.Features.Identity.Account.Commands;
public record LoginCommand(string UserName, string Password) : IRequest<(bool isSuccess, string result)>;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, (bool isSuccess, string result)>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<(bool isSuccess, string result)> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authService.Login(request.UserName, request.Password);
            return result;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
