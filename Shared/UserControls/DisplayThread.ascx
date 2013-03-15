<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DisplayThread.ascx.cs" Inherits="Shared_UserControls_DisplayThread" %>


<div class="thread">

    <%-- Title --%>
    <asp:HyperLink 
        ID="EnterThreadHyperLink" runat="server"
        NavigateUrl='<%# "~/ViewThread.aspx?id=" + ThreadObject.ThreadID %>'>
        <h3><asp:Literal ID="TitleLiteral" runat="server" Text='<%# ThreadObject.Title %>' /></h3>
    </asp:HyperLink>
    
    <%-- Text --%>
    <p><asp:Literal ID="TextLiteral" runat="server" Text='<%# ThreadObject.Text %>' /></p>
                

    <%-- Thread Info --%>
    <div class="thread-info">
        <%-- ThreadCategory --%>                
        <span class="thread-category"><asp:Literal ID="ThreadCategoryLiteral" runat="server" Text='<%# ThreadObject.ThreadCategoryID %>' /></span>

        <%-- UserName --%>
        <span class="username"><asp:Literal ID="UserNameLiteral" runat="server" Text='<%# ThreadObject.UserName %>' /></span>

        <%-- Date --%>                    
        <span class="date"><asp:Literal ID="DateLiteral" runat="server" Text='<%# ThreadObject.Date %>' /></span>
    </div>

    <%-- Change/Delete -
    <div class="thread-buttons">
        <asp:Button CommandName="Edit" ID="ChangeButton" runat="server" Text="change" CssClass="btn btn-warning btn-small"/>
        <asp:Button CommandName="Delete" ID="DeleteButton" runat="server" Text="x" CssClass="btn btn-danger btn-small"/>
    </div>
    --%>
</div>