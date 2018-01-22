using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class ValidationModel
    {
        private readonly string _message;
        private readonly string _class;
        private readonly string _namespace;
        private readonly string _identifier;

        public ValidationModel(
            string message,
            string className,
            string namespaceName,
            string identifier)
        {
            _message = message;
            _class = className;
            _namespace = namespaceName;
            _identifier = identifier;
        }

        public string Message { get { return _message; } }

        public string Class { get { return _class; } }

        public string Namespace { get { return _namespace; } }

        public string Identifier { get { return _identifier; } }
    }
}
