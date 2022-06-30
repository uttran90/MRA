<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0022.aspx.vb" Inherits=".MRA_FE_0022" %>
<%@ Register Tagprefix="hed" Tagname="PageHeader" Src="PageHeader.ascx" %>
<%@ Register Tagprefix="menu" Tagname="Menu" Src="Menu.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <script type="text/javascript" src="js/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="./css/lou-multi-select/css/multi-select.css" />
    <script type="text/javascript" src="js/jquery.multi-select.js"></script>
    <script type="text/javascript" src="js/jquery.quicksearch.js"></script>
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
     <!-- bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" src="js/mra.js"></script>
    <title>Menu Edit</title>

</head>
<body>
    <hed:PageHeader ID="PageHeader" runat="server"></hed:PageHeader>
    <!--Header -->
     <div class="header">
                 <div class="logo"> </div><div class="title">Menu Edit</div>
                <div class="top-info" onclick="menuToggle();">
                    <div class="menu">
                      <ul>
                         <li><a href="MRA-FE-0011.aspx">Log out</a></li>
                      </ul>
                        </div>
                </div>
            </div>
    <form id="form1" runat="server" method="post" >
            <!--Content -->
            <div>
                <div class="container-fluid">
                    <div class="row flex-nowrap">
                        <!--add menu -->
                        <menu:Menu ID="Menu" runat="server"></menu:Menu>
                        <div class="col py-3 content">
                            <div class="row flex-nowrap">
                                <!-- input area-->
                                <div class="col">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label class="col-l lable-c" id="LBL_ID" >Menu ID</label>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:label id="LBL_MENU_ID" runat="server" ></asp:label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Menu name VN</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_NAME_VN" type="text" runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Menu name JP</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_NAME_JP" type="text" runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Menu name EN</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_NAME_EN" type="text" runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Note</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" TextMode="MultiLine" Height="96px" id="TXT_NOTE" type="text"  runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Image</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <label for="TXT_FILEPATH" id="LBL_FILEPATH" runat="server" class="form-control" >Click to choose image</label>
                                                <input name='upload' type="file" id="TXT_FILEPATH" runat="server" style="display:none;"/>
                                                <div class="input-group-append">
                                                    <asp:Button class="button" CssClass="btn btn-info" id="BTN_UPLOAD" Text="Upload" runat="server" OnClick="UPLOAD_Click"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Products</span>
                                        </div>
                                        <div class="col">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div style="float:left"><label class="col-l lable-c">Product list</label> </div>
                                                    <div style="float:right"><label class="col-l lable-c">Products chosen</label> </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <asp:ListBox id="SELECT_SEND_PRODUCT_LEFT"  Rows="10" Width="200px" SelectionMode="Multiple"  runat="server" ></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--button -->
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon"></span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group content-input flex-nowrap">
                                                <div class="my-4"><asp:button id="BTN_ADD" cssclass="btn btn-success" runat="server" Text="Add" autopostback="false" Width="120px"/></div>
                                                <div class="my-4"><asp:button id="BTN_UPDATE" cssclass="btn btn-primary" runat="server" Text="Update" autopostback="false" Width="120px"/></div>
                                                <div class="p-4"><asp:button id="BTN_DELETE" CssClass="btn btn-danger" runat="server" Text="Delete" autopostback="false"  Width="120px" OnClientClick="Confirm('Delete')"/></div>
                                                <div class="my-4"><asp:button id="BTN_BACK" cssclass="btn bg-dark text-white" runat="server" Text="BACK" autopostback="false" Width="120px"/></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 <!-- image area-->
                                 <div class="col-auto col-md-4">
                                     <div class="image">
                                        <asp:Image ID="IMG_ID" runat="server"/>
                                    </div>
                                 </div>
                            </div>
                        </div> <!-- div right-col end-->
                    </div>
                </div>
            </div> <!-- content-area end --> 
        <input type="text" id="TXT_PATH" runat="server" style="display:none;"/>
        <script type="text/javascript">
            $(function () {
                // マルチセレクトボックス化
                $('#SELECT_SEND_PRODUCT_LEFT').multiSelect({
                    afterInit: function (ms) {
                        var that = this,
                            $selectableSearch = that.$selectableUl.prev(),
                            $selectionSearch = that.$selectionUl.prev(),
                            selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                            selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

                        that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                            .on('keydown', function (e) {
                                alert(that.$selectableUl);
                                if (e.which == 40) {
                                    that.$selectableUl.focus();
                                    return false;
                                }
                            });

                        that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                            .on('keydown', function (e) {
                                if (e.which == 40) {
                                    that.$selectionUl.focus();
                                    return false;
                                }
                            });
                    },
                    afterSelect: function () {
                        this.qs1.cache();
                        this.qs2.cache();
                    },
                    afterDeselect: function () {
                        this.qs1.cache();
                        this.qs2.cache();
                    }

                });
            });
                $('#TXT_FILEPATH').change(function () {
            var file = $('#TXT_FILEPATH')[0].files[0];
                if (file != null) {
                    $(this).prev('label').text(file.name);
            }
                else {
                    $(this).prev('label').text("");
            }
        });
        </script>
         <!-- ############ /javascript  ############ -->
         <!-- ############  style  ############ -->
         <style>
            .ms-container{ width:100%;}
            .ms-list{height:300px !important;}
         </style>
         <!-- ############  /style  ############ -->
    </form>
    <!--End Container -->
    <script>/*user icon header menu*/
        function menuToggle() {
            const toggleMenu = document.querySelector('.menu');
            toggleMenu.classList.toggle('active');
        }
    </script>
    </body>
</html>
