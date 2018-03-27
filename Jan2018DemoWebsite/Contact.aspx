<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Jan2018DemoWebsite.Contact" %>
<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>

    <div class="row">
        <div class="col-sm-12">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>

    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
        <br />
        <asp:Label ID="UsernameLabel" runat="server" Text="Username:" Font-Bold="True"></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="Username" runat="server"></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="EmployeeID" runat="server"></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="EmployeeName" runat="server"></asp:Label>&nbsp;&nbsp;
        <asp:LinkButton ID="GetUsername" runat="server" OnClick="GetUsername_Click">Get Username</asp:LinkButton>
    </address>
</asp:Content>
