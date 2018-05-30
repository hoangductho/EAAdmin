<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Teca.Admin.Models.NotificationModels>" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<%@ Import Namespace="Teca.Admin" %>
<li class="dropdown notifications-menu">
    <a href="#" id="notifi-button" class="dropdown-toggle" data-toggle="dropdown">
        <i class="fa fa-bell-o"></i>
        <span class="label label-warning"><%=Model.totalNotification %></span>
    </a>
    <ul class="dropdown-menu">
        <li class="header">Bạn có <span id="notif-count"><%=Model.totalNotification %> </span>thông báo mới </li>

        <li>

            <!-- inner menu: contains the actual data -->
            <ul class="menu">
                <%foreach (var item in Model.Notifications)
                  { %>
                <li>
                    <a href="/Article/Edit?id=<%=item.ArticleId %>">
                        <i class="fa fa-warning text-yellow"></i>
                        <%=item.Description !=null && item.Description.Length > 35 ?string.Format("{0}...",item.Description.Substring(0,30)) : item.Description %>
                    </a>
                </li>
                <%} %>
            </ul>
        </li>
        <li class="footer"><a href="/Notification/Index">Xem hết</a></li>
    </ul>
    <script>
        $(document).ready(function () {
            $('#notifi-button').click(function () {
                var count = $('#notifi-button .label').text();
                if (count > 0) {
                    $.ajax({
                        type: "POST",
                        url: "/Notification/Clear",
                        success: function (msg) {
                            console.log(msg);
                            $('#notifi-button .label').text(0);

                        }, error: function (msg) {

                        }
                    });
                }

            });
        });
    </script>

</li>
