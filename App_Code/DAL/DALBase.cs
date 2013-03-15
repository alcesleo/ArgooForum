using Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Contains easy access properties to the SQL connection string.
/// </summary>
public abstract class DALBase
{
    private static string _connectionString;

    /// <summary>
    /// A generic error message.
    /// </summary>
    protected static readonly string GenericErrorMessage = Strings.Generic_DAL_Error;

    /// <summary>
    /// Create a connection to the database.
    /// </summary>
    /// <returns></returns>
    protected SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    /// <summary>
    /// Initialize the connectionstring
    /// </summary>
	static DALBase()
	{
        // Get connectionstring from Web.config
        _connectionString = WebConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
	}
}