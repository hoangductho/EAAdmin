<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPagedList<Contact>>" %>

<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh sách liên hệ
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h4 class="box-title"><i class="fa fa-user"></i>
                        Danh sách liên hệ
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
                                        <th>Tên
                                        </th>
                                        <th>Email
                                        </th>
                                        <th>Address
                                        </th>
                                        <th width="120px">Ngày tạo
                                        </th>                                       
                                        <th style="width: 90px">Hiển thị
                                        </th>
                                        <th width="50px">Xem
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
                                        <td><%=Html.Encode(Items.Name)%></td>
                                        <td><%=Html.Encode(Items.Email)%></td>
                                        <td><%=Html.Encode(Items.Address)%></td>
                                        <td><%=Html.Encode(Items.CreatedDate.ToString("dd/MM/yyyy"))%></td>                                        
                                        <td style="text-align: center">
                                            <%=Items.Active?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>"%>
                                        </td>
                                        <td style="text-align: center">
                                            <a class="fa fa-eye" href="/Contact/Detail?id=<%=Items.Id%>" title="Detail"></a>
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
                            controller = "Contact"
                        })%>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript">
        function OnDelete(id) {
            if (!confirm("Bạn có muốn xóa liên hệ này không ?"))
                return;
            document.location = "/Contact/Delete?id=" + id;
        }
    </script>

</asp:Content>
