<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Teca.Admin.Models.RoleModel>" %>
<%@ Import Namespace="IdentityManagement.Domain" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    fieldset {
        margin: 20px 0 10px 300px !important;
        width: 400px;
    }
</style>
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<script type="text/javascript">
    // chon tat ca cac checkbox
    function selectAll() {
        // xác dinh xem trang thai checkbox
        if ($('input[name=all]').is(':checked')) {            
            // set selected toi tat ca cac checkbox ten permissions
            $('input[type=checkbox]').attr('checked', true);
        }
        else {            
            $('input[type=checkbox]').attr('checked', false);
        }
    }

</script>
<%=Html.Hidden("roleid", Model.Id)%>
<div>    
    <h4 style="margin-bottom: 10px; text-align: center">QUYỀN TRUY CẬP HỆ THỐNG</h4><hr />    
    <div class="box">
        <div class="box-header">
            <div class="box-title"></div>
        </div>
        <div class="box-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-1">  Tên: <span style="color: red">(*)</span></label>
                    <div class="col-sm-11">
                           <%if (Model.Id > 0)
      {%>
        <label><%=Html.Encode(Model.name) %></label>
        <%}else{ %>
        <%=Html.TextBox("name", Model.name, new { style = "width:300px", @class = "form-control required", title = "Nhập tên mô tả!", maxlength="50"})%>
        <%} %>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-8">Chọn các permission cho role: <span style="color: red">(*)</span></label>
                    <div class="col-sm-4"> <label class="error" style="display:none">Chưa chọn permissions</label></div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">   <div id="box-permission">
        <label>
            <input style="min-height: 0px;" id="all" name="all" value="" type="checkbox" onclick="selectAll();" />
            Chọn hết
        </label>
        <div class="clear"></div>

        <%
            List<permission> lPermissions = new List<permission>();
            lPermissions = (List<permission>)ViewData["Permissions"];

            foreach (var item in lPermissions)
            {
                string lable = item.Description;
                if (string.IsNullOrWhiteSpace(lable)) lable = item.name;
        %>
        <label>
            <input style="min-height: 0px;" type="checkbox" name="permissions" value="<%=Html.Encode(item.name) %>" <%=FX.Utils.Web.UI.GetChecked(Model.Permissions.Contains(item))%> />
            <%=Html.Encode(lable)%>
        </label>

     
        <%}%> 
    </div></div>
                </div>
                 
            </div>
        </div>
    </div>
  
     
    <!--End div box-permission-->
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('form:first').validate();
        $("form").submit(function () {            
            if (checkValid($('#name').val()) < 1) {
                $('#name').val('');
                alert("Quyền truy cập không được chứa ký tự đặc biệt.");
                return false;
            }
            var atLeastOneIsChecked = $('input[name="permissions"]:checked').length > 0;
            if (!atLeastOneIsChecked) {
                $("form .error").css('display', 'block');
                return false;
            }
        });
    });

    function checkValid(text) {
        var dbs = new Array("~", "@", "#", "$", "^", "*", "|", "<", ">", "!", "'", ";");
        var sum = dbs.length;
        var i = 0;
        while (i < sum) {
            for (var k = 0 ; k < text.length; k++) {
                if (dbs[i] == text[k])
                    return 0;
            }
            i++;
        }
        return 1;
    }

</script>