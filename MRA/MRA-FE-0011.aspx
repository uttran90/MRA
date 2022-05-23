<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0011.aspx.vb" Inherits="MRA.MRA_FE_0011" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/mrascss.css" rel="stylesheet" media="screen,print" />
    <title>Login</title>
</head>
<body>
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
                          <input id="TXT_ID" class="input" type="text" runat="server" maxlength="20"  placeholder="User name"/>
                          </p>
                      </div>
                      <div><p>
                             <input id="TXT_PW"  class="input" type="password" runat="server" maxlength="20" placeholder="Password"/> 
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
    <footer class="div-footer">
    <p>Copyright © 2022 MEO SYSTEM</p>
</footer>
</body>
</html>
