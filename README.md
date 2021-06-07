# LibraryConsoleApp

Assumptions:
* Only one copy of book (not unlimited)
* Book.OnLoanToWhom is Book.Name are considered unique

Notes:
* Reads JSON at the start to add books to library class object, saves library to json on exit command
* No separate class for users
* If closed without exit command all active changes disappear

To-do:
* Add separate user class (with appropriate funcitonality)
* Save to JSON after every change
* Move object uniquness to ID properties (e.g. ISBN, userID)
