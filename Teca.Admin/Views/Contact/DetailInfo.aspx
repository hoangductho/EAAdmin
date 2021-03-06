﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<Contact>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Chi tiết thông tin liên hệ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
               Thông tin liên hệ
            </div>
            <div class="pull-right">
                <a href="/Contact/Info" class="btn btn-default btn-sm mr-15"><i class="fa fa-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <fieldset>
                <%=Html.Hidden("id", Model.Id) %>
                <ol>
                    <li>
                        <label class="control-label mt-10">Tên :</label>
                        <span><%=Model.Name %></span>            
                    </li>
                     <li>
                        <label class="control-label mt-10">Email :</label>
                        <span><%=Model.Email %></span>            
                    </li>
                    <li>
                        <label class="control-label mt-10">Địa chỉ :</label>
                        <span><%=Model.Address %></span>            
                    </li>
                     <li>
                        <label class="control-label mt-10">Số điện thoại :</label>
                        <span><%=Model.Phone %></span>            
                    </li>
                    <li>
                        <label class="control-label mt-10">Kích hoạt :</label>
                        <span><% if(Model.Active){ %> Đang hiển thị <% }else{ %> Đã khóa <%} %></span>            
                    </li>
                       <li>
                        <label class="control-label mt-10">Ngày tạo :</label>
                        <span><%=Model.CreatedDate.ToString("dd/MM/yyyy") %></span>            
                    </li>
                </ol>
            </fieldset>
        </div>
    </div>
</div>
</asp:Content>
