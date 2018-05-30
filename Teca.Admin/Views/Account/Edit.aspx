<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AccountModels>" %>
<%@ Import Namespace="IdentityManagement.Domain" %>
<%@ Import Namespace="Teca.Admin.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Người dùng hệ thống
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% using (Html.BeginForm("Update", "Account", FormMethod.Post))
      { %>
      <% Html.RenderPartial("AccountShareElement", Model); %>
        <div class="textc">
        <button class="btn btn-sm btn-success" type="submit"><i class="fa fa-check"></i> Lưu dữ liệu</button>
        <button class="btn btn-sm" type="button" onclick="document.location = '/Account/index'">
            <i class="fa fa-backward"></i> Quay lại</button>
    </div>
   <% } %>

</asp:Content>
