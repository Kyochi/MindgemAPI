using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.exception
{
    public class UrlBuilderException : Exception
    {
        public UrlBuilderException (String message) : base(message)
        {        }
    }
}