using AutoMapper;

namespace Application.Features.CommonLookups.Queries;
public class CommonLookupResponse
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string Name { get; set; }
    public string? NameBN { get; set; }
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public bool Status { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CommonLookup, CommonLookupResponse>().ReverseMap();
        }
    }
}

