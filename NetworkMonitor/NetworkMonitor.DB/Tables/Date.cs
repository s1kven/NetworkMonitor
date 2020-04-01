using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMonitor.DB.Tables
{
    [Table("Dates")]
    public class Date
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime ConnectionDate { get; set; }

        [OneToMany]
        public List<Traffic> Traffics { get; set; }
        
        [OneToMany]
        public List<Connection> Connections { get; set; }
    }
}
