<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSCRUD.aspx.cs" Inherits="Jan2018DemoWebsite.DemoPages.ODSCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ODS CRUD of Albums</h1>

    <!-- Deletion requires DataKeyNames parameter -->
    <asp:ListView ID="Albums" runat="server" 
        DataSourceID="Albums_ODS" 
        InsertItemPosition="LastItem" 
        DataKeyNames="AlbumID">
        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF; color: #284775;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumID") %>' runat="server" ID="AlbumIDLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ArtistID") %>' runat="server" ID="ArtistIDLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color: #999999;">
                <td>
                    <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumID") %>' runat="server" ID="AlbumIDTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ArtistID") %>' runat="server" ID="ArtistIDTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBox" /></td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumID") %>' runat="server" ID="AlbumIDTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ArtistID") %>' runat="server" ID="ArtistIDTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBox" /></td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #E0FFFF; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumID") %>' runat="server" ID="AlbumIDLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ArtistID") %>' runat="server" ID="ArtistIDLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                <th runat="server"></th>
                                <th runat="server">AlbumID</th>
                                <th runat="server">Title</th>
                                <th runat="server">ArtistID</th>
                                <th runat="server">ReleaseYear</th>
                                <th runat="server">ReleaseLabel</th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                        <asp:DataPager runat="server" ID="DataPager1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                <asp:NumericPagerField></asp:NumericPagerField>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumID") %>' runat="server" ID="AlbumIDLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ArtistID") %>' runat="server" ID="ArtistIDLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>

    <asp:ObjectDataSource 
        ID="Albums_ODS" runat="server" 
        DataObjectTypeName="Chinook.Data.Entities.Album" 
        OldValuesParameterFormatString="original_{0}" 
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add" 
        SelectMethod="Albums_ToList" 
        UpdateMethod="Album_Update"
        TypeName="ChinookSystem.BLL.AlbumController">
    </asp:ObjectDataSource>
</asp:Content>
