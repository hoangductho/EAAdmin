<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<VideoIndexModel>" %>
<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="FX.Utils.MvcPaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh sách video
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="row">
        <div class="col-xs-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                        <h4 class="box-title"><i class="fa fa-user"></i>
                        Quản lý video
                    </h4>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-11">
                            <form id="Searchform" name="Searchform" method="post" action="/Videos/Index" class="form-inline">
                                <label>Chuyên mục:</label>                                
                                <%=Html.DropDownList("TypeID", new SelectList(Model.VideoTypes,"Id", "NameVNI", Model.TypeID),"---Chuyên mục---", new {style="width: 250px; margin-left: 10px; margin-right: 10px", maxlength="50", onchange="document.Searchform.submit()" })%>                                
                            </form>
                        </div>
                        <div class="col-xs-1">
                            <a href="/Videos/Create/" class="btn btn-info btn-sm element-right"><i class="fa fa-plus"></i>Tạo mới</a>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-12">

                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th width="30px">Stt
                                        </th>
                                        <th>Tiêu đề tiếng việt
                                        </th>
                                        <th>Tiêu đề tiếng anh
                                        </th>
                                        <th width="120px">Ngày tạo
                                        </th>
                                        <th style="width: 120px">Người tạo
                                        </th>
                                        <th style="width: 90px">Hiển thị
                                        </th>                                        
                                        <th width="50px">Sửa
                                        </th>
                                        <th width="50px">Xóa
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%
                                        int i = Model.Videos.PageIndex * Model.Videos.PageSize + 1;
                                        foreach (var Items in Model.Videos.ToList())
                                        { 
                                    %>
                                    <tr>
                                        <td style="text-align: center"><%=i%></td>
                                        <td><%=Html.Encode(Items.NameVNI)%></td>
                                        <td><%=Html.Encode(Items.NameENG)%></td>
                                        <td><%=Html.Encode(Items.CreatedDate.ToString("dd/MM/yyyy"))%></td>
                                        <td><%=Html.Encode(Items.CreatedBy)%></td>
                                        <td>
                                            <%=Items.Active && Items.VideoTypes.Active ? "<i class=\"fa fa-check\"></i>":"<i class=\"fa fa-eye-slash\"></i>"%>
                                        </td>                                        
                                        <td style="text-align: center">
                                            <a class="fa fa-edit" href="/Videos/Edit?id=<%=Items.Id%>" title="Edit"></a>
                                        </td>
                                        <td style="text-align: center">
                                            <a class="fa fa-remove" href="#" onclick="OnDelete('<%=Items.Id %>')" title="Delete"></a>
                                        </td>
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
                            <%=Html.Pager(Model.Videos.PageSize, Model.Videos.PageIndex +1, Model.Videos.TotalItemCount, new
                        {
                            action = "index",
                            controller = "Videos",
                            Keyword = Model.Keyword
                        })%>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function OnDelete(id) {
            if (!confirm("Bạn có muốn xóa video này không ?"))
                return;
            document.location = "/Videos/Delete?id=" + id;
        }
    </script>

</asp:Content>
