<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Subscribe.aspx.cs" Inherits="FlyerMe.Subscribe" Async="true" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-content" id="divContentSubscribe" runat="server">
        <div class="content subscribe">
            <h1>Subscribe & Start Receiving  Our Email Flyers</h1>
            <p>Not receiving our Email Flyer Campaigns? No problem at all. Just fill in this form with your marketing area and we will start delivering our Email Flyers to your inbox soon.</p>

            <p><strong>Interested in sending flyers to other agents? <a href="<%= RootURL %>createflyer.aspx">Start here</a></strong></p>
            <form runat="server" method="post" enctype="application/x-www-form-urlencoded">
                <div class="form-content">
                    <div class="item">
                        <label>Email<span class="required">*</span></label>
                        <input type="text" id="inputEmail" runat="server" data-clientname="Email" />
                    </div>
                    <div class="item">
                        <label>First Name<span class="required">*</span></label>
                        <input type="text" id="inputFirstName" runat="server" data-clientname="FirstName" />
                    </div>
                    <div class="item">
                        <label>Middle Name</label>
                        <input type="text" id="inputMiddleName" runat="server" />
                    </div>
                    <div class="item">
                        <label>Last Name<span class="required">*</span></label>
                        <input type="text" id="inputLastName" runat="server" data-clientname="LastName" />
                    </div>
                    <div class="item">
                        <label>State<span class="required">*</span></label>
                        <asp:DropDownList ID="ddlStates" runat="server" DataSourceID="odsStates" DataTextField="StateName" DataValueField="StateAbr" OnDataBound="ddlStates_DataBound" AutoPostBack="True" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsStates" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetStates" TypeName="FlyerMe.StatesBLL"></asp:ObjectDataSource>
                    </div>
                    <div class="item">
                        <label>County</label>
                        <asp:DropDownList ID="ddlCounty" runat="server" DataSourceID="sqlDataSourceCounty" DataTextField="market" DataValueField="market"></asp:DropDownList>
                        <asp:SqlDataSource ID="sqlDataSourceCounty" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT [market] FROM [fly_county_listsize] WHERE ([state] = @state) order by market">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlStates" Name="state" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                    <div class="item">
                        <label>Association Name</label>
                        <asp:DropDownList ID="ddlAssociationName" runat="server" DataSourceID="sqlDataSourceAssociation" DataTextField="market" DataValueField="market"></asp:DropDownList>
                        <asp:SqlDataSource ID="sqlDataSourceAssociation" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT [market] FROM [fly_association_listsize] WHERE ([state] =@state) order by [market]">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlStates" Name="state" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                    <div class="item">
                        <label>MSA Name</label>
                        <asp:DropDownList ID="ddlMsaName" AppendDataBoundItems="true" runat="server" DataSourceID="sqlDataSourceMSA" DataTextField="market" DataValueField="market">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sqlDataSourceMSA" runat="server" ConnectionString="<%$ ConnectionStrings:projectDBConnectionString %>" SelectCommand="SELECT [market] FROM [fly_msa_listsize] WHERE ([state] =@state) order by [market]">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlStates" Name="state" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                    <asp:Button ID="btnSubscribe" runat="server" Text="Subscribe" OnClick="btnSubscribe_Click" />
	            </div>
            </form>
	    </div>
    </div>
    <div class="sendm" id="divSuccess" runat="server" visible="false">
    	<div class="icon ok"></div>
    	<h1>Almost there</h1>
        <div class="text">
            <p>We've just sent a verification email to <%= Request["email"] %>. This email contains a verification link  that you'll need to click before your subscription becomes active.</p>
            <p>If you have any questions or didn't receive your confirmation email, please <a href="<%= RootURL %>contacts.aspx">get in touch</a> with us.</p>
        </div>
    </div>
</asp:Content>