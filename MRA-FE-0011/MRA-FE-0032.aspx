<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0032.aspx.vb" Inherits=".MRA_FE_0032" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <script type="text/javascript" src="js/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="./css/lou-multi-select/css/multi-select.css" />
    <script type="text/javascript" src="js/jquery.multi-select.js"></script>
    <script type="text/javascript" src="js/jquery.quicksearch.js"></script>
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <!-- bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <title>Product Edit</title>
</head>
<body>
    <form id="form1" runat="server">    
            <!--Header -->
            <div class="header">
                 <div class="logo"></div>
                <div class="title">Product Detail</div>
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
                 <div class="right-col" >
                      <!-- Content area-->
                      <div class="div-field">
                          <div class="image">
                              <asp:Image ID="IMG_ID" runat="server" Height="146px" Width="146px" />
                          </div>
                          <div class="opt-list">                                                        
                                 <button id="BTN_ADD_ROW" runat="server" class="add-row button">Add Opt</button>
                                 <asp:GridView ID="GRD_OPT" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" AllowPaging="True" 
                                     PageSize="10" class="table table-bordered"
                                     OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting"
                                     DataKeyNames="product_opt_id">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="10px" DataField="num" HeaderText="No" ReadOnly="true"/>
                                        <asp:BoundField ItemStyle-Width="10px" DataField="product_opt_id" HeaderText="ID" ReadOnly="true"/>
                                        <asp:BoundField DataField="product_opt_nm" HeaderText="Name"  ControlStyle-Width="80px"/>
                                        <asp:BoundField DataField="product_opt_price" HeaderText="Price"  ControlStyle-Width="60px"/>
                                        <asp:BoundField DataField="note" HeaderText="Note"  ControlStyle-Width="80px"/>
                                        <asp:TemplateField ControlStyle-Font-Size="Small" ControlStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Edit" runat="server" CommandName="Edit"/>
                                            </ItemTemplate>
                                            <EditItemTemplate>      
                                                <asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" />
                                                <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                                            </EditItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Font-Size="Small"  ControlStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Delete" runat="server" CommandName="Delete" />
                                            </ItemTemplate>                                         
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                              </div><!-- div opt end--> 
                          <div class="content">                         
                              <div class="field">
                                <label class="col-l lable-c" id="LBL_PRODUCT_ID" >Product ID</label>
                                <asp:label id="LBL_ID" style="margin-left:15px;width:50px;align-content:center;" runat="server" ></asp:label> 
                              </div><!-- ID end -->
                              <div class="field">
                                  <label class="col-l lable-c" id="LBL_NM_VN_ON" >Name VN On<span class="required_mark">*</span></label>
                                  <asp:TextBox class="input-c2" id="TXT_NM_VN_ON" type="text" runat="server" style="margin-left:15px;"/>
                              </div>
                              <div class="field">
                                  <label class="col-l lable-c" id="LBL_NM_JP_ON" >Name JP On<span class="required_mark">*</span></label>
                                  <asp:TextBox class="input-c2" id="TXT_NM_JP_ON" type="text" runat="server" style="margin-left:15px;"/>
                              </div>
                              <div class="field">
                                  <label class="col-l lable-c" id="LBL_NM_EN_ON" >Name EN On</label>
                                  <asp:TextBox class="input-c2" id="TXT_NM_EN_ON" type="text" runat="server" style="margin-left:15px;"/>
                              </div>
                              <div class="field">
                                  <label class="col-l lable-c" id="LBL_NM_OFF" >Name Off<span class="required_mark">*</span></label>
                                  <asp:TextBox class="input-c2" id="TXT_NM_OFF" type="text" runat="server" style="margin-left:15px;"/>
                              </div><!-- Name end -->
                              <div class="field">
                                  <label class="col-l lable-c" id="LBL_MENU" >Menu</label>
                                  <asp:TextBox id="TXT_MENU_VALUE" style="margin-left:15px;" runat="server" Enabled="true"></asp:TextBox>
                              </div><!-- Menu end -->
                              <div class="field">
                                  <label class="col-l lable-c" id="LBL_PRICE" >Price<span class="required_mark">*</span></label>
                                  <asp:TextBox class="input-c1" id="TXT_PRICE" type="text" runat="server"  style="margin-left:15px;" />                                  
                                  <label style="margin-left:6px;" >円</label>
                              </div>
                              <div class="field">
                                  <label class="col-l lable-c" id="LBL_PRICE_SHOW" >Price Show<span class="required_mark">*</span></label>
                                  <asp:TextBox class="input-c1" id="TXT_PRICE_SHOW" type="text" runat="server"  style="margin-left:15px;" />                                  
                                  <label style="margin-left:6px;" >円</label>
                              </div><!-- Price end -->
                              <div class="field">
                                  <label class="col-l lable-c" id="LBL_TAX" >Tax</label>
                                  <input type="checkbox" id="CHKB_TAX1" style="margin-left:15px;"/>
                                  <label for="CHKB_TAX1"> Tax 1</label>
                                  <input type="checkbox" id="CHKB_TAX2" />
                                  <label for="CHKB_TAX2"> Tax 2</label>
                              </div><!-- Tax end -->
                              <div class="field">
                                   <div class="col-l"><label class="lable-c" id="LBL_IMAGE" >Image</label> </div>
                                   <div class="choose-image">
                                       <label for="TXT_FILEPATH" id="LBL_FILEPATH" runat="server" class="image-input" >Click to choose image</label>
                                      <input name='upload' type="file" id="TXT_FILEPATH" runat="server" style="display:none;"/>
                                       <asp:Button class="button" style="position:absolute;margin-left:20px;" id="BTN_UPLOAD" Text="Upload" runat="server" OnClick="UPLOAD_Click" />
                                    </div>
                               </div> <!-- Upload image end -->
                              <div class="field">
                                 <label class="col-l lable-c" id="LBL_STS" >Status</label>
                                 <select class="select-c"  id="DDL_STS" runat="server">
                                      <option value="ON" selected="selected">ON</option>
                                      <option value="OFF">OFF </option>
                                 </select>                                 
                                 <label style="margin-left:6px;" id="LBL_STS_DETAIL" >(ON: Aviable  OFF: Not Aviable)</label>
                              </div><!-- Sts end -->
                              <div class="field">
                                 <label class="col-l lable-c" id="LBL_DES" >Description</label>
                                  <asp:TextBox class="input-c2" id="TXT_DES" type="text" runat="server"/>
                              </div><!-- Description end -->
                              <div class="field">
                                 <label class="col-l lable-c" id="LBL_NOTE" >Note</label>
                                  <asp:TextBox class="input-c2" id="TXT_NOTE" type="text" runat="server"/>
                              </div><!-- Note end -->          
                          </div><!-- div content end--> 
                     <!--Button ADD UPDATE DELETE -->
                       <div class="button-end-center">
                                  <asp:button id="BTN_ADD" class="button" runat="server" Text="Add" autopostback="false" />
                                  <asp:button id="BTN_UPDATE" class="button" runat="server" Text="Update" autopostback="false" />
                                  <asp:button id="BTN_DELETE" class="button" runat="server" Text="Delete" autopostback="false" />
                       </div>
                     <!-- model opt add row start -->
                      <div class="modal fade" id="addModal" tabindex="-1" role="dialog" aria-labelledby="addModal" style="position: absolute" data-backdrop="false" data-keyboard="false">
                            <div class="modal-dialog" role="document" style="width: 600px;">
                                <div class="modal-content">
                                    <div class="modal-header">
                                         <label style="font-weight: bold; display: block;">Product Option Add</label>
                                        <asp:label id="LBL_PRO" style="font-weight: bold; display: block;" runat="server"></asp:label>
                                    </div>
                                    <div class="modal-body">
                                        <div style="margin-top: 10px;">
                                            <label style="font-weight: bold; display: block;">Option Name<span class="required_mark">*</span></label>
                                            <asp:TextBox runat="server" ID="TXT_OPT_NM"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: 10px;">
                                            <label style="font-weight: bold; display: block;">Option Price<span class="required_mark">*</span> </label>
                                            <asp:TextBox runat="server" ID="TXT_OPT_PRICE"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: 10px;">
                                            <label style="font-weight: bold; display: block;">Note</label>
                                            <asp:TextBox runat="server" ID="TXT_OPT_NOTE"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="BTN_SAVE" class="btn btn-success" Text="Save"></asp:Button>
                                       <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>                                                       
                                     </div>
                               </div>
                           </div>   
                      </div><!-- model opt add row end -->
                    </div><!-- div field end-->
                 </div> <!-- div right-col end-->
            </div><!-- content-area end --> 
         <input type="text" id="TXT_PATH" runat="server" style="display:none;"/>
    </form>
    <script>
        $('#TXT_FILEPATH').change(function () {
            var file = $('#TXT_FILEPATH')[0].files[0];
            if (file != null) {
                $(this).prev('label').text(file.name);
            }
            else {
                $(this).prev('label').text("");
            }
        });
        </script>
    <!-- footer -->
     <footer class="div-footer">
    <p>Copyright © 2022 MEO SYSTEM</p>
    </footer>
    <!--End Container -->
</body>
</html>
