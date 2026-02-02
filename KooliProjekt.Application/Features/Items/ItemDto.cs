namespace KooliProjekt.Application.Features.Items
{
    public class ItemDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Photo { get; set; }
    }
}
