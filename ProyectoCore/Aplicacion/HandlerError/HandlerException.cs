using System;
using System.Net;

namespace Aplicacion.HandlerError
{
    public class HandlerException : Exception
    {
        public HttpStatusCode _code {get;}

        public object _errors {get;}
        public HandlerException(HttpStatusCode code, object errors = null){
            _code = code;
            _errors = errors;
        }
    }
}