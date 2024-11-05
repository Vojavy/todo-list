using System;
using System.Data.SQLite;
using System.IO;

namespace todo_list.Database
{
    public class DatabaseContext : IDisposable
    {
        private static DatabaseContext _instance;
        private SQLiteConnection _connection;
        private readonly string _databasePath = "todo_app.db";

        private DatabaseContext()
        {
            InitializeDatabase();
        }

        public static DatabaseContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseContext();
                }
                return _instance;
            }
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
            }

            _connection = new SQLiteConnection($"Data Source={_databasePath};Version=3;");
            _connection.Open();
        }

        public SQLiteConnection Connection
        {
            get { return _connection; }
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _instance = null;
        }
    }
}
