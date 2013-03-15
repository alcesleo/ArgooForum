using Resources;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    /// <summary>
    /// Display a fading notification.
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <param name="alertClass">The bootstrap alert-class suffix. Ex "success", "error", "warning".</param>
    private void DisplayNotification(string message, string alertClass)
    {
        NotificationPanel.Visible = true;
        NotificationPanel.CssClass = String.Format("alert alert-{0}", alertClass);
        NotificationLabel.Text = message;

        // TODO Implement JS-fading of panel.
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // TODO Validate username input?a

    }


    protected void ThreadObjectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        // Do not show listview if threads could not be retrieved.
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.PageError_Thread_NotFound);
            ThreadListView.Visible = false;
            e.ExceptionHandled = true;
        }
    }

    protected void ThreadObjectDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        var thread = (Thread)e.InputParameters[0];

        if (!thread.IsValid)
        {
            Page.AddErrorMessage(thread, "InsertValidationGroup");
            e.Cancel = true;
        }
    }
    protected void ThreadObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            // TODO Redirect to thread
        }
        else
        {
            Page.AddErrorMessage(Strings.PageError_Thread_NotPosted);
            e.ExceptionHandled = true;
        }
    }

    protected void ThreadObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        var thread = (Thread)e.InputParameters[0];

        if (!thread.IsValid)
        {
            Page.AddErrorMessage(thread);
            e.Cancel = true;
        }
    }


    protected void ThreadObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            DisplayNotification(Strings.PageSuccess_Thread_Updated, "success");
        }
        else
        {
            Page.AddErrorMessage(Strings.PageError_Thread_NotUpdated);
            e.ExceptionHandled = true;
        }
    }
    protected void ThreadObjectDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            DisplayNotification(Strings.PageSuccess_Thread_Deleted, "success");
        }
        else
        {
            Page.AddErrorMessage(Strings.PageError_Thread_NotDeleted);
            e.ExceptionHandled = true;
        }
    }
}