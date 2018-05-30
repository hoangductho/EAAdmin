<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VideoModels>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Admin.Models" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />\
    <link href="/Content/FileUpload/jquery.fileupload-ui.css" rel="stylesheet"/>
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<script type="text/javascript" src="/Content/js/jquery.form.js"></script>
 
        <script src='/Content/FileUpload/tmpl.min.js'></script>
        
     <script src='/Content/FileUpload/jquery.iframe-transport.js'></script>
     <script src='/Content/FileUpload/jquery.fileupload.js'></script>

     <script src='/Content/FileUpload/jquery.fileupload-ip.js'></script>
     <script src='/Content/FileUpload/jquery.fileupload-ui.js'></script>
     <script src='/Content/FileUpload/locale.js'></script>
     <script src='/Content/FileUpload/main.js'></script>

<script type="text/javascript">
 
</script>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                Thông tin video
            </div>
            <div class="pull-right">
                <a href="/Video/index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
          
    <div class="container" id="upload-video">
        <div class="row">
            <div class="col-lg-12">               
                <p class="error"> <i>Lưu ý : Các video sẽ được kiểm duyệt trước khi đăng lên trang chủ .</i></p>

              
    <div >    
                             <div class="row">
                                 <div class="col-lg-12">
<form id="fileupload" action="/Upload/UploadHandler.ashx" method="POST" enctype="multipart/form-data">
        <!-- The fileupload-buttonbar contains buttons to add/delete files and start/cancel the upload -->
        <div class="row fileupload-buttonbar">
            <div class="col-lg-7">
                <!-- The fileinput-button span is used to style the file input field as button -->
                <span class="btn btn-success fileinput-button">
                    <i class="icon-plus icon-white"></i>
                    <span>Tải lên </span>
                    <input type="file" name="files[]" multiple>
                </span>
                <button type="submit" class="btn btn-primary start">
                    <i class="icon-upload icon-white"></i>
                    <span>Bắt đầu tải </span>
                </button>
                <button type="reset" class="btn btn-warning cancel">
                    <i class="icon-ban-circle icon-white"></i>
                    <span>Hủy</span>
                </button>
                  
            </div>
         
        </div>
        <!-- The loading indicator is shown during image processing -->
        <div class="fileupload-loading"></div>
        <br>
        <!-- The table listing the files available for upload/download -->
        <table class="table table-striped"><tbody class="files" data-toggle="modal-gallery" data-target="#modal-gallery"></tbody></table>
    </form>
    
    <!-- modal-gallery is the modal dialog used for the image gallery -->
    <div id="modal-gallery" class="modal modal-gallery hide fade">
        <div class="modal-header">
            <a class="close" data-dismiss="modal">&times;</a>
            <h3 class="modal-title"></h3>
        </div>
        <div class="modal-body"><div class="modal-image"></div></div>
        <div class="modal-footer">
            <a class="btn btn-primary modal-next">
                <span>Next</span>
                <i class="icon-arrow-right icon-white"></i>
            </a>
            <a class="btn btn-info modal-prev">
                <i class="icon-arrow-left icon-white"></i>
                <span>Previous</span>
            </a>
            <a class="btn btn-success modal-play modal-slideshow" data-slideshow="5000">
                <i class="icon-play icon-white"></i>
                <span>Slideshow</span>
            </a>
            <a class="btn modal-download" target="_blank">
                <i class="icon-download"></i>
                <span>Download</span>
            </a>
        </div>
    </div>
        
   

