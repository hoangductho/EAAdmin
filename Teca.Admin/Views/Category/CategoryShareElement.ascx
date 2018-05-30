<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CategoryModels>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                Thông tin danh mục tin
            </div>
            <div class="pull-right">
                <a href="/Category/Index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <%=Html.Hidden("Id",Model.ArtCategory.Id)%>
            <fieldset>
                <ol>
                    <li>
                        <label class="control-label mt-10">Thuộc loại tin</label>
                        <%=Html.DropDownList("ParentID", new SelectList(Model.ParentCategory,"Id","NameVNI",Model.ArtCategory.ParentID), "---Chọn loại tin---", new { @class = "form-control", maxlength="100", style="width:300px" })%>
                    </li>
                    <li>
                        <label class="control-label mt-10">Tên tiếng việt<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("NameVNI", Model.ArtCategory.NameVNI, new { @class = "required checklength", maxlength="100", title = " Chưa nhập tên loại tin tức tiếng Việt", style="width:600px" })%>            
                    </li>
                    <li>
                        <label class="control-label mt-10">Tên tiếng anh<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("NameENG", Model.ArtCategory.NameENG, new { @class = "required checklength", maxlength="100", title = " Chưa nhập tên loại tin tức tiếng Anh", style="width:600px" })%>
                    </li>

                    <%--<li>
                        <label class="control-label mt-10">Css</label>
                        <%=Html.TextBox("Css", Model.ArtCategory.Css, new { @class = "checklength", maxlength="50"})%>
                    </li>--%>
                    <li>
                        <label class="control-label mt-10">Mức độ ưu tiên </label>
                        <input name="Priority" id="priority" type="number" min="1" max="1000" value="<%:string.IsNullOrEmpty(Model.ArtCategory.Priority.ToString())||Model.ArtCategory.Priority==0?1:Model.ArtCategory.Priority %>" />
                    </li>
                    <%if (Model.articleNumbers == null || Model.articleNumbers == 0 )
                      {%>
                          <li>
                        <div id="checkbox" class="checkbox">
                            <label class="mt-10"><%=Html.CheckBox("Active", Model.ArtCategory.Active)%>Hiển thị</label>
                        </div>
                    </li>
  
                      <%} %>
                  
                    <li>
                        <label class="control-label mt-10">Mô tả(Tiếng việt)</label>
                        <div class="input-group col-md-12">
                            <%=Html.TextArea("DescriptionsVNI", Model.ArtCategory.DescriptionsVNI, new {cols="50", rows="2", @class = "checklength form-control", maxlength="255" })%>
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">Mô tả(Tiếng anh)</label>
                        <div class="input-group col-md-12">
                            <%=Html.TextArea("DescriptionsENG", Model.ArtCategory.DescriptionsENG, new {cols="50", rows="2", @class = "checklength form-control", maxlength="255" })%>
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">URLName <span style="color: red">(*)</span></label>
                        <%=Html.TextBox("UrlName", Model.ArtCategory.UrlName, new { @class = "required checklength",  title = " Chưa nhập đường dẫn truy cập cho loại tin tin",maxlength="200", style="width:600px"})%>           
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
