﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teca.Admin.Models.RoleModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Phân quyền người dùng
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("Edit", "IDMManager", FormMethod.Post))
       { %>
    <% Html.RenderPartial("IDMShareElement", Model); %>
    <div class="text-center">
    <button class="btn btn-sm" type="submit"><i class="fa fa-check"></i> Lưu dữ liệu</button>
    <button class="btn btn-sm" type="button" onclick="document.location = '/IDMManager/index'">
        <i class="fa fa-backward"></i> Quay lại</button>
        </div>
    <% } %>
</asp:Content>
