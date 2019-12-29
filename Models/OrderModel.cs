namespace KafeWeb.Models {
    public class Order {
        public int Id {get; set;}
        public KafeWeb.Models.Menu menu {get; set;}
        public int quantity {get; set;}
    }
}