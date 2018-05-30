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
                            <form id="Searchform" method="post" action="/Article/Index?isEn=true" class="form-inline">
                                <label>Chuyên mục:</label>
                                <%=Html.DropDownList("CatID",new SelectList(Model.Categories, "Id","NameENG",Model.CatID), "---Chuyên mục tin---") %>
                                <label>Tiêu đề tin:</label>                                
                                <%=Html.TextBox("Keyword", Model.Keyword, new {style="width: 250px; margin-left: 10px; margin-right: 10px", maxlength="50" })%>
                                <button class="btn-sm btn btn-info" type="submit"><i class="glyphicon glyphicon-search"></i>Tìm kiếm</button>
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
                                        <th>Tiêu đề tiếng việt
                                        </th>
                                        <th>Tiêu đề
                                        </th>                                        
                                        <th width="120px">Ngày tạo
                                        </th>                                        
                                        <th style="width: 90px">Hiển thị
                                        </th>    
                                        <th style="width: 90px">Sự kiện
                                        </th>                                        
                                        <th style="width: 80px">Biên tập
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
                                        <td style="text-align: center">
                                            <%=Items.Active?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>"%>
                                        </td>  
                                            <td style="text-align: center">
                                            <%=Items.IsHot?  "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-remove\"></i>"%>
                                        </td>                                            
                                        <td style="text-align: center">
                                            <a class="fa fa-edit" href="/Article/Edit?id=<%=Items.Id%>&isEn=true" title="Edit"></a>
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
                            CatID=  Model.CatID,
                            isEn = true
                        })%>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>    
</asp:Content>
