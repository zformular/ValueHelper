using System;
using System.Web.Mvc;

namespace WebHelper.ValueTag.Infrastructure
{
    public interface IHtmlTag
    {
        MvcHtmlString Form(String url, String innerHtml);
        MvcHtmlString Form(String url, Object routeValues, String innerHtml);
        MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, String innerHtml);
        MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml);

        MvcHtmlString Submit();
        MvcHtmlString Submit(Object htmlAttrubute);
    }
}
