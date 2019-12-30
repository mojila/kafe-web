using System;

namespace KafeWeb.Models {
    public class TableOrder {
        public int Id {get; set;}
        public DateTime Date {get; set;}
        public Boolean DoneStatus {get; set;}
        public KafeWeb.Models.Order Order {get;set;}
    }
}