﻿
namespace Application.Features.CommonLookups.Commands;
public record CreateCommonLookupCommand : IRequest<int>
{
    public string? Code { get; set; }
    public string Name { get; set; }
    public string? NameBN { get; set; }
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public bool Status { get; set; }
}

public class CreateCommonLookupCommandHandler : IRequestHandler<CreateCommonLookupCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCommonLookupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateCommonLookupCommand request, CancellationToken cancellationToken)
    {
        var entity = new CommonLookup
        {
            Code = request.Code,
            Name = request.Name,
            NameBN = request.NameBN,
            Description = request.Description,
            ParentId = request.ParentId,
            Status = request.Status,
        };

        await _context.CommonLookups.AddAsync(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
