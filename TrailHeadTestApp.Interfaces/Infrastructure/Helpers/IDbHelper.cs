using SQLite;

namespace TrailHeadTestApp.Interfaces.Infrastructure.Helpers
{
    public interface IDbHelper
    {
        SQLiteAsyncConnection GetAsyncConnection(string dbName);
        string GetUserDocumentsFolderPath(string userFolder);
        void Initialize();
    }
}
