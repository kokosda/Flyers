<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActionCell.ascx.cs" Inherits="FlyerMe.Admin.Controls.Grid.ActionCellControl" %>
<%@ Register Src="~/Admin/Controls/Grid/DataCell.ascx" TagPrefix="uc" TagName="DataCell" %>
<%@ Register Src="~/Admin/Controls/Grid/DataCellsList.ascx" TagPrefix="uc" TagName="DataCellsList" %>
<td id="tdCell" runat="server">
    <asp:Literal ID="ltlText" runat="server"></asp:Literal>
    <asp:LinkButton ID="lbAction" runat="server" OnCommand="lbAction_Command"></asp:LinkButton>
    <asp:LinkButton ID="lbCancel" runat="server" OnCommand="lbAction_Command"></asp:LinkButton>
    <asp:HiddenField ID="hfActionState" runat="server"></asp:HiddenField>
</td>