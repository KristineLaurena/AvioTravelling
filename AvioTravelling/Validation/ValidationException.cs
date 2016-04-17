using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvioTravelling.Validation
{
    public class ValidationException : Exception
    {
        public IList<Error> Errors { get; set; }

        public ValidationException(IEnumerable<Error> errors)
        {
            this.Errors = new List<Error>();
            (this.Errors as List<Error>).AddRange(errors);
        }
    }

    public class Error
    {
        public string Text { get; set; }
        public Guid Id { get; set; }
    }
}