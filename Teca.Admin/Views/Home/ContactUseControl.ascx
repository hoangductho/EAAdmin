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
                toolbar: [['Styles', 'Format', 'Font', 'FontSize', 'Bold', 'Italic', 'Underline', 'Paste', 'NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'Table', 'Image', 'Link', 'Source']],
                height: mheight
            });
        });
    }
</script>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
               Thông tin liên hệ                
            </div>
            <div class="pull-right">
                <a href="/Home/Index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
              
                <div class="form-horizontal">
                    <div class="box-body">                          
                        <%=Html.Hidden("Id",Model.Id) %>
                        <%=Html.Hidden("Type",Model.Type) %>
                        <div class="nav-tabs-custom">                            
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