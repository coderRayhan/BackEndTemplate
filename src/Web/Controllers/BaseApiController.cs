using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure;

namespace Web.Controllers;
[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private ISender? _mediator;
    private IMapper? _mapper;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
}
