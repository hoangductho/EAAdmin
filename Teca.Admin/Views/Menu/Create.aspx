<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teca.Core.Domain.Menu>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tạo mới menu    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%using (Html.BeginForm("Create", "Menu", FormMethod.Post))
      { %>
    <% Html.RenderPartial("MenuShareElement", Model); %>
 
     <button type="submit" class="btn btn-primary btn-sm"><i class="fa fa-save"></i>Tạo mới</button>
       <a class="btn btn-default btn-sm" href="/Menu/Index"><i class="fa fa-backward"></i>&nbsp;Quay lại</a>
    <% } %>

</asp:Content>
