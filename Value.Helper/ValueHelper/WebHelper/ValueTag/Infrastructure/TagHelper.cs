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
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ValueHelper.Infrastructure;

namespace WebHelper.ValueTag.Infrastructure
{
    public static class TagHelper
    {
        private static String attrbuteTemplate = "{0}=\"{1}\"";

        public static KeyvalList<String, String> ConvertoKeyvalList(Object datas)
        {
            String data = datas.ToString();
            if (String.IsNullOrEmpty(data))
                return new KeyvalList<String, String>();

            data = data.Substring(data.IndexOf("{") + 1, data.IndexOf("}") - 1);

            KeyvalList<String, String> keyvalList = new KeyvalList<String, String>();
            String[] keyvalPairs = data.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String item in keyvalPairs)
            {
                String[] keyvalPair = item.Split('=');
                keyvalList.Add(new Keyval<String, String>
                {
                    Key = keyvalPair[0].Trim(),
                    Value = keyvalPair[1].Trim()
                });
            }
            return keyvalList;
        }

        public static String PackAction(String url, Object routeValues)
        {
            String action = "action=\"";
            String routeValue = String.Empty;
            if (routeValues != null)
            {
                var routeValueKeyval = TagHelper.ConvertoKeyvalList(routeValues);
                routeValue = String.Concat("?", routeValueKeyval.ToString('=', '&'));
            }

            if (url == null)
                action = String.Concat(action, "/", routeValue, "\"");
            else
                action = String.Concat(action, url, routeValue, "\"");
            return action;
        }

        public static String PackHtmlAttrbute(Object htmlAttrubute)
        {
            String htmlAttr = String.Empty;
            if (htmlAttrubute != null)
            {
                var htmlAttrKeyval = TagHelper.ConvertoKeyvalList(htmlAttrubute);
                foreach (Keyval<String, String> keyval in htmlAttrKeyval)
                    htmlAttr = String.Concat(htmlAttr, String.Format(attrbuteTemplate, keyval.Key, keyval.Value), " ");
            }
            return htmlAttr;
        }

        public static String PackFormMethod(FormMethod formMethod)
        {
            return "method=\"" + formMethod.ToString() + "\"";
        }

        /// <summary>
        ///  MVC2 里面异步AjaxOptions绑定的结果
        ///  MVC3 比较简洁
        /// </summary>
        /// <param name="ajaxOptions"></param>
        /// <returns></returns>
        public static String packMvcTwOptions(AjaxOptions ajaxOptions)
        {
            String package = String.Empty;
            package += "insertionMode:  Sys.Mvc.InsertionMode.replace, ";
            if (ajaxOptions == null)
                return package;
            if (!String.IsNullOrEmpty(ajaxOptions.Confirm))
                package += String.Format("confirm: '{0}', ", ajaxOptions.Confirm);
            if (!String.IsNullOrEmpty(ajaxOptions.HttpMethod))
                package += String.Format("httpMethod: '{0}', ", ajaxOptions.HttpMethod);
            if (!String.IsNullOrEmpty(ajaxOptions.LoadingElementId))
                package += String.Format("loadingElementId: '{0}', ", ajaxOptions.LoadingElementId);
            if (!String.IsNullOrEmpty(ajaxOptions.UpdateTargetId))
                package += String.Format("updateTargetId: '{0}', ", ajaxOptions.UpdateTargetId);
            if (!String.IsNullOrEmpty(ajaxOptions.Url))
                package += String.Format("url: '{0}, '", ajaxOptions.Url);
            if (!String.IsNullOrEmpty(ajaxOptions.OnBegin))
                package += String.Format("onBegin: Function.createDelegate(this, {0}), ", ajaxOptions.OnBegin);
            if (!String.IsNullOrEmpty(ajaxOptions.OnComplete))
                package += String.Format("onComplete: Function.createDelegate(this, {0}), ", ajaxOptions.OnComplete);
            if (!String.IsNullOrEmpty(ajaxOptions.OnFailure))
                package += String.Format("onFailure: Function.createDelegate(this, {0}), ", ajaxOptions.OnFailure);
            if (!String.IsNullOrEmpty(ajaxOptions.OnSuccess))
                package += String.Format("onSuccess: Function.createDelegate(this, {0}), ", ajaxOptions.OnSuccess);

            return package.Substring(0, package.Length - 2);
        }
    }
}
