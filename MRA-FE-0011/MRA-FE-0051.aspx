<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0051.aspx.vb" Inherits=".MRA_FE_0051" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
       <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <title>Table List</title>
</head>
<body>
<form id="form1" runat="server">    
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
                               <div class="table-container"> 
                                   <div> <!-- div add row-->                             
                                        <button id="BTN_ADD_ROW" runat="server" class="add-row button">Add row</button>
                                         <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates" style="position: absolute" data-backdrop="false" data-keyboard="false">
                                            <div class="modal-dialog" role="document" style="width: 600px;">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                         <label style="font-weight: bold; display: block;">Table Content Add</label>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div style="margin-top: 10px;">
                                                            <label style="font-weight: bold; display: block;">Name VN</label>
                                                            <asp:TextBox runat="server" ID="TXT_VN"></asp:TextBox>
                                                        </div>
                                                        <div style="margin-top: 10px;">
                                                            <label style="font-weight: bold; display: block;">Name EN </label>
                                                            <asp:TextBox runat="server" ID="TXT_JP"></asp:TextBox>
                                                        </div>
                                                        <div style="margin-top: 10px;">
                                                            <label style="font-weight: bold; display: block;">Name JP </label>
                                                            <asp:TextBox runat="server" ID="TXT_EN"></asp:TextBox>
                                                        </div>
                                                        <div style="margin-top: 10px;">
                                                            <label style="font-weight: bold; display: block;">Capacity</label>
                                                            <asp:TextBox runat="server" ID="TXT_CAP"></asp:TextBox>
                                                        </div>
                                                        <div style="margin-top: 10px;">
                                                            <label style="font-weight: bold; display: block;">Note </label>
                                                            <asp:TextBox runat="server" ID="TXT_NOTE"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <asp:Button runat="server" ID="BTN_SAVE" class="btn btn-success" Text="Save"></asp:Button>
                                                       <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>                                                       
                                                     </div>
                                               </div>
                                           </div>
                                       </div>
                                   </div> <!-- div add row end-->   
                                 <asp:GridView ID="GRD_DATA" runat="server" AutoGenerateColumns="False"
                                     AllowPaging="True" PageSize="10" class="table table-bordered"
                                     OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting"
                                     DataKeyNames="table_id">
                                    <Columns>        
                                        <asp:BoundField DataField="num" HeaderText="No" ControlStyle-Width="25px" ReadOnly="true"/>
                                        <asp:BoundField DataField="table_id" HeaderText="ID" ControlStyle-Width="35px" ReadOnly="true"/>
                                        <asp:BoundField DataField="table_nm_vn" HeaderText="Name VN" ControlStyle-Width="80px"/>
                                        <asp:BoundField DataField="table_nm_jp" HeaderText="Name JP" ControlStyle-Width="80px"/>
                                        <asp:BoundField DataField="table_nm_en" HeaderText="Name EN" ControlStyle-Width="80px"/>
                                        <asp:BoundField DataField="capacity" HeaderText="Capacity" ControlStyle-Width="50px"/>
                                        <asp:BoundField DataField="table_stt" HeaderText="Status" ControlStyle-Width="80px" ReadOnly="true"/>
                                        <asp:BoundField DataField="description" HeaderText="Note" ControlStyle-Width="120px"/>
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
