<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<IPagedList<GalleryType>>" %>

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
                        <h4 class="box-title"><i class="fa fa-user"></i>
                        Quản lý chuyên mục ảnh
                    </h4>
                </div>
                <div class="box-body">
                    <div class="row">                        
                        <div class="col-xs-1" style="float:right;margin-bottom:5px">
                            <a href="/Gallerytype/Create/" class="btn btn-info btn-sm element-right"><i class="fa fa-plus"></i>Tạo mới</a>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-12">

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th width="30px">Stt
                                        </th>
                                        <th>Tiêu đề tiếng việt
                                        </th>
                                        <th>Tiêu đề tiếng anh
                                        </th>
                                        <th width="100px">Ngày tạo
                                        </th>
                                        <th width="100px">Người tạo
                                        </th>
                                        <th style="width: 80px">Hiển thị
                                        </th>
                                        <th style="width: 80px">Kho ảnh</th>
                                        <th width="50px">Sửa
                                        </th>
                                        <th width="50px">Xóa
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%
                                        int i = Model.PageIndex * Model.PageSize + 1;
                                        foreach (var Items in Model.ToList())
                                        { 
                                    %>
                                    <tr>
                                        <td style="text-align: center"><%=i%></td>
                                        <td><%=Html.Encode(Items.TitleVNI)%></td>
                                        <td><%=Html.Encode(Items.TitleENG)%></td>
                                        <td><%=Html.Encode(Items.CreateDate.ToString("dd/MM/yyyy"))%></td>
                                        <td><%=Html.Encode(Items.CreateBy)%></td>
                                        <td style="text-align: center">
                                            <%=Items.Active?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>" %>
                                        </td>
                                        <td style="text-align: center">
                                            <a class="glyphicon glyphicon-picture" href="/Gallerytype/IndexGallery?typeid=<%=Items.Id%>" title="Edit"></a>
                                        </td>
                                        <td style="text-align: center">
                                            <a class="fa fa-edit" href="/GalleryType/Edit?id=<%=Items.Id%>" title="Edit"></a>
                                        </td>
                                        <td style="text-align: center">
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
                <div class="box-footer">

                    <div class="pager">
                        <div class="page-a">
                            <%=Html.Pager(Model.PageSize, Model.PageIndex +1, Model.TotalItemCount, new
                        {
                            action = "index",
                            controller = "GalleryType"
                        })%>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function OnDelete(id) {
            if (!confirm("Bạn có muốn xóa chuyên mục này không ?"))
                return;
            document.location = "/GalleryType/Delete?id=" + id;
        }
    </script>

</asp:Content>
