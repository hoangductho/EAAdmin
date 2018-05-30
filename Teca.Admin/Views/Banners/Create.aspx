<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Banners>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Create", "Banners", FormMethod.Post))
      { %>
    <% Html.RenderPartial("BannerShareElement", Model); %>
    <button class="btn btn-sm btn-success" type="submit"><i class="fa fa-check"></i> Tạo mới</button>
    <a class="btn btn-default btn-sm" onclick="window.history.back()"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
    <% } %>
</asp:Content>
