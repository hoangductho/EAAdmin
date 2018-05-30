﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IndexAccountModel>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="IdentityManagement.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Người dùng hệ thống
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h4 class="box-title"><i class="fa fa-user"></i>Người dùng hệ thống</h4>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-11">
                            <form id="Searchform" method="post" action="/Account/Index" class="form-inline">
                                Tên tài khoản:
                            <%=Html.TextBox("username", Model.username, new {style="width: 250px; margin-left: 10px; margin-right: 10px", maxlength="50" })%>
                                <button class="btn-sm btn btn-info" type="submit"><i class="glyphicon glyphicon-search"></i>Tìm kiếm</button>
                            </form>
                        </div>
                        <div class="col-xs-1">
                            <a href="/Account/New" class="btn btn-info btn-sm element-right"><i class="fa fa-user"></i>Tạo mới</a>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-12">

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th style="width: 40px">STT
                                        </th>
                                        <th>Tên tài khoản</th>
                                        <th>Địa chỉ mail</th>
                                        <th width="100px">Kích hoạt</th>
                                        <th width="100px">Khóa</th>
                                        <th width="80px">Ngày tạo
                                        </th>
                                        <th width="40px">Sửa</th>
                                        <th style="width: 40px">Xóa</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%         
                                        IList<user> userlst = Model.PageListUser.ToList();
                                        int i = Model.PageListUser.PageIndex * Model.PageListUser.PageSize + 1;
                                        foreach (user ur in userlst)
                                        {
                                    %>
                                    <tr>
                                        <td style="text-align: right"><%=i%></td>
                                        <td><%=Html.Encode(ur.username)%></td>
                                        <td><%=Html.Encode(ur.email)%></td>
                                        <td style="text-align: center">
                                            <img src="<%=ur.IsApproved ? "/Content/Images/valid.png" : "/Content/Images/cross.gif" %>" title="<%=ur.IsApproved ? "Sử dụng" :"Khóa"%>" />
                                        </td>
                                        <td style="text-align: center">
                                            <img src="<%=ur.IsLockedOut ? "/Content/Images/valid.png" : "/Content/Images/cross.gif" %>" title="<%=!ur.IsLockedOut ? "Sử dụng" :"Khóa"%>" /></td>
                                        <td><%=ur.CreateDate.ToString("dd/MM/yyyy")%></td>
                                        <td style="text-align: center"><a href="/Account/Edit/<%=ur.userid%>">
                                            <i class="fa fa-edit"></i></a></td>
                                        <td style="text-align: center"><a href="#" onclick="OnDelete('<%=ur.userid%>')" title="Delete">
                                            <i class="fa fa-remove"></i></a></td>
                                    </tr>
                                    <%i++;
                                        }
                                    %>
                                </tbody>

                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-footer">

                    <div class="pager">
                        <div class="page-a">
                            <%=Html.Pager(Model.PageListUser.PageSize, Model.PageListUser.PageIndex + 1, Model.PageListUser.TotalItemCount,new
                {
                   action = "index",
                   controller = "Account",
                   username = Model.username                                     
               })%>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function OnDelete(id) {
            alertify.confirm("Xóa người dùng?", function (e) {
                if (e) {
                    document.location = "/Account/delete?id=" + id;
                }
                else return;
            });
        }
    </script>
</asp:Content>
