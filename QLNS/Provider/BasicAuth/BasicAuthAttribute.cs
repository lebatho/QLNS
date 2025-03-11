﻿using Microsoft.AspNetCore.Mvc;

namespace QLNS.Provider.BasicAuth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        public BasicAuthAttribute(string realm = @"JWT Authen") : base(typeof(BasicAuthFilter))
        {
            Arguments = new object[] { realm };
        }
    }
}
