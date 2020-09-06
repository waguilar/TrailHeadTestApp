using SQLite;
using TrailHeadTestApp.Interfaces.Infrastructure.Helpers;

namespace TrailHeadTestApp.Droid
{
    public class DbHelperAndroid : IDbHelper
    {
        public SQLiteAsyncConnection GetAsyncConnection(string sqliteFilename)
        {
            // Create the connection
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(GetDbFilename(sqliteFilename));
            // Return the database connection
            return conn;
        }
        
        private string GetDbFilename(string sqliteFilename)
        {
            string libraryPath = GetUserDocumentsFolderPath(sqliteFilename);
            System.IO.Directory.CreateDirectory(libraryPath);
            string path = System.IO.Path.Combine(libraryPath, sqliteFilename + ".db3");
            return path;
        }
        public string GetUserDocumentsFolderPath(string userFolder)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            return System.IO.Path.Combine(documentsPath, userFolder);
        }

        public void Initialize() { }

    }
}