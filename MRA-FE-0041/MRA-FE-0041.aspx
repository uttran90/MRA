<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0041.aspx.vb" Inherits=".MRA_FE_0041" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <title>ORDERS LIST</title>
</head>
<body>
    <form id="form1" runat="server" EnableViewState="false">    
            <!--Header -->
            <div class="header">
                 <div class="logo"></div><div class="title">Order List</div>
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
                      <div class="div-search3" >
                          <div class="search-content-m pad-25" >
                               <!--Search by order -->
                                 <label class="lbl1" style="margin-top:18px;" id="LBL_ORDER" >Order</label>
                                 <input class="ip-search top bo" style="width:50%;margin:10px 0 0 90px;" id="TXT_SEARCH"  type="text" runat="server" placeholder="Input customer, table, note "/>                               
                          </div>                     
                          <div class="search-content-m pad-25" >
                              <!--Search by date -->
                              <label class="lbl1" id="LBL_DATE" >Date</label>                                 
                              <button id="BTN_SEARCH" class="btn1 button" runat="server" style="margin-right:40px;"><span>Search</span></button>
                              <input type="date" class="date" style="margin-left:90px;" id="DATE_FROM" runat="server"/>
                              <label style="padding:0.15rem;" >-</label> 
                              <input type="date" class="date"  id="DATE_TO" runat="server" />
                           </div>
                          <div class="search-content-m" >
                              <!--Search by time --> 
                             <label class="lbl1" id="LBL_TIME" >Time</label> 
                              <!-- Export CSV-->
                              <input type="time" class="hour" style="margin-left:90px;" id="TIME_FROM" runat="server" step="1" />                            
                              <label style="padding:0.15rem;" >-</label> 
                              <input type="time" class="hour" id="TIME_TO" runat="server" step="1"/> 
                              <button id="BTN_CSV" class="csv-btn"  runat="server" >Export CSV</button>                              
                               </div> 
                        </div>
                      <!--Grid Table-->
                     <div class="div-grid3" > 
                         <div class="tile is-parent is-vertical" >
                             <div class="table-container" >
                                   <asp:GridView ID="GRD_DATA" runat="server" AutoGenerateColumns="False" AllowPaging="True"  
                                       PageSize="10" class="table table-bordered" ShowFooter="true" 
                                       OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting"
                                     DataKeyNames="table_info_id">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="15px" DataField="num" HeaderText="No" ReadOnly="true"/>
                                        <asp:hyperlinkfield ItemStyle-Width="20px" datatextfield="table_info_id" HeaderText="ID" 
                                            datanavigateurlfields="table_info_id"
                                            datanavigateurlformatstring="MRA-FE-0042.aspx?table_info_id={0}"/>
                                        <asp:BoundField DataField="guess_nm" HeaderText="Customer" />
                                        <asp:BoundField DataField="guess_count" HeaderText="Customer Count" />
                                        <asp:BoundField DataField="guess_phone" HeaderText="Customer Phone" />
                                        <asp:BoundField DataField="product_count" HeaderText="Product Count" ReadOnly="true"/>
                                        <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:N2}" ReadOnly="true"/>
                                        <asp:BoundField DataField="serve_date" HeaderText="Date" />
                                        <asp:BoundField DataField="serve_time" HeaderText="Time" />
                                        <asp:BoundField DataField="tax" HeaderText="Tax" />
                                        <asp:BoundField DataField="table_nm_vn" HeaderText="Table" ReadOnly="true"/>
                                        <asp:BoundField DataField="note_tx" HeaderText="Note" />
                                         <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" />
                                            </ItemTemplate>
                                            <EditItemTemplate>      
                                                <asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" />
                                                <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                                            </EditItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Delete" runat="server" CommandName="Delete" />
                                            </ItemTemplate>                                         
                                            </asp:TemplateField>
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
