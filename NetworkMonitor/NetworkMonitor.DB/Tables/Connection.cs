using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace NetworkMonitor.DB.Tables
{
    [Table("Connections")]
    public class Connection
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string IP { get; set; }

        //[OneToMany]
        //public List<Traffic> Traffics { get; set; }

        [ForeignKey(typeof(Date))]
        public int IdDate { get; set; }
    }
}
