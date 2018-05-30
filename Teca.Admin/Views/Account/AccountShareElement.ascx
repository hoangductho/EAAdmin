<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AccountModels>" %>
<%@ Import Namespace="IdentityManagement.Domain" %>
<%@ Import Namespace="Teca.Admin.Models" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<style type="text/css">fieldset{margin:20px 50px 10px 50px !important}fieldset label{float:left;margin-right:0px;min-width:150px}label.error{padding:0 4px;background-color:#fff;line-height:22px !important;font-weight:normal !important;border:none;border-bottom:1px dashed #f00}input[type=text].error{border:none;border-bottom:1px dashed #f00}input[type=text]:focus{outline:none;border:none;border-bottom:1px dashed #000}input[type=file]{display:none;visibility:hidden}input[readonly=readonly]{cursor:pointer;border:none;border-bottom:1px dashed #ccc}textarea{min-height:55px;background-color:#fee}.textr{text-align:right}.textc{text-align:center}.widget-header{margin-top:3px;text-align:center;padding-bottom:7px}.frm-header{margin:0;padding:0;overflow:hidden}div.line{padding:3px 0;overflow:hidden}div.line > div.control{overflow:hidden;padding-left:7px}div.line > div.control input{width:100%;-moz-box-sizing:border-box;-webkit-box-sizing:border-box;box-sizing:border-box}</style>
<style type="text/css">
    .tt {
        border: 1px solid #ccc;
        height: 120px;        
        margin: 10px 0px 10px 0px;
        padding: 10px 0px 10px 10px;
    }
        .tt input[type=checkbox] {
            vertical-align:middle;
        }       
</style>
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<%=Html.Hidden("userid", Model.tmpUser.userid)%>
<fieldset>
    <legend>Thông tin về tài khoản</legend>
    <ol>
        <li>
            <label for="username">Tên tài khoản<span style="color: red">(*)</span></label>
            <%if (Model.tmpUser.userid > 0)
              {%>
            <%=Html.Label("username", Model.tmpUser.username)%>
            <%}
              else
              { %>
            <%=Html.TextBox("username", Model.tmpUser.username, new { style = "width:250px", @class = "required", title="Tên tài khoản không được để trống", onkeyup="validText(username)", maxlength="50"  })%>
            <%} %>
        </li>
        <li>
            <label for="password">Mật khẩu<span style="color: red">(*)</span></label>
            <%=Html.Password("password", Model.tmpUser.password, new { style = "width:250px", @class = "required",  title="Mật khẩu không được để trống",minlength = "8", maxlength="64" })%>
        </li>
        <li>
            <label for="RetypePassword">Nhập lại mật khẩu<span style="color: red">(*)</span></label>
            <%=Html.Password("RetypePassword", Model.RetypePassword, new { style = "width:250px", @class = "required", minlength = "8", maxlength="64"})%>          
        </li>
        <li>
            <label for="email">Địa chỉ mail<span style="color: red">(*)</span></label>
         
            <%=Html.TextBox("email", Model.tmpUser.email, new { style = "width:250px", @class = "required email", title="Định dạng email không phù hợp", maxlength="128" })%>
         
        </li>
        <li>
            <label for="IsApproved">Kích hoạt</label>
            <%=Html.CheckBox("IsApproved", Model.tmpUser.IsApproved, new {@checked="checked", @onclick = "checkboxa()" })%>
        </li>
        <li>
            <label for="IsLockedOut">Khóa</label>
            <%=Html.CheckBox("IsLockedOut", Model.tmpUser.IsLockedOut, new { @onclick = "checkboxb()" })%>
        </li>

        <li style="border-bottom: none">            
            <div>
                <b>Quyền truy cập</b>
                <table style="width:100%" class="tt">
                    <%
                        int rowcount = Model.AllRoles.Length / 3;
                        int odd = Model.AllRoles.Length % 3;
                        for (int i = 0; i < rowcount; i++)
                        {
                    %>
                    <tr>
                        <td>
                            <input style="margin-left:10px" type="checkbox" name="AssignRoles" value="<%=Html.Encode(Model.AllRoles[i * 3])%>" <%= FX.Utils.Web.UI.GetChecked(Model.UserRoles.Contains(Model.AllRoles[i * 3]))%> />
                            <span><%=Html.Encode(Model.AllRoles[i * 3])%></span>
                        </td>
                        <td>
                            <input style="margin-left:10px" type="checkbox" name="AssignRoles" value="<%=Html.Encode(Model.AllRoles[i * 3 + 1])%>" <%= FX.Utils.Web.UI.GetChecked(Model.UserRoles.Contains(Model.AllRoles[i * 3+1]))%> />
                            <span><%=Html.Encode(Model.AllRoles[i * 3 + 1])%></span>
                        </td>
                        <td>
                            <input style="margin-left:10px" type="checkbox" name="AssignRoles" value="<%=Html.Encode(Model.AllRoles[i * 3 + 2])%>" <%= FX.Utils.Web.UI.GetChecked(Model.UserRoles.Contains(Model.AllRoles[i * 3+2]))%> />
                            <span><%=Html.Encode(Model.AllRoles[i * 3 + 2])%></span>
                        </td>
                    </tr>
                    <%}
                    if (odd != 0)
                    { %>
                    <tr>
                        <td>
                            <input style="margin-left:10px" type="checkbox" name="AssignRoles" value="<%=Model.AllRoles[rowcount * 3]%>" <%= FX.Utils.Web.UI.GetChecked(Model.UserRoles.Contains(Model.AllRoles[rowcount * 3]))%>>
                            <span><%=Html.Encode(Model.AllRoles[rowcount * 3])%></span>
                        </td>
                        <td>
                            <% if (odd > 1)
                               {%>
                            <input style="margin-left:10px" type="checkbox" name="AssignRoles" value="<%=Model.AllRoles[rowcount * 3 +1]%>" <%= FX.Utils.Web.UI.GetChecked(Model.UserRoles.Contains(Model.AllRoles[rowcount * 3 + 1]))%>>
                            <span><%=Html.Encode(Model.AllRoles[rowcount * 3 + 1])%></span>
                            <%} %>
                        </td>
                        <td>
                            <% if (odd > 2)
                               {%>
                            <input style="margin-left:10px" type="checkbox" name="AssignRoles" value="<%=Model.AllRoles[rowcount * 3 +2 ]%>" <%= FX.Utils.Web.UI.GetChecked(Model.UserRoles.Contains(Model.AllRoles[rowcount * 3 + 2]))%>>
                            <span><%= Html.Encode(Model.AllRoles[rowcount * 3 + 2])%></span>
                            <%} %>
                        </td>
                    </tr>
                    <%} %>
                </table>
            </div>
        </li>
    </ol>
</fieldset>
<script type="text/javascript">
    //Check userName AntiInjection
    $(document).ready(function () {
        $('#username').keypress(function (event) {
            var keypressed = null;
            var idName = $(this).attr('id');
            if (window.event) { //IE
                keypressed = window.event.keyCode;
                if (keypressed < 48
                        || (keypressed > 57 && keypressed < 65)
                        || (keypressed > 90 && keypressed < 97
                        || keypressed > 122)) {
                    return false;
                }
            }
            else {
                keypressed = e.which; //NON-IE, Standard
                if (keypressed < 48
                        || (keypressed > 57 && keypressed < 65)
                        || (keypressed > 90 && keypressed < 97
                        || keypressed > 122)) {
                    if (e.charCode == 0) {// không phải kí tự thì vẫn ok           
                        return;
                    }
                    return false;
                }
            }
        });
        $('form:first').validate({
            rules: {
                password: {
                    required: true,
                    minlength: 6
                },
                RetypePassword: {
                    required: true,
                    equalTo: "#password"
                }
            },
            messages: {
                password: {
                    required: "Nhập vào mật khẩu(Password)",
                    minlength: $.format("Mật khẩu phải có ít nhất 8 ký tự")
                },
                RetypePassword: {
                    required: "Nhập đúng mật khẩu của bạn",
                    equalTo: "Nhập đúng mật khẩu của bạn"
                }
            }
        });

        $("form").submit(function () {
            $("input[type='text']").each(function (i) {
                checkTextMaxLength(this);
            });
            if ($('#username').val().indexOf(" ") >= 0 || $('#password').val().indexOf(" ") >= 0 || $('#RetypePassword').val().indexOf(" ") >= 0) {
                alert("Tài khoản hoặc mật khẩu không được chứa khoảng cách.");
                return false;
            }
        });
    });
    
    function checkboxa() {
        var isChecked = $('#IsApproved').is(':checked');
        if (isChecked == true) {
            $('input[name=IsApproved]').attr('checked', true);
            $('input[name=IsLockedOut]').attr('checked', false);
        }
        else {
            $('input[name=IsApproved]').attr('checked', false);
            $('input[name=IsLockedOut]').attr('checked', true);
        }
    }
    function checkboxb() {
        var isChecked = $('#IsLockedOut').is(':checked');
        if (isChecked == true) {
            $('input[name=IsApproved]').attr('checked', false);
            $('input[name=IsLockedOut]').attr('checked', true);
        }
        else {
            $('input[name=IsApproved]').attr('checked', true);
            $('input[name=IsLockedOut]').attr('checked', false);
        }
    }
</script>
