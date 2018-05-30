<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Introduction>" %>
<%@ Import Namespace="Teca.Core.Domain" %>

<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>

<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<script type="text/javascript" src="/Content/js/jquery.form.js"></script>
<script type="text/javascript" src="/Content/CKEditor/ckeditor.js"></script>
<script type="text/javascript" src="/Content/ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="/Content/js/EditorJAdapter.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        LoadEditor(); //load editor       
        //validate
        $('form:first').validate();
    });

    function LoadEditor() {
        $('textarea.editor').each(function () {
            var mheight = $(this).attr('height');
            $(this).ckeditor(function () { CKFinder.setupCKEditor(this, '/Content/ckfinder'); },
            {
                toolbar: [['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt', 'Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat', 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField',
                '/', 'Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript', 'NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock',
	'Link', 'Unlink', 'Anchor', 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', '/', 'Styles', 'Format', 'Font', 'FontSize', 'TextColor', 'BGColor', 'Maximize', 'ShowBlocks', '-', 'VideoDetector']],
                height: mheight
            });
        });
    }
</script>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                <%if(Model.Type == IntroductionType.LichSu) {%>
                Lịch sử hình thành
                <%} %>
                <%if(Model.Type == IntroductionType.CoCau) {%>
                Cơ cấu tổ chức
                <%} %>
                <%if(Model.Type == IntroductionType.ThanhTich) {%>
                Thành tích đạt được
                <%} %>
            </div>
            <div class="pull-right">
                <a href="/Article/Index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
              
                <div class="form-horizontal">
                    <div class="box-body">                          
                        <%=Html.Hidden("Id",Model.Id) %>
                        <%=Html.Hidden("Type",Model.Type) %>
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#vietnamese" data-toggle="tab">Tiếng Việt</a></li>
                                <li><a href="#english" data-toggle="tab">Tiếng Anh</a></li>

                            </ul>
                            <div class="tab-content">
                                <div class="active tab-pane" id="vietnamese">

                                    <div class="form-group">
                                        <label for="title" class="col-sm-2 control-label">Tiêu đề<span style="color: red">(*)</span></label>
                                        <div class="col-sm-10">
                                            <%=Html.TextBox("Title", Model.Title, new {@class="required checklength form-control",title = "Bạn chưa nhập tiêu đề", maxlength="150", style=""})%>
                                        </div>
                                    </div>                                    
                                    <div class="form-group">
                                        <label for="title" class="col-sm-2 control-label">Nội dung<span style="color: red">(*)</span></label>
                                        <div class="col-sm-10">
                                            <%=Html.TextArea("Description", Model.Description,  new { style = "width: 100%;", @class="editor" })%>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="english">
                                    <div class="form-group">
                                        <label for="title" class="col-sm-2 control-label">Tiêu đề:</label>
                                        <div class="col-sm-10">
                                            <%=Html.TextBox("TitleENG", Model.TitleENG, new {@class="form-control checklength", maxlength="150", style=""})%>
                                        </div>
                                    </div>                                    
                                    <div class="form-group">
                                        <label for="title" class="col-sm-2 control-label">Nội dung :</label>
                                        <div class="col-sm-10">
                                            <%=Html.TextArea("DescriptionENG", Model.DescriptionENG,  new { style = "width: 100%;", @class="editor" })%>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                            </div>
                            <!-- /.tab-content -->
                        </div>
                        <!-- /.nav-tabs-custom -->
                        <div class="form-group">                            
                            <label class="col-sm-2 control-label"><%=Html.CheckBox("Active", Model.Active)%>Hiển thị</label>
                        </div>                        
                    </div>
                    <!-- /.box-body -->
                </div>
        </div>
    </div>
</div>