<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WaiterAdmin.aspx.cs" Inherits="CommandPages_WaiterAdmin" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Waiter Admin</h1>
    <br />
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Select Waiter for Update"></asp:Label>&nbsp;&nbsp;
    <asp:DropDownList ID="WaiterList" runat="server" DataSourceID="ODSWaiters" DataTextField="FullName" DataValueField="WaiterID" AppendDataBoundItems="True">
        <asp:ListItem Value="0">Select Waiter</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;
    <asp:LinkButton ID="FetchWaiter" runat="server" OnClick="FetchWaiter_Click">Fetch Waiter</asp:LinkButton>
    <table align="center" style="width: 70%">
        <tr>
            <td>ID:</td>
            <td>
                <asp:Label ID="WaiterID" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>FirstName:</td>
            <td>
                <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>LastName:</td>
            <td>
                <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>PhoneNumber:</td>
            <td>
                <asp:TextBox ID="Phone" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Address:</td>
            <td>
                <asp:TextBox ID="Address" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Date Hired (mm/dd/yyyy)</td>
            <td>
                <asp:TextBox ID="DateHired" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Date Released (mm/dd/yyyy)</td>
            <td>
                <asp:TextBox ID="DateReleased" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="InsertWaiter" runat="server">Insert</asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton ID="UpdateWaiter" runat="server">Update</asp:LinkButton>
            </td>
        </tr>
    </table>

    <asp:ObjectDataSource ID="ODSWaiters" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Waiters_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
    <br />
</asp:Content>

