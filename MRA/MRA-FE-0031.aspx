<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0031.aspx.vb" Inherits="MRA.MRA_FE_0031" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
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
                      <div class="div-search2">
                           <div class="search-content-m" style="padding:0.25rem;">
                               <!--Search by Menu-->
                                 <label class="lbl1" style="width:10%;margin-top:18px;" id="LBL_MENU" >Menu name</label>
                                 <select class="ip-search top" style="width:30%;margin:10px 0 0 90px;" id="DDL_MENU_NM" runat="server"></select>                                
                               <!--Search by Status--> 
                              <label style="width:10%;" id="LBL_STATUS" >Status</label>&nbsp;
                                 <select class="ip-search" style="width:20%;" id="DDL_STATUS" runat="server">
                                      <option id="ON">ON</option>
                                      <option id="OFF">OFF</option>
                                 </select>
                          </div> 
                           <!--Search by Key-->
                          <div class="search-content-m">
                                    <input class="text-b" id="TXT_SEARCH" type="text" style="margin:10px 0 0 60px" runat="server" placeholder="Input menu name, product name, note"/>                                                                                                            
                                     <button id="BTN_SEARCH" class="button" runat="server">
                                       <span>Search</span>
                                     </button>
                         </div>
                     </div>
                     <!--Grid Table-->
                     <div class="div-grid2">
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
                                                    <th class="th" style="width:10%">ID</th>
                                                    <th class="th" style="width:15%">Poduct Name</th>
                                                    <th class="th" style="width:15%">Menu Name</th>
                                                    <th class="th" style="width:10%">Image</th>                                                    
                                                    <th class="th" style="width:10%">Tax</th>
                                                    <th class="th" style="width:10%">Price</th>
                                                    <th class="th" style="width:5%">Sts</th>
                                                    <th class="th" style="width:20%">Note</th>
                                                </tr>
                                            </thead>
                                                </table>
                                    </HeaderTemplate>
                            </div><!-- div header end-->
                    </div> <!-- div navi - header-->
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
