using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Thread
/// </summary>
[Serializable]
public class Thread : BusinessObjectBase
{
    #region Fields
    private string _title;
    private string _text;
    private string _userName;
    #endregion

    #region Properties
    public int ThreadID { get; set; }

    // Properties are according to what the SPROC returns.
    // These are ID:s in the database.
    public int ThreadCategoryID { get; set; }
    public string UserName
    {
        get { return _userName; }
        set
        {
            this.ValidationErrors.Remove("UserName");

            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("UserName", Strings.ValidationError_UserName_Empty);
            }
            else if (value.Length > 15)
            {
                this.ValidationErrors.Add("UserName", Strings.ValidationError_UserName_TooLong);
            }

            _userName = value != null ? value.Trim() : null;
        }
    } 

    public DateTime Date { get; set; }

    public string Title
    {
        get { return _title; }
        set 
        {
            this.ValidationErrors.Remove("Title");

            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("Title", Strings.ValidationError_Title_Empty);
            }
            else if (value.Length > 50)
            {
                this.ValidationErrors.Add("Title", Strings.ValidationError_Title_TooLong);
            }
            _title = value != null ? value.Trim() : null; 
        }
    }

    public string Text
    {
        get { return _text; }
        set 
        {
            this.ValidationErrors.Remove("Text");

            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("Text", Strings.ValidationError_Text_Empty);
            }
            else if (value.Length > 4000)
            {
                this.ValidationErrors.Add("Text", Strings.ValidationError_Text_TooLong);
            }
            _text = value != null ? value.Trim() : null; 
        }
    }
   
    #endregion

    public Thread()
    {
        // Set properties to null to prevent bypassing validation logic
        this.Title = null;
        this.Text = null;

        // TODO Implement category-picker and remove this.
        this.ThreadCategoryID = 1;

        // TODO Implement login and remove this.
        this.UserName = "User";
    }
}