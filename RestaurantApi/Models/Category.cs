namespace RestaurantApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsActive { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
