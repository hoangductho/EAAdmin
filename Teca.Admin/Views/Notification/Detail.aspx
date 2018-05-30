<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<ArticleModels>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Update", "Article", FormMethod.Post))
      {
    %>
    <% Html.RenderPartial("/Article/ArticleShareElements", Model); %>

    <div class="textc">
        <button class="btn btn-sm btn-success" type="submit"><i class="fa fa-check"></i>Sửa đổi</button>
        <button class="btn btn-sm" type="button" onclick="document.location = '/Article/index'">
            <i class="fa fa-backward"></i> Quay lại</button>
    </div>    
    <% } %>
</asp:Content>
