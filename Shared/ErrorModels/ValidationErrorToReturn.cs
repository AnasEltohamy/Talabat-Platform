using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorToReturn
    {
        public int StatusCode { get; set; }
        public string ErrorMassage { get; set; } = default!;
        public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
    }
}
