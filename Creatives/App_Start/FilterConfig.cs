﻿using System.Web;
using System.Web.Mvc;
using Creatives.Filters;

namespace Creatives
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new InitializeSimpleMembershipAttribute());
        }
    }
}