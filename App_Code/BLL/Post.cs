using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// The standing table in enum-form.
/// </summary>
public enum Standing
{
    Neutral = 1,
    Positive = 2,
    Negative = 3
}

/// <summary>
/// A response in a thread.
/// </summary>
public class Post : BusinessObjectBase
{
    #region Fields

    private string _userName;
    private string _text;

    #endregion

    #region Properties

    public int PostID { get; set; }
    public int ThreadID { get; set; }
    public Standing Standing { get; set; }

    public string UserName {
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

    public string Text
    {
        get { return _text; }
        set {
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
    public DateTime Date { get; set; }
    
    #endregion

    public Post()
    {
        this.Text = null;

        // TODO Implement standing-picker and remove this
        this.Standing = (Standing)1;

        // TODO Implement login and remove this
        this.UserName = "User";
    }
}