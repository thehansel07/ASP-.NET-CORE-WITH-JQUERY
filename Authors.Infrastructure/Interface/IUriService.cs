using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authors.Infrastructure.Interface
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(string actionUrl);
    }
}
