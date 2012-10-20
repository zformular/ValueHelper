using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using WebHelper.ValueTag.Infrastructure;

namespace WebHelper.ValueTag.TagBase
{
    public class AjaxTagBase : HtmlTagBase
    {
        public override MvcHtmlString Form(String url, String innerHtml)
        {
            return Form(url, null, innerHtml);
        }

        public override MvcHtmlString Form(String url, Object routeValues, String innerHtml)
        {
            return Form(url, null, routeValues, innerHtml);
        }

        public override MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, String innerHtml)
        {
            return Form(url, null, routeValues, formMethod, innerHtml);
        }

        public override MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml)
        {
            return Form(url, null, routeValues, formMethod, htmlAttribute, innerHtml);
        }

        public MvcHtmlString Form(String url, AjaxOptions ajaxOptions, String innerHtml)
        {
            return Form(url, ajaxOptions, null, innerHtml);
        }

        public virtual MvcHtmlString Form(String url, AjaxOptions ajaxOptions, Object routeValues, String innerHtml)
        {
            throw new NotImplementedException();
        }

        public MvcHtmlString Form(String url, AjaxOptions ajaxOptions, Object routeValues, FormMethod formMethod, String innerHtml)
        {
            return Form(url, ajaxOptions, routeValues, formMethod, null, innerHtml);
        }

        public virtual MvcHtmlString Form(String url, AjaxOptions ajaxOptions, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml)
        {
            throw new NotImplementedException();
        }
    }
}
