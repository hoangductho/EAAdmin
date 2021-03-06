﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPagedList<role>>" %>

<%@ Import Namespace="IdentityManagement.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quyền truy cập hệ thống
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/css/grid.css" rel="stylesheet" type="text/css" />
    <div class="box box-primary">
        <div class="box-header with-border">
            <h4 class="box-title"><i class="fa-paper-plane fa"></i>Phân quyền hệ thống</h4>
        </div>


        <div class="box-body no-padding">
            <div style="padding: 10px 10px 10px 5px; float: right">
                <a href="/IDMManager/Create/" class="btn btn-info btn-sm">
                    <i class="fa fa-user"></i>Tạo mới</a>
            </div>
            <table class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th width="40px">STT
                        </th>
                        <th>Tên
                        </th>
                        <th width="40px">Sửa
                        </th>
                        <th width="40px">Xóa
                        </th>
                    </tr>
                </thead>

                <tbody>
                    <%
                        IList<role> Rolst = Model.ToList();
                        int i = Model.PageIndex * Model.PageSize + 1;
                        foreach (role ro in Rolst)
                        {
                    %>
                    <tr>
                        <td style="text-align: right"><%=i %></td>
                        <td><%= Html.Encode(ro.name) %></td>
                        <td style="text-align: center"><a href="/IDMManager/Edit/<%=ro.roleid%>" title="EditRole">
                            <i class="fa fa-pencil"></i></a></td>
                        <%string roleName = ro.name;
                          if (roleName != "Root")
                          {%>
                        <td style="text-align: center"><a href="#" onclick="OnDelete('<%=ro.roleid %>')" title="Delete">
                            <i class="fa fa-trash"></i></a></td>
                        <%}
                          else
                          { %>
                        <td style="text-align: center"><i class="fa fa-lock"></i></td>
                        <%} %>
                    </tr>
                    <% i++;
                        }%>
                </tbody>
            </table>
        </div>
        <div class="box-footer clearfix">
            <div class="pager">
                <div class="page-a">
                    <%=Html.Pager(Model.PageSize, Model.PageIndex + 1, Model.TotalItemCount, new 
        {
            action = "Index",
            controller = "IDMManager"
        }
        )%>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function OnDelete(roleid) {
            alertify.confirm("Bạn có chắc chắn xóa role này không?", function (e) {
                if (e) {
                    document.location = "/IDMManager/Delete?roleid=" + roleid;
                }
                else return;
            });
        }
    </script>

</asp:Content>
