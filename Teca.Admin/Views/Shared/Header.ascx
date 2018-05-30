<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<nav class="navbar navbar-static-top" role="navigation">
    <div class="navbar-header">
        <a href="/" class="logo">
            <img src="/Content/images/logo.png" alt="Alternate Text" /></a>
        <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
            <span class="sr-only">Toggle navigation</span>
        </a>
        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-collapse">
            <i class="fa fa-bars"></i>
        </button>
    </div>

    <div class="collapse navbar-collapse pull-left" id="navbar-collapse">
        <ul class="nav navbar-nav">
            <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("DanhMucTin"))
              { %>
            <li class="dropdown">
                <a href="/Category/index">DANH MỤC TIN</a>
            </li>
            <%} %>
            <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("TinTuc"))
              { %>
            <li class="dropdown menulv-1">
                <a href="/Article/index">DANH SÁCH TIN<span class="caret"></span></a>
                <ul class="dropdown-menu menulv-2" role="menu">
                    <li><a href="/Article/index?isEn=true"><i class="fa fa-asterisk"></i>TIN TIẾNG ANH</a></li>
                </ul>
            </li>
            <%} %>
           <%-- <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("DuyetTin"))
              { %>
            <li class="dropdown menulv-1">
                <a href="/Article/ApproveIndex">DUYỆT TIN</a>
            </li>
            <%} %>--%>
            <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("HinhAnh"))
              { %>
            <li class="dropdown">
                <a href="/GalleryType/index">HÌNH ẢNH</a>
            </li>
            <%} %>
            <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("Video"))
              { %>
            <li class="dropdown menulv-1">
                <a href="/Videos/index">VIDEO<span class="caret"></span></a>
                <ul class="dropdown-menu menulv-2" role="menu">
                    <li><a href="/VideoType/index"><i class="fa fa-asterisk"></i>Danh mục video</a></li>

                </ul>
            </li>
            <%} %>
            
            <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("LienHe"))
              { %>
             <li class="dropdown"><a href="/Contact/index">LIÊN HỆ</a></li>
            <%} %>
           <%-- <%if (((IdentityManagement.Authorization.IFanxiPrincipal)HttpContext.Current.User).IsInPermission("Document"))
              { %>
             <li class="dropdown"><a href="/Document/index">VĂN BẢN PHÁP QUY</a></li>
            <%} %>--%>
            <%if (HttpContext.Current.User.IsInRole("Root"))
              { %>
            <li class="dropdown menulv-1">
                <a href="/Account/Index">HỆ THỐNG<span class="caret"></span></a>
                <ul class="dropdown-menu menulv-2" role="menu">
                    <li><a href="/IDMManager/index"><i class="fa fa-asterisk"></i>Phân quyền</a></li>
                  
                </ul>
            </li>
            <%} %>
        </ul>
    </div>
    <div class="navbar-custom-menu">
        <ul class="nav navbar-nav">
            <!-- User Account: style can be found in dropdown.less -->
            <%Html.RenderAction("GetNotification", "Notification"); %>
            <li class="dropdown user user-menu">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                    <span><%=HttpContext.Current.User.Identity.Name%></span>
                </a>
                <ul class="dropdown-menu">
                    <!-- Menu Footer-->
                    <li class="user-footer">
                        <div class="pull-left">
                            <a href="/Account/Changepassword" class="btn btn-default btn-flat">Đổi mật khẩu</a>
                        </div>
                        <div class="pull-right">
                            <a href="/Account/Logout" class="btn btn-default btn-flat">Đăng xuất</a>
                        </div>
                    </li>
                </ul>
            </li>
        </ul>
    </div>
</nav>
