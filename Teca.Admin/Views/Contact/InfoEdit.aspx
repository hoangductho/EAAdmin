<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<Contact>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
       Danh mục thông tin liên hệ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("InfoEdit", "Contact", FormMethod.Post))
      { %>
    <% Html.RenderPartial("InfoShareElement", Model); %>
    <button type="submit" class="btn btn-success btn-sm"><i class="fa fa-save"></i>Sửa đổi</button>
   
    <a class="btn btn-default btn-sm" href="/Category/Info"><i class="fa fa-backward"></i>&nbsp;Quay lại</a>
    <% } %>
</asp:Content>
