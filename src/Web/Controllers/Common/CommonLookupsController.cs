using Application.Common.Security;
using Application.Features.CommonLookups.Commands;
using Application.Features.CommonLookups.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Common;
[Authorize(Policy = "Permission", Roles = "SuperAdmin")]
public class CommonLookupsController : BaseApiController
{
    [HttpGet]
    public async Task<IResult> GetAll()
    {
        var list = await Mediator.Send(new GetCommonLookupQuery());
        return Results.Ok(list);
    }

    [HttpPost]
    public async Task<IResult> Create(CreateCommonLookupCommand command)
    {
        return Results.Ok(await Mediator.Send(command));
    }
}