<!-- The template to display files available for upload -->
<script id="template-upload" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-upload fade">
        <td class="preview"><span class="fade"></span></td>
        <td class="name"><span>{%=file.name%}</span></td>
        <td class="size"><span>{%=o.formatFileSize(file.size)%}</span></td>
        {% if (file.error) { %}
            <td class="error" colspan="2"><span class="label label-important">{%=locale.fileupload.error%}</span> {%=locale.fileupload.errors[file.error] || file.error%}</td>
        {% } else if (o.files.valid && !i) { %}
            <td>
                <div class="progress progress-striped active"><div class="progress-bar progress-bar-success bar" style="width:0%;"></div></div>
            </td>
            <td class="start">{% if (!o.options.autoUpload) { %}
                <button class="btn btn-primary">
                    <i class="icon-upload icon-white"></i>
                    <span>{%=locale.fileupload.start%}</span>
                </button>
            {% } %}</td>
        {% } else { %}
            <td colspan="2"></td>
        {% } %}
        <td class="cancel">{% if (!i) { %}
            <button class="btn btn-warning">
                <i class="icon-ban-circle icon-white"></i>
                <span>{%=locale.fileupload.cancel%}</span>
            </button>
        {% } %}</td>
    </tr>
{% } %}
</script>
<!-- The template to display files available for download -->
<script id="template-download" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { 
 
    %}
    <tr class="template-download fade {%=file.id%}">
        {% if (file.error) { %}
            <td></td>
            <td class="name"><span>{%=file.name%}</span></td>
            <td class="size"><span>{%=o.formatFileSize(file.size)%}</span></td>
            <td class="error" colspan="2"><span class="label label-important">{%=locale.fileupload.error%}</span> {%=locale.fileupload.errors[file.error] || file.error%}</td>
        {% } else { %}
            <td class="preview">{% if (file.thumbnail_url) { %}
                <a href="#" title="{%=file.name%}" rel="gallery"><img src="/Uploads/videos/{%=file.thumbnail_url%}" width="100px" height="80px"/></a>
            {% } %}</td>
            <td class="name">
                <a href="#" title="{%=file.name%}" rel="{%=file.thumbnail_url&&'gallery'%}" download="{%=file.name%}">{%=file.name%}</a>
            </td>
            <td class="size"><span>{%=o.formatFileSize(file.size)%}</span></td>
            <td colspan="2"></td>
        {% } %}
        <td class="delete">
            <button class="btn btn-danger" data-type="{%=file.delete_type%}" data-url="{%=file.delete_url%}">
                <i class="icon-trash icon-white"></i>
                <span>{%=locale.fileupload.destroy%}</span>
            </button>
            
        </td>       
    </tr>
    <tr class="{%=file.id%}">
        <td colspan="4">
                <div class="col-md-12 widget-module">
                            <div class="square-widget widget-collapsible">
                                <div class="widget-head clearfix"> 
                                    <h4 class="pull-left"><i class="icon-paragraph-justify-2"></i> Thông tin bổ xung  </h4>
                                    <span class="pull-right widget-action"><a href="#" class="widget-collapse"><i class="icon-arrow-down"></i></a><a href="#" class="widget-remove"><i class=" icon-remove-sign"></i></a></span>
                                </div>
                                <div class="widget-container">        
                                                                 
                                    <form class="form-horizontal" class="mainform" name="mainform">
                                        <div class="form-group">
                                            <label class="col-lg-4 control-label">Tiêu đề :</label>
                                            <div class="col-lg-8">
                                                <input type="text" value="{%=file.name%}" name="title{%=file.id%}" id="title{%=file.id%}" class="form-control" placeholder=""                                                  
                                                    />    
                                                  <input type="hidden" value="{%=file.url_video%}" name="url{%=file.id%}" id="url{%=file.id%}"                                                  
                                                    /> 
                                                 
                                                  <input type="hidden" value="{%=file.thumbnail_url%}" name="image{%=file.id%}" id="image{%=file.id%}"                                                
                                                    />
                                                                                         
                                            </div>
                                        </div>     
                                        <div class="form-group">
                                            <label class="col-lg-4 control-label">Mô tả :</label>
                                            <div class="col-lg-8">
                                                <input type="text" value="" name="describe{%=file.id%}" id="describe{%=file.id%}" class="form-control" placeholder=""                                                  
                                                    />                                                                                        
                                            </div>
                                        </div>     
                                           
                                      <div class="form-group">
                                            <label class="col-lg-4 control-label">Thể loại video  :</label>
                                            <div class="col-lg-8">
                                                
                                               <select class="form-control" name="id_type{%=file.id%}" id="id_type{%=file.id%}">
                                                   <% foreach (var item in Model.VideoTypes)
                                                      {%>
                                                     <option value="<%=item.Id %>"><%=item.NameVNI %></option>
                                                   <%
                                                      }%>                                                                                                    
                                               </select>                                                                
                                            </div>
                                        </div>                                                                                                                                                                                                                                                                          
                                    </form>                                                       
                         
                                         <div class="form-group" style="margin-top:5px">
                                            <label class="col-lg-4 control-label">&nbsp;</label>
                                            <div class="col-lg-8">
                                                <div class="form-actions">
                                                    <input type="button" onclick=test("{%=file.id%}") class="btn btn-primary submit_video" value="Lưu"/>
                                                    <button type="button" class="btn">Hủy</button>
                                                </div>
                                            </div>
                                        </div>

                                </div>
                            </div>
                        </div>    
    </td>
    </tr>
{% } %}    
</script><!-- <form class="form-horizontal" class="mainform" method="POST" action = "/Admin/Videos/create">-->
    
                                 </div>                                                           
      
    </div></div>
     <script>
         function SubmitButtonOnclick(name) {
             alert($("#file" + name).file.name);
             var formData = new FormData();

             formData.append("file", $("#file" + name).file);


             $.ajax({
                 type: "POST",
                 url: '/Video/ChangeVideoImg',
                 data: formData,
                 dataType: 'json',
                 contentType: "multipart/form-data",
                 processData: false,
                 success: function (response) {
                     alert(response);
                 },
                 error: function (error) {
                     alert("error");
                 }
             });
         }


         function test(id) {
             var url = "/Videos/ConfirmUpload?id_type=" + $("#id_type" + id).val() + "&title="
                     + $("#title" + id).val() + "&url=" + $("#url" + id).val()
                     + "&describle=" + $("#describe" + id).val()
                      +"&image=" + $("#image" + id).val();

             $.ajax({
                 type: "POST",
                 url: url,
                 success: function (data) {
                     alert(data);
                     $("tr." + id).remove();
                 }, error: function (data) {
                     alert(data);
                 }
             });
         }
     </script>
            </div>
     
        </div>
    </div> 
        </div>
    </div>
</div>
<script type="text/javascript">
     
</script>
