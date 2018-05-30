<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teca.Core.Domain.Articles>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Chỉnh sửa tin
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    input[type=file] {
        display: none;
        visibility: hidden;
    }

    input[type=text] {
        resize: none;
        padding: 0px 5px;
        border: none;
        border-bottom: 1px dashed #ccc;
        height: 22px;
        background-color: #fee !important;
    }
</style>
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-file-word-o"></i>
                Chỉnh sửa tin
            </div>
            <div class="pull-right">
                <a href="/Article/Approveindex" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <form action="/Article/UpdateArt" method="post" enctype = "multipart/form-data">
                <%=Html.Hidden("Id",Model.Id) %>
                <fieldset>
                    <ol>
                        <li>
                            <label>Tiêu đề tiếng việt: </label>
                            <%=Html.Encode(Model.NameVNI)%>
                        </li>
                        <li>
                            <label class="control-label mt-10">Tiêu đề tiếng anh: </label>
                            <%=Html.Encode(Model.NameENG) %>
                        </li>
                        <li>
                            <label class="control-label mt-10">Ảnh đại diện</label>
                            <div class="input-group">
                                <img src="<%=Model.ImagePath %>" id="Img1" height="80" style="border: 1px solid #CCCCCC; float: left; margin-right: 5px;" />
                            </div>
                        </li>
                        <li>
                            <label class="control-label mt-10">Tóm tắt: </label>
                            <p><%=Html.Encode(Model.Summary) %></p>
                        </li>
                        <li>
                            <label class="control-label mt-10">Mức độ ưu tiên: </label>
                            <%=Model.Priority %>
                        </li>
                        <li>
                            <label class="control-label mt-10">Nội dung: </label>
                        </li>
                        <li>
                            <%=Model.Descriptions%>            
                        </li>
                        <li>
                            <div class="checkbox">
                                <label class="mt-10"><%=Html.CheckBox("Active", Model.Active)%>Hiển thị</label>
                            </div>
                        </li>
                        <li>
                            <div class="checkbox">
                                <label class="mt-10"><%=Html.CheckBox("IsHot", Model.IsHot)%>Tin nóng</label>
                            </div>
                        </li>
                        <li>
                            <label class="control-label mt-10">Văn bản đính kèm</label>
                            <p style="padding-top:6px;"> <%=Model.AttachData!=null ? "<a target='_blank' href='/Article/ViewAttachment?id="+Model.Id+"'>Click để xem chi tiết</a>" : "Không "  %></p>
                        </li>

                    </ol>
                </fieldset>
                <div class="textc">
                    <button class="btn btn-sm btn-success" type="submit"><i class="fa fa-check"></i>Duyệt tin</button>
                </div>
            </form>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
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
                    var _extl = ['pdf', 'doc', 'docx', 'xls', 'xlsx'];
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
    </script>
</asp:Content>
