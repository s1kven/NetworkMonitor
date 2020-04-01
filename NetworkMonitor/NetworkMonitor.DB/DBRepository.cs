using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkMonitor.DB.Tables;

namespace NetworkMonitor.DB
{
    public class DBRepository
    {
        private SQLiteConnection database;
        public DBRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Date>();
            database.CreateTable<Connection>();
            database.CreateTable<Traffic>();
        }
    }
}
