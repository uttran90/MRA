<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0031.aspx.vb" Inherits=".MRA_FE_0031" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <!-- bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <title>Product List</title>
</head>
<body>
    <form id="form1" runat="server">
            <!--Header -->
            <div class="header">
                 <div class="logo"></div><div class="title">Product List</div>
                <div class="top-info">
                    
                </div>
            </div>
            <!--Content -->
            <div class="content-area">  
                <!-- Side Navigator -->
                 <div class="left-col">
                         <ul class="nav-links">
                             <li>
                                 <div class="icon-link">
                                      <a href="#">
                                          <i class='bx bxs-food-menu'></i>
                                          <span class="link_name">MENU</span> 
                                          </a>   
                                 </div>
                                 <ul class="sub-menu">
                                      <li><a href="MRA-FE-0021.aspx">Menu List</a></li>
                                      <li><a href="MRA-FE-0022.aspx">Menu Edit</a></li>
                                      <li><a href="MRA-FE-0031.aspx">Product List</a></li>
                                      <li><a href="MRA-FE-0032.aspx">Product Edit</a></li>
                                  </ul>
                             </li>
                             <li>
                                 <div class="icon-link">
                                      <a href="#">
                                          <i class='bx bx-list-ul' ></i>
                                          <span class="link_name">ORDERS</span> 
                                          </a> 
                                  </div>
                                 <ul class="sub-menu">
                                     <li><a href="MRA-FE-0041.aspx">Orders List</a></li>
                                     <li><a href="MRA-FE-0042.aspx">Orders Detail</a></li>
                                     <li><a href="MRA-FE-0051.aspx">Table List</a></li>
                                 </ul>
                              </li>
                        </ul>
                     </div>
                
                <!-- Detail Content -->
                 <div class="right-col">
                      <!-- Search area-->
                      <div class="div-search1">
                          <%-- <div class="search-content-m" style="padding:0.25rem;">
                               <!--Search by Menu-->
                                 <label class="lbl1" style="width:10%;margin-top:18px;" id="LBL_MENU" >Menu name</label>
                                 <select class="ip-search top" style="width:30%;margin:10px 0 0 90px;" id="DDL_MENU_NM" runat="server"></select>
                               <!--Search by Status--> 
                              <label style="width:10%;" id="LBL_STATUS" >Status</label>&nbsp;
                                 <select class="ip-search" style="width:20%;" id="DDL_STATUS" runat="server">
                                      <option id="ON">ON</option>
                                      <option id="OFF">OFF</option>
                                 </select>
                          </div> --%>
                           <!--Search by Key-->
                          <div class="search-content-m">
                                    <input class="text-b" id="TXT_SEARCH" type="text" style="margin:10px 0 0 60px" runat="server" placeholder="Input menu name, product name, note"/>
                                     <button id="BTN_SEARCH" class="button" runat="server">
                                       <span>Search</span>
                                     </button>
                         </div>
                     </div>
                     <!--Grid Table-->
                     <!--Grid Table-->
                     <div class="div-grid1">
                         <div class="tile is-parent is-vertical">
                             <div class="table-container">
                                 <asp:GridView ID="GRD_DATA" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" class="table table-bordered">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="15px" DataField="num" HeaderText="No"/>
                                        <asp:hyperlinkfield ItemStyle-Width="20px" datatextfield="product_nm_jp" HeaderText="Name" 
                                            datanavigateurlfields="product_id"
                                            datanavigateurlformatstring="MRA-FE-0032.aspx?product_id={0}"/>
                                         <asp:BoundField ItemStyle-Width="150px" DataField="menu_nm_jp" HeaderText="Menu" />
                                         <asp:BoundField ItemStyle-Width="150px" DataField="price_show" HeaderText="price"/>
                                         <asp:BoundField ItemStyle-Width="150px" DataField="product_stt" HeaderText="status"/>
                                         <asp:BoundField ItemStyle-Width="150px" DataField="product_note" HeaderText="Note" />
                                    </Columns>
                                </asp:GridView>
                             </div>
                         </div>
                    </div> <!-- div grid end-->
                 </div> <!-- div right-col end-->
             </div><!-- content-area end -->
    </form>
    <!-- footer -->
     <footer class="div-footer">
    <p>Copyright © 2022 MEO SYSTEM</p>
    </footer>
    <!--End Container -->
</body>
</html>
