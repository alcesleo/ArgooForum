using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _ViewThread : System.Web.UI.Page
{
    /// <summary>
    /// Encapsulate ThreadID querystring.
    /// </summary>
    private int ThreadID
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["id"]);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // TODO Thread does not exist-page

        // Set properties of Thread-box
        // TODO Get thread without querying the database, not necessary
        var db = new Service();
        var thread = db.GetThreadByID(ThreadID);

        if (thread != null)
        {
            DisplayThread.ThreadObject = thread;
            DisplayThread.DataBind();
        }
        else
        {
            Page.AddErrorMessage(Strings.PageError_Posts_ThreadDoesNotExist);
        }

        
    }


    protected void PostObjectDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {

        // Get the post object that is being inserted
        var post = (Post)e.InputParameters[0];

        if (post.IsValid)
        {
            // Set its ThreadID to the current thread
            post.ThreadID = ThreadID;
        }
        else
        {
            // Prevent inserting invalid object
            Page.AddErrorMessage(post, "InsertValidationGroup");
            e.Cancel = true;
        }
        
    }

    protected void PostObjectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        // Don't show listview if posts could not be retrieved
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.PageError_Posts_NotRetrieved);
            PostListView.Visible = false;
            e.ExceptionHandled = true;
        }
    }

    protected void PostObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.PageError_Posts_NotPosted);
            e.ExceptionHandled = true;
        }
    }


    protected void PostObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        var post = (Post)e.InputParameters[0];

        if (!post.IsValid)
        {
            Page.AddErrorMessage(post);
            e.Cancel = true;
        }
    }

    protected void PostObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.PageError_Posts_NotUpdated);
            e.ExceptionHandled = true;
        }
    }


    protected void PostObjectDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.PageError_Posts_NotDeleted);
            e.ExceptionHandled = true;
        }
    }
}