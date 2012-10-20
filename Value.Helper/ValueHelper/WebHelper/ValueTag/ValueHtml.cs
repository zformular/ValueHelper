using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using WebHelper.ValueTag.TagBase;
using WebHelper.ValueTag.Infrastructure;

namespace WebHelper.ValueTag
{
    public class ValueHtml
    {
        static HtmlTagBase tagHtml = new MvcTwTagHtml();
        static AjaxTagBase tagAjax = new MvcTwTagAjax();

        public static MvcHtmlString Form(String url, String innerHtml)
        {
            return tagHtml.Form(url, innerHtml);
        }

        public static MvcHtmlString Form(String url, Object routeValues, String innerHtml)
        {
            return tagHtml.Form(url, routeValues, innerHtml);
        }

        public static MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, String innerHtml)
        {
            return tagHtml.Form(url, routeValues, formMethod, innerHtml);
        }

        public static MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml)
        {
            return tagHtml.Form(url, routeValues, formMethod, htmlAttribute, innerHtml);
        }

        public static MvcHtmlString AjaxForm(String url, String innerHtml)
        {
            return tagAjax.Form(url, innerHtml);
        }

        public static MvcHtmlString AjaxForm(String url, Object routeValues, String innerHtml)
        {
            return tagAjax.Form(url, routeValues, innerHtml);
        }

        public static MvcHtmlString AjaxForm(String url, Object routeValues, FormMethod formMethod, String innerHtml)
        {
            return tagAjax.Form(url, routeValues, formMethod, innerHtml);
        }

        public static MvcHtmlString AjaxForm(String url, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml)
        {
            return tagAjax.Form(url, routeValues, formMethod, htmlAttribute, innerHtml);
        }

        public static MvcHtmlString AjaxForm(String url, AjaxOptions ajaxOptions, String innerHtml)
        {
            return tagAjax.Form(url, ajaxOptions, innerHtml);
        }

        public static MvcHtmlString AjaxForm(String url, AjaxOptions ajaxOptions, Object routeValues, String innerHtml)
        {
            return tagAjax.Form(url, ajaxOptions, routeValues, innerHtml);
        }

        public static MvcHtmlString AjaxForm(String url, AjaxOptions ajaxOptions, Object routeValues, FormMethod formMethod, String innerHtml)
        {
            return tagAjax.Form(url, ajaxOptions, routeValues, formMethod, innerHtml);
        }

        public static MvcHtmlString AjaxForm(String url, AjaxOptions ajaxOptions, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml)
        {
            return tagAjax.Form(url, ajaxOptions, routeValues, formMethod, htmlAttribute, innerHtml);
        }

        public static MvcHtmlString Submit()
        {
            return tagHtml.Submit();
        }

        public static MvcHtmlString Submit(Object htmlAttrubute)
        {
            return tagHtml.Submit(htmlAttrubute);
        }
    }
}
