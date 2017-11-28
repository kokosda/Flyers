<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Head.ascx.cs" Inherits="FlyerMe.Admin.Controls.Grid.Head" %>
<%@ Import Namespace="FlyerMe" %>
<thead>
    <tr>
        <asp:Repeater ID="rptHead" runat="server" OnItemDataBound="rptHead_ItemDataBound">
            <ItemTemplate>
                <th id="thHeaderCell" runat="server">
                    <asp:Literal ID="ltlText" runat="server"></asp:Literal>
                </th>
            </ItemTemplate>
        </asp:Repeater>
    </tr>
</thead>