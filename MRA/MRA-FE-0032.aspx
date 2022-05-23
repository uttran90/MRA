<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0032.aspx.vb" Inherits="MRA.MRA_FE_0032" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <title>Product Edit</title>
</head>
<body>
    <form id="form1" runat="server">    
            <!--Header -->
            <div class="header">
                 <div class="logo"></div><div class="title">Product Detail</div>
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
                      <!-- Content area-->
                      <div class="div-field">
                          <div class="image"></div>
                          <div class="content">                              
                              <div class="field pad-75">
                                <label class="col-l lable-c" id="LBL_PRODUCT_ID" >Product ID</label>
                                 <input class="input-c1" style="background:#c7c7c7;" id="TXT_ID" type="text" runat="server"/>
                              </div>
                              <div class="field pad-75">
                                  <label class="col-l lable-c" id="LBL_PRODUCT_NM_ON" >Product Name On</label>
                                  <input class="input-c2" id="TXT_PRODUCT_NM_ON" type="text" runat="server"/>
                              </div>
                              <div class="field pad-75">
                                  <label class="col-l lable-c" id="LBL_PRODUCT_NM_OFF" >Product Name Off</label>
                                  <input class="input-c2" id="TXT_PRODUCT_NM_OFF" type="text" runat="server"/>
                              </div>
                              <div class="field pad-75">
                                  <label class="col-l lable-c" id="LBL_MENU" >Menu</label>
                                  <asp:ListBox id="LIST_MENU_CHOSEN" style="margin-left:20px;" Rows="5" Width="200px" SelectionMode="Multiple"  runat="server" ></asp:ListBox>
                              </div>
                              <div class="field pad-75">
                                  <label class="col-l lable-c" id="LBL_PRICE" >Price</label>
                                  <input class="input-c1" id="TXT_PRICE" type="text" runat="server"/>                                  
                                  <label style="margin-left:6px;" id="LBL_YEN" >円</label>
                              </div>
                              <div class="field pad-75">
                                  <label class="col-l lable-c" id="LBL_TAX" >Tax</label>
                                  <select class="select-c" id="DDL_TAX" runat="server"></select>
                              </div>
                              <div class="field pad-75">
                                  <label class="col-l lable-c" id="LBL_Image" >Image</label>
                                  <input class="input-c2" id="TXT_PATH" type="text" runat="server"/>
                                  <button class="button" id="BTN_UPLOAD">Upload</button>
                              </div>                            
                              <div class="field pad-75">
                                 <label class="col-l lable-c" id="LBL_STS" >Status</label>
                                 <select class="select-c"  id="DDL_STS" runat="server">
                                      <option value="ON" selected="selected">ON</option>
                                      <option value="OFF">OFF </option>
                                 </select>                                 
                                 <label style="margin-left:6px;" id="LBL_STS_DETAIL" >(ON: Aviable  OFF: Not Aviable)</label>
                              </div>
                              <div class="field pad-75">
                                 <label class="col-l lable-c" id="LBL_NOTE" >Note</label>
                                  <input class="input-c2" id="TXT_NOTE" type="text" runat="server"/>
                              </div>          
                          </div><!-- div content end-->   
                     <!--Button ADD UPDATE DELETE -->
                      <div class="button-end-center" >
                           <button id="BTN_ADD" class="button" runat="server"><span>Add</span></button>
                           <button id="BTN_UPDATE" class="button" runat="server"><span>Update</span></button>
                           <button id="BTN_DELETE" class="button" runat="server"><span>Delete</span></button>
                      </div> 
                       </div><!-- div field end-->
                     
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
