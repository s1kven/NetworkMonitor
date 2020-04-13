using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkMonitor.DB.Tables;
using System.IO;
using SQLiteNetExtensions.Extensions;

namespace NetworkMonitor.DB
{
    public class DBRepository
    {
        private const string DATABASE_NAME = "MYDB10.db";
        private static DBRepository instance;
        private DBRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Date>();
            database.CreateTable<Connection>();
            database.CreateTable<Traffic>();
        }
        public static DBRepository GetInstance()
        {
            if (instance == null)
                instance = new DBRepository(
                        Path.Combine(
                            System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
            return instance;
        }
        private SQLiteConnection database;
        public IEnumerable<Date> GetDates()
        {
            return database.Table<Date>().ToList();
        }
        public IEnumerable<Connection> GetConnections(int id)
        {
            return database.Table<Connection>().Where(i => i.IdDate == id).ToList();
        }
        public IEnumerable<Traffic> GetTraffics(/*int idCon, */int idDate)
        {
            return database.Table<Traffic>().Where(k => k.IdDate == idDate)/*.Where(i => i.IdConnection == idCon)*/.ToList();
        }
        public Date GetDate(int id)
        {
            return database.Get<Date>(id);
        }
        public Connection GetConnection(int id)
        {
            return database.Get<Connection>(id);
        }
        public Traffic GetTraffic(int id)
        {
            return database.Get<Traffic>(id);
        }
        public int SaveDate(Date date)
        {
            if (date.Id != 0)
            {
                database.Update(date);
                return date.Id;
            }
            else
            {
                return database.Insert(date);
            }
        }
        public int SaveConnection(Connection connection)
        {
            if (connection.Id != 0)
            {
                database.Update(connection);
                return connection.Id;
            }
            else
            {
                return database.Insert(connection);
            }
        }
        public int SaveTraffic(Traffic traffic)
        {
            if (traffic.Id != 0)
            {
                database.Update(traffic);
                return traffic.Id;
            }
            else
            {
                return database.Insert(traffic);
            }
        }
        public void UpdateWithChildren(Date date)
        {
            database.UpdateWithChildren(date);
        }
    }
}
