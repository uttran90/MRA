<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0041.aspx.vb" Inherits=".MRA_FE_0041" %>
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
    <title>Orders List</title>
</head>
<body>
    <hed:PageHeader ID="PageHeader" runat="server"></hed:PageHeader>
    <!--Header -->
    <div class="header">
         <div class="logo"></div><div class="title">Order List</div>
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
                                        <span class="input-group-addon">Order</span>
                                    </div>
                                    <div class="col">
                                        <div class="input-group mb-3">
                                            <input type="text" class="form-control" id="TXT_SEARCH" runat="server" placeholder="Input menu name, table name,product name, note"/>
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
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"><span class="input-group-addon">Time</span></div>
                                    <div class="col">
                                        <div class="input-group">
                                            <input type="time" class="form-control" id="TIME_FROM" name="TIME_FROM" runat="server" step="1" placeholder="Time from"/>
                                            <label style="padding:0.15rem;" >-</label>
                                            <input type="time" class="form-control" id="TIME_TO" name="TIME_TO" runat="server" step="1" placeholder="Time to"/>
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
                        <div class="row flex-nowrap px-4 py-2" style="width: 165px !important">
                            <button id="BTN_CSV" class="btn btn-success"  runat="server">Export CSV</button>
                        </div>
                        <div class="row flex-nowrap">
                            <div class="div-grid">
                                <div class="tile is-parent is-vertical">
                                    <div class="table-container table-responsive">
                                         <asp:GridView ShowHeaderWhenEmpty="true" ID="GRD_DATA" runat="server" AutoGenerateColumns="False" AllowPaging="True"  
                                               PageSize="10" class="table table-bordered" ShowFooter="true" 
                                               OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting"
                                             DataKeyNames="table_info_id">
                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="10px" DataField="num" HeaderText="No" ReadOnly="true"/>
                                                <asp:hyperlinkfield ItemStyle-Width="10px" datatextfield="table_info_id" HeaderText="ID" 
                                                    datanavigateurlfields="table_info_id"
                                                    datanavigateurlformatstring="MRA-FE-0042.aspx?table_info_id={0}"/>
                                                 <asp:BoundField ItemStyle-Width="200px" ItemStyle-CssClass="text-nowrap" DataField="guess_nm" HeaderText="Customer" />
                                                <asp:BoundField ItemStyle-Width="15px" ItemStyle-CssClass="text-nowrap" DataField="guess_count" HeaderText="Customer <br /> Count" HtmlEncode="false" />
                                                <asp:BoundField ItemStyle-Width="20px" ItemStyle-CssClass="text-nowrap" DataField="guess_phone" HeaderText="Customer <br />Phone" HtmlEncode="false" />
                                                <asp:BoundField ItemStyle-Width="15px" ItemStyle-CssClass="text-nowrap" DataField="product_count" HeaderText="Product <br /> Count" ReadOnly="true" HtmlEncode="false"/>
                                                <asp:BoundField ItemStyle-Width="15px" ItemStyle-CssClass="text-nowrap" DataField="total" HeaderText="Total" DataFormatString="{0:N2}" ReadOnly="true"/>
                                                <asp:BoundField ItemStyle-Width="100px" ItemStyle-CssClass="text-nowrap" DataField="serve_date" HeaderText="Date" />
                                                <asp:BoundField ItemStyle-Width="30px" ItemStyle-CssClass="text-nowrap" DataField="serve_time" HeaderText="Time" />
                                                <asp:BoundField ItemStyle-Width="45px" ItemStyle-CssClass="text-nowrap" DataField="table_nm_vn" HeaderText="Table" ReadOnly="true"/>
                                                <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="note_tx" HeaderText="Note" />
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