using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class ErrorBuilder
    {
        private List<Error> _errors = new List<Error>();

        public List<Error> Errors => _errors;

        public bool HasErrors => _errors.Any();

        public void AddError(object source, string error)
        {
            _errors.Add(new Error()
            {
                Element = source,
                Message = error

            });
        }

    }

    public class Error
    { 
        public object Element { get; set; }
        public string Message { get; set; }
    }
}
