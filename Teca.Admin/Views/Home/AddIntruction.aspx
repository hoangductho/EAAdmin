<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<Introduction>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Giới thiệu
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%using (Html.BeginForm("SaveIntruction", "Home", FormMethod.Post))
      { %>
    <% Html.RenderPartial("Introduction", Model); %>

    <div class="textc">
        <button class="btn btn-sm btn-success" type="submit"><i class="fa fa-check"></i>Lưu dữ liệu</button>
        <button class="btn btn-sm" type="button" onclick="document.location = '/'">
            <i class="fa fa-backward"></i> Quay lại</button>
    </div>      
    <% } %>

</asp:Content>
