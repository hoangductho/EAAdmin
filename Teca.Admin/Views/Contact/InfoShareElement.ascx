<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Contact>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-user"></i>
                Thông tin liên hệ
            </div>
            <div class="pull-right">
                <a href="/Contact/Info" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <%=Html.Hidden("Id",Model.Id)%>
            <fieldset>
                <ol>
                    <li>
                        <label class="control-label mt-10">Tên <span style="color: red">(*)</span></label>
                        <%=Html.TextBox("Name", Model.Name, new { @class = "required checklength", maxlength="100", title = " Chưa nhập tên ", style="width:600px" })%>            
                    </li>
                    <li>
                        <label class="control-label mt-10">Địa chỉ<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("Address", Model.Address, new { @class = "required checklength", maxlength="400", title = " Chưa nhập địa chỉ", style="width:600px" })%>
                    </li>
                     <li>
                        <label class="control-label mt-10">Email<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("Email", Model.Email, new { @class = "required email checklength", maxlength="400", title = " Chưa nhập địa chỉ email", style="width:600px" })%>
                    </li>
                     <li>
                        <label class="control-label mt-10">Số điện thoại<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("Phone", Model.Phone, new { @class = "required checklength", maxlength="400", title = " Chưa nhập số điện thoại", style="width:600px" })%>
                    </li>
                      <li>
                        <label class="control-label mt-10">Hiển thị: </label>
                        <%=Html.CheckBox("Active", Model.Active )%>
                    </li>
                </ol>
            </fieldset>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('form:first').validate();
    });
</script>
