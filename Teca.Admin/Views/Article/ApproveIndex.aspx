<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<ArticleIndexModel>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Phê duyệt tin tức
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h4 class="box-title"><i class="fa fa-user"></i>
                        Phê duyệt tin tức
                    </h4>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-11">
                            <form id="Searchform" method="post" action="/Article/ApproveIndex" class="form-inline">
                                <label>Chuyên mục:</label>
                                <%=Html.DropDownList("CatID", new SelectList(Model.Categories, "Id", "NameVNI", Model.CatID), new { onchange="$('form:first').submit()"})%>
                                <label>Tiêu đề tin:</label>
                                <%=Html.TextBox("Keyword", Model.Keyword, new {style="width: 250px; margin-left: 10px; margin-right: 10px", maxlength="50" })%>
                                <label>Tin đã duyệt:</label>
                                <%=Html.CheckBox("approved", ViewData["approved"])%>
                                <button class="btn-sm btn btn-info" type="submit"><i class="glyphicon glyphicon-search"></i>Tìm kiếm</button>
                            </form>
                        </div>
                    </div>
                    <form id="danhsachTin" method="post" action="/Article/hiddenart">
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
                                            <th style="width: 110px">Ngày tạo
                                            </th>
                                            <th style="width: 120px">Người tạo
                                            </th>
                                            <th style="width: 90px">Hiển thị
                                            </th>
                                              <th style="width: 90px">Sự kiện
                                            </th>
                                            <th style="width: 90px">Phê duyệt
                                            </th>
                                            <th style="width: 60px">Sửa
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <%
                                            int i = Model.Articles.PageIndex * Model.Articles.PageSize + 1;
                                            foreach (var Items in Model.Articles.ToList())
                                            { 
                                        %>
                                        <tr>
                                            <td style="text-align: center"><%=i%></td>
                                            <td><%=Html.Encode(Items.NameVNI)%></td>
                                            <td><%=Html.Encode(Items.NameENG)%></td>
                                            <td><%=Html.Encode(Items.CreatedDate.ToString("dd/MM/yyyy"))%></td>
                                            <td><%=Html.Encode(Items.CreatedBy)%></td>
                                            <td style="text-align: center">
                                             <%=Items.Active? "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-remove\"></i>"%>
                                            </td>
                                             <td style="text-align: center">
                                                <%=Items.IsEvent? "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-remove\"></i>"%>
                                            </td>
                                            <td style="text-align: center">
                                                <%if (Items.Approved)
                                                  {%>
                                                <i class="fa fa-check"></i>
                                                <%}
                                                  else
                                                  {%>
                                                <a href="/Article/Approve/<%=Items.Id%>">duyệt tin</a>
                                                <%}%>
                                            </td>
                                            <td style="text-align: center">
                                                <a class="glyphicon glyphicon-edit" href="/Article/Editart?id=<%=Items.Id%>" title="Edit"></a>
                                            </td>
                                        </tr>
                                        <% i++;
                                            }%>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </form>
                </div>
                <div class="box-footer">

                    <div class="pager">
                        <div class="page-a">
                            <%=Html.Pager(Model.Articles.PageSize, Model.Articles.PageIndex +1, Model.Articles.TotalItemCount, new
                        {
                            action = "ApproveIndex",
                            controller = "Article",
                            Keyword = Model.Keyword,
                            CatID=  Model.CatID,
                            approved = ViewData["approved"]
                        })%>
                        </div>
                    </div>                    
                </div>
            </div>

        </div>
    </div>    
</asp:Content>
