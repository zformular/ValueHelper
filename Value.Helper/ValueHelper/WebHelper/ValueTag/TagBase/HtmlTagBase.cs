using System;
using System.Web.Mvc;
using WebHelper.ValueTag.Infrastructure;

namespace WebHelper.ValueTag.TagBase
{
    public class HtmlTagBase : IHtmlTag
    {
        public virtual MvcHtmlString Form(String url, String innerHtml)
        {
            return Form(url, null, innerHtml);
        }

        public virtual MvcHtmlString Form(String url, Object routeValues, String innerHtml)
        {
            throw new NotImplementedException();
        }

        public virtual MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, String innerHtml)
        {
            return Form(url, null, formMethod, null, innerHtml);
        }

        public virtual MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml)
        {
            throw new NotImplementedException();
        }

        public MvcHtmlString Submit()
        {
            return MvcHtmlString.Create("<input type='submit'/>");
        }

        public MvcHtmlString Submit(Object htmlAttrubute)
        {
            String submit = String.Concat("<input type=\"submit\" ", TagHelper.ConvertoKeyvalList(htmlAttrubute), "/>");
            return MvcHtmlString.Create(submit);
        }
    }
}
