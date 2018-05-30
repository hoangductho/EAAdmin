<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPagedList<Notification>>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh sách thông báo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h4 class="box-title"><i class="fa fa-user"></i>
                        Danh sách thông báo
                    </h4>
                </div>
                <div class="box-body">


                    <div class="row">
                        <div class="col-xs-12">

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th width="30px">Stt
                                        </th>
                                        <th width="120px">Ngày tạo 
                                        </th>
                                        <th>Mô tả
                                        </th>
                                        <th width="200px">Người gửi
                                        </th>

                                        <th width="80px">Chi tiết
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
                                        <td><%=Html.Encode(Items.CreateDate.ToString("dd/MM/yyyy"))%></td>
                                        <td><%=Html.Encode(Items.Description)%></td>
                                        <td><%=Html.Encode(Items.ApproveBy)%></td>
                                        <td style="text-align: center">
                                            <a class="fa fa-eye" href="/Article/Edit?id=<%=Items.ArticleId%>" title="Edit"></a>
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
                            controller = "Notification" 
                        })%>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function OnDelete(id) {
            if (!confirm("Bạn có muốn xóa thông báo này không ?"))
                return;
            document.location = "/Notification/Delete?id=" + id;
        }
    </script>

</asp:Content>
