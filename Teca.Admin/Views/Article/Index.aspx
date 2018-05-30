<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<ArticleIndexModel>" %>
<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh sách tin
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                        <h4 class="box-title"><i class="fa fa-user"></i>
                        Quản lý tin tức
                    </h4>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-11">
                            <form id="Searchform" method="post" action="/Article/Index" class="form-inline">
                                <label>Chuyên mục:</label>
                                <%=Html.DropDownList("CatID",new SelectList(Model.Categories, "Id","NameVNI",Model.CatID), "---Chuyên mục tin---") %>
                                <label>Tiêu đề tin:</label>                                
                                <%=Html.TextBox("Keyword", Model.Keyword, new {style="width: 250px; margin-left: 10px; margin-right: 10px", maxlength="50" })%>
                                <button class="btn-sm btn btn-info" type="submit"><i class="glyphicon glyphicon-search"></i>Tìm kiếm</button>
                            </form>
                        </div>
                        <div class="col-xs-1">
                            <a href="/Article/Create/" class="btn btn-info btn-sm element-right"><i class="fa fa-plus"></i>Tạo mới</a>
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
                                        <th width="120px">Ngày tạo
                                        </th>                                        
                                        <th style="width: 90px">Hiển thị
                                        </th>
                                        <th style="width: 90px">Sự kiện
                                        </th>
                                        <th style="width: 90px">Phê duyệt
                                        </th>
                                        <th width="50px">Sửa
                                        </th>
                                        <th width="50px">Xóa
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%
                                        int i = Model.Articles.PageIndex * Model.Articles.PageSize + 1;
                                        bool kiemtra = true;
                                        foreach (var Items in Model.Articles.ToList())
                                        { 
                                    %>
                                    <tr>
                                        <td style="text-align: center"><%=i%></td>
                                        <td><%=Html.Encode(Items.NameVNI)%></td>
                                        <td><%=Html.Encode(Items.NameENG)%></td>
                                        <td><%=Html.Encode(Items.CreatedDate.ToString("dd/MM/yyyy"))%></td>                                        
                                        <td style="text-align: center">
                                            <%=Items.Active?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>"%>
                                        </td>
                                        <td style="text-align: center">
                                            <%=Items.IsEvent?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-remove\"></i>"%>
                                        </td>
                                        <td style="text-align: center">                                            
                                            <%=Items.Approved? "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-times\"></i>"%>
                                        </td>
                                        <td style="text-align: center">
                                            <a class="fa fa-edit" href="/Article/Edit?id=<%=Items.Id%>" title="Edit"></a>
                                        </td>
                                        <td style="text-align: center">                                            
                                            <%=kiemtra?  "<a class=\"fa fa-remove\" href=\"#\" onclick=\"OnDelete('"+ Items.Id + "')\" title=\"Delete\"></a> " : "<i class=\"fa fa-lock\"></i>"%>
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
                            <%=Html.Pager(Model.Articles.PageSize, Model.Articles.PageIndex +1, Model.Articles.TotalItemCount, new
                        {
                            action = "index",
                            controller = "Article",
                            Keyword = Model.Keyword,
                            CatID=  Model.CatID
                        })%>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function OnDelete(id) {
            if (!confirm("Bạn có muốn xóa loại tin tức này không ?"))
                return;
            document.location = "/Article/Delete?id=" + id;
        }
    </script>

</asp:Content>
