<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeClientSupport.aspx.cs" Inherits="Jan2018DemoWebsite.SamplePages.EmployeeClientSupport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>ODS Repeater with LINQ Query</h3>

    <div class="row" style="padding-left:16px">
        <asp:Repeater
            ID="ECS" runat="server" 
            DataSourceID="ECS_ODS"
            ItemType="Chinook.Data.DTOs.EmployeeClients">
            <HeaderTemplate>
                <h3>My Header</h3>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="row">
                    <!-- Results as a grid -->
                    <div class="col-md-5">
                        <!-- Employee name & Number of clients -->
                        <%# Item.Name %> (<%# Item.ClientCount %>)

                        <!-- Client data -->
                        <asp:GridView 
                            ID="ClientGrid" runat="server" 
                            DataSource="<%# Item.ClientList %>">
                        </asp:GridView>
                        <br />
                    </div>

                    <!-- Results as a list -->
                    <div class="col-md-5">
                        <br />
                        <asp:ListView 
                            ID="ClientList" runat="server" 
                            DataSource="<%# Item.ClientList %>">
                            <ItemTemplate> <!-- Encapsulating <tr> tag ensures columns repeat vertically -->
                                <tr>
                                    <td>
                                        <asp:Label Text='<%# Eval("Client") %>' runat="server" ID="ClientLabel" /></td>
                                    <td>
                                        <asp:Label Text='<%# Eval("Phone") %>' runat="server" ID="PhoneLabel" /></td>
                                </tr>
                            </ItemTemplate>

                            <LayoutTemplate>
                                <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                                <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                                    <th runat="server">Client</th>
                                                    <th runat="server">Phone</th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder"></tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                            <asp:DataPager runat="server" ID="DataPager1" PageSize="10">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:ObjectDataSource
            ID="ECS_ODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="Employee_GetClientList" 
            TypeName="ChinookSystem.BLL.EmployeeController">
        </asp:ObjectDataSource>
    </div>
</asp:Content>
