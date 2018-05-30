<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GalleryType>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<script type="text/javascript" src="/Content/CKEditor/ckeditor.js"></script>
<script type="text/javascript" src="/Content/ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="/Content/js/EditorJAdapter.js"></script>
<style type="text/css">
    .skin-item {
        box-shadow: 1px 2px 6px #F2F2F2;        
        overflow: hidden;
    }

    .skin-item {
        border: solid 1px #f2f2f2;
        height: 105px;        
        position: relative;
        width: 150px;
    }

    .fl {
        float: left;
    }        

    .skin-item img {
        height: 100px;
        width: 120px;
        border: 2px solid #cc0000;
        border-radius: 10px;
    }
</style>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                Chuyên mục ảnh
            </div>
            <div class="pull-right">
                <a href="/GalleryType/Index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <fieldset>
                <%=Html.Hidden("id", Model.Id) %>
                <ol>
                    <li>
                        <label class="control-label mt-10">Tiêu đề tiếng việt<span style="color: red">(*)</span> </label>
                        <%=Html.TextBox("TitleVNI",Model.TitleVNI, new {@class="checklength required",title="Chưa nhập tiêu đề tiếng Việt", maxlength="100", style="width:600px"})%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Tiêu đề tiếng anh<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("TitleENG",Model.TitleENG, new {@class="checklength required",title="Chưa nhập tiêu đề tiếng Anh", maxlength="100", style="width:600px"})%>                        
                    </li>
                    <li>
                        <label class="control-label mt-10">Ảnh đại diện</label>
                        <%=Html.Hidden("ImgPath", Model.ImgPath) %>
                        <div class="input-group">
                            <img src="<%=Model.ImgPath %>" id="Img1" height="102" width="150" style="border: 1px solid #CCCCCC; float: left; margin-right: 5px;" />
                            <input type="button" value="Chọn ảnh" onclick="browseImg()" />
                        </div>
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
                        <label for="ImageLib">Thư viện ảnh:</label><input type="button" value="Chọn ảnh" onclick="BrowseImageLib()" /></li>
                </ol>

                <%=Html.Hidden("ImageLib")%>
                <div id="imagelibdiv" style="clear: both">
                    <% foreach (var it in Model.Galleries)
                       {%>
                    <div class="skin-item fl">
                        <img src="<%=it.ImgPath %>" class="imagelib"/>
                        <a class="deleteImage" title="delete this image"><i class="fa fa-trash"></i></a>
                    </div>

                    <%}%>
                </div>
            </fieldset>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        //validate
        $('form:first').validate();
        $('a.deleteImage').click(function () {
            $(this).parent().remove();
        });
    });
    /*upload image */
    function browseImg() {
        var finder = new CKFinder();
        finder.basePath = "/Content/ckfinder/";
        finder.selectActionFunction = function (fileUrl) {
            $("#Img1").attr("src", fileUrl);
            $('#ImgPath').val(fileUrl);
        };
        var v = '<%=HttpContext.Current.User.IsInRole("Root") || HttpContext.Current.User.IsInRole("Admin")%>';
        if (v == 'True' || v == true)
            finder.resourceType = 'Files';
        else
            finder.resourceType = '<%=HttpContext.Current.User.Identity.Name%>';
        finder.popup();
    }

    function BrowseImageLib() {
        var finder = new CKFinder();
        finder.basePath = "/Content/ckfinder/";
        finder.selectActionFunction = function (fileUrl) {
            var divhtml = "<div class='skin-item fl'><image class='imagelib' src = '" + fileUrl + "'/><a class ='deleteImage' title ='delete this image'><i class='fa fa-trash'></i></a></div>";
            $("#imagelibdiv").append(divhtml);
            $('a.deleteImage').click(function () {
                $(this).parent().remove();
            });
        };

        finder.resourceType = '<%=HttpContext.Current.User.Identity.Name%>';
        finder.popup();
    }
    function PrepareSubmit() {

        var mImageLib = "";
        $('img.imagelib').each(function () {
            mImageLib += $(this).attr('src') + ';'
        });
        if (mImageLib.length > 0) {
            $(".error-no-image").css('display', 'none');
            $('#ImageLib').val(mImageLib);
            $("form").submit();
        } else {
            $(".error-no-image").css('display', 'block');
            return false;
        }
    }
</script>
