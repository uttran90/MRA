<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0022.aspx.vb" Inherits=".MRA_FE_0022" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
     <script type="text/javascript" src="js/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="./css/lou-multi-select/css/multi-select.css" />
    <script type="text/javascript" src="js/jquery.multi-select.js"></script>
    <script type="text/javascript" src="js/jquery.quicksearch.js"></script>
    <!-- Boxicons CSS -->
    <link href='https://unpkg.com/boxicons@2.1.2/css/boxicons.min.css' rel='stylesheet'/>
    <title>Menu Add/ Update</title>
</head>
<body>
    <form id="form1" runat="server" method="post" >   
            <!--Header -->
            <div class="header">
                 <div class="logo"></div>
                <div class="title">Menu Detail</div>
                <div class="top-info" >
                    
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
                                      <li><a href="MRA-FE-0032.aspx?product_id=">Product Edit</a></li>
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
                     <div class="div-field">
                         <div class="image">

                             <asp:Image ID="IMG_ID" runat="server" Height="146px" Width="146px" />

                         </div>
                          <div class="content" style="height:620px;">   
                               <div class="field hi-25">
                                    <!-- Menu id -->
                                    <label class="col-l lable-c" id="LBL_ID" >Menu ID</label>
                                   <asp:label class="col-l lable-c" id="LBL_MENU_ID" style="margin-left:20px;" runat="server" ></asp:label> 
                                </div>
                               <div class="field hi-25">
                                    <!-- Menu name -->
                                    <label class="col-l lable-c" id="LBL_NAME_VN" >Menu name VN</label>
                                   <asp:TextBox  class="input-c2" id="TXT_NAME_VN" type="text" runat="server"/>
                                </div>
                                 <div class="field hi-25">
                                    <!-- Menu name -->
                                    <label class="col-l lable-c" id="LBL_NAME_JP" >Menu name JP</label>
                                   <asp:TextBox  class="input-c2" id="TXT_NAME_JP" type="text" runat="server"/>
                                </div>
                                <!--<div class="field hi-25">
                                     <label class="col-l lable-c"  id="LBL_PRODUCT_COUNT" >Product count</label>                                
                                     <label class="col-l lable-c" style="margin-left:20px;" id="LBL_PRODUCT_COUNT_SHOW" >5</label>
                                </div>-->
                                <div class="field hi-25">
                                    <!-- Product Note -->
                                     <label class="col-l lable-c" id="LBL_NOTE" >Note</label> 
                                    <asp:TextBox class="input-c2" id="TXT_NOTE" type="text" runat="server"/>
                                </div> 
                                <div class="field hi-25">
                                    <!-- Menu image -->
                                     <label class="col-l lable-c" id="LBL_IMAGE" >Image</label> 
                                    <asp:FileUpload id="FILE_PATH" runat="server" style="margin-left:20px;" accept="image/png, image/jpeg"/>
                                </div> 
                              <div class="field hi-25">
                                    <!-- Menu image -->
                                     <asp:label for="FILE_PATH" runat="server" ID="LBL_FILE" style="margin-left:125px; max-width:150px;max-height:25px;"></asp:label>
                                     <asp:Button class="button" style="margin-left:200px;position:fixed;" 
                                          id="BTN_UPLOAD" Text="Upload" runat="server" OnClick="UPLOAD_Click" 
                                         OnClientClick="javascript:document.getElementById('IMG_ID').style.display = 'block';"/>
                                </div> 
                                <div class="field hi-25">
                                  <label class="col-l lable-c" id="LBL_PRODUCT">Products</label> 
                                  <label class="col-l lable-c" style="margin-left:20px;" >Product list</label> 
                                  <label class="col-l lable-c" style="margin-left:95px; width:120px;">Products chosen</label>  
                                </div>
                                   <!--Content product-->
                                   <div class="div-menu-product" style="margin-left:125px;">
                                        <asp:ListBox id="SELECT_SEND_PRODUCT_LEFT"  Rows="10" Width="200px" Height="250" SelectionMode="Multiple"  runat="server" ></asp:ListBox>
                                        <%--<div class="div-list">
                                          <asp:ListBox id="TXT_PRODUCT_CHOSEN"  Rows="15" Width="200px" Height="250px" SelectionMode="Multiple"  runat="server"></asp:ListBox>
                                       </div> 
                                       <div class="div-chosen">
                                          <asp:ListBox id="TXT_PRODUCT_LIST"  Rows="15" Width="200px" Height="250px" SelectionMode="Multiple"  runat="server" ></asp:ListBox>
                                        </div>--%>

                                 </div> <!-- div product end-->  
                              </div>
                    
                            <div class="button-end-center">
                                  <asp:button id="BTN_ADD" class="button" runat="server" Text="Add" autopostback="false" />
                                  <asp:button id="BTN_UPDATE" class="button" runat="server" Text="Update" autopostback="false" />
                                  <asp:button id="BTN_DELETE" class="button" runat="server" Text="Delete" autopostback="false" />
                             </div>
                    </div><!-- div field end-->
                 </div> <!-- div right-col end-->
             </div> <!-- content-area end --> 
        <script type="text/javascript">
            $(function () {
                // マルチセレクトボックス化
                $('#SELECT_SEND_PRODUCT_LEFT').multiSelect({
                    afterInit: function (ms) {
                        var that = this,
                            $selectableSearch = that.$selectableUl.prev(),
                            $selectionSearch = that.$selectionUl.prev(),
                            selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                            selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

                        that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                            .on('keydown', function (e) {
                                alert(that.$selectableUl);
                                if (e.which == 40) {
                                    that.$selectableUl.focus();
                                    return false;
                                }
                            });

                        that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                            .on('keydown', function (e) {
                                if (e.which == 40) {
                                    that.$selectionUl.focus();
                                    return false;
                                }
                            });
                    },
                    afterSelect: function () {
                        this.qs1.cache();
                        this.qs2.cache();
                    },
                    afterDeselect: function () {
                        this.qs1.cache();
                        this.qs2.cache();
                    }

                });
            });
        </script>
         <!-- ############ /javascript  ############ -->
         <!-- ############  style  ############ -->
         <style>
            .ms-container{ width:100%;}
            .ms-list{height:300px !important;}
         </style>
         <!-- ############  /style  ############ -->
    </form>
    <!-- footer -->
     <footer class="div-footer">

    <p>Copyright © 2022 MEO SYSTEM</p>
         
    </footer>
    <!--End Container -->
    <script>
        var profilePic = document.getElementById('FILE_PATH'); /* finds the input */
        function changeLabelText() {
            var profilePicValue = profilePic.value; /* gets the filepath and filename from the input */
            var fileNameStart = profilePicValue.lastIndexOf('\\'); /* finds the end of the filepath */
            profilePicValue = profilePicValue.substr(fileNameStart); /* isolates the filename */
            var profilePicLabelText = document.getElementById('LBL_FILE'); /* finds the label text */
            if (profilePicValue !== '') {
                profilePicLabelText.textContent = profilePicValue; /* changes the label text */
            }
        }
        profilePic.addEventListener('change', changeLabelText, false); /* runs the function whenever the filename in the input is changed */
    </script>
    </body>
</html>
