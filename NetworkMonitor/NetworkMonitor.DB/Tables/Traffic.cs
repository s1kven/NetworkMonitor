using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMonitor.DB.Tables
{
    [Table("Traffics")]
    public class Traffic
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public long ReceivedBytes { get; set; }
        public long TransmittedBytes { get; set; }

        //[ForeignKey(typeof(Connection))]
        //public int IdConnection { get; set; }

        [ForeignKey(typeof(Date))]
        public int IdDate { get; set; }
    }
}
