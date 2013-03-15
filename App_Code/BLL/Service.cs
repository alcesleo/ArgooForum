using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Service class for accessing database.
/// </summary>
public class Service
{
    #region Data access objects
    private ThreadDAL _threadDAL;
    private ThreadDAL ThreadDAL
    {
        get
        {
            return _threadDAL ?? (_threadDAL = new ThreadDAL());
        }
    }

    private PostDAL _postDAL;
    private PostDAL PostDAL
    {
        get
        {
            return _postDAL ?? (_postDAL = new PostDAL());
        }
    }
    #endregion

    #region Thread access methods

    /// <summary>
    /// Get all threads.
    /// </summary>
    /// <returns>A list with all threads.</returns>
    public List<Thread> GetThreads()
    {
        return ThreadDAL.GetThreads();
    }

    /// <summary>
    /// Get one thread.
    /// </summary>
    /// <param name="threadID">ID of the thread to get.</param>
    /// <returns>A thread object.</returns>
    public Thread GetThreadByID(int threadID) 
    {
        return ThreadDAL.GetThreadByID(threadID);
    }

    /// <summary>
    /// Save a thread to the database. Inserts or updates accordingly.
    /// </summary>
    /// <param name="thread">The thread to save.</param>
    public void SaveThread(Thread thread)
    {
        if (thread.IsValid)
        {
            // New thread
            if (thread.ThreadID == 0)
            {
                ThreadDAL.CreateThread(thread);
            }
            // Old thread
            else
            {
                ThreadDAL.UpdateThread(thread);
            }
        }
        else
        {
            // Throw error with object
            var ex = new ApplicationException(thread.Error);
            ex.Data.Add("Thread", thread);
            throw ex;
        }
    }

    /// <summary>
    /// Delete a thread from the database.
    /// </summary>
    /// <param name="threadID">ID of the thread to delete.</param>
    public void DeleteThread(int threadID)
    {
        ThreadDAL.DeleteThread(threadID);
    }

    /// <summary>
    /// Delete a thread from the database.
    /// </summary>
    /// <param name="thread">The thread to delete.</param>
    public void DeleteThread(Thread thread)
    {
        ThreadDAL.DeleteThread(thread.ThreadID);
    }
    #endregion

    #region Post access methods

    /// <summary>
    /// Get all posts for a specific thread.
    /// </summary>
    /// <param name="threadID">The ID of the thread.</param>
    /// <returns>A list of Post objects.</returns>
    public List<Post> GetPosts(int threadID)
    {
        return PostDAL.GetPosts(threadID);
    }

    /// <summary>
    /// Get a specific post.
    /// </summary>
    /// <param name="postID">ID of the post.</param>
    /// <returns>A post object.</returns>
    public Post GetPostByID(int postID) 
    {
        return PostDAL.GetPostByID(postID);
    }

    /// <summary>
    /// Saves a post to the database. Inserts or updates accordingly.
    /// </summary>
    /// <param name="post">The post to save.</param>
    public void SavePost(Post post)
    {
        if (post.IsValid)
        {
            // New post
            if (post.PostID == 0)
            {
                PostDAL.CreatePost(post);
            }
            // Old post
            else
            {
                PostDAL.UpdatePost(post);
            }
        }
        else
        {
            var ex = new ApplicationException(post.Error);
            ex.Data.Add("Post", post);
            throw ex;
        }
    }

    /// <summary>
    /// Delete a post from the database.
    /// </summary>
    /// <param name="postID">ID of the post.</param>
    public void DeletePost(int postID)
    {
        PostDAL.DeletePost(postID);
    }

    /// <summary>
    /// Delete a post from the database.
    /// </summary>
    /// <param name="post">The post to delete.</param>
    public void DeletePost(Post post)
    {
        PostDAL.DeletePost(post.PostID);
    }

    #endregion
}