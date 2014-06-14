using System;
using UnityEngine;

namespace PYIV.Persistence.Errors
{
	public class RestException : System.Exception
    {
        public ErrorType ErrorType { get; set; }
       

        public RestException() : base() { }


        public RestException(String message) : base(message)
        {
        }

        public RestException(ErrorType type, String message): base(message)
        {
            this.ErrorType = type;
        }


    }
}

