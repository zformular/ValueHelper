/*
  >>>------ Copyright (c) 2012 zformular ----> 
 |                                            |
 |            Author: zformular               |
 |        E-mail: zformular@163.com           |
 |             Date: 10.10.2012               |
 |                                            |
 ╰==========================================╯
*/

using System;
using System.Text;
using System.Web.Mvc;
using WebHelper.ValueTag.TagBase;
using WebHelper.ValueTag.Infrastructure;

namespace WebHelper.ValueTag.TagBase
{
    public class MvcTwTagHtml : HtmlTagBase
    {
        public override MvcHtmlString Form(string url, object routeValues, string innerHtml)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<form ");
            stringBuilder.Append(TagHelper.PackAction(url, routeValues));
            stringBuilder.Append(String.Concat(TagHelper.PackFormMethod(FormMethod.Post), " "));
            stringBuilder.AppendLine(">");
            stringBuilder.AppendLine(innerHtml);
            stringBuilder.Append("</form>");
            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        public override MvcHtmlString Form(String url, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<form ");
            stringBuilder.Append(TagHelper.PackHtmlAttrbute(htmlAttribute));
            stringBuilder.Append(String.Concat(TagHelper.PackFormMethod(formMethod), " "));
            stringBuilder.Append(TagHelper.PackAction(url, routeValues));
            stringBuilder.AppendLine(">");
            stringBuilder.AppendLine(innerHtml);
            stringBuilder.Append("</form>");
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}
