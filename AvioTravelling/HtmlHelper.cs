using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvioTravelling
{
    public static class HtmlHelper
    {
        public static string GeoApiUrl(this WebViewPage page, string action, string Id = null)
        {
            if (String.IsNullOrWhiteSpace(Id))
                return page.Url.HttpRouteUrl("DefaultApi", new { controller = "Geo", action = action });
            else
                return page.Url.HttpRouteUrl("DefaultApi", new { controller = "Geo", action = action, id = Id });
        }

        public static string GeoHtmlUrl(this WebViewPage page, string action)
        {
            return page.Url.HttpRouteUrl("Default", new { controller = "Geo", action = action });
        }
    }
}