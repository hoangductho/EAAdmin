<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teca.Admin.Models.GalleryModels>" %>

<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Chuyên mục ảnh
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <i class="fa fa-user"></i>
                    Quản lý hình ảnh
                    <a href="/GalleryType/Index" id="crtInvoice" class="btn btn-primary btn-sm"><i class="fa fa-backward"></i>Quay lại</a>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-11">
                            <form id="Searchform" name="Searchform" method="post" action="/GalleryType/IndexGallery" class="form-inline">
                                <label>Chuyên mục:</label>
                                <%=Html.DropDownList("TypeID", new SelectList(Model.GalleryTypes, "Id", "TitleVNI", Model.TypeID), "---Chuyên mục ảnh---", new { onchange="document.Searchform.submit()"})%>
                            </form>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-12">

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th width="30px">Stt
                                        </th>
                                        <th>Hình ảnh
                                        </th>
                                        <th width="100px">Ngày tạo
                                        </th>
                                        <th width="100px">Người tạo
                                        </th>
                                        <th style="width: 90px">Hiển thị
                                        </th>
                                        <th style="width: 30px">Xóa</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%
                                        int i = Model.Galleries.PageIndex * Model.Galleries.PageSize + 1;
                                        foreach (var Items in Model.Galleries.ToList())
                                        { 
                                    %>
                                    <tr>
                                        <td style="text-align: center; vertical-align: middle"><%=i%></td>
                                        <td>
                                            <img src="<%=Items.ImgPath%>" style="width: 100px; height: 80px" /></td>
                                        <td style="vertical-align: middle"><%=Html.Encode(Items.CreateDate.ToString("dd/MM/yyyy"))%></td>
                                        <td style="vertical-align: middle"><%=Html.Encode(Items.CreateBy)%></td>
                                        <td style="text-align: center; vertical-align: middle">
                                            <%=Items.Active && Items.GalleryTypes.Active ?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>"%>
                                        </td>
                                        <td style="text-align: center; vertical-align: middle">
                                            <a class="fa fa-remove" href="#" onclick="OnDelete('<%=Items.Id %>')" title="Delete"></a>
                                        </td>
                                    </tr>
                                    <% i++;
                                        }%>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-footer">

                <div class="pager">
                    <div class="page-a">
                        <%=Html.Pager(Model.Galleries.PageSize, Model.Galleries.PageIndex +1, Model.Galleries.TotalItemCount, new
                        {
                            action = "indexGallery",
                            controller = "GalleryType",
                            TypeID = Model.TypeID
                        })%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function OnDelete(id) {
            if (!confirm("Bạn có muốn xóa chuyên mục này không ?"))
                return;
            document.location = "/GalleryType/DeleteGallery?id=" + id;
        }
    </script>
</asp:Content>
