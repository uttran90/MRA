﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0022.aspx.vb" Inherits="MRA.MRA_FE_0022" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <title>Menu Add/ Update</title>
</head>
<body>
    <form id="form1" runat="server">    
            <!--Header -->
            <div class="header">
                 <div class="logo"></div><div class="title">Menu Detail</div>
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
                     <div class="div-field">
                         <div class="image"></div>
                          <div class="content" style="height:580px;">   
                          <div class="field hi-25">
                               <!-- Menu name -->
                               <label class="col-l lable-c"  id="LBL_MENU" >Menu name</label>
                               <select class="select-c" id="DDL_MENU_NM" runat="server"></select>
                           </div>
                           <!--<div class="field hi-25">
                                <label class="col-l lable-c"  id="LBL_PRODUCT_COUNT" >Product count</label>                                
                                <label class="col-l lable-c" style="margin-left:20px;" id="LBL_PRODUCT_COUNT_SHOW" >5</label>
                           </div>-->
                           <div class="field hi-25">
                               <!-- Product Note -->
                                <label class="col-l lable-c" id="LBL_NOTE" >Note</label> 
                               <input class="input-c2" id="TXT_NOTE" type="text"/>
                           </div> 
                           <div class="field hi-25">
                               <!-- Menu image -->
                                <label class="col-l lable-c" id="LBL_IMAGE" >Image</label> 
                               <input class="input-c2" id="TXT_PATH" type="text"/>
                                <button class="button" id="BTN_UPLOAD">Upload</button>
                           </div> 
                           <div class="field hi-25">
                             <label class="col-l lable-c" id="LBL_PRODUCT">Products</label> 
                               
                             <label class="col-l lable-c" style="margin-left:20px;" >Product list</label> 
                             <label class="col-l lable-c" style="margin-left:95px; width:120px;">Products chosen</label>  
                           </div>
                           <div class="field">
                              <!--Content image manu and list product-->
                              <div class="div-menu-product" style="margin-left:125px;">
                                   <div class="div-list">
                                     <asp:ListBox id="TXT_PRODUCT_LIST"  Rows="20" Width="200px" SelectionMode="Multiple"  runat="server"></asp:ListBox>
                                  </div> 
                                  <div class="div-chosen">
                                         <asp:ListBox id="TXT_PRODUCT_CHOSEN"  Rows="20" Width="200px" SelectionMode="Multiple"  runat="server" ></asp:ListBox>
                                   </div>
                            </div> <!-- div Content image manu and list product end-->  
                         </div>
                    </div>
                      <div class="button-end-center">
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
