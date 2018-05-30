<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<Contact>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh mục thông tin liên hệ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("InfoCreate", "Contact", FormMethod.Post))
      { %>
    <% Html.RenderPartial("InfoShareElement", Model); %>
    <div class="textc">
        <button class="btn btn-sm btn-success" type="submit"><i class="fa fa-save"></i>Tạo mới</button>
        <button class="btn btn-sm" type="button" onclick="document.location = '/Contact/Info'">
            <i class="fa fa-backward"></i> Quay lại</button>
    </div>    
    <% } %>
</asp:Content>
