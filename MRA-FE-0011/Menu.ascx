<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Menu.ascx.vb" Inherits="MRA_FW.Menu" %>
<div class="col-auto col-md-3 col-xl-2 px-sm-2 px-0 menu min-vh-100">
    <div class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2">
        <% If ViewState("ROLE_ID") = "1" Or ViewState("ROLE_ID") = "2" Then %>
        <span class="d-flex align-items-center pb-3 mb-md-0 me-md-auto text-decoration-none">
            <i class='bx bxs-food-menu'></i><span class="fs-5 d-none d-sm-inline">MENU</span>
        </span>
        <ul class="nav nav-pills flex-column mb-0 align-items-center align-items-sm-start" style="padding: 0px 0px 0px 48px;width:100%">
             <li class="w-100"><a class="nav-link px-0" href="MRA-FE-0021.aspx"><span class="d-none d-sm-inline">Menu List</span></a></li>
             <li class="w-100"><a class="nav-link px-0" href="MRA-FE-0022.aspx"><span class="d-none d-sm-inline">Menu Edit</span></a></li>
             <li class="w-100"><a class="nav-link px-0" href="MRA-FE-0031.aspx"><span class="d-none d-sm-inline">Product List</span></a></li>
             <li class="w-100"><a class="nav-link px-0" href="MRA-FE-0032.aspx"><span class="d-none d-sm-inline">Product Edit</span></a></li>
        </ul>
        <% End if %>
         <% If ViewState("ROLE_ID") = "1" Then %>
        <span class="d-flex align-items-center pb-3 mb-md-0 me-md-auto text-decoration-none">
            <i class='bx bx-list-ul' ></i><span class="fs-5 d-none d-sm-inline">ORDERS</span>
        </span>
        <ul class="nav nav-pills flex-column mb-0 align-items-center align-items-sm-start" style="padding: 0px 0px 0px 48px;width:100%">
            <li class="w-100"><a class="nav-link px-0" href="MRA-FE-0041.aspx"><span class="d-none d-sm-inline">Orders List</span></a></li>
            <li class="w-100"><a class="nav-link px-0" href="MRA-FE-0042.aspx"><span class="d-none d-sm-inline">Orders Detail</span></a></li>
            <li class="w-100"><a class="nav-link px-0" href="MRA-FE-0051.aspx"><span class="d-none d-sm-inline">Table List</span></a></li>
        </ul>
        <% End if %>
    </div>
    <!-- footer -->
    <footer class="text-center text-lg-start" style="position: absolute;bottom: 0;">
        <!-- Copyright Footer -->
        <div class="text-center p-3">
          Copyright © 2022:
          <a class="text-dark" target="_blank" href="https://meosys.com/">MEO SYSTEM</a>
        </div>
        <!-- Copyright Footer-->
    </footer>
</div>