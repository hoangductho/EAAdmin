<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<CategoryModels>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh mục tin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Update", "Category", FormMethod.Post))
      { %>
    <% Html.RenderPartial("CategoryShareElement", Model); %>
    <button type="submit" class="btn btn-success btn-sm"><i class="fa fa-save"></i>Sửa đổi</button>
   
    <a class="btn btn-default btn-sm" href="/Category/Index"><i class="fa fa-backward"></i>&nbsp;Quay lại</a>
    <% } %>
</asp:Content>
