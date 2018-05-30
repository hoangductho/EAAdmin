<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<section class="sidebar">

    <ul class="sidebar-menu">
        <li class='treeview treeviewlv1' data-submenu-id="submenu-0">
            <a href="/"><i class="fa fa-home "></i><span>TRANG CHỦ</span>
                <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("TinTuc"))
                  { %>
                <i class="fa fa-angle-left pull-right"></i></a>
            <ul class="treeview-menu treeview-menu1" id="submenu-0">
                <li>
                    <a href="/Home/AddIntruction"><i class="fa fa-circle-o"></i><span>Lịch sử hình thành</span></a>
                </li>
                <li>
                    <a href="/Home/AddIntruction?type=<%=Teca.Core.Domain.IntroductionType.CoCau %>"><i class="fa fa-circle-o"></i><span>Cơ cấu tổ chức</span></a>
                </li>
                <li>
                    <a href="/Home/AddIntruction?type=<%=Teca.Core.Domain.IntroductionType.ThanhTich %>"><i class="fa fa-circle-o"></i><span>Thành tích đạt được</span></a>
                </li>
            </ul>
            <%}
                  else
                  {%>
            </a>
            <%} %>

        </li>
        <li class='treeview treeviewlv1' data-submenu-id="submenu-1">
            <a href="/Articles/index"><i class="fa fa-files-o"></i><span>DANH MỤC</span><i class="fa fa-angle-left pull-right"></i></a>
            <ul class="treeview-menu treeview-menu1" id="submenu-1">
                <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("Menu"))
                  { %>
                <li>
                    <a href="/Menu/index"><i class="fa fa-circle-o"></i><span>Danh mục menu</span></a>
                </li>
                <%} %>
                <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("Banner"))
                  { %>
                <li>
                    <a href="/Banners/index"><i class="fa fa-circle-o"></i><span>Danh mục banner</span></a>
                </li>
                <%} %>
                <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("DanhMucTin"))
                  { %>
                <li>
                    <a href="/Category/index"><i class="fa fa-circle-o"></i><span>Danh mục tin</span></a>
                </li>
                <%} %>
                <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("TinTuc"))
                  { %>
                <li>
                    <a href="/Article/index"><i class="fa fa-circle-o"></i><span>Danh sách tin</span></a>
                </li>
                <li>
                    <a href="/Article/index?isen=true"><i class="fa fa-circle-o"></i><span>Tin tiếng anh</span></a>
                </li>
                <%} %>
               <%-- <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("DuyetTin"))
                  { %>
                <li>
                    <a href="/Article/ApproveIndex"><i class="fa fa-circle-o"></i><span>Duyệt tin</span></a>
                </li>
                <%} %>--%>
            </ul>
        </li>
         <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("LienHe"))
          { %>
        <li>
            <a href="/Contact/Info"><i class="fa-info fa"></i><span>THÔNG TIN LIÊN HỆ</span></a>
        </li>
          <%} %>
        <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("HinhAnh"))
          { %>
        <li>
            <a href="/GalleryType/index"><i class="fa fa-picture-o"></i><span>HÌNH ẢNH</span></a>
        </li>
        <%} %>
        <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("Video"))
          { %>
        <li class='treeview treeviewlv1' data-submenu-id="submenu-4">
            <a href="/Videos/index"><i class="fa fa-video-camera"></i><span>VIDEO</span><i class="fa fa-angle-left pull-right"></i></a>
            <ul class="treeview-menu treeview-menu1" id="submenu-4">
                <li>
                    <a href="/VideoType/index"><i class="fa fa-circle-o"></i><span>Danh mục video</span></a>
                </li>
                <li>
                    <a href="/Videos/index"><i class="fa fa-circle-o"></i><span>Video</span></a>
                </li>
            </ul>
        </li>
        <%} %>
        <%if (HttpContext.Current.User.IsInRole("Root"))
          { %>

        <li class='treeview treeviewlv1' data-submenu-id="submenu-3">
            <a href="/Account/Index"><i class="fa fa-user"></i><span>QUẢN TRỊ HỆ THỐNG</span><i class="fa fa-angle-left pull-right"></i></a>
            <ul class="treeview-menu treeview-menu1" id="submenu-3">
                <li class="treeview">
                    <a tabindex="-1" href="/Account/Index"><i class="fa fa-circle-o"></i><span>Quản lý người dùng</span></a>
                </li>
                <li class="treeview">
                    <a tabindex="-1" href="/IDMManager/index"><i class="fa fa-circle-o"></i><span>Phân quyền</span></a>
                </li>
                <li class="treeview">
                    <a tabindex="-1" href="/LogSystem/index"><i class="fa fa-circle-o"></i><span>Quản lý log</span></a>
                </li>
            </ul>
        </li>
        <%} %>
    </ul>
</section>
<script>
    var $menu = $(".sidebar-menu");

    // jQuery-menu-aim: <meaningful part of the example>
    // Hook up events to be fired on menu row activation.
    $menu.menuAim({
        activate: activateSubmenu,
        deactivate: deactivateSubmenu
    });
    // jQuery-menu-aim: </meaningful part of the example>

    // jQuery-menu-aim: the following JS is used to show and hide the submenu
    // contents. Again, this can be done in any number of ways. jQuery-menu-aim
    // doesn't care how you do this, it just fires the activate and deactivate
    // events at the right times so you know when to show and hide your submenus.

    function activateSubmenu(row) {
        var $row = $(row),
            submenuId = $row.data("submenuId"),
            $submenu = $("#" + submenuId),
            height = $menu.outerHeight(),
            width = $menu.outerWidth();
        // Show the submenu              
        $submenu.addClass("activeSubMenu");
    }

    function deactivateSubmenu(row) {
        var $row = $(row),
            submenuId = $row.data("submenu-id");
        $submenu = $("#" + submenuId);

        $submenu.removeClass("activeSubMenu");
    }

    // Bootstrap's dropdown menus immediately close on document click.
    // Don't let this event close the menu if a submenu is being clicked.
    // This event propagation control doesn't belong in the menu-aim plugin
    // itself because the plugin is agnostic to bootstrap.

    $(document).ready(function () {
        $(".content-wrapper").hover(function () {
            if ($(".treeview-menu1").hasClass("activeSubMenu")) {
                $(".treeview-menu1").addClass("hideSub");
            }
        });
        $(".treeview").hover(function () {
            if ($(this).find(".treeview-menu1").hasClass("hideSub")) {
                $(this).find(".treeview-menu1").removeClass("hideSub");
            }
        });
        $(".treeview-menu1").hover(
        function () {
            $(this).removeClass("hideSub");
            $(this).addClass("activeSubMenu");
            // Called when the mouse enters the element
        },
        function () {
            $(this).removeClass("activeSubMenu");
            $(this).addClass("hideSub");
            // Called when the mouse leaves the element
        }
       );
    });
</script>
