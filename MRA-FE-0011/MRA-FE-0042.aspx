<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0042.aspx.vb" Inherits=".MRA_FE_0042" %>

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
    <form id="form1" runat="server">    
            <!--Header -->
            <div class="header">
                 <div class="logo"></div><div class="title">Order Detail</div>
                <div class="top-info">
                    
                </div>
            </div>
            <!--Content -->
            <div class="content-area" >  
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
                                      <li><a href="MRA-FE-0022.aspx?menu_id=">Menu Edit</a></li>
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
                                     <li><a href="MRA-FE-0042.aspx?table_info_id=">Orders Detail</a></li>
                                     <li><a href="MRA-FE-0051.aspx">Table List</a></li>
                                 </ul>
                              </li>
                        </ul>
                     </div>
                
                <!-- Detail Content -->
                 <div class="right-col" >
                      <!-- Search area-->
                      <div class="div-search2">
                          <div class="search-content-m pad-25">
                               <!--Search by order -->
                                 <label class="lbl1" style="width:10%;margin-top:18px;" id="LBL_ORDER" >Order ID</label>
                                 <asp:TextBox class="ip-search top bo" style="width:50%;margin:10px 0 0 90px;" id="TXT_SEARCH"  type="text" runat="server"/>     
                              <button id="BTN_SEARCH" class="button" runat="server"><span>Search</span></button>        
                          </div>     
                           <div class="search-content-m pad-25">
                               <!--Search by order -->
                                 <label class="lbl1" style="width:10%;margin-top:18px;" id="LBL_DATE" >Date</label>
                                  <asp:TextBox class="top bo" style="width:50%;margin:10px 0 0 90px;background-color:#c7c7c7" id="TXT_DATE"  type="text" runat="server" ReadOnly="true"/>    
                          </div> 
                        </div>  
                      <!--Grid Table-->
                     <div class="div-grid2">
                         <div class="tile is-parent is-vertical">
                             <div class="table-container">
                                <asp:GridView ID="GRD_DATA" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" 
                                       class="table table-bordered" OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting"
                                       DataKeyNames="table_order_id">
                                    <Columns>
                                        <asp:BoundField DataField="num" HeaderText="No" ControlStyle-Width="25px" ReadOnly ="true"/>
                                        <asp:BoundField DataField="table_order_id" HeaderText="Table Order ID" ControlStyle-Width="30px" ReadOnly ="true"/>
                                        <asp:BoundField DataField="product_id" HeaderText="Product ID" ControlStyle-Width="30px"/>
                                        <asp:BoundField DataField="product_nm_vn" HeaderText="Product" ControlStyle-Width="80px" ReadOnly ="true"/>
                                        <asp:BoundField DataField="product_count" HeaderText="Count" ControlStyle-Width="30px"/>
                                        <asp:BoundField DataField="price" HeaderText="Price"  DataFormatString="{0:N2}" ControlStyle-Width="50px" ReadOnly ="true"/>
                                        <asp:BoundField DataField="product_opt_id" HeaderText="Option ID" ControlStyle-Width="80px"/>
                                        <asp:BoundField DataField="product_opt_nm" HeaderText="Option" ControlStyle-Width="80px" ReadOnly ="true"/>
                                        <asp:BoundField DataField="product_opt_count" HeaderText="Option Count" ControlStyle-Width="30px"/>
                                        <asp:BoundField DataField="product_opt_price" HeaderText="Option Price" ControlStyle-Width="50px" ReadOnly ="true"/>
                                        <asp:BoundField DataField="note_tx" HeaderText="Note" ControlStyle-Width="80px"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" />
                                            </ItemTemplate>
                                            <EditItemTemplate>      
                                                <asp:LinkButton Text="Update" runat="server" OnClick="Update" />
                                                <asp:LinkButton Text="Cancel" runat="server" OnClick="Cancel" />
                                            </EditItemTemplate> 
                                         </asp:TemplateField>
                                         <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Delete" runat="server" CommandName="Delete" />
                                         </ItemTemplate>                                         
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>  
                            </div><!--div table-container end-->
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
