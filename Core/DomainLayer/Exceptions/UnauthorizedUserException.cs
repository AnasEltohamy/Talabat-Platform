using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Identity
{
    public sealed class UnauthorizedUserException(string massage) : Exception(massage)
    {
    }
}
