using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization;
public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public DefaultAuthorizationPolicyProvider FallBackPolicyProvider { get; }
    public async Task<AuthorizationPolicy> GetDefaultPolicyAsync() => await FallBackPolicyProvider.GetDefaultPolicyAsync();

    public async Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => await FallBackPolicyProvider.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if(policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(policyName));
            return Task.FromResult(policy.Build());
        }
        return FallBackPolicyProvider.GetPolicyAsync(policyName);
    }
}
