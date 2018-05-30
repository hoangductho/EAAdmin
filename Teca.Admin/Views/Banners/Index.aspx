<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teca.Admin.Models.BannerModels>" %>

<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý banner
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h4 class="box-title"><i class="fa fa-user"></i>
                        Quản lý banner
                    </h4>
                </div>
                <div class="box-body">
                    <div class="row">  
                        <div class="col-xs-11">
                            <form id="Searchform" method="post" action="/Banners/Index" class="form-inline">
                                <label>Vị trí xuất hiện:</label>
                                <%= Html.EnumDropDownListFor(m => m.Type, Model.Type)%>                                
                                <button class="btn-sm btn btn-info" type="submit"><i class="glyphicon glyphicon-search"></i>Tìm kiếm</button>
                            </form>
                        </div>                      
                        <div class="col-xs-1" style="float:right;margin-bottom:5px">
                            <a href="/Banners/Create/" class="btn btn-info btn-sm element-right"><i class="fa fa-plus"></i>Tạo mới</a>
                        </div>
                    </div>
                    <div class="row" >
                        <div class="col-xs-12">

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th style="width: 40px">STT
                                        </th>
                                        <th>Tên tiếng việt</th>
                                        <th>Tên tiếng anh</th>
                                        <th>Đường dẫn
                                        </th>
                                        <th>Mô tả
                                        </th>
                                        <th style="width: 70px">Ưu tiên
                                        </th>
                                        <th style="width: 90px">Trạng thái
                                        </th>
                                        <th>Vị trí
                                        </th>
                                        <th style="width: 40px">Sửa
                                        </th>
                                        <th style="border-right-color: #EEE; width: 40px;">Xóa
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%    
                                        int i = Model.PageListBanners.PageIndex * Model.PageListBanners.PageSize + 1;
                                        foreach (var Items in Model.PageListBanners)
                                        {
                                    %>
                                    <tr>
                                        <td style="text-align: center">
                                            <%=i%>
                                        </td>
                                        <td><%=Html.Encode(Items.NameVNI) %></td>
                                        <td><%=Html.Encode(Items.NameENG) %></td>
                                        <td>
                                            <%=Html.Encode(Items.NavigateUrl)%>
                                        </td>
                                        <td>
                                            <%=Html.Encode(Items.DescriptionsVNI)%>
                                        </td>
                                        <td>
                                            <%=Html.Encode(Items.Priority)%>
                                        </td>
                                        <td>
                                            <%=Items.Active?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>"%>
                                        </td>
                                        <td>
                                            <%=Teca.Core.Utils.GetDescription(Items.TypeID)%>
                                        </td>
                                        <td style="text-align: center">
                                            <a class="fa fa-edit" href="/Banners/Edit/?id=<%=Items.Id %>" title="Edit"></a>
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
                            <%=Html.Pager(Model.PageListBanners.PageSize, Model.PageListBanners.PageIndex+1, Model.PageListBanners.TotalItemCount, new
                        {
                            action = "Index",
                            controller = "Banners",
                            type = Model.Type                            
                        })%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">        

        function OnDelete(id) {
            if (!confirm("Bạn có muốn xóa?"))
                return;
            document.location = "/Banners/Delete?id=" + id;
        }
    </script>
</asp:Content>
