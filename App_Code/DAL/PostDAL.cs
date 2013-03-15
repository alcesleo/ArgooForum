using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Database interface against Post-table.
/// </summary>
public class PostDAL : DALBase
{
    /// <summary>
    /// Get all posts in a thread.
    /// </summary>
    /// <param name="threadID">The thread to collect the posts from.</param>
    /// <returns>A list with Post objects</returns>
    public List<Post> GetPosts(int threadID)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                // Initialize command object
                var cmd = new SqlCommand("app.usp_GetPosts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4).Value = threadID;

                // Initialize list
                var posts = new List<Post>(10);

                conn.Open();

                // Read the data
                using (var reader = cmd.ExecuteReader())
                {
                    // Find column indexes
                    int postIDIx = reader.GetOrdinal("PostID");
                    int standingIDIx = reader.GetOrdinal("StandingID");
                    int userNameIx = reader.GetOrdinal("UserName");
                    int dateIx = reader.GetOrdinal("Date");
                    int textIx = reader.GetOrdinal("Text");

                    // Read each row
                    while (reader.Read())
                    {
                        // Add new post object to the list
                        posts.Add(new Post
                        {
                            PostID = reader.GetInt32(postIDIx),
                            ThreadID = threadID,
                            Standing = (Standing)reader.GetInt32(standingIDIx),
                            UserName = reader.GetString(userNameIx),
                            Date = reader.GetDateTime(dateIx),
                            Text = reader.GetString(textIx)

                        });
                    }
                }

                // Remove unused fields in array
                posts.TrimExcess();

                return posts;
            }
            catch
            {
                // Prevent fallthrough of error information.
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    /// <summary>
    /// Get a specific post.
    /// </summary>
    /// <param name="postID">ID of the post.</param>
    /// <returns>A post object.</returns>
    public Post GetPostByID(int postID)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("app.usp_GetPosts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@PostID", SqlDbType.Int, 4).Value = postID;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Find column indexes
                        int postIDIx = reader.GetOrdinal("PostID");
                        int threadIDIx = reader.GetOrdinal("ThreadID");
                        int standingIDIx = reader.GetOrdinal("StandingID");
                        int userNameIx = reader.GetOrdinal("UserName");
                        int dateIx = reader.GetOrdinal("Date");
                        int textIx = reader.GetOrdinal("Text");

                        // Return post object
                        return new Post
                        {
                            PostID = postID, // Already got this..
                            ThreadID = reader.GetInt32(threadIDIx),
                            Standing = (Standing)reader.GetInt32(standingIDIx),
                            UserName = reader.GetString(userNameIx),
                            Date = reader.GetDateTime(dateIx),
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
    /// Insert a post.
    /// </summary>
    /// <param name="post">The post to insert</param>
    public void CreatePost(Post post)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("app.usp_CreatePost", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.Add("@ThreadID", SqlDbType.Int, 4).Value = post.ThreadID;
                cmd.Parameters.Add("@StandingID", SqlDbType.Int, 4).Value = (int)post.Standing;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15).Value = post.UserName;
                cmd.Parameters.Add("@Text", SqlDbType.VarChar, 4000).Value = post.Text;

                // Output parameter
                cmd.Parameters.Add("@PostID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                // Run the query
                conn.Open();
                cmd.ExecuteNonQuery();

                // Get the output parameter
                post.PostID = (int)cmd.Parameters["@PostID"].Value;
            }
            catch
            {
                throw;
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }


    /// <summary>
    /// Update a post.
    /// </summary>
    /// <param name="post">The post to update.</param>
    public void UpdatePost(Post post)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("app.usp_UpdatePost", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.Add("@PostID", SqlDbType.Int, 4).Value = post.PostID;
                cmd.Parameters.Add("@StandingID", SqlDbType.Int, 4).Value = (int)post.Standing;
                cmd.Parameters.Add("@Text", SqlDbType.VarChar, 4000).Value = post.Text;

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
    /// Delete a post.
    /// </summary>
    /// <param name="postID">ID of post to delete.</param>
    public void DeletePost(int postID)
    {
         using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("app.usp_DeletePost", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@PostID", SqlDbType.Int, 4).Value = postID;

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