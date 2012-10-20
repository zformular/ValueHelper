<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="WebHelper.ValueTag" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
    <style type="text/css">
        .Form
        {
            border: 1px solid gray;
        }
        .form
        {
            border: 1px solid red;
        }
        #destination
        {
            border: 1px solid gray;
            width: 260px;
            height: 200px;
            display: block;
            position: fixed;
            top: 20px;
            right: 30px;
            background-color: White;
        }
    </style>
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
</head>
<body>
    <div class="Form">
        <span>ValueHtml.Form("/Tag/Form",ValueHtml.Submit().ToHtmlString()) </span>
        <%=ValueHtml.Form("/Tag/Form",ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.Form("/Tag/Form", new { formName="formname"}, ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.Form("/Tag/Form", new { formName="formname"}, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.Form("/Tag/Form", new { formName = "formname" }, FormMethod.Get,ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.Form("/Tag/Form", new { formName = "formname" }, FormMethod.Get, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.Form("/Tag/Form", new { formName = "formname" }, FormMethod.Post, new
            { id="formID"}, ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.Form("/Tag/Form", new { formName = "formname" }, FormMethod.Post, new { @class="form"}, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <span class="Cut"></span>
    <div class="Form">
        <span>ValueHtml.AjaxForm("/Tag/Form", ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.AjaxForm("/Tag/Form", ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.AjaxForm("/Tag/Form", new { formName="formName" }, ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.AjaxForm("/Tag/Form", new { formName = "formName" }, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.AjaxForm("/Tag/Form", new { formName = "formName" }, FormMethod.Post,
            ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.AjaxForm("/Tag/Form", new { formName = "formName" }, FormMethod.Post, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.AjaxForm("/Tag/Form", new { formName = "formName" }, FormMethod.Post,
            new { @class="form"}, ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.AjaxForm("/Tag/Form", new { formName = "formName" }, FormMethod.Post, new { @class="form"}, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.AjaxForm("/Tag/Form", new AjaxOptions { UpdateTargetId = "destination"
            }, ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.AjaxForm("/Tag/Form", new AjaxOptions { UpdateTargetId = "destination" }, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.AjaxForm("/Tag/Form", new AjaxOptions { UpdateTargetId = "destination"
            }, new { formName="formName"}, ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.AjaxForm("/Tag/Form", new AjaxOptions { UpdateTargetId = "destination" }, new { formName="formName"}, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.AjaxForm("/Tag/Form", new AjaxOptions { UpdateTargetId = "destination"
            }, new { formName="formName"},FormMethod.Post, ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.AjaxForm("/Tag/Form", new AjaxOptions { UpdateTargetId = "destination" }, new { formName="formName"},FormMethod.Get, ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div class="Form">
        <span>ValueHtml.AjaxForm("/Tag/Form", new AjaxOptions { UpdateTargetId = "destination"
            }, new { formName = "formName" }, FormMethod.Post, new { @class = "form" }, ValueHtml.Submit().ToHtmlString())</span>
        <%=ValueHtml.AjaxForm("/Tag/Form", 
            new AjaxOptions { UpdateTargetId = "destination" },
            new { formName = "formName" },
            FormMethod.Post,
            new { @class = "form" }, 
            ValueHtml.Submit().ToHtmlString())%>
    </div>
    <div id="destination">
    </div>
</body>
</html>
