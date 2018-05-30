<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<VideoModels>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tạo mới video
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
    <div class="col-xs-12">
        <div class="box box-danger">
            <div class="box-header with-border">
                <div class="panel-title pull-left" style="line-height: 30px">
                    <i class="fa fa-user"></i>
                    Thông tin video
                </div>
                <div class="pull-right">
                    <a href="/Videos/index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
                </div>
            </div>
            <div class="box-body">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <%--<li class="active"><a href="#local" data-toggle="tab">Đăng video</a></li>--%>
                        <li class="active"><a href="#youtube" data-toggle="tab">Chọn từ Youtube</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane" id="local">
                            <div class="" id="upload-video">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="row">
                                              <form class="form-horizontal" class="mainform" name="mainform">
                                         <div class="form-group">
                                        <label for="title" class="col-md-4 control-label">Tiêu đề tiếng Việt <span style="color: red">(*)</span> :</label>
                                        <div class="col-md-8">
                                            <input type="text" style="width:600px" value="" name="titlevi" id="titlevi" class="form-control" placeholder=""                                                  
                                                    />    
                                             <div class="error error-vi" style="display:none;">Tiêu đề tiếng Việt không được để trống</div>    
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="title" class="col-md-4 control-label">Tiêu đề tiếng Anh <span style="color: red">(*)</span>:</label>
                                        <div class="col-md-8">
                                              <input type="text" style="width:600px" value="" name="titleen" id="titleen" class="form-control" placeholder=""                                                  
                                                    />   
                                               <div class="error error-en" style="display:none;">Tiêu đề tiếng Anh không được để trống</div>     
                                        </div>
                                    </div>
                                        <div class="form-group">                              
                                            <div class="col-md-8">
                                                  <input type="hidden" value="{%=file.url_video%}" name="url{%=file.id%}" id="url{%=file.id%}"                                                  
                                                    /> 
                                                  <input type="hidden" value="{%=file.thumbnail_url%}" name="image{%=file.id%}" id="image{%=file.id%}"                                                
                                                    />                                 
                                            </div>
                                        </div>     
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Mô tả :</label>
                                            <div class="col-md-8">
                                            <textarea style="width:600px"  name="describe"  id="describe"   rows="2" cols="20" class="form-control"></textarea>
                                                                                                                                      
                                            </div>
                                        </div>     
                                        <div class="form-group">
                                         <label class="col-md-4 control-label">Kích hoạt</label>
                                            <div class="col-md-8">
                                                <input type="checkbox" value="" checked="checked" name="Active" id="Active"                                              
                                                    />                                                                                        
                                            </div>
                                        </div>     
                                      <div class="form-group">
                                            <label class="col-md-4 control-label">Video nổi bật</label>
                                            <div class="col-md-8">
                                                 <input name="IsHot" id="IsHot" type="checkbox"/>                                                                                   
                                            </div>
                                        </div> 
                                         <div class="form-group">
                                            <label class="col-md-4 control-label">Mức độ ưu tiên:</label>
                                            <div class="col-md-8">
                                                 <input name="Priority" id="Priority" type="number" value="1" min="1" max="1000"/>                                                                                   
                                            </div>
                                        </div>     
                                          
                                      <div class="form-group">
                                            <label class="col-md-4 control-label">Chuyên mục video <span style="color: red">(*)</span>:</label>
                                            <div class="col-md-8">
                                                
                                               <select class="form-control" style="width:300px" name="id_type" id="id_type">
                                                   <% foreach (var item in Model.VideoTypes)
                                                      {%>
                                                     <option value="<%=item.Id %>"><%=item.NameVNI %></option>
                                                   <%
                                                      }%>  
                                                    <option selected value>---Chuyên mục video---</option>                                                                                                  
                                               </select>   
                                               <div class="error error-type" style="display:none;">Chưa chọn chuyên mục cho Video</div>                                                                  
                                            </div>
                                        </div>                                                                                                                                                                                                                                                                          
                                    </form>   

                                        </div>
                                        <div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <form id="fileupload" action="/Upload/UploadHandler.ashx" method="POST" enctype="multipart/form-data">
                                                        <!-- The fileupload-buttonbar contains buttons to add/delete files and start/cancel the upload -->
                                                        <p><i>Chú ý <span style="color: red">(*)</span>:Chỉ đăng các video có định dạng <span style="color: red">.mp4</span> và <span style="color:red">.webm</span></i></p>
                                                        <div class="row fileupload-buttonbar">
                                                            <div class="col-lg-7">
                                                                <!-- The fileinput-button span is used to style the file input field as button -->
                                                                <span class="btn btn-success fileinput-button">
                                                                    <i class="fa fa-upload"></i>
                                                                    <span>Đăng Video </span>
                                                                    <input type="file" name="files[]" multiple id="ChooseFile">
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <!-- The loading indicator is shown during image processing -->
                                                        <div class="fileupload-loading"></div>
                                                        <br>
                                                        <!-- The table listing the files available for upload/download -->
                                                        <table class="table table-striped" style="margin-bottom:0">
                                                            <tbody class="files" data-toggle="modal-gallery" data-target="#modal-gallery"></tbody>
                                                        </table>
                                                    </form>

                                                    <!-- modal-gallery is the modal dialog used for the image gallery -->
                                                    <div id="modal-gallery" class="modal modal-gallery hide fade">
                                                        <div class="modal-header">
                                                            <a class="close" data-dismiss="modal">&times;</a>
                                                            <h3 class="modal-title"></h3>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="modal-image"></div>
                                                        </div>
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
                                                                <td colspan="4">
                                                                    <div class="row">
                                                                        <div class="col-sm-2">{%=file.name%}</div>
                                                                         <div class="col-sm-2">{%=o.formatFileSize(file.size)%}</div>
                                                                            {% if (file.error) { %}
                                                                    <div class="col-sm-4"><span class="label label-important">{%=locale.fileupload.error%}</span> {%=locale.fileupload.errors[file.error] || file.error%}</div>
                                                                 <div class="cancel col-sm-2">
                                                                    <button class="btn btn-warning">
                                                                        <i class="icon-ban-circle icon-white"></i>
                                                                        <span>Hủy</span>
                                                                    </button></div>
                                                                {% } else if (o.files.valid && !i) { %}
                                                                     <div class="col-sm-4">
                                                                        <div class="progress progress-striped active"><div class="progress-bar progress-bar-success bar" style="width:0%;"></div></div>
                                                                    </div>
                                                                     <div class="col-sm-4 ">{% if (!o.options.autoUpload) { %}
                                                            <div class="start" style="float:left;margin-right:10px;">
                                                                        <button class="btn btn-primary">
                                                                            <i class="icon-upload icon-white"></i>
                                                                            <span>Bắt đầu</span>
                                                                        </button></div>
                                                            <div class="cancel"  style="float:left">
                                                                    <button class="btn btn-warning cancel-upload">
                                                                        <i class="icon-ban-circle icon-white"></i>
                                                                        <span >Hủy</span>
                                                                    </button></div>
                                                                    {% } %}</div>
                                                                {% } else { %}
                                                                    <div class="col-sm-2"></div>
                                                                {% } %}
               
                                                                    </div>
                                                                </td>
                                                            </td>
       
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
                <a href="#" title="{%=file.name%}" rel="gallery"><img src="{%=file.thumbnail_url%}?thumb=1" width="100px" height="80px"/></a>
            {% } %}</td>
            <td class="name">
                <a href="#" title="{%=file.name%}" rel="{%=file.thumbnail_url&&'gallery'%}" download="{%=file.name%}">{%=file.name%}</a>
            </td>
            <td class="size"><span>{%=o.formatFileSize(file.size)%}</span></td>
            <td colspan="2"></td>
        {% } %}
        <td class="delete">
            <button class="btn btn-danger cancel-upload" onclick="removeInfo()" data-type="{%=file.delete_type%}" data-url="{%=file.delete_url%}">
                <i class="icon-trash icon-white"></i>
                <span>Hủy</span>
            </button>
        </td>       
    </tr>
    <tr class="{%=file.id%}">
        <td colspan="6">
                <div class="col-md-12 widget-module">
                            <div class="square-widget widget-collapsible">
                                <div class="widget-head clearfix"> 
                                    <h4 class="pull-left"><i class="icon-paragraph-justify-2"></i> Thông tin bổ sung  </h4>
                                    <span class="pull-right widget-action"><a href="#" class="widget-collapse"><i class="icon-arrow-down"></i></a><a href="#" class="widget-remove"><i class=" icon-remove-sign"></i></a></span>
                                </div>
                                <div class="widget-container">                   
                                                                                      
                         
                                         <div class="form-group" style="margin-top:5px">
                                            <label class="col-md-4 control-label">&nbsp;</label>
                                            <div class="col-md-8">
                                                <div class="form-actions">
                                            <button type="button" onclick=test("{%=file.id%}") class="btn btn-primary submit_video btn-sm"><i class="fa fa-save"></i>Lưu </button>
                                                   
                                               <a href="/Videos/Index" class="btn btn-default btn-sm" ><i class="fa fa-backward"></i>Quay lại</a>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>    
    </td>
    </tr>
{% } %}    
                                                    </script>
                                                    <!-- <form class="form-horizontal" class="mainform" method="POST" action = "/Admin/Videos/create">-->
                                                </div>
                                            </div>
                                        </div>
                                        <script>
                                            $(document).ready(function () {

                                            });
                                            function removeInfo() {
                                                $('#fileupload').find('tr.in').remove();
                                            }
                                            function test(id) {
                                                $('#local .error-vi').css('display', 'none');
                                                $('#local  .error-en').css('display', 'none');
                                                $('#local  .error-type').css('display', 'none');
                                                if ($("#local  #titlevi" + id).val().length <= 0) {

                                                    $('#local  .error-vi').css('display', 'block');
                                                } else if ($("#local  #titleen" + id).val().length <= 0) {

                                                    $('#local .error-en').css('display', 'block');
                                                } else if ($("#local  #id_type" + id).val() == "") {
                                                    $('#local  .error-type').css('display', 'block');
                                                } else {

                                                    var video = {};
                                                    video.NameVNI = $("#titlevi" + id).val();
                                                    video.NameENG = $("#titleen" + id).val();
                                                    video.Summary = $("#describe" + id).val();
                                                    video.VideoPath = $("#url" + id).val();
                                                    video.VideoTypeID = $("#id_type" + id).val();
                                                    video.Active = $("#Active" + id).prop('checked') ? true : false;
                                                    video.Priority = $("#Priority" + id).val();
                                                    video.IsHot = $("#IsHot" + id).prop('checked') ? true : false;
                                                    $.ajax({
                                                        type: "POST",
                                                        url: "/Videos/ConfirmUpload",
                                                        data: video,
                                                        success: function (data) {
                                                            window.location = "/Videos/Index";
                                                        }, error: function (data) {
                                                            swal("Lỗi", "Có lỗi xảy ra xin vui lòng thử lại", "error");

                                                        }
                                                    });
                                                }
                                            }
                                        </script>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="active tab-pane" id="youtube">
                            <form action="/Videos/Create" method="post" class="form-horizontal" id="YoutubeForm">
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Đường dẫn  <span style="color: red">(*)</span> </label>
                                    <div class="col-md-8">
                                        <input type="text" name="VideoPath" id="VideoPath" value="" class="form-control" style="width: 600px;"  placeholder="https://www.youtube.com/watch?v=2rKiW90FlmY"/>
                                        <div class="error error-empty-video" style="display: none;">Chưa nhập đường dẫn cho video</div>
                                        <div class="error error-not-youtube" style="display: none;">Chỉ được chọn video từ Youtube.com</div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Tiêu đề tiếng Việt <span style="color: red">(*)</span> </label>
                                    <div class="col-md-8">
                                        <input type="text" name="NameVNI" id="NameVNI" value="" class="form-control required" style="width: 600px;" />
                                        <div class="error error-vi" style="display: none;">Tiêu đề tiếng Việt không được để trống</div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Tiêu đề tiếng Anh <span style="color: red">(*)</span> </label>
                                    <div class="col-md-8">
                                        <input type="text" name="NameENG" id="NameENG" value="" class="form-control required" style="width: 600px;" />
                                        <div class="error error-vi" style="display: none;">Tiêu đề tiếng Anh không được để trống</div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Mô tả :</label>
                                    <div class="col-md-8">
                                        <textarea style="width: 600px" name="Summary" id="Summary" rows="2" cols="20" class="form-control"></textarea>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Kích hoạt</label>
                                    <div class="col-md-8">
                                          <%=Html.CheckBox("Active", Model.Video.Active)%>
                                       
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Video nổi bật</label>
                                    <div class="col-md-8">
                                          <%=Html.CheckBox("IsHot", Model.Video.IsHot)%>
                                       
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Mức độ ưu tiên:</label>
                                    <div class="col-md-8">
                                        <input name="Priority" id="Priority" type="number" value="1" min="1" max="1000" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Chuyên mục video <span style="color: red">(*)</span>:</label>
                                    <div class="col-md-8">

                                        <select class="form-control " style="width: 300px" name="VideoTypeID" id="VideoTypeID">
                                            <% foreach (var item in Model.VideoTypes)
                                               {%>
                                            <option value="<%=item.Id %>"><%=item.NameVNI %></option>
                                            <%
                                                      }%>
                                            <option selected value>---Chuyên mục video---</option>
                                        </select>
                                        <div class="error error-type" style="display: none; float: left">Chưa chọn chuyên mục cho Video</div>
                                    </div>
                                </div>
                                <div class="form-group" style="margin-top: 5px">
                                    <label class="col-md-4 control-label">&nbsp;</label>
                                    <div class="col-md-8">
                                        <div class="form-actions">
                                            <button type="submit" class="btn btn-primary btn-sm"><i class="fa fa-save"></i>Lưu </button>

                                            <a href="/Videos/Index" class="btn btn-default btn-sm"><i class="fa fa-backward"></i>Quay lại</a>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            //validate
            $('#YoutubeForm').submit(function () {
                $('#youtube  .error-empty-video').css('display', 'none');
                $('#youtube  .error-not-youtube').css('display', 'none');
                $('#youtube .error-vi').css('display', 'none');
                $('#youtube .error-en').css('display', 'none');
                $('#youtube  .error-type').css('display', 'none');
                if ($("#youtube #VideoPath").val().length <= 0) {
                    $('#youtube  .error-empty-video').css('display', 'block');
                    return false;
                } else if ($("#youtube #VideoPath").val().indexOf("youtube.com") < 0) {
                    $('#youtube  .error-not-youtube').css('display', 'block');
                    return false;
                }
                else if ($("#youtube  #NameVNI").val().length <= 0) {
                    $('#youtube  .error-vi').css('display', 'block');
                    return false;
                } else if ($("#youtube  #NameENG").val().length <= 0) {
                    $('#youtube .error-en').css('display', 'block');
                    return false;
                } else if ($("#youtube  #VideoTypeID").val() == "") {
                    $('#youtube  .error-type').css('display', 'block');
                    return false;
                }
        })
        });
    </script>
</asp:Content>
