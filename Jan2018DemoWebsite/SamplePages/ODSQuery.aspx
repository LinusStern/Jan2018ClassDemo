<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSQuery.aspx.cs" Inherits="Jan2018DemoWebsite.SamplePages.ODSQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>ODS Query</h3>

    <div class="row">
        <!-- Data grid -->
        <asp:GridView 
            ID="Albums" runat="server" 
            AutoGenerateColumns="False" 
            DataSourceID="Albums_ODS" 
            AllowPaging="True" PageSize="15" 
            BorderStyle="None" GridLines="Horizontal" 
            CellPadding="5" CellSpacing="2" 
            OnSelectedIndexChanged="Albums_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="ID" SortExpression="AlbumId">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("AlbumId") %>' ID="AlbumID_Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("Title") %>' ID="Label2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Artist" SortExpression="ArtistID">
                    <ItemTemplate>
                        <asp:DropDownList 
                            ID="Artists" runat="server" 
                            DataSourceID="Artists_ODS" 
                            DataTextField="Name" DataValueField="ArtistId" 
                            SelectedValue='<%# Eval("ArtistId") %>' 
                            Width="360px">
                        </asp:DropDownList>
                        <%--<asp:Label runat="server" Text='<%# Bind("ArtistID") %>' ID="Label3"></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Released" SortExpression="ReleaseYear">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ReleaseYear") %>' ID="Label4"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Label" SortExpression="ReleaseLabel">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ReleaseLabel") %>' ID="Label5"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField HeaderText="Options" 
                    SelectText="View" ShowSelectButton="True">
                </asp:CommandField>
            </Columns>
        </asp:GridView>

        <!-- Data sources -->
        <asp:ObjectDataSource 
            ID="Albums_ODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="Albums_ToList" 
            TypeName="ChinookSystem.BLL.AlbumController">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource 
            ID="Artists_ODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="Artists_ToList" 
            TypeName="ChinookSystem.BLL.ArtistController">
        </asp:ObjectDataSource>
    </div>

    <!-- Pulled from text file -->
     <div class="row">
        <asp:Button ID="CountAlbums" runat="server" Text="Count Albums" OnClick="CountAlbums_Click"  />&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Number of Albums per Artist"></asp:Label>
    </div>

    <div>
        <asp:ListView ID="ArtistAlbumCountList" runat="server"
              ItemType="Chinook.Data.POCOs.ArtistAlbumCounts">
            <LayoutTemplate>
                <div >
                    <span runat="server" id="itemPlaceholder" />
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <div>
                    <asp:DropDownList ID="ArtistList2" runat="server" 
                        DataSourceID="Artists_ODS" 
                        DataTextField="Name" DataValueField="ArtistId"
                        SelectedValue ='<%# Item.ArtistId %>'
                        Enabled="False">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <%# Item.AlbumCount %> 
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
