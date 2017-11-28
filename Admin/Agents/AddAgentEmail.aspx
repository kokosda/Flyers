<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAgentEmail.aspx.cs" Inherits="FlyerMe.Admin.Agents.AddAgentEmail" Theme="" MasterPageFile="~/Admin/MasterPage.master" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <div class="back color">
                    <asp:HyperLink ID="hlBackToAgents" runat="server"><span></span>Back to agents</asp:HyperLink>
                </div>
                <uc:Message id="message" runat="server" />
                <form method="post" enctype="application/x-www-form-urlencoded" runat="server">
                    <div class="add-agents">
                        <div class="item-box">
                            <div class="item">
                                <label>Email<span class="required">*</span></label>
                                <input type="text" id="inputEmail" runat="server" required />
                            </div>
                            <div class="item">
                                <label>First Name<span class="required">*</span></label>
                                <input type="text" id="inputFirstName" runat="server" required />
                            </div>
                            <div class="item">
                                <label>Middle Name</label>
                                <input type="text" id="inputMiddleName" runat="server" />
                            </div>
                            <div class="item">
                                <label>Last Name</label>
                                <input type="text" id="inputLastName" runat="server" />
                            </div>
                        </div>
                        <div class="item-box">
                            <div class="item">
                                <label>State<span class="required">*</span></label>
                                <asp:DropDownList ID="ddlState" runat="server" DataSourceID="sdsStates" DataTextField="StateName" DataValueField="StateAbr" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsStates" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT [StateName], [StateAbr] FROM [fly_states] order by [StateName]">
                                </asp:SqlDataSource>
                            </div>
                            <div class="item">
                                <label>County</label>
                                <asp:DropDownList ID="ddlCounty" runat="server" DataSourceID="sdsCounty" DataTextField="market" DataValueField="market">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCounty" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT [market] FROM [fly_county_listsize] WHERE ([state] = @state) order by market">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlState" Name="state" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="item">
                                <label>Association</label>
                                <asp:DropDownList ID="ddlAssociation" runat="server" DataSourceID="sdsAssociation" DataTextField="market" DataValueField="market">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsAssociation" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT [market] FROM [fly_association_listsize] WHERE ([state] = @state) order by market">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlState" Name="state" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="item">
                                <label>MSA Name</label>
                                <asp:DropDownList ID="ddlMsa" runat="server" DataSourceID="sdsMsa" DataTextField="market" DataValueField="market">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsMsa" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" 
                                    SelectCommand="SELECT [market] FROM [fly_msa_listsize] WHERE ([state] = @state) order by market">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlState" Name="state" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="item-box">
                            <div class="item">
                                <label>&nbsp;</label>
                                <div class="chekbox small right">
                                    <input type="checkbox" id="inputUnsubscribed" runat="server" checked />
                                    <asp:Label runat="server" AssociatedControlID="inputUnsubscribed">Unsubscribed</asp:Label>
                                    <input type="checkbox" id="inputSkip" runat="server" checked />
                                    <asp:Label runat="server" AssociatedControlID="inputSkip">Skip</asp:Label>
                                </div>
                            </div>
                            <div class="item">
                                <label>Vresult</label>
                                <input type="text" id="inputVresult" runat="server" />
                            </div>
                            <div class="item">
                                <label>Rcode</label>
                                <input type="text" id="inputRcode" runat="server" />
                            </div>
                        </div>
                        <div class="item-submit">
                            <asp:Button ID="btnAddAgentEmail" runat="server" OnCommand="btnAddAgentEmail_Command" Text="Add Agent Email" />
                        </div>
                    </div>
                </form>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>