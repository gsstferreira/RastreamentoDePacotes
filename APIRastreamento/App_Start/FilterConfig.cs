using Common.DBServices;
using System;
using System.Web;
using System.Web.Mvc;

namespace APIRastreamento
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
