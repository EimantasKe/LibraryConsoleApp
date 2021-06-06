/*
Assumptions:
    Only one copy of book (not unlimited)

Notes:
    Reads JSON at the start to add books to library class object, saves library to json on exit command
    For simplification no separate class for users
    If closed without exit command all active changes disappear
    OnLoanToWhom is unique for separate people (userID)
*/

namespace LibraryConsoleApp
{
    class Program
    {
        static void Main()
        {
            CLI Window = new();
            Window.Start();
        }
    }
}