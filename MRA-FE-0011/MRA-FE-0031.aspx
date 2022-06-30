<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0031.aspx.vb" Inherits=".MRA_FE_0031" %>
<%@ Register Tagprefix="hed" Tagname="PageHeader" Src="PageHeader.ascx" %>
<%@ Register Tagprefix="menu" Tagname="Menu" Src="Menu.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <title>Menu List</title>
</head>
<body>
    <hed:PageHeader ID="PageHeader" runat="server"></hed:PageHeader>
    <!--Header -->
    <div class="header">
                 <div class="logo"> </div><div class="title">Product List</div>
                <div class="top-info" onclick="menuToggle();">
                    <div class="menu">
                      <ul>
                         <li><a href="MRA-FE-0011.aspx">Log out</a></li>
                      </ul>
                        </div>
                </div>
            </div>
    <form id="form1" runat="server" autocomplete="off">
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
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="TXT_SEARCH" runat="server" placeholder="Input menu name, product name, note"/>
                                            <div class="input-group-append">
                                              <button id="BTN_SEARCH" class="btn btn-primary" type="submit" runat="server">Search</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row flex-nowrap">
                                <div class="div-grid">
                                    <div class="tile is-parent is-vertical">
                                        <div class="table-container table-responsive">
                                            <asp:GridView ID="GRD_DATA" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" class="table table-bordered">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-Width="10px" DataField="num" HeaderText="No"/>
                                                    <asp:hyperlinkfield ItemStyle-CssClass="text-nowrap" ItemStyle-Width="150px" datatextfield="product_nm_jp" HeaderText="Name JP" 
                                                        datanavigateurlfields="product_id"
                                                        datanavigateurlformatstring="MRA-FE-0032.aspx?product_id={0}"/>
                                                     <asp:BoundField ItemStyle-CssClass="text-nowrap" ItemStyle-Width="150px" DataField="product_nm_vn" HeaderText="Name VN" />
                                                     <asp:BoundField ItemStyle-CssClass="text-nowrap" ItemStyle-Width="50px" DataField="menu_nm_jp" HeaderText="Menu" />
                                                     <asp:BoundField ItemStyle-CssClass="text-nowrap" ItemStyle-Width="50px" DataField="price_show" HeaderText="price"/>
                                                     <asp:BoundField ItemStyle-CssClass="text-nowrap" ItemStyle-Width="10px" DataField="product_stt" HeaderText="status"/>
                                                     <asp:BoundField ItemStyle-CssClass="text-nowrap" ItemStyle-Width="150px" DataField="product_note" HeaderText="Note" />
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
    <script>/*user icon header menu*/
          function menuToggle() {
              const toggleMenu = document.querySelector('.menu');
              toggleMenu.classList.toggle('active');
          }
    </script>
</body>
</html>