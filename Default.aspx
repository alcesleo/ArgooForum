<%@ Page Title="Frontpage - Argoo Forum" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="ThreadContent" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <%-- Notifications --%>
    <asp:ValidationSummary ID="InsertValidationSummary" runat="server" ValidationGroup="InsertValidationGroup" CssClass="alert alert-error" />

    <asp:Panel ID="NotificationPanel" runat="server" Visible="false">
        <asp:Label ID="NotificationLabel" runat="server" />
    </asp:Panel>
    
    <%-- Display threads --%>
    <asp:ObjectDataSource 
        ID="ThreadObjectDataSource" runat="server" 
        SelectMethod="GetThreads" 
        TypeName="Service" 
        DataObjectTypeName="Thread" 
        DeleteMethod="DeleteThread" 
        InsertMethod="SaveThread" 
        UpdateMethod="SaveThread"
        OnSelected="ThreadObjectDataSource_Selected"
        OnInserting="ThreadObjectDataSource_Inserting"
        OnInserted="ThreadObjectDataSource_Inserted"
        OnUpdating="ThreadObjectDataSource_Updating"
        OnUpdated="ThreadObjectDataSource_Updated"
        OnDeleted="ThreadObjectDataSource_Deleted">
    </asp:ObjectDataSource>

    <asp:ListView 
        ID="ThreadListView" 
        runat="server" 
        DataSourceID="ThreadObjectDataSource"
        DataKeyNames="ThreadID"
        InsertItemPosition="FirstItem">
        
        <%-- Layout --%>
        <LayoutTemplate>
            <asp:Panel ID="ThreadPanel" ClientIDMode="Static" runat="server">
                <span runat="server" id="ItemPlaceholder" />
            </asp:Panel>

            <div id="Paging">
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

        <%-- Not found --%>
        <EmptyDataTemplate>
            <span>No threads were found.</span>
        </EmptyDataTemplate>

        <%-- New thread --%>
        <InsertItemTemplate>
            <div class="well">
                <%-- Title --%>
                <asp:TextBox ID="TitleTextBox" Text='<%# Bind("Title") %>' MaxLength="50" runat="server" placeholder="Title"/>
                <asp:RequiredFieldValidator 
                    ID="TitleRequiredFieldValidator" runat="server" 
                    ErrorMessage="Title cannot be empty." 
                    ControlToValidate="TitleTextBox" 
                    Display="None" 
                    ValidationGroup="InsertValidationGroup"/>

                <%-- Username --%>
                <asp:TextBox ID="UserNameTextBox" Text='<%# Bind("UserName") %>' MaxLength="15" runat="server" placeholder="Username" />
                <asp:RequiredFieldValidator 
                    ID="UserNameRequiredFieldValidator" runat="server" 
                    ErrorMessage="Username cannot be empty." 
                    ControlToValidate="TitleTextBox" 
                    Display="None" 
                    ValidationGroup="InsertValidationGroup"/>

                <%-- TODO Category --%>
                <asp:DropDownList ID="DropDownList" runat="server"></asp:DropDownList>

                <%-- Text --%>
                <asp:TextBox ID="BodyTextBox" Text='<%# Bind("Text") %>' TextMode="MultiLine" runat="server" />
                <asp:RequiredFieldValidator 
                    ID="BodyRequiredFieldValidator" runat="server" 
                    ErrorMessage="Textarea cannot be empty." 
                    ControlToValidate="BodyTextBox" 
                    Display="None" 
                    ValidationGroup="InsertValidationGroup"/>

                <%-- Button --%>
                <asp:Button ID="InsertButton" runat="server" Text="Post thread" CommandName="Insert" CssClass="btn btn-primary btn-large"  />
            </div>
        </InsertItemTemplate>
        
        <%-- Thread list --%>
        <ItemTemplate>
            <uc:DisplayThread runat="server" 
                ID="DisplayThread"
                ThreadObject='<%# Container.DataItem %>'/>

        </ItemTemplate>

        
    </asp:ListView>
</asp:Content>

