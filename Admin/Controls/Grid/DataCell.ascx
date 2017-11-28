<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataCell.ascx.cs" Inherits="FlyerMe.Admin.Controls.Grid.DataCellControl" %>
<%@ Import Namespace="FlyerMe" %>
<%@ Import Namespace="FlyerMe.Admin.Models" %>
<td id="tdCell" runat="server">
    <asp:Literal ID="ltlText" runat="server"></asp:Literal>
    <% if (DataInputType == DataInputTypes.Checkbox) { %>
        <input type="checkbox" id="gridInputCheckbox" runat="server" /><asp:Label runat="server" AssociatedControlID="gridInputCheckbox"></asp:Label>
    <% } else if (DataInputType == DataInputTypes.Text) { %>
    <div class="item">
        <input type="text" id="gridInputText" runat="server" />
    </div>
    <% } else if (DataInputType == DataInputTypes.Hidden) { %>
        <input type="hidden" id="gridInputHidden" runat="server" />
    <% } %>
    <% else if (DataInputType == DataInputTypes.Select) { %>
    <div class="item">
        <asp:DropDownList ID="gridDdl" runat="server"></asp:DropDownList>
    </div>
    <% } else if (DataInputType == DataInputTypes.Submit) { %>
        <asp:Button ID="gridSubmit" runat="server" OnCommand="gridSubmit_Command" />
    <% } %>
    <% if (DataInputType != DataInputTypes.None) { %>
    <asp:HiddenField ID="hfInputState" runat="server" />
    <% } %>
</td>