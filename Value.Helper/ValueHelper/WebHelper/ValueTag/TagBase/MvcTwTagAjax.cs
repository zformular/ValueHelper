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
using System.Web.Mvc.Ajax;
using WebHelper.ValueTag.TagBase;
using WebHelper.ValueTag.Infrastructure;

namespace WebHelper.ValueTag.TagBase
{
    public class MvcTwTagAjax : AjaxTagBase
    {
        private String onsubmitTemplate = @"onsubmit=""Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), {{ {0} }});"" ";
        private String onclickTemplate = @"onclick=""Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));""";

        public override MvcHtmlString Form(String url, AjaxOptions ajaxOptions, Object routeValues, String innerHtml)
        {
            return Form(url, ajaxOptions, routeValues, FormMethod.Post, innerHtml);
        }

        public override MvcHtmlString Form(String url, AjaxOptions ajaxOptions, Object routeValues, FormMethod formMethod, Object htmlAttribute, String innerHtml)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<form ");
            stringBuilder.Append(TagHelper.PackHtmlAttrbute(htmlAttribute));
            stringBuilder.Append(String.Concat(TagHelper.PackFormMethod(formMethod), " "));
            stringBuilder.Append(String.Concat(onclickTemplate, " "));
            stringBuilder.Append(String.Format(onsubmitTemplate, TagHelper.packMvcTwOptions(ajaxOptions)));
            stringBuilder.Append(TagHelper.PackAction(url, routeValues));
            stringBuilder.AppendLine(">");
            stringBuilder.AppendLine(innerHtml);
            stringBuilder.Append("</form>");
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}
