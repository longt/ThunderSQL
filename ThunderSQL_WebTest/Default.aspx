<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ThunderSQL_WebTest.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:ListView ID="ListView1" runat="server">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("car_id") %>'></asp:Label>
                </ItemTemplate>
            </asp:ListView>
            <asp:GridView ID="GridView1"   runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
