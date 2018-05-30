<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ArticleModels>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Admin.Models" %>
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<script type="text/javascript" src="/Content/js/jquery.form.js"></script>
<script type="text/javascript" src="/Content/CKEditor/ckeditor.js"></script>
<script type="text/javascript" src="/Content/ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="/Content/js/EditorJAdapter.js"></script>
<style type="text/css">
    input[type=text] {
        resize: none;
        padding: 0px 5px;
        border: none;
        border-bottom: 1px dashed #ccc;
        height: 22px;
        background-color: #fee !important;
    }
</style>

<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                Thông tin bài viết
            </div>
            <div class="pull-right">
                <a href="/Article/Index?isEn=true" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">

            <div class="form-horizontal">
                <div class="box-body">
                    <%=Html.Hidden("Id",Model.Article.Id) %>
                    <div class="form-group">
                        <label for="title" class="col-sm-2 control-label">Tiêu đề tiếng việt</label>
                        <div class="col-sm-10">
                            <%=Html.Label("NameVNI", Model.Article.NameVNI)%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="title" class="col-sm-2 control-label">Tiêu đề<span style="color: red">(*)</span></label>
                        <div class="col-sm-10">
                            <%=Html.TextBox("NameENG", Model.Article.NameENG, new {@class="required form-control checklength",title = "Bạn chưa nhập tiêu đề tiếng anh của tin tức", maxlength="200", style=""})%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="title" class="col-sm-2 control-label">Tóm tắt<span style="color: red">(*)</span></label>
                        <div class="col-sm-10">
                            <%=Html.TextArea("SummaryENG",Model.Article.SummaryENG, new {@class = "required checklength form-control" ,title = "Bạn chưa nhập tóm tắt của tin tức ",height = "100", style = ""}) %>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="title" class="col-sm-2 control-label">Nội dung<span style="color: red">(*)</span></label>
                        <div class="col-sm-10">
                            <%=Html.TextArea("DescriptionsENG", Model.Article.DescriptionsENG,  new { style = "width: 100%;", @class="editor" })%>
                        </div>
                    </div>
                        <div class="form-group">
                        <label for="" class="col-sm-2 control-label">Sự kiện?</label>
                        <div class="col-sm-10">
                            <%=Html.CheckBox("IsEvent", Model.Article.IsEvent)%>
                        </div>
                    </div>
                    <div id="eventfields"  <%=Model.Article.IsEvent ? "style='display:block'" : "style='display:none'" %>>
                        <div class="form-group">
                            <label for="" class="col-sm-2 control-label">Ngày bắt đầu sự kiện</label>
                            <div class="col-sm-5" style="position: relative">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <%=Html.TextBox("StartDate", Model.Article.StartDate, new { style = " margin:0px 5px 0px 0px", @placeholder="__/__/____",  @class = "datepicker  form-control"})%>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-2 control-label">Ngày kết thúc sự kiện</label>
                            <div class="col-sm-5">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <%=Html.TextBox("EndDate", Model.Article.EndDate, new { style = " margin:0px 5px 0px 0px", @placeholder="__/__/____",  @class = "datepicker  form-control"})%>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="" class="col-sm-2 control-label">Chuyên mục<span style="color: red">(*)</span></label>
                        <div class="col-sm-10">
                            <%=Html.DropDownList("CategoryID", new SelectList(Model.Categories, "Id", "NameENG",Model.Article.CategoryID), "---Chuyên mục tin---", new { @class = "required form-control  ", title="Bạn chưa chọn loại tin tức", style="width:300px" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="" class="col-sm-2 control-label">Mức độ ưu tiên</label>
                        <div class="col-sm-10">
                            <input name="Priority" id="priority" type="number" min="1" max="1000" value="<%:string.IsNullOrEmpty(Model.Article.Priority.ToString())||Model.Article.Priority==0?1:Model.Article.Priority %>" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="" class="col-sm-2 control-label">Sự kiện?</label>
                        <div class="col-sm-10">
                            <%=Html.CheckBox("IsEvent", Model.Article.IsEvent)%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="" class="col-sm-2 control-label">Ảnh đại diện</label>
                        <div class="col-sm-10">
                            <div class="input-group">
                                <%=Html.Hidden("ImagePath",Model.Article.ImagePath) %>
                                <img src="<%=Model.Article.ImagePath %>" id="Img1" height="40" style="border: 1px solid #CCCCCC; float: left; margin-right: 5px;" />
                                <input type="button" value="Chọn ảnh" onclick="browseImg()" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#IsEvent").change(function () {

            if ($(this).is(":checked")) {
                $("#eventfields").fadeIn();
                $("#eventfields input").addClass('required');
            } else {
                $("#eventfields").fadeOut();
                $("#eventfields input").removeClass('required');
            }
        });
        $(".datepicker").datetimepicker({ format: 'DD/MM/YYYY H:m' });

        LoadEditor(); //load editor       
        //validate
        $('form:first').validate();
    });

    function LoadEditor() {
        $('textarea.editor').each(function () {
            var mheight = $(this).attr('height');
            $(this).ckeditor(function () { CKFinder.setupCKEditor(this, '/Content/ckfinder'); },
            {
                //toolbar: [['Styles', 'Format', 'Font', 'FontSize', 'Bold', 'Italic', 'Underline', 'Paste', 'NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'Table', 'Image', 'Link', 'Source']],
                height: mheight
            });
        });
    }
</script>

<script type="text/javascript">
    /*upload image */
    function browseImg() {
        var finder = new CKFinder();
        finder.basePath = "/Content/ckfinder/";
        finder.selectActionFunction = function (fileUrl) {
            $("#Img1").attr("src", fileUrl);
            $('#ImagePath').val(fileUrl);
        };

        var v = '<%=HttpContext.Current.User.IsInRole("Root") || HttpContext.Current.User.IsInRole("Admin")%>';
        if (v == 'True' || v == true)
            finder.resourceType = 'Files';
        else
            finder.resourceType = '<%=HttpContext.Current.User.Identity.Name%>';
        finder.popup();
    }
</script>
