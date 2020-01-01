using System;

namespace KafeWeb.Models {
    public class TableOrder {
        public int Id {get; set;}
        public DateTime Date {get; set;}
        public Boolean DoneStatus {get; set;}

        public User User { get; set; }

        public Table Table {get; set;}

        public int IdTable {get; set;}
    }
}