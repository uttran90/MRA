<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0032.aspx.vb" Inherits=".MRA_FE_0032" %>
<%@ Register Tagprefix="hed" Tagname="PageHeader" Src="PageHeader.ascx" %>
<%@ Register Tagprefix="menu" Tagname="Menu" Src="Menu.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/mra.js"></script>
    <title>Product Edit</title>
</head>
<body>
    <hed:PageHeader ID="PageHeader" runat="server"></hed:PageHeader>
    <!--Header -->
    <div class="header">
         <div class="logo"></div>
        <div class="title">Product Detail</div>
        <div class="top-info" >
            
        </div>
    </div>
    <form id="form1" runat="server">
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
                                            <span class="input-group-addon">Product ID</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:label id="LBL_ID" runat="server" ></asp:label> 
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Name VN On<span class="required_mark">*</span></span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_NM_VN_ON" type="text" runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Name JP On<span class="required_mark">*</span></span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_NM_JP_ON" type="text" runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Name EN On<span class="required_mark">*</span></span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_NM_EN_ON" type="text" runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Name Off<span class="required_mark">*</span></span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_NM_OFF" type="text" runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Menu</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3" style="width: 93px !important;">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_MENU_VALUE" type="text" runat="server" Enabled="true"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Price<span class="required_mark">*</span></span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3" style="width: 115px !important;">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_PRICE" type="text" runat="server"/>
                                                <label style="margin-left:4px;">円</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Price Show<span class="required_mark">*</span></span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3" style="width: 115px !important;">
                                                <asp:TextBox  Cssclass="form-control" id="TXT_PRICE_SHOW" type="text" runat="server"/>
                                                <label style="margin-left:4px;">円</label>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Image -->
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
                                            <span class="input-group-addon">Status</span>
                                        </div>
                                        <div class="col">
                                            <label id="LBL_STS_DETAIL" >(ON: Aviable  OFF: Not Aviable)</label>
                                            <div class="input-group mb-3" style="width: 115px !important;">
                                                <select class="form-select"  id="DDL_STS" runat="server">
                                                    <option value="ON" selected="selected">ON</option>
                                                    <option value="OFF">OFF</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Description -->
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Description</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3 content-input">
                                                <asp:TextBox  Cssclass="form-control" TextMode="MultiLine" Height="96px" id="TXT_DES" type="text"  runat="server"/>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Note -->
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
                                    <!-- Product option-->
                                    <div class="row flex-nowrap">
                                        <div class="col-md-2">
                                            <span class="input-group-addon"></span>
                                        </div>
                                        <div class="col">
                                            <div class="row flex-nowrap p-2">
                                                <button id="BTN_ADD_ROW" runat="server" class="btn btn-info" style="width: 120px !important">Add row</button>
                                                <!-- model opt add row start -->
                                                <div class="modal fade" id="addModal" tabindex="-1" role="dialog" aria-labelledby="addModal" style="position: absolute" data-backdrop="false" data-keyboard="false">
                                                    <div class="modal-dialog" role="document" style="width: 600px;">
                                                        <div class="modal-content">
                                                            <div class="modal-header" style="background: #ff8640;color: white;">
                                                                <label style="font-weight: bold">Product Option Add</label>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="input-group mb-3">
                                                                    <span class="input-group-text">Option Name<span class="required_mark">*</span></span>
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TXT_OPT_NM"></asp:TextBox>
                                                                </div>
                                                                <div class="input-group mb-3">
                                                                    <span class="input-group-text">Option Price<span class="required_mark">*</span></span>
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TXT_OPT_PRICE"></asp:TextBox>
                                                                </div>
                                                                <div class="input-group mb-3">
                                                                    <span class="input-group-text">Note </span>
                                                                    <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Height="200px" ID="TXT_OPT_NOTE"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <asp:Button runat="server" ID="BTN_SAVE" class="btn btn-success" Text="Save"></asp:Button>
                                                                <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                                                            </div>
                                                       </div>
                                                   </div>
                                                </div>
                                            </div>
                                            <!--grid product option -->
                                            <div class="row flex-nowrap">
                                                <div class="div-grid">
                                                    <div class="is-vertical">
                                                        <div class="table-container table-responsive">
                                                            <asp:GridView ID="GRD_OPT" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" AllowPaging="True" 
                                                                 PageSize="5" class="table table-bordered"
                                                                 OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting"
                                                                 DataKeyNames="product_opt_id">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="10px" DataField="num" HeaderText="No" ReadOnly="true"/>
                                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  ItemStyle-Width="10px" DataField="product_opt_id" HeaderText="ID" ReadOnly="true"/>
                                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="product_opt_nm" HeaderText="Name"  ControlStyle-Width="80px"/>
                                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="product_opt_price" HeaderText="Price"  ControlStyle-Width="60px"/>
                                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="note" HeaderText="Note"  ControlStyle-Width="80px"/>
                                                                    <asp:TemplateField ControlStyle-Font-Size="Small" ControlStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton Text="Edit" runat="server" CommandName="Edit"/>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" />
                                                                            <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                                                                        </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                    <asp:TemplateField ControlStyle-Font-Size="Small"  ControlStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton Text="Delete" runat="server" CommandName="Delete"/>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
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
                                                <div class="p-4"><asp:button  id="BTN_DELETE" CssClass="btn btn-danger" runat="server" Text="Delete" autopostback="false"  Width="120px" OnClientClick="Confirm('Delete')"/></div>
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
        <script>
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
    </form>
    <!--End Container -->
    </body>
</html>
