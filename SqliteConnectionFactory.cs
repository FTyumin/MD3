using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3
{
    public class SqliteConnectionFactory
    {
        public ISQLiteAsyncConnection CreateConnection()
        {
            // šo kodu ņēmu no "Connect Your .NET MAUI Application to a Database - Singleton Sean"
            return new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "MD3.db3"),
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        }
    }
}
