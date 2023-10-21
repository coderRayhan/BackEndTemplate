using Application.Common.Exceptions;
using Application.Common.Security;
using System.Reflection;

namespace Application.Common.Behaviours;
public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(
        IUser user,
        IIdentityService identityService)
    {
        _user = user;
        _identityService = identityService;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttribute = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
        if (authorizeAttribute.Any())
        {
            //must be authenticated user
            if (_user.Id == null)
                throw new UnauthorizedAccessException();

            //role based authorization
            var authorizationAttributesWithRoles = authorizeAttribute.Where(a => string.IsNullOrWhiteSpace(a.Roles));
            if (authorizationAttributesWithRoles.Any())
            {
                var authorized = false;
                foreach (var roles in authorizationAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (var role in roles)
                    {
                        var isInRole = await _identityService.IsInRoleAsync(_user.Id, role);
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }

            //permission based authorization
            var authorizeAttributeWithPolicy = authorizeAttribute.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            if (authorizeAttributeWithPolicy.Any())
            {
                foreach (var policy in authorizeAttributeWithPolicy.Select(a => a.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);
                    if (!authorized)
                        throw new ForbiddenAccessException();
                }
            }
        }

        //User is authorized / authorization not required
        return await next();
    }
}
