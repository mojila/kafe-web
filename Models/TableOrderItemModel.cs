namespace KafeWeb.Models {
    public class TableOrderItem {
        public int Id {get; set;}
        public TableOrder TableOrder {get; set;}
        public KafeWeb.Models.Order Order {get; set;}
    }
}