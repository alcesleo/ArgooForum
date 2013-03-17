ArgooForum
==========

A simple forum written in ASP.NET WebForms

## General

This is a basic forum application written for an assignment, with a quite 
stressful timeframe. Hence, there is some 'quick-fixes' in here that I really
would like to see done in another way, but I just had to get them working to
meet the requirements of the assignment before it was due. 

Except for obvious lacks of functionality, these are the most important changes 
I want to make, but probably won't have the time to implement.

- The threads should be editable only in ViewThread - I let the ListView in Default.aspx handle it because it's much easier.
- Editing and inserting of threads and posts should be handled by a user-control. Once again cheating using the power of ListViews.
- Post/Redirect/Get should be used to avoid double-inserts when refreshing.
- UserName-field should be removed and replaced by full-fledged login capability.
- Editing and deleting should be possible only when you are logged in, and only on your own posts and threads.
- Standing should display the standing and not the ID
- You should be able to pick a category, right now it defaults to CategoryID = 1, and only displays the ID.