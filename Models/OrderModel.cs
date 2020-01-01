using System;

namespace KafeWeb.Models {
    public class Order {
        public int Id {get; set;}
        public KafeWeb.Models.Menu Menu {get; set;}
        public Boolean DoneStatus {get; set;}
        public int quantity {get; set;}
    }
}