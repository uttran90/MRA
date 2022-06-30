<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0051.aspx.vb" Inherits=".MRA_FE_0051" %>
<%@ Register Tagprefix="hed" Tagname="PageHeader" Src="PageHeader.ascx" %>
<%@ Register Tagprefix="menu" Tagname="Menu" Src="Menu.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <title>Table List</title>
</head>
<body>
    <hed:PageHeader ID="PageHeader" runat="server"></hed:PageHeader>
   <!--Header -->
   <div class="header">
        <div class="logo"></div><div class="title">Table List</div>
       <div class="top-info">
           
       </div>
   </div>
    <form id="form1" runat="server" EnableViewState="false" autocomplete="off">
            <!--Content -->
            <div>
                <div class="container-fluid">
                    <div class="row flex-nowrap">
                        <!--add menu -->
                        <menu:Menu ID="Menu" runat="server"></menu:Menu>
                        <div class="col py-3 content">
                            <div class="row flex-nowrap px-4">
                                <!-- Search area-->
                                <div class="div-search  py-2" style="width: 600px !important">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span class="input-group-addon">Search</span>
                                        </div>
                                        <div class="col">
                                            <div class="input-group mb-3">
                                                <input type="text" class="form-control" id="TXT_SEARCH" runat="server" placeholder="Input table name, capacity, note"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><span class="input-group-addon">Date</span></div>
                                        <div class="col">
                                            <div class="input-group mb-3">
                                                <input name="DATE_FROM" type="date" id="DATE_FROM" class="form-control"  runat="server" placeholder="Date from"/>
                                                <label style="padding:0.15rem;" >-</label>
                                                <input type="date" class="form-control"  id="DATE_TO" name="DATE_TO" runat="server" placeholder="Date to"/>
                                                <div class="input-group  mt-3">
                                                <div class="input-group-append">
                                                    <button id="BTN_SEARCH" class="btn btn-primary" type="submit" runat="server">Search</button>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row flex-nowrap px-4 py-2">
                                <button id="BTN_ADD_ROW" runat="server" class="btn btn-info"  style="width: 120px !important">Add row</button>
                                <!--modal add row -->
                                <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates" style="position: absolute" data-backdrop="false" data-keyboard="false">
                                    <div class="modal-dialog" role="document" style="width: 600px;">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background: #ff8640;color: white;">
                                                 <label style="font-weight: bold">Table Content Add</label>
                                            </div>
                                            <div class="modal-body">
                                                <div class="input-group mb-3">
                                                  <span class="input-group-text">Name VN</span>
                                                  <asp:TextBox runat="server" CssClass="form-control" ID="TXT_VN"></asp:TextBox>
                                                </div>
                                                <div class="input-group mb-3">
                                                  <span class="input-group-text">Name JP </span>
                                                  <asp:TextBox runat="server" CssClass="form-control" ID="TXT_JP"></asp:TextBox>
                                                </div>
                                                <div class="input-group mb-3">
                                                  <span class="input-group-text">Name EN </span>
                                                  <asp:TextBox runat="server" CssClass="form-control" ID="TXT_EN"></asp:TextBox>
                                                </div>
                                                <div class="input-group mb-3">
                                                  <span class="input-group-text">Capacity </span>
                                                  <asp:TextBox runat="server" CssClass="form-control" ID="TXT_CAP"></asp:TextBox>
                                                </div>
                                                <div class="input-group mb-3">
                                                  <span class="input-group-text">Note </span>
                                                  <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Height="200px" ID="TXT_NOTE"></asp:TextBox>
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
                            <div class="row flex-nowrap">
                                <div class="div-grid">
                                    <div class="tile is-parent is-vertical">
                                        <div class="table-container table-responsive">
                                            <asp:GridView ShowHeaderWhenEmpty="true" ID="GRD_DATA" runat="server" AutoGenerateColumns="False"
                                                 AllowPaging="True" PageSize="10" class="table table-bordered"
                                                 OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting" OnRowDataBound = "OnRowDataBound"
                                                 DataKeyNames="table_id">
                                                <Columns>
                                                    <asp:BoundField DataField="num" HeaderText="No" ControlStyle-Width="15px" ReadOnly="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="table_id" HeaderText="ID" ControlStyle-Width="15px" ReadOnly="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="table_nm_vn" HeaderText="Name VN" ControlStyle-Width="80px"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="table_nm_jp" HeaderText="Name JP" ControlStyle-Width="80px"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="table_nm_en" HeaderText="Name EN" ControlStyle-Width="80px"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="capacity" HeaderText="Capacity" ControlStyle-Width="50px"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="table_stt" HeaderText="Status" ControlStyle-CssClass="bg-danger" ControlStyle-Width="15px" ReadOnly="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap"  DataField="description" HeaderText="Note" ControlStyle-Width="120px"/>
                                                    <asp:TemplateField ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" />
                                                            <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                                                        </EditItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton Text="Delete" runat="server" CommandName="Delete" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div> <!-- div grid end-->
                            </div><!--Grid Table-->     
                        </div> <!-- div right-col end-->
                    </div>
                </div>
            </div><!-- content-area end -->
    </form>
    <!--End Container -->
</body>
</html>
