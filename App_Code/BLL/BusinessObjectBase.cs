using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

/// <summary>
/// Baseclass for business layer classes containing common error handling.
/// </summary>
public abstract class BusinessObjectBase : IDataErrorInfo
{
    // Fields
    private Dictionary<string, string> _validationErrors;
    protected string CommonErrorMessage; 

    /// <summary>
    /// Generic error-message.
    /// </summary>
    public string Error 
    { 
        get 
        {
            if (!this.IsValid)
            {
                return this.CommonErrorMessage;
            }
            return null;
        }
    }

    /// <summary>
    /// If the object is valid.
    /// </summary>
    public bool IsValid 
    {
        get { return !this.ValidationErrors.Any(); } 
    }

    /// <summary>
    /// Access properties.
    /// </summary>
    /// <param name="propertyName">Name of property.</param>
    /// <returns>Value of property.</returns>
    public string this[string propertyName] 
    { 
        get 
        {
            string error;
            if (this.ValidationErrors.TryGetValue(propertyName, out error))
            {
                return error;
            }
            return null;
        }
    }

    public Dictionary<string, string> ValidationErrors 
    { 
        get 
        {
            // Lazy initialization
            return this._validationErrors ?? (this._validationErrors = new Dictionary<string,string>());
        }
    }

    public BusinessObjectBase()
        :this(Strings.BusinessObject_CommonErrorMessage)
    { 
        // Empty
    }

    public BusinessObjectBase(string commonErrorMessage)
    {
        this.CommonErrorMessage = commonErrorMessage;
    }
}