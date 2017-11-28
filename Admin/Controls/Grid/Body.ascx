<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Body.ascx.cs" Inherits="FlyerMe.Admin.Controls.Grid.Body" %>
<%@ Register Src="~/Admin/Controls/Grid/DataCellsList.ascx" TagPrefix="uc" TagName="DataCellsList" %>
<%@ Register Src="~/Admin/Controls/Grid/ActionCellsList.ascx" TagPrefix="uc" TagName="ActionCellsList" %>
<%@ Import Namespace="FlyerMe" %>
<tbody>
    <asp:Repeater ID="rptBodyRows" runat="server" OnItemDataBound="rptBodyRows_ItemDataBound">
        <ItemTemplate>
            <tr>
                <uc:DataCellsList ID="dataCellsList" runat="server" />
                <uc:ActionCellsList ID="actionCellsList" runat="server" />
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</tbody>