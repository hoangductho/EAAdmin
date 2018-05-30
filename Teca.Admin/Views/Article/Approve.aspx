<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teca.Core.Domain.Articles>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Duyệt bài viết
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-file-word-o"></i>
                Duyệt bài viết
            </div>
            <div class="pull-right">
                <a href="/Article/ApproveIndex" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <form action="/Article/Approved" method="post" name="approveArt">
                <%=Html.Hidden("Id",Model.Id) %>
                <%=Html.Hidden("error", false) %>
                <fieldset>
                    <ol>
                        <li>
                            <label>Thuộc chuyên mục: </label>
                            <%=Html.Encode(Model.Categories.NameVNI) %>
                        </li>
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
                            <%=Html.Encode(Model.Descriptions)%>            
                        </li>
                        <li>
                            <div class="checkbox">
                                <label class="mt-10"><%=Html.CheckBox("IsEvent", Model.IsEvent, new {})%>Sự kiện</label>
                            </div>
                        </li>

                        <li class="eventfields" <%=Model.IsEvent ? "style='display:block'" : "style='display:none'" %>>
                            <label for="" class="col-sm-2 control-label">Ngày bắt đầu sự kiện</label>
                            <div class="col-sm-5" style="position: relative">
                                <%=Model.StartDate.ToString("dd/MM/yyyy HH:mm") %>
                            </div>
                        </li>
                        <li class="eventfields" <%=Model.IsEvent ? "style='display:block'" : "style='display:none'" %>>
                            <label for="" class="col-sm-2 control-label">Ngày kết thúc sự kiện</label>
                            <div class="col-sm-5">
                                <%=Model.EndDate.ToString("dd/MM/yyyy HH:mm") %>
                            </div>
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
                            <p style="padding-top: 6px;"><%=Model.AttachData!=null ? "<a target='_blank' href='/Article/ViewAttachment?id="+Model.Id+"'>Click để xem chi tiết</a>" : "Không "  %></p>
                        </li>
                        <li>
                            <label class="control-label mt-10">Góp ý: </label>
                        </li>
                        <li>
                            <%=Html.TextArea("Comment",Model.Comment, new {@class = "checklength" , height = "100", style = "width: 100%;",maxlength="512"}) %> 
                        </li>
                    </ol>
                </fieldset>
                <div class="textc">
                    <button class="btn btn-sm btn-success" type="submit"><i class="fa fa-check"></i>Duyệt tin</button>
                    <button class="btn btn-sm btn-danger" type="button" onclick="preSubmit()"><i class="fa fa-times"></i>Gửi lỗi</button>
                    <button class="btn btn-sm btn-default btn-sm mr-15" type="button" onclick="document.location='/Article/ApproveIndex'"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</button>
                </div>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#IsEvent").change(function () {

                if ($(this).is(":checked")) {
                    $(".eventfields").fadeIn();

                } else {
                    $(".eventfields").fadeOut();

                }
            });

        });
        function preSubmit() {
            if (!$('#Comment').val()) {
                alert("Cần nhập thông tin góp ý.");
                return false;
            }
            $('#error').val(true);
            document.approveArt.submit();
        }
    </script>
</asp:Content>
