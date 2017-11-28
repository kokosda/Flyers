<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="FlyerMe.Admin.Controls.MasterPage.Footer" %>
<footer>
    <div class="footer">
        © <%= DateTimeOffset.Now.Year %> <%= clsUtility.ProjectName %>. All rights reserved
    </div>
</footer>