<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftSidebar.ascx.cs" Inherits="FlyerMe.Admin.Controls.MasterPage.LeftSidebar" %>
<%@ Register Src="~/Admin/Controls/MasterPage/Header.ascx" TagPrefix="uc" TagName="Header" %>
<div id="left">
    <div class="main-menu" id="divMainMenu" runat="server">
        <asp:HyperLink runat="server" NavigateUrl="~/admin"><span id="spanMainMenu" runat="server" visible="false"></span>Home</asp:HyperLink>
    </div>
    <ul>
        <% foreach (var i in Items) 
            {
               if (i.Value == ActiveItemLevel1)
               {
        %>
            <li class="active">
            <% if (i.Value.Items != null)
                {
            %>
                <a class="active" href="<%= ResolveUrl("~/admin/" + i.Value.Value + "/" + i.Value.Items.First().Value.Value + ".aspx") %>">
                    <%= i.Value.Text %>
                </a>
                <ul>
                    <% foreach (var i2 in ActiveItemLevel1.Items)
                        {
                            if (i2.Value == ActiveItemLevel2)
                            {
                    %>
                    <li>
                        <a href="<%= ResolveUrl("~/admin/" + i.Value.Value + "/" + i2.Value.Value + ".aspx") %>" class="active">
                            <%= i2.Value.Text %>
                        </a>
                    </li>
                    <%
                            }
                            else
                            {
                    %>
                    <li>
                        <a href="<%= ResolveUrl("~/admin/" + i.Value.Value + "/" + i2.Value.Value + ".aspx") %>">
                            <%= i2.Value.Text %>
                        </a>
                    </li>
                    <%
                            }
                        }
                    %>
                </ul>
            <%
                }
                else
                {
            %>
                <a class="active" href="<%= ResolveUrl("~/admin/" + i.Value.Value + ".aspx") %>">
                    <%= i.Value.Text %>
                </a>
            <%
                }
            %>
            </li>
            <%
               }
               else
               {
            %>
            <li>
                <% if (i.Value.Items != null)
                   {
                %>
                <a href="<%= ResolveUrl("~/admin/" + i.Value.Value + "/" + i.Value.Items.First().Value.Value + ".aspx") %>">
                    <%= i.Value.Text %>
                </a>
                <%
                   }
                   else
                   {
                %>
                <a href="<%= ResolveUrl("~/admin/" + i.Value.Value + ".aspx") %>">
                    <%= i.Value.Text %>
                </a>
                <%
                   } 
                %>
            </li>
            <%
               }
        %>
        <%  
            } 
        %>
    </ul>
</div>