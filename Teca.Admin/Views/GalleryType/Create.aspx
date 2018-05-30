﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<GalleryType>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Create", "GalleryType", FormMethod.Post))
      { %>
    <% Html.RenderPartial("GalleryTypeShareElement", Model); %>
    <button class="btn btn-sm btn-success" type="button" onclick="PrepareSubmit();"><i class="fa fa-check"></i> Tạo mới</button>
    <a class="btn btn-default btn-sm" href="/GalleryType/Index"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
    <% } %>
</asp:Content>