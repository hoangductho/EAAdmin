<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LogIndexModel>" %>
<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh sách log
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .datepicker-dropdown.datepicker-orient-bottom:before{
            display:none;
        }
        .datepicker{
            top:180px;
        }
    </style>
<div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                        <h4 class="box-title"><i class="fa fa-user"></i>
                        Quản lý log
                    </h4>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-11">
                           <form id="Searchform" method="post" action="/LogSystem/Index" class="form-inline">
                            <label>Tìm kiếm nhanh:</label>
                            <input id="keyword" name="keyword" type="text" style="width: 150px; margin-left: 10px; margin-right: 10px" value="<%=Model.keyword%>" />
                            <label>Từ ngày:</label> <%=Html.TextBox("fromdate",Model.fromdate, new { style = "width:100px;margin-right:3px;", @class = "datepicker " })%>            
                           <label> Đến ngày:</label> <%=Html.TextBox("todate",Model.todate, new { style = "width:100px;margin-right:3px;", @class = "datepicker " })%>                
                             
                              <button id="Submit1" class="btn btn-primary btn-sm" type="submit"><i class="fa fa-search"></i>Tìm Kiếm</button>
                        </form>
                        </div>
                       
                    </div>

                    <div class="row">
                        <div class="col-xs-12">

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                      <th width="20px">STT
                                        </th>
                                        <th width="120px">Tên quản trị
                                        </th>
                                        <th>Sự kiện
                                        </th> 
                                        <th width="150px">Địa chỉ IP
                                        </th> 
                                        <th width="120px">Trạng thái
                                        </th>                        
                                        <th width="100px">Trình duyệt
                                        </th>   
                                        <th>Ghi chú</th>                    
                                        <th width="120px">Ngày tạo</th>
                                           
                                    </tr>
                                </thead>
                                  <tbody>
                                        <%    
                                            var list = Model.LogData.ToList();
                                            int i = Model.LogData.PageIndex * Model.LogData.PageSize + 1;
                                            foreach (var nt in list)
                                            {                    
                                        %>
                                        <tr>
                                            <td align="center"><%=i%></td>
                                            <td><%=nt.User.username%></td>
                                            <td><%=nt.Event%></td> 
                                            <td><%=nt.IpAddress%></td>
                                            <td>
                                                <%=Teca.Core.Utils.GetDescription(nt.Type)%>
                                            </td>              
                                            <td><%=nt.Browser%></td>  
                                            <td><%=nt.Comment%></td>                           
                                            <td><%=nt.CreateDate.ToString("dd/MM/yyyy")%></td>           
                                                                     
                                        </tr>
                                        <% i++;
                                            }%>
                                    </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-footer">

                    <div class="pager">
                        <div class="page-a">
                                <%=Html.Pager(Model.LogData.PageSize, Model.LogData.PageIndex + 1, Model.LogData.TotalItemCount, new
                                    {
                                        action = "index",
                                        controller = "LogSystem",
                                        fromdate = Model.fromdate,
                                        todate = Model.todate
                                    })%>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script>
        $(document).ready(function () {
            $(".datepicker").datepicker({
                showOn: "button",
                format: "dd/mm/yyyy",
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        });
    </script>
 
</asp:Content>
