using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebHelper.ValueTag.Infrastructure
{
    public interface IAjaxTag
    {
        MvcHtmlString Form(String url, AjaxOptions ajaxOptions, String innerHtml);
        MvcHtmlString Form(String url, AjaxOptions ajaxOptions, Object routeValues, String innerHtml);
        MvcHtmlString Form(String url, AjaxOptions ajaxOptions, Object routeValues, FormMethod formMethod, String innerHtml);
        MvcHtmlString Form(String url, AjaxOptions ajaxOptions, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml);
    }
}
