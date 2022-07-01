<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0042.aspx.vb" Inherits=".MRA_FE_0042" %>
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
    <title>Order Detail</title>
   </head>
<body>
    <hed:PageHeader ID="PageHeader" runat="server"></hed:PageHeader>
    <!--Header -->
    <div class="header">
          <div class="logo"> </div><div class="title">Order Detail</div>
                <div class="top-info" onclick="menuToggle();">
                    <div class="menu">
                      <ul>
                         <li><a href="MRA-FE-0011.aspx">Log out</a></li>
                      </ul>
                    </div>
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
                        <div class="row flex-nowrap px-4">
                            <!-- Search area-->
                            <div class="div-search  py-2" style="width: 600px !important">
                                <div class="row">
                                    <div class="col-md-2">
                                        <span class="input-group-addon">Order ID</span>
                                    </div>
                                    <div class="col">
                                        <div class="input-group mb-3">
                                            <asp:TextBox cssclass="form-control" type="text" id="TXT_SEARCH" runat="server"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2"><span class="input-group-addon">Order Date</span></div>
                                    <div class="col">
                                        <div class="input-group">
                                            <asp:TextBox cssclass="form-control" id="TXT_DATE"  type="text" runat="server" ReadOnly="true"/>
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
                        <div class="row flex-nowrap">
                            <div class="div-grid">
                                <div class="tile is-parent is-vertical">
                                    <div class="table-container table-responsive">
                                         <asp:GridView ShowHeaderWhenEmpty="true" ID="GRD_DATA" runat="server" AutoGenerateColumns="False" AllowPaging="True"  
                                               PageSize="10" class="table table-bordered"  OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting" DataKeyNames="table_order_id">
                                                <Columns>
                                                    <asp:BoundField DataField="num" HeaderText="No" ControlStyle-Width="10px" ReadOnly ="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="table_order_id" HeaderText="Table  <br /> Order ID" HtmlEncode="false" ControlStyle-Width="15px" ReadOnly ="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="product_id" HeaderText="Product ID" ControlStyle-Width="50px" ControlStyle-CssClass="form-control"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="product_nm_vn" HeaderText="Product" ControlStyle-Width="80px" ReadOnly ="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="product_count" HeaderText="Count" ControlStyle-Width="50px" ControlStyle-CssClass="form-control"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="price" HeaderText="Price"  DataFormatString="{0:N2}" ControlStyle-Width="50px" ReadOnly ="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="product_opt_id" HeaderText="Option ID" ControlStyle-Width="50px" ControlStyle-CssClass="form-control"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="product_opt_nm" HeaderText="Option" ControlStyle-Width="100px" ReadOnly ="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="product_opt_count" HeaderText="Option <br /> Count" HtmlEncode="false" ControlStyle-Width="50px" ControlStyle-CssClass="form-control"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="product_opt_price" HeaderText="Option <br /> Price" HtmlEncode="false" ControlStyle-Width="50px" ReadOnly ="true"/>
                                                    <asp:BoundField ItemStyle-CssClass="text-nowrap" DataField="note_tx" HeaderText="Note" ControlStyle-CssClass="form-control"/>
                                                    <asp:TemplateField ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>      
                                                            <asp:LinkButton Text="Update" runat="server" OnClick="Update" />
                                                            <asp:LinkButton Text="Cancel" runat="server" OnClick="Cancel" />
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
    <script>/*user icon header menu*/
          function menuToggle() {
              const toggleMenu = document.querySelector('.menu');
              toggleMenu.classList.toggle('active');
          }
    </script>
</body>
</html>
