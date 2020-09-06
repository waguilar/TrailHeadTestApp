using SQLite;
using TrailHeadTestApp.Interfaces.Infrastructure.Helpers;

namespace TrailHeadTestApp.Infrastructure.DataAccessLayer
{
    public class BaseDAL<T> where T : new()
    {
        protected bool Initialized { get; set; }  // Used to check if the initialization was completed (could fail when the user logs out and we are running background operations)
        protected SQLiteAsyncConnection SqlAsyncConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:IFSCore.Model.BaseRepository`1"/> class.
        /// </summary>
        /// <remarks>
        /// This base class is used for databases owned by a user
        /// Needs the user id to create the database file
        /// </remarks>
        /// <param name="sqlite">Sqlite.</param>
        /// <param name="applicationPropertiesStore">Application properties reader.</param>
        public BaseDAL(IDbHelper sqlite)
        {
            try
            {
                SqlAsyncConnection = sqlite.GetAsyncConnection(Constants.DB_NAME + "_" + Constants.DB_ENVIRONMENT + "_" + Constants.DB_USERID);

                //Getting conection and Creating table  
                SqlAsyncConnection.CreateTableAsync<T>();

                Initialized = true;
            }
            catch
            {
                Initialized = false;
            }

        }
    }
}
