<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Teca.Core.Domain.Document>" %>
<%@ Import Namespace="FX.Utils" %>
<%@ Import Namespace="Teca.Core.Domain" %>
<link href="/Content/css/SharedFormElements.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/js/jquery.validate.min.js"></script>
<style type="text/css">
        input[type=file] {
            display: none;
            visibility: hidden;
        }

        input[type=text] {
            resize: none;
            padding: 0px 5px;
            border: none;
            border-bottom: 1px dashed #ccc;
            height: 22px;
            background-color: #fee !important;
        }
    </style>
<div class="col-xs-12">
    <div class="box box-danger">
        <div class="box-header with-border">
            <div class="panel-title pull-left" style="line-height: 30px">
                <i class="fa fa-file"></i>
                Văn bản pháp quy
            </div>
            <div class="pull-right">
                <a href="/Document/Index" class="btn btn-default btn-sm mr-15"><i class="glyphicon glyphicon-backward"></i>&nbsp;Quay lại</a>
            </div>
        </div>
        <div class="box-body">
            <%=Html.Hidden("Id",Model.Id)%>
            <fieldset>
                <ol>
                     <li>
                        <label class="control-label mt-10">Mã số/Ký hiệu:<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("Serial", Model.Serial, new { @class = "required checklength", maxlength="50", title = " Chưa nhập mã số/ký hiệu của văn bản", style="width:600px" })%>            
                    </li>
                    <li>
                        <label class="control-label mt-10">Tên tiếng việt<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("Name", Model.Name, new { @class = "required checklength", maxlength="240", title = " Chưa nhập tên văn bản", style="width:600px" })%>            
                    </li>
                    <li>
                        <label class="control-label mt-10">Tên tiếng anh<span style="color: red">(*)</span></label>
                        <%=Html.TextBox("NameENG", Model.NameENG, new { @class = "required checklength", maxlength="240", title = " Chưa nhập tên văn bản tiếng Anh", style="width:600px" })%>
                    </li>
                    <li>
                        <label class="control-label mt-10">Văn bản pháp quy</label>                        
                        <div class="input-group"> 
                            <input type="file" name="dataFile" id="dataFile" accept=".pdf,.PDF">
                            <input type="text" id="txtFilePath" readonly="readonly" placeholder="Nhấn chuột vào đây để đính kèm file" <%if (Model != null && !String.IsNullOrEmpty(Model.Name)){ %> value="<%=FX.Utils.Common.TextHelper.ToUrlFriendly(Model.Name) + ".pdf" %>" <%} %> style="width:600px" />                                                                                   
                        </div>
                    </li>
                     <li>
                        <label class="control-label mt-10">Cơ quan ban hành</label>                        
                        <div class="input-group"> 
                            <%=Html.TextBox("PublishBy", Model.PublishBy, new { @class = "checklength", maxlength="250", title = " Chưa nhập tên cơ quan ban hành", style="width:600px" })%>  
                        </div>
                    </li>
                     <li>
                        <label class="control-label mt-10">Người ký quyết định </label>                        
                        <div class="input-group"> 
                             <%=Html.TextBox("SignedBy", Model.SignedBy, new { @class = "checklength", maxlength="100", title = " Chưa nhập tên người ký quyết định", style="width:600px" })%>                                  
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">Ngày ban hành </label>                        
                        <div class="input-group"> 
                           
                            <input type="text" name="PublishDate" id="PublishDate" class="datepicker" value="<%=Model.PublishDate.ToString("dd/MM/yyyy")%>" placeholder ="__/__/____" />
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">Ngày bắt đầu có hiệu lực </label>                        
                        <div class="input-group"> 
                            
                               <input type="text" name="StartDate" id="StartDate" class="datepicker" value="<%=Model.StartDate.ToString("dd/MM/yyyy")%>" placeholder ="__/__/____" />
                        </div>
                    </li>
                    <li>
                        <label class="control-label mt-10">Mô tả</label>
                        <div class="input-group col-md-12">
                            <%=Html.TextArea("Description", Model.Description, new {cols="50", rows="2", @class = "checklength form-control", maxlength="255" })%>
                        </div>
                    </li>


                    <li>
                        <div class="checkbox">
                            <label class="mt-10"><%=Html.CheckBox("Active", Model.Active)%>Hiển thị</label>
                        </div>
                    </li>
                </ol>
            </fieldset>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {

     
            $(".datepicker").datepicker({
                showOn: "button",
                format: "dd/mm/yyyy",
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
       


        $('form:first').validate();

        $('#txtFilePath').click(function () {
            $('#dataFile').trigger('click');
        }).keypress(function (e) {
            if (e.keyCode == 13 || e.which == 13) {
                $(this).trigger('click');
                return false;
            }
        });

        $('#dataFile').change(function () {
            var _file = $(this)[0].files[0];
            if (_file) {
                var _extl = ['pdf'];
                var _ext = _file.name.substring(_file.name.lastIndexOf('.') + 1);
                if (this.files[0].size > 3145728) {
                    alert("Dung lượng file phải nhỏ hơn 3MB!");
                    $('#dataFile').val('');
                }
                else {
                    if (_extl.indexOf(_ext) > -1) {
                        $('#txtFilePath').val(_file.name);
                    }
                    else {
                        $(this).val('');
                        alert('Định dạng file đính kèm không được chấp nhận.');
                    }
                }
            } else {
                $('#txtFilePath').val('');
            }
        });
    });
</script>
