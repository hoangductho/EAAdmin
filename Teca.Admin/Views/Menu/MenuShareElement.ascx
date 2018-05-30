<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Teca.Core.Domain.Menu>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Admin" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>

<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                Thông tin menu
            </div>
            <div class="pull-right">
                <a href="/Menu/Index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <fieldset>
                <%=Html.Hidden("id", Model.Id) %>
                <ol>
                    <li>
                        <label class="control-label mt-10">Menu cha</label>
                        <%=Html.DropDownList("ParentID",new SelectList((IList<Teca.Core.Domain.Menu>)ViewData["MenuParents"],"Id","NameVNI", Model.ParentID), "---Menu cha---", new {@class="checklength", maxlength="100", style="width:600px"})%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Tên tiếng việt<span style="color: red">(*)</span> </label>
                        <%=Html.TextBox("NameVNI",Model.NameVNI, new {@class="checklength required", title="Chưa nhập tên tiếng việt cho menu", maxlength="100", style="width:600px"})%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Tên tiếng anh<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("NameENG",Model.NameENG, new {@class="checklength required", title="Chưa nhập tên tiếng anh cho menu",   maxlength="100", style="width:600px"})%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Vị trí xuất hiện</label>
                        <%= Html.EnumDropDownListFor(m => m.Position, Model.Position)%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Đường dẫn <span style="color: red">(*)</span></label>
                        <%=Html.TextBox("NavigateUrl",Model.NavigateUrl, new {@class="checklength required", title="Chưa nhập  đường dẫn cho menu", maxlength="128", style="width:600px"})%>                        
                    </li>
                    
                    <li>
                        <label class="control-label mt-10">Mức độ ưu tiên</label>
                        <input name="Priority" id="priority" type="number" min="1" max="1000" value="<%:string.IsNullOrEmpty(Model.Priority.ToString())||Model.Priority==0?1:Model.Priority %>" />
                    </li>
                    <li>
                        <div id="checkbox" class="checkbox">
                            <label class="mt-10"><%=Html.CheckBox("IsPub", Model.IsPub)%>Hiển thị</label>
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">Css</label>
                        <%=Html.TextBox("Css",Model.Css, new {@class="checklength", maxlength="50"})%>
                    </li>
                </ol>
            </fieldset>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        //validate
        $('form:first').validate();
    });
</script>
