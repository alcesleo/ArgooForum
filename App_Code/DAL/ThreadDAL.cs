using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Database interface against Thread-table
/// </summary>
public class ThreadDAL : DALBase
{
    /// <summary>
    /// Get all threads.
    /// </summary>
    /// <returns>A list of threads.</returns>
    public List<Thread> GetThreads()
    {
        // TODO Get only the threads required for the current page?

        using (var conn = CreateConnection())
        {
            try
            {
                // Initialize command object
                var cmd = new SqlCommand("app.usp_GetThreads", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Initialize list
                var threads = new List<Thread>(10);

                conn.Open();
                
                // Read the data
                using (var reader = cmd.ExecuteReader())
                {
                    // Find column indexes
                    int threadIDIx = reader.GetOrdinal("ThreadID");
                    int threadCategoryIDIx = reader.GetOrdinal("ThreadCategoryID");
                    int userNameIx = reader.GetOrdinal("UserName");
                    int dateIx = reader.GetOrdinal("Date");
                    int titleIx = reader.GetOrdinal("Title");
                    int textIx = reader.GetOrdinal("Text");

                    // Read each row
                    while (reader.Read())
                    {
                        // Create new thread object
                        threads.Add(new Thread
                        {
                            ThreadID = reader.GetInt32(threadIDIx),
                            ThreadCategoryID = reader.GetInt32(threadCategoryIDIx),
                            UserName = reader.GetString(userNameIx),
                            Date = reader.GetDateTime(dateIx),
                            Title = reader.GetString(titleIx),
                            Text = reader.GetString(textIx)
                        });
                    }
                }

                // Remove unused fields in array
                threads.TrimExcess();

                return threads;
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }


    /// <summary>
    /// Get a specific thread.
    /// </summary>
    /// <param name="threadID">The ID of the thread.</param>
    /// <returns>A thread object.</returns>
    public Thread GetThreadByID(int threadID)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("app.usp_GetThreads", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4).Value = threadID;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int threadIDIx = reader.GetOrdinal("ThreadID");
                        int threadCategoryIDIx = reader.GetOrdinal("ThreadCategoryID");
                        int userNameIx = reader.GetOrdinal("UserName");
                        int dateIx = reader.GetOrdinal("Date");
                        int titleIx = reader.GetOrdinal("Title");
                        int textIx = reader.GetOrdinal("Text");

                        return new Thread
                        {
                            ThreadID = reader.GetInt32(threadIDIx),
                            ThreadCategoryID = reader.GetInt32(threadCategoryIDIx),
                            UserName = reader.GetString(userNameIx),
                            Date = reader.GetDateTime(dateIx),
                            Title = reader.GetString(titleIx),
                            Text = reader.GetString(textIx)
                        };
                    }
                }

                return null;
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }


    /// <summary>
    /// Create a thread.
    /// </summary>
    /// <param name="thread"></param>
    public void CreateThread(Thread thread)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("app.usp_CreateThread", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.Add("@ThreadCategoryID", SqlDbType.Int, 4).Value = thread.ThreadCategoryID;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15).Value = thread.UserName;
                cmd.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = thread.Title;
                cmd.Parameters.Add("@Text", SqlDbType.VarChar, 4000).Value = thread.Text;

                // Output parameter
                cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                // Run the query
                conn.Open();
                cmd.ExecuteNonQuery();

                // Get the output parameter
                thread.ThreadID = (int)cmd.Parameters["@ThreadID"].Value;
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }


    /// <summary>
    /// Changes an existing thread.
    /// </summary>
    /// <param name="thread">The thread object to be updated.</param>
    public void UpdateThread(Thread thread)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("app.usp_UpdateThread", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4).Value = thread.ThreadID;
                cmd.Parameters.Add("@ThreadCategoryID", SqlDbType.Int, 4).Value = thread.ThreadCategoryID;
                cmd.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = thread.Title;
                cmd.Parameters.Add("@Text", SqlDbType.VarChar, 4000).Value = thread.Text;

                // Run
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch 
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    /// <summary>
    /// Delete a thread
    /// </summary>
    /// <param name="threadID">ID of the thead.</param>
    public void DeleteThread(int threadID)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("app.usp_DeleteThread", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4).Value = threadID;

                // Run
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }
}