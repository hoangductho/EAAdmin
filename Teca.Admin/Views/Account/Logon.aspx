<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Teca.Admin.Models.LogOnModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="ctl00_Head1">
    <title>Đăng nhập hệ thống</title>
    <link rel="Shortcut Icon" href="/Content/favicon.ico" />
    <link rel="stylesheet" href="/Content/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="/Content/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="/Content/dist/css/skins/_all-skins.min.css" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <style>
        #main {
            width: 420px;
            clear: left;
            margin: 100px auto;
        }

        #main-nav {
            width: 100%;
            padding: 12px 10% 15px;
            border-bottom: 1px solid #ccc;
        }

        .box.box-primary {
            box-shadow: 0 1px 1px #3c8dbc;
        }
    </style>
</head>
<body>
    <nav id="main-nav">
        <p>
        <img src="/Content/images/logo.png" class="minds-com">        
        <label style="margin-left:20px;font-size:x-large;font-weight:700;color:#4c79f3" >TRUNG TÂM GÌN GIỮ HÒA BÌNH VIỆT NAM</label>
            </p>
    </nav>
    <div class="row" id="">
        <div id="main">
            <div class="box box-primary">
                <form method="post" action="/Account/logon" class=" form-horizontal">
                    <div class="box-body">
                        <div class="form-group">
                            <%=Html.AntiForgeryToken() %>                            
                            <div class="col-sm-12">
                                <div id="lblErrorMessage"><%=Html.Encode(Model.lblErrorMessage) %></div>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-user"></i>
                                    </div>
                                    <input type="text" value="" name="username" id="username" maxlength="50" class="form-control" placeholder="Tài khoản" autocomplete="off"/>
                                </div>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-lock"></i>
                                    </div>
                                    <input type="password" value="" name="password" id="password" maxlength="50" class="form-control" placeholder="Mật khẩu" autocomplete="off"/>
                                </div>
                            </div>
                        </div>

                        <%Html.RenderPartial("Captcha", new Teca.Admin.Models.Captcha()); %>
                    </div>
                    <div class="box-footer">
                        <input type="submit" value="Đăng nhập" class="btn btn-sm btn-primary"/>
                        <a href="/Account/ResetPassword" class="pull-right">Quên mật khẩu</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function checkTextMaxLength(textBox) {
            var mLen = textBox.getAttribute("maxlength");
            var maxLength = parseInt(mLen);

            if (textBox.value.length > maxLength - 1) {
                textBox.value = textBox.value.substring(0, maxLength);
            }
        }
        $(document).ready(function () {
            var error = $('#lblErrorMessage').text();
            if (error.length > 0) {
                $('#lblErrorMessage').addClass('alert alert-danger');
            }
            $('input[type=text]:first').focus();
            $("input[type='text']").blur(function () {
                checkTextMaxLength(this);
            });
            $("input[type='password']").blur(function () {
                checkTextMaxLength(this);
            });
            $('#captch').val('');
            $("form").submit(function () {
                if (!$('#username').val() || !$('#password').val())
                    return false;
            });
        });
    </script>
</body>
</html>
