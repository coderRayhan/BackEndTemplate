using Application.Common.Security;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Features.CommonLookups.Queries;
[Authorize(Roles = "admin")]
public record GetCommonLookupQuery : IRequest<IList<CommonLookupResponse>>;

public class GetCommonLookupQueryHandler : IRequestHandler<GetCommonLookupQuery, IList<CommonLookupResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCommonLookupQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IList<CommonLookupResponse>> Handle(GetCommonLookupQuery request, CancellationToken cancellationToken)
    {
        return await _context.CommonLookups
            .OrderByDescending(x => x.Created)
            .ProjectTo<CommonLookupResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
