<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MRA-FE-0011.aspx.vb" Inherits="MRA.MRA_FE_0011" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>connect DB sample</title>
        <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="USER_NM" HeaderText="User Name" ItemStyle-Width="100" />
            <asp:BoundField DataField="CRT_DT" HeaderText="Create Date" ItemStyle-Width="150" />
            <asp:BoundField DataField="CRT_USER_ID" HeaderText="Create User ID" ItemStyle-Width="150" />
        </Columns>
    </asp:GridView>
        </div>
    </form>
</body>
</html>
