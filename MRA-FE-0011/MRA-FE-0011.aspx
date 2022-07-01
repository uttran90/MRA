<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0011.aspx.vb" Inherits=".MRA_FE_0011" %>
<%@ Register Tagprefix="hed" Tagname="PageHeader" Src="PageHeader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <title>Login</title>
</head>
<body>
    <hed:PageHeader ID="PageHeader" runat="server"></hed:PageHeader>
    <form id="form1" runat="server">
        <divclass="container">
            <div class="div-l1">
                <img src="image/bunbo1.jpg" />
            </div>
            <div class="div-r1">
                 <div class="div-r-c1">
                     <div style="margin-top:45px;font-size:25px;color:#C65911; text-shadow:4px 4px 3px #f8c1a4"><p>Welcome!</p><br />
                     </div> 
                      <div ><p>
                          <input id="TXT_ID" class="form-control" type="text" runat="server" maxlength="20"  placeholder="User name"/>
                          </p>
                      </div>
                      <div><p>
                             <input id="TXT_PW"  class="form-control" type="password" runat="server" maxlength="20" placeholder="Password"/> 
                          </p>
                      </div><br />
                      <div style="padding-top:24px">
                            <div style="padding-bottom:10%">
                              <button id="BTN_LOGIN" class="button" runat="server">
                                <span>Login</span>
                              </button>
                            </div>
                      </div> 
                 </div>
            </div> 
    </form>
    <footer style ="padding:14px; position: fixed;height: 50px;background-color: #F8CBAD; color:black;bottom: 0px;left: 0px;right: 0px;margin-bottom: 0px; text-align:center">
        <p>Copyright © 2022 MEO SYSTEM</p>
    </footer>
</body>
</html>
