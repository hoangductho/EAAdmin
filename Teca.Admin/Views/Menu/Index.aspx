<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teca.Admin.Models.MenusModels>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh mục menu
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                        <h4 class="box-title"><i class="fa fa-user"></i>
                        Quản lý menu hệ thống
                    </h4>
                </div>
                <div class="box-body">
                    <div class="row">  
                        <div class="col-xs-11">
                            <form id="Searchform" method="post" action="/Menu/Index" class="form-inline">
                                <label>Vị trí xuất hiện:</label>
                                <%= Html.EnumDropDownListFor(m => m.Type, Model.Type)%>                                
                                <button class="btn-sm btn btn-info" type="submit"><i class="glyphicon glyphicon-search"></i>Tìm kiếm</button>
                            </form>
                        </div>                       
                        <div class="col-xs-1" style="float:right;margin-bottom:5px">
                            <a href="/Menu/Create/" class="btn btn-info btn-sm element-right"><i class="fa fa-plus"></i>Tạo mới</a>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-12">

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th width="30px">Stt
                                        </th>
                                        <th>Tên tiếng việt
                                        </th>
                                        <th>Tên tiếng anh
                                        </th>
                                        <th>Đường dẫn
                                        </th>
                                        <th width="120px">Vị trí
                                        </th>
                                        <th width="80px">Ưu tiên
                                        </th>
                                        <th style="width: 90px">Hiển thị
                                        </th>
                                        <th width="50px">Sửa
                                        </th>
                                        <th width="50px">Xóa
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%
                                        int i = Model.PageListMenus.PageIndex * Model.PageListMenus.PageSize + 1;
                                        foreach (var Items in Model.PageListMenus.ToList())
                                        { 
                                    %>
                                    <tr>
                                        <td style="text-align: center"><%=i%></td>
                                        <td><%=Html.Encode(Items.NameVNI)%></td>
                                        <td><%=Html.Encode(Items.NameENG)%></td>
                                        <td><%=Html.Encode(Items.NavigateUrl)%></td>
                                        <td><%=Html.Encode(Teca.Admin.Utils.GetDescription(Items.Position))%></td>
                                        <td style="text-align: center"><%=Html.Encode(Items.Priority)%></td>
                                        <td style="text-align: center">
                                            <%=Items.IsPub?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>"%>
                                        </td>
                                        <td style="text-align: center">
                                            <a class="fa fa-edit" href="/Menu/Edit?id=<%=Items.Id%>" title="Edit"></a>
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
                            <%=Html.Pager(Model.PageListMenus.PageSize, Model.PageListMenus.PageIndex +1, Model.PageListMenus.TotalItemCount, new
                        {
                            action = "index",
                            controller = "Menu",
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
            if (!confirm("Bạn có muốn xóa menu này không ?"))
                return;
            document.location = "/Menu/Delete?id=" + id;
        }
    </script>
</asp:Content>
