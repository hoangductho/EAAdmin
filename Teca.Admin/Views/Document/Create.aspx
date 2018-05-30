<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teca.Core.Domain.Document>" %>

<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Văn bản pháp quy
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%using (Html.BeginForm("Create", "Document", FormMethod.Post, new { enctype = "multipart/form-data" }))
  { %>
    <% Html.RenderPartial("DocumentShareElement", Model); %>

    <div class="textc">
        <button class="btn btn-sm btn-success" type="submit"><i class="fa fa-check"></i>Tạo mới</button>
        <button class="btn btn-sm" type="button" onclick="document.location = '/Document/index'">
            <i class="fa fa-backward"></i> Quay lại</button>
    </div>      
    <% } %>

</asp:Content>
