<%@ Page Title="Thread" Language="C#" AutoEventWireup="true" CodeFile="ViewThread.aspx.cs" Inherits="_ViewThread" %>


<asp:Content ID="Main" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <%-- Display thread content --%>
    <uc:DisplayThread runat="server" ID="DisplayThread" />

    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="alert alert-error" />
    <asp:ValidationSummary ID="InsertValidationSummary" runat="server" ValidationGroup="InsertValidationGroup" CssClass="alert alert-error" />

    <%-- Get posts from thread --%>
    <asp:ObjectDataSource 
        ID="PostObjectDataSource" runat="server" 
        SelectMethod="GetPosts"
        TypeName="Service" 
        DataObjectTypeName="Post" 
        InsertMethod="SavePost" 
        OnInserting="PostObjectDataSource_Inserting"
        OnSelected="PostObjectDataSource_Selected" 
        DeleteMethod="DeletePost" 
        UpdateMethod="SavePost"
        OnInserted="PostObjectDataSource_Inserted"
        OnUpdating="PostObjectDataSource_Updating"
        OnUpdated="PostObjectDataSource_Updated"
        OnDeleted="PostObjectDataSource_Deleted">

        <%-- Use querystring as parameter --%>
        <SelectParameters>
            <asp:QueryStringParameter Name="threadID" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <%-- Display posts --%>
    <asp:ListView ID="PostListView" runat="server" 
        InsertItemPosition="LastItem" 
        DataSourceID="PostObjectDataSource" 
        DataKeyNames="PostID">
        <LayoutTemplate>
            <asp:Panel ID="ThreadPanel" ClientIDMode="Static" runat="server">
                <span runat="server" id="ItemPlaceholder" />
            </asp:Panel>

            <div id="Paging">
                <asp:DataPager ID="DataPager" runat="server">
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>

        <EmptyDataTemplate>
            <span>No posts exist in this thread.</span>
        </EmptyDataTemplate>
        
        <InsertItemTemplate>
            <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />

            <%-- Text --%>
            <asp:TextBox ID="PostTextBox" runat="server" Text='<%# Bind("Text") %>' TextMode="MultiLine" />
            <asp:RequiredFieldValidator 
                ID="PostRequiredFieldValidator" 
                runat="server" 
                ErrorMessage="Textarea canot be empty." 
                ControlToValidate="PostTextBox"
                Display="None"
                ValidationGroup="InsertValidationGroup" />

            <asp:Button ID="InsertButton" runat="server" Text="Post response" CommandName="Insert" CssClass="btn btn-primary btn-large"  />
        </InsertItemTemplate>

        <ItemTemplate>
            <div class="post">

                <%-- Text --%>
                <p><asp:Literal ID="TextLiteral" runat="server" Text='<%# Eval("Text") %>' /></p>

                <%-- Standing --%>
                <asp:Literal ID="ThreadCategoryLiteral" runat="server" Text='<%# Eval("Standing") %>' />

                <%-- UserName --%>
                <asp:Literal ID="UserNameLiteral" runat="server" Text='<%# Eval("UserName") %>' />

                <%-- Date --%>
                <asp:Literal ID="DateLiteral" runat="server" Text='<%# Eval("Date") %>' />

                <div id>
                    <asp:Button CommandName="Edit" ID="ChangeButton" runat="server" Text="change" CssClass="btn btn-warning btn-small"/>
                    <asp:Button CommandName="Delete" ID="DeleteButton" runat="server" Text="x" CssClass="btn btn-danger btn-small" />
                </div>
            </div>
        </ItemTemplate>


        <EditItemTemplate>

            <div class="post">
                <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' Enabled="false"/>

                <%-- Text --%>
                <asp:TextBox ID="PostTextBox" runat="server" Text='<%# Bind("Text") %>' TextMode="MultiLine" />
                <asp:RequiredFieldValidator 
                    ID="PostRequiredFieldValidator" 
                    runat="server" 
                    ErrorMessage="Textarea canot be empty." 
                    ControlToValidate="PostTextBox"
                    Display="None" />

                <asp:Button ID="UpdateButton" runat="server" Text="Save changes" CommandName="Update" CssClass="btn btn-success btn-small"  />
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-small"  />
            </div>
        </EditItemTemplate>

        
    </asp:ListView>
</asp:Content>

