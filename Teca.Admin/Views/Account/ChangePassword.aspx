<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ChangePasswordModel>" %>

<%@ Import Namespace="Teca.Admin.Models" %>
<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
    <style type="text/css">fieldset{border-top:none;border:1px solid #DDD;border-radius:5px 5px 5px 5px;box-shadow:0px 0px 8px #EEE;width:530px;height:250px;margin:0 auto;margin-top:20px;padding:25px 0px 0px 35px}li{border-bottom:none !important}#strengthPass{margin-left:5px}.short{color:#FF0000}.weak{color:#E66C2C}.good{color:#2D98F3}.strong{color:#66FF33}</style>
    <% using (Html.BeginForm("ChangePassword", "Account", FormMethod.Post))
       { %>
    <%=Html.Hidden("UserName", Model.UserName)%>
    <h4 style="margin-top:10px; text-align:center">ĐỔI MẬT KHẨU</h4>
    <fieldset>
        <ul>
            <li>
                <label style="min-width: 130px">Mật khẩu cũ<span style="color: red">(*)</span></label>
                <%= Html.Password("oldPassword","", new { style = "width:200px;margin-left:-14px", @class = "required" })%>                    
            </li>

            <li>
                <label style="min-width: 130px">Mật khẩu mới<span style="color: red">(*)</span></label>
                <%= Html.Password("NewPassword","", new { style = "width:200px;margin-left:-14px", @class = "required"})%>
                <b><span id="strengthPass"></span></b>
            </li>

            <li>
                <label style="min-width: 130px">Nhập lại mật khẩu<span style="color: red">(*)</span></label>
                <%= Html.Password("RetypePassword","", new { style = "width:200px;margin-left:-14px", @class = "required" })%>
            </li>
            <li>  
                <div class="text-center">                            
                    <button class="btn btn-sm" type="submit"><i class="fa fa-check"></i> Lưu</button>
                    <button class="btn btn-sm" type="reset"><i class="fa fa-refresh"></i> Làm lại</button> 
                </div>    
            </li>
            <li>
                <p style="margin: 10px 5px 0px 0px;font-size:13.5px">Chú ý: Mật khẩu mạnh phải lớn hơn 8 ký tự, có chứa chữ hoa, chữ thường, số và ký tự đặc biệt.</p>
            </li>
        </ul>
    </fieldset>
    
    <% }%>
    <script type="text/javascript">        
        $(document).ready(function () {
            $('#oldPassword').val("");
            $('#NewPassword').val("");
            $('#RetypePassword').val("");
            $('form:first').validate({
                rules: {
                    oldPassword: {
                        required: true
                    },
                    NewPassword: {
                        required: true,
                        minlength: 8
                    },
                    RetypePassword: {
                        required: true,
                        minlength: 8,
                        equalTo: "#NewPassword"
                    }
                },
                messages: {
                    oldPassword: {
                        required: "Nhập mật khẩu cũ của bạn"
                    },
                    NewPassword: {
                        required: "Nhập mật khẩu mới",
                        minlength: $.format("Mật khẩu phải có ít nhất 8 ký tự")
                    },
                    RetypePassword: {
                        required: "Nhập đúng mật khẩu của bạn",
                        equalTo: "Nhập đúng mật khẩu của bạn"
                    }
                }
            });
            $('#NewPassword').keyup(function () {
                if (checkStrengthPass($('#NewPassword').val()) == 0)
                    $('#strengthPass').html('Mật khẩu phải từ 8 ký tự.');
                if (checkStrengthPass($('#NewPassword').val()) == 1)
                    $('#strengthPass').html("Mật khẩu yếu.");
                if (checkStrengthPass($('#NewPassword').val()) == 2)
                    $('#strengthPass').html("Mật khẩu trung bình.");
                if (checkStrengthPass($('#NewPassword').val()) == 3)
                    $('#strengthPass').html("Mật khẩu tốt.");
                if (checkStrengthPass($('#NewPassword').val()) == 4)
                    $('#strengthPass').html("Mật khẩu mạnh.");
            });
            $("form").submit(function () {
                if (!$('form').valid()) {
                    $('#oldPassword').val("");
                    $("#NewPassword").val("");
                    $("#RetypePassword").val("");
                    $('#strengthPass').html("");
                    return false;
                }
                if ($('#NewPassword').val().indexOf(" ") >= 0 || $('#RetypePassword').val().indexOf(" ") >= 0) {
                    alertify.alert("Mật khẩu không được chứa khoảng cách.");
                    return false;
                }
                if (checkStrengthPass($('#NewPassword').val()) < 4) {
                    $('#oldPassword').val("");
                    $("#RetypePassword").val("");
                    alertify.alert("Thông báo: mật khẩu không đủ mạnh!\n\nChú ý: Mật khẩu mạnh phải lớn hơn 8 ký tự, có chứa chữ hoa, chữ thường, số và ký tự đặc biệt.");
                    return false;
                }
            });
        });
        function checkStrengthPass(password) {
            var strength = 0;
            if (password.length < 8) {
                return 0;
            }
            if (password.length > 7) {
                if (password.match(/(?=.*[A-Z])/) != null && password.match(/(?=.*[A-Z])/))
                    strength += 1;
                if (password.match(/([a-z])/) != null && password.match(/([a-z])/))
                    strength += 1;

                if (password.match(/([0-9])/) != null && password.match(/([0-9])/))
                    strength += 1;

                if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/) != null && password.match(/([!,%,&,@,#,$,^,*,?,_,~])/))
                    strength += 1;

                return strength;
            }
        }
    </script>
</asp:Content>
