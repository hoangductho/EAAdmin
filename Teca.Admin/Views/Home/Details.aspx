<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<Introduction>" %>

<%@ Import Namespace="Teca.Core.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%if (Model.Type == IntroductionType.LichSu)
      {%>
                Lịch sử hình thành
                <%} %>
    <%if (Model.Type == IntroductionType.CoCau)
      {%>
                Cơ cấu tổ chức
                <%} %>
    <%if (Model.Type == IntroductionType.ThanhTich)
      {%>
                Thành tích đạt được
                <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-file-word-o"></i>
                <%if (Model.Type == IntroductionType.LichSu)
                  {%>
                Lịch sử hình thành
                <%} %>
                <%if (Model.Type == IntroductionType.CoCau)
                  {%>
                Cơ cấu tổ chức
                <%} %>
                <%if (Model.Type == IntroductionType.ThanhTich)
                  {%>
                Thành tích đạt được
                <%} %>
            </div>
            <div class="pull-right">
                <a href="/" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <%=Html.Hidden("Id",Model.Id) %>
            <fieldset>
                <ol>
                    <li>
                        <label>Tiêu đề tiếng việt: </label>
                        <%=Model.Title%>
                    </li>
                    <li>
                        <label class="control-label mt-10">Tiêu đề tiếng anh: </label>
                        <%=Model.TitleENG %>
                    </li>
                    <li>
                        <label class="control-label mt-10">Nội dung: </label>
                    </li>
                    <li style="text-align:left">
                        <%=Model.Description%>            
                    </li>
                </ol>
            </fieldset>
        </div>
    </div>
</asp:Content>
