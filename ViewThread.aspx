<%@ Page Title="Thread" Language="C#" AutoEventWireup="true" CodeFile="ViewThread.aspx.cs" Inherits="_ViewThread" %>


<asp:Content ID="Main" ContentPlaceHolderID="MainContent" Runat="Server">

    <%-- Display thread content --%>
    <div class="well">
        <uc:DisplayThread runat="server" ID="DisplayThread" Editable="true" />
    </div>

    <%-- Notifications --%>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="alert alert-error" />
    <asp:ValidationSummary ID="InsertValidationSummary" runat="server" ValidationGroup="InsertValidationGroup" CssClass="alert alert-error" />
    <%-- TODO Put this in master-page? --%>
     <asp:Panel ID="NotificationPanel" runat="server" Visible="false">
        <asp:Label ID="NotificationLabel" runat="server" />
    </asp:Panel>

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

            <div id="Paging" class="pull-right">
                <asp:DataPager ID="DataPager" PageSize="10" QueryStringField="page" runat="server">
                    <Fields>
                        <asp:NextPreviousPagerField 
                            ButtonType="Link" 
                            ButtonCssClass="label" 
                            ShowFirstPageButton="False" 
                            ShowNextPageButton="False" 
                            ShowPreviousPageButton="True" />

                        <asp:NumericPagerField 
                            ButtonType="Link" 
                            CurrentPageLabelCssClass="badge badge-info" 
                            NumericButtonCssClass="badge" />

                        <asp:NextPreviousPagerField 
                            ButtonType="Link" 
                            ButtonCssClass="label" 
                            ShowLastPageButton="False" 
                            ShowNextPageButton="True" 
                            ShowPreviousPageButton="False" />
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>
        
        <InsertItemTemplate>
            <div class="well">

                <%-- Text --%>
                <asp:TextBox ID="PostTextBox" runat="server" Text='<%# Bind("Text") %>' TextMode="MultiLine" />
                <asp:RequiredFieldValidator 
                    ID="PostRequiredFieldValidator" 
                    runat="server" 
                    ErrorMessage="Textarea canot be empty." 
                    ControlToValidate="PostTextBox"
                    Display="None"
                    ValidationGroup="InsertValidationGroup" />

                <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' placeholder="Username" />
                <asp:RequiredFieldValidator 
                    ID="UserNameRequiredFieldValidator" 
                    runat="server" 
                    ErrorMessage="Username canot be empty." 
                    ControlToValidate="UserNameTextBox"
                    Display="None"
                    ValidationGroup="InsertValidationGroup" />

                <%-- TODO Standing radiobuttons --%>

                <%-- Submit --%>
                <asp:Button ID="InsertButton" runat="server" Text="Submit" CommandName="Insert" CssClass="btn btn-primary btn-large"  />
            </div>
        </InsertItemTemplate>

        <ItemTemplate>
            <div class="well">

                <%-- Text --%>
                <p><asp:Literal ID="TextLiteral" runat="server" Text='<%# Eval("Text") %>' /></p>

                <%-- Standing --%>
                <asp:Literal ID="ThreadCategoryLiteral" runat="server" Text='<%# Eval("Standing") %>' />

                <%-- UserName --%>
                <asp:Literal ID="UserNameLiteral" runat="server" Text='<%# Eval("UserName") %>' />

                <%-- Date --%>
                <asp:Literal ID="DateLiteral" runat="server" Text='<%# Eval("Date") %>' />

                <div class="btn-group pull-right">
                    <asp:Button CommandName="Edit" ID="ChangeButton" runat="server" Text="change" CssClass="btn btn-small"/>
                    <asp:Button CommandName="Delete" ID="DeleteButton" runat="server" Text="x" CssClass="btn btn-danger btn-small" />
                </div>
            </div>
        </ItemTemplate>


        <EditItemTemplate>

            <div class="well">

                <%-- Text --%>
                <asp:TextBox ID="PostTextBox" runat="server" Text='<%# Bind("Text") %>' TextMode="MultiLine" />
                <asp:RequiredFieldValidator 
                    ID="PostRequiredFieldValidator" 
                    runat="server" 
                    ErrorMessage="Textarea canot be empty." 
                    ControlToValidate="PostTextBox"
                    Display="None" />

                <%-- Username (cannot be edited) --%>
                <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Eval("UserName") %>' Enabled="false"/>

                <%-- Buttons --%>
                <div class="btn-group pull-right">
                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-small"  />
                    <asp:Button ID="UpdateButton" runat="server" Text="Save changes" CommandName="Update" CssClass="btn btn-success btn-small"  />
                </div>
            </div>
        </EditItemTemplate>

        
    </asp:ListView>
</asp:Content>

