<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0051.aspx.vb" Inherits="MRA.MRA_FE_0051" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <title>Table List</title>
</head>
<body>
    <form id="form1" runat="server">    
            <!--Header -->
            <div class="header">
                 <div class="logo"></div><div class="title">Table List</div>
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
                      <div class="div-search1">
                           <div class="search-content-m">
                                    <input class="text-b" id="TXT_SEARCH" type="text" style="margin:20px 0 0 50px" runat="server" placeholder="Input table name, capacity, status, note"/>                                                                                                            
                                     <button id="BTN_SEARCH" class="button" runat="server">
                                       <span>Search</span>
                                     </button>
                         </div>
                       </div>
                     <!--Grid Table-->
                     <div class="div-grid1">
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
                                 <div><button id="BTN_ADD_ROW" runat="server" class="add-row button">Add row</button></div>
                                    <HeaderTemplate>
                                        <table class="table" style="width:100%">
                                            <thead>
                                                <tr>
                                                    <th class="th" style="width:5%">No</th>
                                                    <th class="th" style="width:20%">Name</th>
                                                    <th class="th" style="width:10%">Capacity</th>
                                                    <th class="th" style="width:40%">Note</th>
                                                    <th class="th" style="width:20%">Status</th>
                                                </tr>
                                            </thead>
                                                </table>
                                    </HeaderTemplate>
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
