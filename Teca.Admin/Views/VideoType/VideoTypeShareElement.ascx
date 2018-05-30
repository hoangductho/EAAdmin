<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VideoTypeModels>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                Thông tin danh mục video
            </div>
            <div class="pull-right">
                <a href="/VideoType/Index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <%=Html.Hidden("Id",Model.ArtVideoType.Id)%>
            <fieldset>
                <ol> 
                    <li>
                        <label class="control-label mt-10">Tên tiếng việt<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("NameVNI", Model.ArtVideoType.NameVNI, new { @class = "required checklength", maxlength="100", title = " Chưa nhập tên loại video", style="width:600px" })%>            
                    </li>
                    <li>
                        <label class="control-label mt-10">Tên tiếng anh<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("NameENG", Model.ArtVideoType.NameENG, new { @class = "required checklength", maxlength="100", title = " Chưa nhập tên loại video", style="width:600px" })%>
                    </li>

                    <li>
                        <label class="control-label mt-10">Css</label>
                        <%=Html.TextBox("Css", Model.ArtVideoType.Css, new { @class = "checklength", maxlength="50"})%>
                    </li>
                    <li>
                        <label class="control-label mt-10">Mức độ ưu tiên </label>
                        <input name="Priority" id="priority" type="number" min="1" max="1000" value="<%:string.IsNullOrEmpty(Model.ArtVideoType.Priority.ToString())||Model.ArtVideoType.Priority==0?1:Model.ArtVideoType.Priority %>" />
                    </li>
                    <li>
                        <div id="checkbox" class="checkbox">
                            <label class="mt-10"><%=Html.CheckBox("Active", Model.ArtVideoType.Active)%>Hiển thị</label>
                        </div>
                    </li>
                    
                </ol>
            </fieldset>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('form:first').validate();
    });
</script>
