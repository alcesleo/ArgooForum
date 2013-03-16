using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Adds simple error-handling to all the pages.
/// </summary>
public static class PageExtensions
{

    /// <summary>
    /// Adds a CustomValidator object to the ValidatorCollection
    /// </summary>
    /// <param name="message">Error message to be displayed in the ValidationSummary</param>
    /// <param name="validationGroup">The validationgroup to be used</param>
    public static void AddErrorMessage(this Page page, string message, string validationGroup = null)
    {
        // Create new customvalidator
        var validator = new CustomValidator
        {
            IsValid = false,
            ErrorMessage = message,
            ValidationGroup = validationGroup
        };

        // Add it to the page
        page.Validators.Add(validator);
    }

    /// <summary>
    /// Calls AddErrorMessage for all errormessages contained in an IDataErrorInfo-object
    /// </summary>
    /// <param name="obj">The object containing the error data.</param>
    /// <param name="validationGroup">The validationgroup to add the validators to.</param>
    public static void AddErrorMessage(this Page page, IDataErrorInfo obj, string validationGroup = null)
    {
        // Add all error-messages of the object to the validationsummary
        obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name)
            .Where(n => !String.IsNullOrWhiteSpace(obj[n]))
            .ToList()
            .ForEach(n => AddErrorMessage(page, obj[n], validationGroup));
    }
}