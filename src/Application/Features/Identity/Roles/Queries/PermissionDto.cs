using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Roles.Queries;
public class PermissionDto
{
    public string Type { get; set; }
    public string Value { get; set; }
    public bool IsSelected { get; set; }
}
