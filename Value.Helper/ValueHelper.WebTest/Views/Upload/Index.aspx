<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
    <style type="text/css">
        .asyncForm
        {
            border: 1px solid gray;
        }
    </style>
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var options = {
                url: "/Upload/UploadFile",
                type: "POST",
                success: function (data) {
                    $("#dest").html(data)
                }
            };

            $("#frmupload").ajaxForm(options);
        })

    </script>
</head>
<body>
    <%using (Html.BeginForm("UploadFile", "Upload", FormMethod.Post, new { enctype = "multipart/form-data" }))
      { %>
    <input type="file" name="uploadfile" />
    <input type="submit" value="提交" name="btnsubmit" />
    <%} %>
    <div class="asyncForm">
        异步上传文件
        <%using (Html.BeginForm("UploadFile", "Upload", FormMethod.Post, new { id = "frmupload" }))
          { %>
        <input type="file" name="uploadfile" />
        <input type="submit" value="提交" name="btnsubmit" />
        <%} %>
        <div id="dest">
        </div>
    </div>
</body>
</html>
