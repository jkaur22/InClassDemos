<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SpecialEventsAdmin.aspx.cs" Inherits="SamplePages_SpecialEventsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Special Events Administration</h1>
        <table align="center" style="width: 80%">
            <tr>
                <td align="right" style="width:50%">Select an Event: &nbsp;&nbsp;</td>
                <td>
                    <asp:DropDownList ID="SpecialEventList" runat="server" Width="200px" DataSourceID="ODSSpecialEvents" DataTextField="Description" DataValueField="EventCode" AppendDataBoundItems="True">
                        <asp:ListItem Value="z">Select Event</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="FetchReservations" runat="server">Fetch Reservations</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="ReservationListGV" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ODSReservations" PageSize="5">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:BoundField DataField="CustomerName" HeaderText="Name" SortExpression="CustomerName" />
                            <asp:BoundField DataField="ReservationDate" DataFormatString="{0:MMM dd, yyyy}" HeaderText="Date" SortExpression="ReservationDate" />
                            <asp:BoundField DataField="NumberInParty" HeaderText="Size" SortExpression="NumberInParty" />
                            <asp:BoundField DataField="ContactPhone" HeaderText="Phone" SortExpression="ContactPhone">
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ReservationStatus" HeaderText="Status" SortExpression="ReservationStatus">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            No data to Display at this time.
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="Gray" Font-Size="Large" />
                        <PagerSettings Position="TopAndBottom" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align ="center">
                    <asp:DetailsView ID="ReservationListDV" runat="server" Height="50px" Width="125px" AllowPaging="True" DataSourceID="ODSReservations">
                        <EmptyDataTemplate>
                            No Data to Display at this time.
                        </EmptyDataTemplate>
                    </asp:DetailsView>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SpecialEvent_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSReservations" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetReservationsByEventCode" TypeName="eRestaurantSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="SpecialEventList" Name="eventcode" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

