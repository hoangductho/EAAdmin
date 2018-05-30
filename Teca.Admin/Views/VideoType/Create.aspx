<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VideoTypeModels>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Create", "VideoType", FormMethod.Post))
      { %>
    <% Html.RenderPartial("VideoTypeShareElement", Model); %>
        <button type="submit" class="btn btn-primary btn-sm"><i class="fa fa-save"></i>Tạo mới</button>
    <a class="btn btn-default btn-sm" onclick="window.history.back()"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
    <% } %>
</asp:Content>
