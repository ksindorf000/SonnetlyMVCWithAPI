# SonnetlyMVCWithAPI
URL Hasher that incorporates MVC and API

### Assignment Requirements: 

#### Normal Mode

- Create an API project for a bookmarking site. Users should be able to POST urls to a 
bookmark resource to create a shortened URL record.
- Allow an anonymous user (no bearer token) to post to this bookmark resource and don't set a user on the bookmark (a public / anonymous bookmark).
- If a logged in user (with a bearer token) posts to the bookmark resource, track the user and set it as a foreign key on the bookmark.
-When an anonymous user performs a GET method on the bookmark list resource, show them all of the anonymous (no user FK) bookmarks and all bookmarks a user has marked public.
-When a logged in user preforms a GET method on the bookmark list resource, show them all of the anonymous bookmarks, all public bookmarks from all users, and their own personal bookmarks (public and private).
-The shortened code should work exactly as it did in your MVC version of the Url Shortener assignment.
-A logged in user should be able to PUT (update) a bookmark ONLY if they are marked as the owner. This means no users can PUT to an anonymous bookmark.


#### Hard Mode

- Instead of starting a new project and implementing this functionality as a standalone resource, 
adapt your MVC project to also include Web API endpoints that utilize shareable code between the 
two parts of the site.

#### My Notes
Rather than reuse an old MVC project and add API endpoints, I created an API and am attempting to add MVC functionality to it.
