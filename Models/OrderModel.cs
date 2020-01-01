using System;

namespace KafeWeb.Models {
    public class Order {
        public int Id {get; set;}
        public MenuItem MenuItem {get; set;}
        public int IdMenuItem {get; set;}
        public Boolean DoneStatus {get; set;}
        public int quantity {get; set;}
    }
}