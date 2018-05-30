<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Banners>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Admin" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<script type="text/javascript" src="/Content/CKEditor/ckeditor.js"></script>
<script type="text/javascript" src="/Content/ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="/Content/js/EditorJAdapter.js"></script>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                Thông tin banner
            </div>
            <div class="pull-right">
                <a href="/Banners/Index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <fieldset>
                <%=Html.Hidden("id", Model.Id) %>
                <ol>
                    <li>
                        <label class="control-label mt-10">Tên tiếng việt<span style="color: red">(*)</span> </label>
                        <%=Html.TextBox("NameVNI",Model.NameVNI, new {@class="checklength required", title="Bạn chưa nhập tiêu đề tiếng Việt", maxlength="512", style="width:600px"})%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Tên tiếng anh<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("NameENG",Model.NameENG, new {@class="checklength required", title="Bạn chưa nhập tiêu đề tiếng Anh", maxlength="512", style="width:600px"})%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Nội dung tiếng việt  </label>
                        <div class="input-group col-md-12">
                            <%=Html.TextArea("DescriptionsVNI",Model.DescriptionsVNI, new {@class="checklength form-control  ",    maxlength="500", cols="20", rows=2})%>
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">Nội dung tiếng anh</label>
                        <div class="input-group col-md-12">
                            <%=Html.TextArea("DescriptionsENG",Model.DescriptionsENG, new {@class="checklength form-control  ",   maxlength="500", cols="20", rows=2})%>
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">Vị trí xuất hiện</label>
                        <%= Html.EnumDropDownListFor(m => m.TypeID, Model.TypeID)%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Đường dẫn <span style="color: red">(*)</span></label>                        
                        <%=Html.TextBox("NavigateUrl",Model.NavigateUrl, new {@class="checklength required", title="Chưa nhập đường dẫn cho banner", maxlength="128", style="width:600px"})%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Mức độ ưu tiên</label>
                        <input name="Priority" id="priority" type="number" min="1" max="1000" value="<%:string.IsNullOrEmpty(Model.Priority.ToString())||Model.Priority==0?1:Model.Priority %>" />                        
                    </li>
                    <li>
                        <div id="checkbox" class="checkbox">
                            <label class="mt-10"><%=Html.CheckBox("Active", Model.Active)%>Hiển thị</label>
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">Chọn ảnh</label>
                        <%=Html.Hidden("ImagePath", Model.ImagePath) %>
                        <div class="input-group">                                                        
                            <img src="<%=Model.ImagePath %>" id="Img1" height="102" width="150" style="border: 1px solid #CCCCCC; float: left; margin-right: 5px;" />
                            <input type="button" value="Chọn ảnh" onclick="browseImg()" />
                        </div>
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
        $("#NavigateUrl").autocomplete({
            focus: function (event, ui) { },
            minLength: 1,
            select: function (event, ui) {
                $("#NavigateUrl").val(ui.item.Url);
            },
            source: function (request, response) {
                $.ajax({
                    data: {
                        searchText: request.term
                    },
                    dataType: "JSON",
                    success: function (data) {
                        response($.map(data, function (item) {

                            return {
                                id: item.NameVNI,
                                value: item.Url,
                                Name: item.NameVNI
                            }
                        }))
                    },
                    type: "POST",
                    url: "/AjaxData/ArticlesSearch"
                })
            }
        }).change(function () {
            if (!$(this).val()) {
                $("#NavigateUrl").val("");
            }
        });
    });
    /*upload image */
    function browseImg() {
        var finder = new CKFinder();
        finder.basePath = "/Content/ckfinder/";
        finder.selectActionFunction = function (fileUrl) {
            $("#Img1").attr("src", fileUrl);
            $('#ImagePath').val(fileUrl);
        };

        finder.resourceType = 'Banners';
        finder.popup();
    }

    
</script>
