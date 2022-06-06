<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0042.aspx.vb" Inherits="MRA.MRA_FE_0042" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <title>ORDERS LIST</title>
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
                                      <li><a href="MRA-FE-0022.aspx">Menu Add</a></li>
                                      <li><a href="MRA-FE-0031.aspx">Product List</a></li>
                                      <li><a href="MRA-FE-0032.aspx">Product Add</a></li>
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
                      <div class="div-search3">
                          <div class="search-content-m" style="padding:0.25rem;">
                               <!--Search by order -->
                                 <label class="lbl1" style="width:10%;margin-top:18px;" id="LBL_ORDER" >ORDER</label>
                                 <input class="ip-search top bo" style="width:50%;margin:10px 0 0 90px;" id="TXT_ORDER"  type="text" runat="server" placeholder="Input customer, table, note "/>                               
                          </div>                    
                          <div class="search-content-m" style="padding:0.25rem;">
                              <!--Search by date -->
                              <label class="lbl1" style="width:10%;" id="LBL_DATE" >DATE</label>                                 
                              <button id="BTN_SEARCH" class="btn1 button" runat="server"><span>Search</span></button>
                              <input type="date" class="date" style="width:30%; margin-left:90px;" id="DATE_FROM" name="order_date_from"/>
                              <label style="padding:0.15rem;" >-</label> 
                              <input type="date" class="date"  id="DATE_TO" name="order_date_to"/>
                           </div>
                          <div class="search-content-m">
                              <!--Search by time -->                              
                             <label class="lbl1" style="width:10%;" id="LBL_TIME" >TIME</label> 
                               <input type="time" class="hour" style="margin-left:95px;" id="HOUR_FROM" name="order_hour_from"/>                            
                              <label style="padding:0.15rem;" >-</label> 
                              <input type="time" class="hour" id="HOUR_TO" name="order_hour_to"/> 
                           </div>
                        </div>  
                
                      <!--Grid Table-->
                     <div class="div-grid3">
                     <div class="tile is-parent is-vertical">
                                <button id="PAGE_PREV" runat="server" class="button previous">Previous</button>
                                <button id="PAGE_NEXT" runat="server" class="button next">Next</button>
                                <ul>
                                  <li class="pagedivided">
                                      <span id="CURRENT_PAGE" runat="server"></span>
                                      <span >/</span>
                                      <span id="TOTAL_PAGE" runat="server"></span>
                                  </li>
                                </ul>
                             <div class="table-container">
                                    <HeaderTemplate>
                                        <table class="table" style="width:100%">
                                            <thead>
                                                <tr>
                                                    <th class="th" style="width:5%">No</th>
                                                    <th class="th" style="width:20%">Product name</th>
                                                    <th class="th" style="width:5%">Quantity</th>
                                                    <th class="th" style="width:13%">Price one</th>
                                                    <th class="th" style="width:13%">Price total</th>
                                                    <th class="th" style="width:10%">Date</th>
                                                    <th class="th" style="width:5%">Table</th>
                                                    <th class="th" style="width:9%">Status</th>
                                                    <th class="th" style="width:20%">Note</th>
                                                </tr>
                                               
                                            </thead>
                                                </table>
                                    </HeaderTemplate>
                            </div>
                    </div>
                         <div class="sum">
                             <label class="lable-l" id="LBL_TOTAL" >Total</label>
                             <label class="lable-m" id="LBL_TOTAL_CASH" >5,000</label>
                             <label >円</label><br />
                             <label class="lable-l" id="LBL_VAT" >VAT</label>
                             <label class="lable-m" id="LBL_VAT_CASH" >8</label>
                             <label >％</label><br />
                             <label class="lable-l" id="LBL_OTHERS" >Others</label>
                             <label class="lable-m" id="LBL_OTHERS_CASH" >2,000</label>
                             <label >円</label><br />
                             <label class="lable-l" id="LBL_TOTAL_END" >Total End</label>
                             <label class="lable-m"id="LBL_TOTAL_END_CASH" >5,400</label>
                             <label >円</label>
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
