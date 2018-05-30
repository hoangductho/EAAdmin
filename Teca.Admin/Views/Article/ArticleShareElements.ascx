<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ArticleModels>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Admin.Models" %>
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<script type="text/javascript" src="/Content/js/jquery.form.js"></script>
<script type="text/javascript" src="/Content/CKEditor/ckeditor.js"></script>
<script type="text/javascript" src="/Content/ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="/Content/js/EditorJAdapter.js"></script>
<style type="text/css">
    input[type=file] {
        display: none;
        visibility: hidden;
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
                <a href="/Article/Index" class="btn btn-default btn-sm mr-15"><i class="fa fa-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">

            <div class="form-horizontal">
                <div class="box-body">
                    <%=Html.Hidden("Id",Model.Article.Id) %>
                    <div class="form-group">
                        <label for="title" class="col-sm-2 control-label">Tiêu đề<span style="color: red">(*)</span></label>
                        <div class="col-sm-10">
                            <%=Html.TextBox("NameVNI", Model.Article.NameVNI, new {@class="required checklength form-control",title = "Bạn chưa nhập tiêu đề của tin tức", maxlength="200", style=""})%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="title" class="col-sm-2 control-label">Tiêu đề(Tiếng anh)</label>
                        <div class="col-sm-10">
                            <%=Html.TextBox("NameENG", Model.Article.NameENG, new {@class="form-control checklength",title = "Bạn chưa nhập tiêu đề tiếng anh của tin tức", maxlength="200", style=""})%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="title" class="col-sm-2 control-label">Tóm tắt<span style="color: red">(*)</span></label>
                        <div class="col-sm-10">
                            <%=Html.TextArea("Summary",Model.Article.Summary, new {@class = "required checklength form-control" ,title = "Bạn chưa nhập tóm tắt của tin tức ",height = "100", maxlength="1024", style = ""}) %>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="title" class="col-sm-2 control-label">Nội dung<span style="color: red">(*)</span></label>
                        <div class="col-sm-10">
                            <%=Html.TextArea("Descriptions", Model.Article.Descriptions,  new { style = "width: 100%;", @class="editor" })%>
                        </div>
                    </div>
                      <div class="form-group">
                        <label for="" class="col-sm-2 control-label">Tin nổi bật?</label>
                        <div class="col-sm-10">
                            <%=Html.CheckBox("IsHot", Model.Article.IsHot)%>
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
                            <%=Html.DropDownList("CategoryID", new SelectList(Model.Categories, "Id", "NameVNI",Model.Article.CategoryID), "---Chuyên mục tin---", new { @class = "required form-control  ", title="Bạn chưa chọn loại tin tức", style="width:300px" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="" class="col-sm-2 control-label">Mức độ ưu tiên</label>
                        <div class="col-sm-10">
                            <input name="Priority" id="priority" type="number" min="1" max="1000" value="<%:string.IsNullOrEmpty(Model.Article.Priority.ToString())||Model.Article.Priority==0?1:Model.Article.Priority %>" />
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
                    <div>
                        <label class="col-sm-2 control-label">Văn bản đính kèm</label>
                        <div class="col-sm-10">
                            <div class="input-group">
                                <input type="file" name="dataFile" id="dataFile" accept=".pdf,.PDF,.doc,.docx">
                                <input type="text" id="txtFilePath" readonly="readonly" placeholder="Nhấn chuột vào đây để đính kèm file .pdf,.doc,.docx,.xls,.xlsx" value="<%=Model.Article.Id > 0 ? string.Format("{0}.{1}",FX.Utils.Common.TextHelper.ToUrlFriendly(Model.Article.NameVNI),Model.Article.TypeData) : ""%>" style="width: 600px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<style>
    .videodetector {
	position      : relative;
	width         : 100%;
	height        : 0;
	padding-bottom: 60%;
}

.videodetector iframe {
	position: absolute;
	top     : 0;
	left    : 0;
	width   : 100%;
	height  : 100%;
}
</style>
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

        $('#txtFilePath').click(function () {
            $('#dataFile').trigger('click');
        }).keypress(function (e) {
            if (e.keyCode == 13 || e.which == 13) {
                $(this).trigger('click');
                return false;
            }
        });

        $('#dataFile').change(function () {
            var _file = $(this)[0].files[0];
            if (_file) {
                var _extl = ['pdf', 'doc', 'docx'];
                var _ext = _file.name.substring(_file.name.lastIndexOf('.') + 1);
                if (this.files[0].size > 3145728) {
                    alert("Dung lượng file phải nhỏ hơn 3MB!");
                    $('#dataFile').val('');
                    $('#txtFilePath').val('');
                }
                else {
                    if (_extl.indexOf(_ext) > -1) {
                        $('#txtFilePath').val(_file.name);
                    }
                    else {
                        $(this).val('');
                        alert('Định dạng file đính kèm không được chấp nhận.');
                    }
                }
            } else {
                $('#txtFilePath').val('');
            }
        });

    });

    function LoadEditor() {
        $('textarea.editor').each(function () {
            var mheight = $(this).attr('height');
            $(this).ckeditor(function () {
                var uFolder = "<%=HttpContext.Current.User.Identity.Name%>"
                var v = '<%=HttpContext.Current.User.IsInRole("Root") || HttpContext.Current.User.IsInRole("Admin")%>';
                if (v == 'True' || v == true)
                    uFolder = 'Files';
                CKFinder.setupCKEditor(this, '/Content/ckfinder', uFolder, uFolder);
            },
            {
              toolbar:[['Source','-','Save','NewPage','Preview','-','Templates','Cut','Copy','Paste','PasteText','PasteFromWord','-','Print', 'SpellChecker', 'Scayt','Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat','Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField',
                '/','Bold','Italic','Underline','Strike','-','Subscript','Superscript','NumberedList','BulletedList','-','Outdent','Indent','Blockquote','CreateDiv','JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock',
	'Link', 'Unlink', 'Anchor', 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', '/', 'Styles', 'Format', 'Font', 'FontSize', 'TextColor', 'BGColor', 'Maximize', 'ShowBlocks', '-', 'VideoDetector']],
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
