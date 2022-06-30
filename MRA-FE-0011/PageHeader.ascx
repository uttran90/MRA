<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PageHeader.ascx.vb" Inherits="MRA_FW.PageHeader" %>
<% If Me._messageErr.Count > 0 Then%>
<!-- error -->
<div>
  <div class="alert alert-danger">
   <strong>
        <% For Each msg As String In Me._messageErr %>
            <%=msg %><br />
        <% Next %>
    </strong>
  </div>
</div>
<% End If %>
<% If Me._messageInfo.Count > 0 Then%>
<!-- infomation -->
<div>
  <div class="alert alert-info">
    <strong>
        <% For Each msg As String In Me._messageInfo %>
            <%=msg %><br />
        <% Next %>
    </strong>
  </div>
</div>
<% End If %>
<% If Me._messageWar.Count > 0 Then%>
<!-- warning -->
<div>
  <div class="alert alert-warning">
    <strong>
        <% For Each msg As String In Me._messageWar %>
            <%=msg %><br />
        <% Next %>
    </strong>
  </div>
</div>
<% End If %>
