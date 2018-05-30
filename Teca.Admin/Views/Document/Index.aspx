<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPagedList<Teca.Core.Domain.Document>>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Văn bản pháp quy
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h4 class="box-title"><i class="fa fa-user"></i>
                        Văn bản pháp quy
                    </h4>
                </div>
                <div class="box-body">
                    <div class="row">                        
                        <div class="col-xs-1" style="float:right;margin-bottom:5px">
                            <a href="/Document/Create/" class="btn btn-info btn-sm element-right"><i class="fa fa-plus"></i>Tạo mới</a>
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
                                        <th style="width: 120px">Người tạo</th>                                        
                                        <th style="width: 90px">Ngày tạo
                                        </th>
                                        <th style="width: 80px">Hiển thị
                                        </th>
                                        <th style="width: 40px">Sửa
                                        </th>
                                        <th style="border-right-color: #EEE; width: 40px;">Xóa
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
                                        <td style="text-align: center">
                                            <%=i%>
                                        </td>
                                        <td><%=Html.Encode(Items.Name) %></td>
                                        <td><%=Html.Encode(Items.NameENG) %></td>
                                        <td><%=Html.Encode(Items.CreateBy) %></td>
                                        <td>
                                            <%=Html.Encode(Items.CreateDate.ToString("dd/MM/yyyy"))%>
                                        </td>                                        
                                        <td style="text-align: center">
                                            <%=Items.Active?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>"%>
                                        </td>                                        
                                        <td style="text-align: center">
                                            <a class="fa fa-edit" href="/Document/Edit/?id=<%=Items.Id %>" title="Edit"></a>
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
                            <%=Html.Pager(Model.PageSize, Model.PageIndex+1, Model.TotalItemCount, new
                        {
                            action = "Index",
                            controller = "Document"                            
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
            document.location = "/Document/Delete?id=" + id;
        }
    </script>

</asp:Content>
